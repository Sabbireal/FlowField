using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomUnitSpriteAssigner : MonoBehaviour
{
	[SerializeField] private List<Sprite> m_Sprites;
	[SerializeField] private SpriteRenderer m_SpriteRenderer;

	private void OnEnable()
	{
		m_SpriteRenderer.sprite = m_Sprites[Random.Range(0, m_Sprites.Count)];
	}
}