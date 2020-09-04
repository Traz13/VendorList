using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public struct Ouptut
	{
		public string Vendor;
		public string Item;
		public string Zone;

		public void Reset()
		{
			Vendor =
			Item =
			Zone = "";
		}

		public bool Validate()
		{
			if (string.IsNullOrEmpty(Vendor))
				return false;
			if (string.IsNullOrEmpty(Item))
				return false;
			if (string.IsNullOrEmpty(Zone))
				return false;

			return true;
		}
	}

	[SerializeField] private GameObject VendorParent = null;
	[SerializeField] private GameObject ItemParent = null;
	[SerializeField] private GameObject ZoneParent = null;

	private Dropdown VendorDropDown;
	private Dropdown ItemDropDown;
	private Dropdown ZoneDropDown;

	[SerializeField] private Button ResetButton = null;
	[SerializeField] private Button SubmitButton = null;

	[SerializeField] private OptionList VendorList = null;

	private Ouptut OutData;

	private int VendorIndex;
	private int ItemIndex;

    // Start is called before the first frame update
    void Start()
    {
		VendorDropDown	= VendorParent.GetComponentInChildren<Dropdown>();
		ItemDropDown	= ItemParent.GetComponentInChildren<Dropdown>();
		ZoneDropDown = ZoneParent.GetComponentInChildren<Dropdown>();

		VendorDropDown.ClearOptions();
		ItemDropDown.ClearOptions();
		ZoneDropDown.ClearOptions();

		VendorDropDown.onValueChanged.AddListener(VendorSelect);
		ItemDropDown.onValueChanged.AddListener(ItemSelect);
		ZoneDropDown.onValueChanged.AddListener(ZoneSelect);

		ResetButton.onClick.AddListener(Reset);
		SubmitButton.onClick.AddListener(Submit);

		Reset();
	}

	private void Reset()
	{
		PopupateDropDown(VendorDropDown, VendorList);

		ItemParent.SetActive(false);
		ZoneParent.SetActive(false);

		SubmitButton.gameObject.SetActive(false);

		OutData.Reset(); 
	}

	private bool Validate()
	{
		return OutData.Validate();
	}

	private void Submit()
	{
		if(!Validate())
		{
			Debug.LogWarning("Failed to validate, unable to submit");
			return;
		}

		Debug.LogFormat("Submission for {0}, {1}, {2}", OutData.Vendor, OutData.Item, OutData.Zone);
	}

	private void VendorSelect(int index)
	{
		if (index == 0)
		{
			OutData.Vendor = null;
			ItemParent.SetActive(false);
			ZoneParent.SetActive(false);
			SubmitButton.gameObject.SetActive(false);
			return;
		}

		VendorIndex = index - 1;

		OutData.Vendor = VendorDropDown.options[index].text;
		ItemParent.SetActive(true);

		PopupateDropDown(ItemDropDown, VendorList.Data[VendorIndex].Data);

		ZoneParent.SetActive(false);
	}

	private void ItemSelect(int index)
	{
		if (index == 0)
		{
			OutData.Item = null;
			ZoneParent.SetActive(false);
			SubmitButton.gameObject.SetActive(false);
			return;
		}

		ItemIndex = index - 1;

		OutData.Item = ItemDropDown.options[index].text;
		ZoneParent.SetActive(true);

		OptionList itemList = VendorList.Data[VendorIndex].Data;
		PopupateDropDown(ZoneDropDown, itemList.Data[ItemIndex].Data);
	}

	private void ZoneSelect(int index)
	{
		if (index == 0)
		{
			OutData.Zone = null;
			SubmitButton.gameObject.SetActive(false);
			return;
		}

		OutData.Zone = ZoneDropDown.options[index].text;
		SubmitButton.gameObject.SetActive(true);
	}

	private void PopupateDropDown(Dropdown dropDown, OptionList options)
	{
		dropDown.ClearOptions();

		dropDown.options.Add(new Dropdown.OptionData("None"));

		foreach (Option option in options.Data)
		{
			dropDown.options.Add(new Dropdown.OptionData(option.Name));
		}
	}
}
