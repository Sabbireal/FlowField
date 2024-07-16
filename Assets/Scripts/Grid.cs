using System;
using UnityEngine;

[Serializable]
public class Grid 
{
	public Cell[,] Cells; 
	public Vector2Int m_GridSize;
	public float m_CellRadius;

	public void CreateGrid()
	{
		Cells = new Cell[m_GridSize.x, m_GridSize.y];
		
		for (int i = 0; i < m_GridSize.x; i++)
		{
			for (int j = 0; j < m_GridSize.y; j++)
			{
				Cells[i, j] = CreateCell(new Vector2Int(i, j));
			}
		}
	}

	private Cell CreateCell(Vector2Int cellIndex)
	{
		return new Cell
		{
			CellIndex = cellIndex,
			WorldPos = new Vector3( cellIndex.x * m_CellRadius + m_CellRadius/2, 0, cellIndex.y * m_CellRadius + m_CellRadius/2),
			Cost = 1,
			BestCost = ushort.MaxValue
		};
	}

	public Cell GetCellByIndex(Vector2Int index)
	{
		if (index.x < 0 || index.y < 0 || index.x >= m_GridSize.x || index.y >= m_GridSize.y)
		{
			return null;
		}
		
		return Cells[index.x, index.y];
	}

	public Cell GetCellByWorldPosition(Vector3 worldPos)
	{
		float clamWorldPosX = Mathf.Clamp(worldPos.x, 0, Cells.GetLength(0) * m_CellRadius);
		float clamWorldPosY = Mathf.Clamp(worldPos.z, 0, Cells.GetLength(1) * m_CellRadius);

		int i = Mathf.FloorToInt(clamWorldPosX / m_CellRadius);
		i = Mathf.Min(Cells.GetLength(0) - 1, i);
		
		int j = Mathf.FloorToInt(clamWorldPosY / m_CellRadius);
		j = Mathf.Min(Cells.GetLength(1) - 1, j);
		
		return Cells[i, j];
	}
}