using UnityEngine;

namespace DefaultNamespace
{
	public class DisplayGui : MonoBehaviour
	{
		public string displayText = "";
		public Rect textRect = new Rect(10, 10, 200, 50);
		public GUIStyle textStyle;

		void Start()
		{
			if (textStyle == null)
			{
				textStyle = new GUIStyle();
				textStyle.fontSize = 72;
				textStyle.normal.textColor = Color.white;
			}
		}

		void OnGUI()
		{
			displayText = $"Total Units: {UnitController.Instance.UnitCount}";
			GUI.Label(textRect, displayText, textStyle);
		}
	}
}