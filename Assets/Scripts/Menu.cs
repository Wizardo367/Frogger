using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	// Load game
	public void LoadGame()
	{
		SceneManager.LoadSceneAsync("Game");
	}
}