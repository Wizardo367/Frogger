using UnityEngine;

public class Player : MonoBehaviour
{
	// Properties
	public int Lives = 3;
	public bool Dead;

	public float MinBoundX, MaxBoundX, MinBoundY, MaxBoundY;

	// Variables
	private Animator _animator;
	private AudioSource _audioSource;

	// Store frequently changed sounds
	private AudioClip _leapSound, _successSound, _deathSound;

	private Vector3 _targetPosition;

	private void Start()
	{
		// Cache components
		_animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();

		// Cache audio clips
		_leapSound = (AudioClip) Resources.Load("Audio/leap");
		_successSound = (AudioClip) Resources.Load("Audio/success");
		_deathSound = (AudioClip)Resources.Load("Audio/death");

		// Initialise variables
		_targetPosition = transform.position;
	}

	private void Update()
	{
		// Check for input
		if (Input.GetKeyDown(KeyCode.W))
			Leap(Direction.Up);
		else if (Input.GetKeyDown(KeyCode.S))
			Leap(Direction.Down);
		else if (Input.GetKeyDown(KeyCode.A))
			Leap(Direction.Left);
		else if (Input.GetKeyDown(KeyCode.D))
			Leap(Direction.Right);

		// Check if the audio pitch needs resetting
		if (!_audioSource.isPlaying && Mathf.Approximately(_audioSource.pitch, 1f))
			_audioSource.pitch = 1;

		// Move player
		if (!Dead)
			transform.position = Vector2.MoveTowards(transform.position, _targetPosition, Time.deltaTime/0.5f);
	}

	private void Leap(Direction direction)
	{
		// Check if the player is still in the middle of a leap or dead
		Vector3 curPos = transform.position;
		if (curPos != _targetPosition || Dead) return;

		// Play animation
		_animator.Play("Leap");

		// Play sound
		if (_audioSource.clip != _leapSound)
			_audioSource.clip = _leapSound;

		// Set pitch
		_audioSource.pitch = 2;
		_audioSource.Play();

		// Move and rotate player
		Vector3 curRotEuler = transform.rotation.eulerAngles;
		_targetPosition = curPos;

		switch (direction)
		{
			case Direction.Up:
				curRotEuler.z = 0;
				_targetPosition.y = Mathf.Clamp(_targetPosition.y + 0.64f, MinBoundY, MaxBoundY);
				break;
			case Direction.Down:
				curRotEuler.z = 180;
				_targetPosition.y = Mathf.Clamp(_targetPosition.y - 0.64f, MinBoundY, MaxBoundY);
				break;
			case Direction.Left:
				curRotEuler.z = 90;
				_targetPosition.x = Mathf.Clamp(_targetPosition.x - 0.64f, MinBoundX, MaxBoundX);
				break;
			case Direction.Right:
				curRotEuler.z = -90;
				_targetPosition.x = Mathf.Clamp(_targetPosition.x + 0.64f, MinBoundX, MaxBoundX);
				break;
		}

		// Set rotation
		transform.rotation = Quaternion.Euler(curRotEuler);
	}

	public void Die()
	{
		// Play death animation
		_animator.Play("Death");

		// Play death sound
		_audioSource.clip = _deathSound;
		_audioSource.Play();

		// Set boolean
		Dead = true;

		// Subtract life
		Lives--;

		// Check for game over
		if (Lives <= 0)
		{
			// TODO Reset game
		}

		// Reset position
		Invoke("ResetPosition", 1f);
	}

	public void ResetPosition()
	{
		// Reset position and target position
		Vector3 newPos = new Vector3(0.0f, MinBoundY, 0.0f);
		transform.position = newPos;
		_targetPosition = newPos;

		// Reset animation
		_animator.Play("Idle");

		// Reset dead boolean
		Dead = false;
	}
}