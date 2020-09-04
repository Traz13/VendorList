using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/OptionList", order = 1)]
public class OptionList : ScriptableObject
{
	[SerializeField] private Option[] Options;
	public Option[] Data { get { return Options; } }
}
