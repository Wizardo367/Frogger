using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	public GameObject Spawnable;
	public int Amount = 3;
	public float Speed = 1;
	public float Spacing = 0;

	private List<GameObject> _spawned = new List<GameObject>();

	private void Start()
	{
		// Create number of spawnables specified
		for (int i = 0; i < Amount; i++)
		{
			// Calculate position
			Vector3 pos = Spawnable.transform.position;
			pos.x += Spacing * i;

			// Spawn
			_spawned.Add(Instantiate(Spawnable, pos, Quaternion.identity));
		}
	}

	private void FixedUpdate()
	{
		// Apply velocity
		if (!Mathf.Approximately(Speed, 0f))
			foreach (var obj in _spawned)
				obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * Time.fixedDeltaTime, 0);
	}
}