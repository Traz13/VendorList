using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Option
{
	[SerializeField] private string DisplayName;
	public string Name { get { return DisplayName; } }

	[SerializeField] private OptionList Options;
	public OptionList Data { get { return Options; } }
}
