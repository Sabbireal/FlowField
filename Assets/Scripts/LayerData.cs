using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Layer Data")]
public class LayerData : ScriptableObject
{
	public List<LayerCost> m_Data;
	
	public byte GetLayerCost(int layer)
	{
		for (var i = 0; i < m_Data.Count; i++)
		{
			if (m_Data[i].m_Layer.HasLayer(layer))
				return m_Data[i].m_Cost;
		}

		return 1;
	}
	
	public LayerMask GetLayerMask()
	{
		LayerMask combinedLayerMask = 0;
		foreach (LayerCost layerCost in m_Data)
		{
			combinedLayerMask |= layerCost.m_Layer;
		}
		return combinedLayerMask;
	}

#if UNITY_EDITOR
	[Button]
	private void PrintObstacleCost()
	{
		Debug.Log($"Cost: {GetLayerCost(7)}");
	}
	
	[Button]
	private void PrintDirtCost()
	{
		Debug.Log($"Cost: {GetLayerCost(8)}");
	}
#endif
}

[Serializable]
public class LayerCost
{
	public LayerMask m_Layer;
	public byte m_Cost;
}

public static class LayerMaskHelper
{
	public static bool HasLayer(this LayerMask layerMask, int layer)
	{
		return layerMask == (layerMask | (1 << layer));
	}
}