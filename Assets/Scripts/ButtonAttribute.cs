using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : PropertyAttribute
{
	public readonly string ButtonName;

	public ButtonAttribute(string buttonName = null)
	{
		ButtonName = buttonName;
	}
	
	public ButtonAttribute()
	{
		ButtonName = null;
	}
}