using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(object), true)]
	public class ButtonEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			var methods = target
				.GetType()
				.GetMethods(
					System.Reflection.BindingFlags.Instance 
				            | System.Reflection.BindingFlags.Static 
				            | System.Reflection.BindingFlags.Public 
							| System.Reflection.BindingFlags.NonPublic);

			foreach (var method in methods)
			{
				if(method.ReturnType != typeof(void)) continue;
				var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
				if (ba == null) continue;
				
				if (GUILayout.Button(string.IsNullOrEmpty(ba.ButtonName) ? method.Name : ba.ButtonName))
				{
					method.Invoke(target, null);
				}
			}
		}
	}
}