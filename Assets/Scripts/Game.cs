using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	// Text variables
	private Text _levelText, _scoreText, _timerText;

	// Game variables
	public int Level = 1;
	public float Time = 10;

	private int _score;
	public int Score
	{
		get { return _score; }
		set
		{
			// Set value and update text
			_score = value;
			_scoreText.text = _score.ToString();
		}
	}

	public int LilypadsOccupied;

	private CountdownTimer _countdownTimer;
	private Player _player;

	// Audio components
	private AudioSource _audioSource;
	private AudioClip _winSound, _successSound, _loseSound;

	private void Start()
	{
		// Get components
		_levelText = GameObject.Find("LevelText").GetComponent<Text>();
		_scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		_timerText = GameObject.Find("TimerText").GetComponent<Text>();

		_player = GameObject.Find("Frog").GetComponent<Player>();
		_audioSource = GetComponent<AudioSource>();

		// Get audio clips
		_winSound = (AudioClip) Resources.Load("Audio/win");
		_successSound = (AudioClip)Resources.Load("Audio/success");
		_loseSound = (AudioClip)Resources.Load("Audio/lose");

		// Set text
		_levelText.text = Level.ToString();
		_scoreText.text = Score.ToString();
		_timerText.text = Time.ToString();

		// Set timer
		_countdownTimer = new CountdownTimer {Seconds = Time};
		_countdownTimer.Begin();
	}

	private void Update()
	{
		// Check if the timer is done
		_countdownTimer.Update();

		if (_countdownTimer.IsDone())
			// Level failed (Game Over is automatically triggered by the player when lives == 0)
			_player.Die();
		else
			// Update timer text
			_timerText.text = _countdownTimer.Seconds.ToString("00");

		// Check if all 5 lilypads have been occupied
		if (LilypadsOccupied == 5)
			NextLevel();
	}

	public void NextLevel()
	{
		// Play sound
		PlaySound("Win");

		// Add a life
		_player.Lives++;
	}

	public void GameOver()
	{
		// Play sound
		PlaySound("Lose");
		// Return to the main menu
		SceneManager.LoadScene("Menu");
	}

	public void PlaySound(string sound)
	{
		switch (sound)
		{
			case "Success":
				_audioSource.clip = _successSound;
				break;
			case "Win":
				_audioSource.clip = _winSound;
				break;
			case "Lose":
				_audioSource.clip = _loseSound;
				break;
		}
	}
}