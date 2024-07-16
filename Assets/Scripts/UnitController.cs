using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UnitController : MonoBehaviour
{
	[SerializeField] private int m_SpawnAmount = 10;
	[SerializeField] private float m_SpawnDelay = 2;
	[SerializeField] private float m_Speed;
	[SerializeField] private Unit m_Unit;
	
	private List<Unit> _units = new();

	private int canSpawn = 0;
	private float lastSpawn = 0;

	public int UnitCount => _units.Count;

	public static UnitController Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			canSpawn += m_SpawnAmount;
		}

		if(canSpawn == 0) return;
		lastSpawn += Time.deltaTime;
		if(lastSpawn < m_SpawnDelay) return;

		lastSpawn = 0;
		canSpawn--;
		SpawnUnit();
	}

	private void SpawnUnit()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      
		mousePos.y = 0;
		_units.Add(Instantiate(m_Unit, mousePos, quaternion.identity));
	}

	private void FixedUpdate()
	{
		foreach (Unit unit in _units)
		{
			Cell cell = FlowField.Instance.m_Grid.GetCellByWorldPosition(unit.transform.position);
			Vector3 moveDirection = new Vector3(cell.BestDirection.Vector.x, 0, cell.BestDirection.Vector.y);
			unit.m_Rigidbody.velocity = m_Speed * moveDirection;
		}
		
	}
}