using UnityEditor;
using UnityEngine;

public class DebugFlowField : MonoBehaviour
{
#if UNITY_EDITOR
	public FlowField m_FlowField;
	public Color m_GridColor;
	public Color m_TextColor;

	public bool m_ShowCostValue;
	public bool m_ShowIntegrationValue;
	
	public bool m_ShowFlowField;
	
	private void Start()
	{
		
	}

	private void OnDrawGizmos()
	{
		if (m_FlowField == null) return;
		if (m_FlowField.m_Grid.Cells == null) return;

		Grid grid = m_FlowField.m_Grid;

		GUIStyle style = new GUIStyle(GUI.skin.label)
		{
			alignment = TextAnchor.LowerCenter,
			normal =
			{
				textColor = m_TextColor
			}
		};

		style.fontSize = 12;
		style.richText = false;

		Gizmos.color = m_GridColor;
		for (int i = 0; i < grid.Cells.GetLength(0); i++)
		{
			for (int j = 0; j < grid.Cells.GetLength(1); j++)
			{
				Gizmos.DrawWireCube(grid.Cells[i,j].WorldPos, new Vector3(1,0,1) * grid.m_CellRadius);

				if (m_ShowCostValue)
					Handles.Label(grid.Cells[i,j].WorldPos, grid.Cells[i,j].Cost.ToString(), style);
				
				if (m_ShowIntegrationValue)
					Handles.Label(grid.Cells[i,j].WorldPos, grid.Cells[i,j].BestCost.ToString(), style);

				if (m_ShowFlowField)
				{
					DrawArrow(grid.Cells[i, j].WorldPos, grid.Cells[i, j].BestDirection);
				}

			}
		}
		
		if(m_FlowField.DestinationCell == null) return;
		Gizmos.DrawWireSphere(m_FlowField.DestinationCell.WorldPos, grid.m_CellRadius / 2);
	}

	private void DrawArrow(Vector3 position, GridDirection direction)
	{
		if(direction == GridDirection.None) return;
		
		Gizmos.color = m_GridColor;
		float arrowLength = 0.3f;
		float arrowHeadAngle = 20.0f;
		float arrowHeadLength = 0.1f;

		Vector3 dir = new Vector3(direction.Vector.x, 0, direction.Vector.y);
		Gizmos.DrawLine(position, position + dir * arrowLength);
					
		Vector3 right = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
		Vector3 left = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;

		Gizmos.DrawLine(position + dir * arrowLength, position + dir * arrowLength + right * arrowHeadLength);
		Gizmos.DrawLine(position + dir * arrowLength, position + dir * arrowLength + left * arrowHeadLength);
	}

#endif
}