using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private Game _game;

	private Player _player;
	private bool _occupied;

	[Tooltip("The number of score points this object is worth")]
	public int Points = 10;

	private void Start()
	{
		// Cache game
		_game = GameObject.Find("GameManager").GetComponent<Game>();
		// Get components
		_player = GameObject.Find("Frog").GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Check player collisions
		if (other.CompareTag("Player"))
		{
			if (gameObject.CompareTag("Lilypad"))
				Lilypad();
			else if (gameObject.CompareTag("Grass"))
				Grass();
		}
	}

	private void Grass()
	{
		// Die
		_player.Die();
	}

	private void Lilypad()
	{
		// Check if lilypad is occupied
		if (_occupied)
		{
			_player.Die(); // Die
			return;
		}

		// Display landed frog
		GameObject landedFrog = Instantiate((GameObject)Resources.Load("Prefabs/Landed Frog"), transform.position, Quaternion.identity, GameObject.Find("Landed Frogs").transform);

		// Marked lilypad as occupied
		_occupied = true;

		// Add points
		_game.Score += Points;

		// Reset player position
		_player.ResetPosition();
	}
}