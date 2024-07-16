using UnityEngine;

public class Cell
{
	public Vector2Int CellIndex;
	public Vector3 WorldPos;

	public byte Cost = 1;
	public ushort BestCost = ushort.MaxValue;

	public GridDirection BestDirection = GridDirection.None;
}