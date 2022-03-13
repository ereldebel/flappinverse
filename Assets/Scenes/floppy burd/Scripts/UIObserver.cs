using TMPro;
using UnityEngine;

namespace Scenes.floppy_burd.Scripts
{
	public class UIObserver : MonoBehaviour
	{
		#region Serialized Fields

		[SerializeField] private TextMeshProUGUI pointsUI, highScoreUI;

		#endregion

		#region Function Events

		private void Awake()
		{
			GameManager.PointsUpdated += UpdatePoints;
			GameManager.HighScoreUpdated += UpdateHighScore;
		}

		private void OnDestroy()
		{
			GameManager.PointsUpdated -= UpdatePoints;
			GameManager.HighScoreUpdated -= UpdateHighScore;
		}

		#endregion

		#region Private Methods

		private void UpdatePoints(int points)
		{
			pointsUI.text = points.ToString();
		}

		private void UpdateHighScore(int highScore)
		{
			highScoreUI.text = highScore.ToString();
		}

		#endregion
	}
}