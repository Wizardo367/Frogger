using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private Game _game;

	private Player _player;
	private bool _occupied;

	private string _lastTriggerTag;

	[Tooltip("The number of score points this object is worth")]
	public int Points = 10;

	private void Start()
	{
		// Cache game
		_game = GameObject.Find("GameManager").GetComponent<Game>();
		// Get components
		_player = GameObject.Find("Frog").GetComponent<Player>();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		// Check player collisions
		if (other.CompareTag("Player"))
		{
			if (gameObject.CompareTag("Lilypad"))
				Lilypad();
			else if (gameObject.CompareTag("Walkable"))
			{
				Debug.Log("Yh");
				// Stick player to object
				other.transform.position = gameObject.transform.position;
			}
			else if (gameObject.CompareTag("Danger"))
			{
				Debug.Log("YOLO");
				Danger();
			}
		}
	}

	private void Danger()
	{
		// Check if the player is still leaping
		if (_player.Leaping) return;

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

		// Add to tracker
		_game.LilypadsOccupied++;

		// Reset player position
		_player.ResetPosition();
	}
}