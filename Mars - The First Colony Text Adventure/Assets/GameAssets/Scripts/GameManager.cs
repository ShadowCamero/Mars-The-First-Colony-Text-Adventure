﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	[SerializeField] State startingState;
	State state;

	// Setup chapters and scenes
	//public Chapter chapter = Chapter.One;
	//public enum Chapter { One, Two, Three };
	//public Scene scene = Scene.Start;
	//public enum Scene { Start };

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		state = startingState;
		if(SceneManager.GetActiveScene().buildIndex == 0)
		{
			StartCoroutine(ToMainMenu());
		}

		if(SceneManager.GetActiveScene().buildIndex == 2)
		{
			GUIManager.Instance.storyText.text = state.GetStateStory();
		}
	}

	private void Update()
	{
		ManageState();

		//switch (chapter)
		//{
		//	case Chapter.One:
		//		ChapterOne();
		//		break;
		//	case Chapter.Two:
		//		ChapterTwo();
		//		break;
		//	case Chapter.Three:
		//		ChapterThree();
		//		break;
		//}

		//if (Input.anyKeyDown)
		//{			
		//	StartCoroutine(GUIManager.Instance.FirstFade());
		//	SaveManager.Instance.Load();
		//	Debug.Log("Current chapter: " + SaveManager.Instance.state.Chapter);
		//}

		//if (Input.GetKeyDown(KeyCode.S))
		//{
		//	SaveManager.Instance.Save();
		//	Debug.Log("Current chapter: " + SaveManager.Instance.state.Chapter);
		//}
	}

	// Chapters
	#region
	// One
	//private void ChapterOne()
	//{
	//	switch (scene)
	//	{
	//		case Scene.Start:
	//			SceneStart();
	//			break;
	//	}
	//}

	//// Two
	//private void ChapterTwo()
	//{

	//}

	//// Three
	//private void ChapterThree()
	//{

	//}

	#region
	// Start
	private void SceneStart()
	{
		GUIManager.Instance.storyText.text = "You wake up in bed. It is 5:00 Martian Time.";
	}
	#endregion

	private IEnumerator ToMainMenu()
	{
		yield return new WaitForSeconds(6);
		SceneManager.LoadScene(1);
	}
	#endregion

	private void ManageState()
	{
		var nextStates = state.GetNextStates();

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			state = nextStates[0];
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			state = nextStates[1];
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			state = nextStates[2];
		}

		GUIManager.Instance.storyText.text = state.GetStateStory();
	}
}