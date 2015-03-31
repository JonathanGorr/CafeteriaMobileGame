using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public bool inMenu;

	[SerializeField, HideInInspector]
	public GameObject _pauseScreen;

	[HideInInspector]
	public bool paused;

	private void Awake()
	{
		Application.targetFrameRate = 60;

		_pauseScreen = GameObject.Find("PauseMenu");

		//turn off on level start
		if(_pauseScreen != null)
			_pauseScreen.SetActive(false);
		else
			print("No pause screen was found");

		inMenu = false;

		//print(inMenu);
	}

	private void Update()
	{
		//restart
		if(Input.GetKeyDown(KeyCode.R))
		{
			//Resume();
			Restart();
		}

		//pause
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;

			if(paused)
			{
				Pause();
			}
			else
				Resume();
		}
	}

	private void Pause()
	{
		inMenu = true;
		_pauseScreen.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void Resume()
	{
		inMenu = false;
		_pauseScreen.SetActive(false);
		Time.timeScale = 1;
	}

	public void Restart()
	{
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void GoToMenu()
	{
		Application.LoadLevel(0);
	}

	public void NextLevel()
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
