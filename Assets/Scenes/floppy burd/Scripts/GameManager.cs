using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Scenes.floppy_burd.Scripts
{
	public class GameManager : MonoBehaviour
	{
		#region Public Properties

		public static float ObstacleResetDistance => _shared.numOfObstacles * _shared.gapBetweenObstacles;

		#endregion

		#region Serialized Private Fields

		[SerializeField] private GameObject obstacle;
		[SerializeField] private float initialGap, gapBetweenObstacles;
		[SerializeField] private int numOfObstacles = 5;

		#endregion

		#region Private Fields

		private int _points;
		private bool _switched = false;

		#endregion

		#region Private Static Fields

		private static int _highScore;
		private static GameManager _shared;

		#endregion

		#region Constants

		private const string HighScorePref = "BurdHighScore";
		private const int FlappInverse = 0;

		#endregion

		#region Public C# Events

		public static event Action<int> PointsUpdated;
		public static event Action<int> HighScoreUpdated;

		#endregion

		#region Function Events

		private void Awake()
		{
			_shared = this;
			for (var i = 0; i < numOfObstacles; ++i)
				Instantiate(obstacle, Vector3.right * (initialGap + i * gapBetweenObstacles), Quaternion.identity);
		}

		private void Start()
		{
			RestoreHighScore();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
			if (Input.GetKeyDown(KeyCode.Tab) || (Input.touchCount == 5 && Input.GetTouch(4).phase == TouchPhase.Began))
				SwitchLevel();
		}

		private void OnDestroy()
		{
			UpdateHighScore();
		}

		#endregion

		#region Public Static Methods

		public static void EndGame()
		{
			UpdateHighScore();
			if (!_shared._switched)
				SceneManager.LoadScene(1);
		}

		public static void AddPoint()
		{
			PointsUpdated?.Invoke(++_shared._points);
		}

		#endregion

		#region Private Static Methods

		private static void SwitchLevel()
		{
			SceneManager.LoadScene(FlappInverse);
			_shared._switched = true;
		}

		private static void UpdateHighScore()
		{
			if (_shared._points <= _highScore) return;
			_highScore = _shared._points;
			HighScoreUpdated?.Invoke(_highScore);
			PlayerPrefs.SetInt(HighScorePref, _highScore);
			PlayerPrefs.Save();
		}

		private static void RestoreHighScore()
		{
			_highScore = PlayerPrefs.GetInt(HighScorePref, 0);
			;
			HighScoreUpdated?.Invoke(_highScore);
		}

		#endregion
	}
}