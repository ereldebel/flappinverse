using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.FlappInverse.Scripts
{
	public class Obstacle : MonoBehaviour
	{
		#region Serialized Fields

		[SerializeField] private float speed, minLength = 3;
		[SerializeField] private GameObject bottomObstacle, topObstacle;

		#endregion

		#region Private Fields

		private float _maxLength;

		private Transform _transform;

		#endregion

		#region Constants

		private const float ScreenHeight = 10;

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_maxLength = ScreenHeight - minLength;
			ChooseDivisionPosition();
		}

		private void Update()
		{
			Vector2 pos = _transform.position;
			pos.x -= speed * Time.deltaTime;
			_transform.position = pos;
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			GameManager.AddPoint();
		}

		#endregion

		#region Public Events

		public void ResetPosition()
		{
			Vector2 pos = _transform.position;
			pos.x += GameManager.ObstacleResetDistance;
			_transform.position = pos;
		}

		#endregion

		#region Private Events

		private void ChooseDivisionPosition()
		{
			var length = Random.Range(minLength, _maxLength);
			var bottomScale = bottomObstacle.transform.localScale;
			bottomScale.y = length;
			bottomObstacle.transform.localScale = bottomScale;
			var topScale = topObstacle.transform.localScale;
			topScale.y = ScreenHeight - length;
			topObstacle.transform.localScale = topScale;
		}

		#endregion
	}
}