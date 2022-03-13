using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Scenes.Scripts
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
		[FormerlySerializedAs("balls")] [SerializeField] private Square[] squares;
		[SerializeField] private float initialSwapTime = 15, minTimeBetweenSwaps = 3, maxTimeBetweenSwaps = 10;
		[SerializeField] private SpriteRenderer bg;

		#endregion

		#region Private Fields

		private float _nextSwapTime;
		private int _points;

		#endregion

		#region Private Static Fields

		private static int _highScore;
		private static GameManager _shared;

		#endregion

		#region Constants

		private const string HighScorePref = "HighScore";

		#endregion

		#region Public C# Events

		public static event Action<int> PointsUpdated;
		public static event Action<int> HighScoreUpdated;

		#endregion

		#region Function Events

		private void Awake()
		{
			_shared = this;
			_nextSwapTime = Time.time + initialSwapTime;
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
			if (Time.time < _nextSwapTime - 1) return;
			SwapBirdColors();
			bg.flipY = !bg.flipY;
			_nextSwapTime += Random.Range(minTimeBetweenSwaps, maxTimeBetweenSwaps);
		}

		private void OnDestroy()
		{
			UpdateHighScore();
		}

		#endregion

		#region Private Methods

		private void SwapBirdColors()
		{
			foreach (var bird in squares)
				bird.ChangeColor();
		}

		#endregion

		#region Public Static Methods

		public static void EndGame()
		{
			UpdateHighScore();
			SceneManager.LoadScene(0);
		}

		public static void AddPoint()
		{
			PointsUpdated?.Invoke(++_shared._points);
		}

		#endregion

		#region Private Static Methods

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