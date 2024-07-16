using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridDirection
{
	public static readonly GridDirection None = new(0, 0);
	public static readonly GridDirection North = new(0, 1);
	public static readonly GridDirection South = new(0, -1);
	public static readonly GridDirection East = new(1, 0);
	public static readonly GridDirection West = new(-1, 0);
	public static readonly GridDirection NorthEast = new(1, 1);
	public static readonly GridDirection NorthWest = new(-1, 1);
	public static readonly GridDirection SouthEast = new(1, -1);
	public static readonly GridDirection SouthWest = new(-1, -1);
	
	public readonly Vector2Int Vector;

	public GridDirection(int x, int y)
	{
		Vector = new Vector2Int(x, y);
	}

	public static implicit operator Vector2Int(GridDirection direction)
	{
		return direction.Vector;
	}

	public static GridDirection GetDirectionV2I(Vector2Int vector)
	{
		return CardinalAndIntercardinalDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
	}

	public static readonly List<GridDirection> CardinalDirections = new List<GridDirection>
	{
		North,
		East,
		South,
		West
	};

	public static readonly List<GridDirection> CardinalAndIntercardinalDirections = new List<GridDirection>
	{
		North,
		NorthEast,
		East,
		SouthEast,
		South,
		SouthWest,
		West,
		NorthWest
	};

	public static readonly List<GridDirection> AllDirections = new List<GridDirection>
	{
		None,
		North,
		NorthEast,
		East,
		SouthEast,
		South,
		SouthWest,
		West,
		NorthWest
	};
}