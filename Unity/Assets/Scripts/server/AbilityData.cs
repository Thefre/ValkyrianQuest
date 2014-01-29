using UnityEngine;
using System;
using System.Collections;

public class AbilityData {
	public string name = "Unknown Ability";
	public string actionID = "blank";
	public string description = "No description available.";
	public int level = 1;
	public int spCost = 0;
	public int hpCost = 0;
	public TimeSpan delay;
	public TimeSpan coolDown;
}
