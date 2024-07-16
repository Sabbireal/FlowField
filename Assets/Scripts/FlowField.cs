using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlowField : MonoBehaviour
{
	public LayerData m_Layer;
	public Grid m_Grid = new Grid();

	public Cell DestinationCell { get; private set; }

	public static FlowField Instance;
	private void Awake()
	{
		Instance = this;
		GenerateFlowField();
	}


	public void SetDestination(Vector3 destination, bool recalculate = true)
	{
		if (recalculate)
		{
			m_Grid.CreateGrid();   
			CalculateCostField();  
		}
		
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);                              
		mousePos.y = 0;                                                                                      
		DestinationCell = m_Grid.GetCellByWorldPosition(mousePos);                                           
                                                                                                      
		CalculateIntegrationField();                                                                         
		CalculateFlowField();                                                                                
	}

	[Button]
	private void GenerateFlowField()
	{
		m_Grid.CreateGrid();
		CalculateCostField();
		CalculateIntegrationField();
		CalculateFlowField();
	}

	private void CalculateCostField()
	{
		LayerMask layerMask = m_Layer.GetLayerMask();
		Vector3 halfExtends = Vector3.one * m_Grid.m_CellRadius / 2;

		for (int i = 0; i < m_Grid.Cells.GetLength(0); i++)
		{
			for (int j = 0; j < m_Grid.Cells.GetLength(1); j++)
			{
				m_Grid.Cells[i, j].Cost = 1;

				Collider[] colliders = new Collider[1];

				var size = Physics.OverlapBoxNonAlloc(
					m_Grid.Cells[i, j].WorldPos,
					halfExtends,
					colliders,
					quaternion.identity,
					layerMask);

				if (size == 0) continue;
				m_Grid.Cells[i, j].Cost = m_Layer.GetLayerCost(colliders[0].gameObject.layer);
			}
		}
	}

	private void CalculateIntegrationField()
	{
		if (DestinationCell == null) return;
		DestinationCell.Cost = 0;
		DestinationCell.BestCost = 0;

		Queue<Cell> queue = new();
		queue.Enqueue(DestinationCell);

		while (queue.Count > 0)
		{
			Cell curCell = queue.Dequeue();
			List<Cell> curNeighbours = GetNeighbours(curCell, GridDirection.CardinalDirections);

			for (var i = 0; i < curNeighbours.Count; i++)
			{
				if (curNeighbours[i].Cost == byte.MaxValue) continue;
				if (curCell.BestCost + curNeighbours[i].Cost < curNeighbours[i].BestCost)
				{
					curNeighbours[i].BestCost = (ushort) (curCell.BestCost + curNeighbours[i].Cost);
					queue.Enqueue(curNeighbours[i]);
				}
			}

		}
	}

	private void CalculateFlowField()
	{
		foreach (Cell curCell in m_Grid.Cells)
		{
			List<Cell> curNeighbours = GetNeighbours(curCell, GridDirection.AllDirections);
			
			int bestCost = curCell.BestCost;
			
			for (var i = 0; i < curNeighbours.Count; i++)
			{
				if (curNeighbours[i].BestCost < bestCost)
				{
					bestCost = curNeighbours[i].BestCost;
					curCell.BestDirection = GridDirection.GetDirectionV2I(curNeighbours[i].CellIndex - curCell.CellIndex);
				}
			}
		}
	}

	private List<Cell> GetNeighbours(Cell cell, List<GridDirection> gridDirections)
	{
		List<Cell> cells = new();
		
		for (int i = 0; i < gridDirections.Count; i++)
		{                 
			Cell nCell = m_Grid.GetCellByIndex(cell.CellIndex + gridDirections[i]);
			if(nCell == null) continue;
			cells.Add(nCell);
		}

		return cells;
	}
}