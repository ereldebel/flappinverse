using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.floppy_burd.Scripts
{
	public class Obstacle : MonoBehaviour
	{
		#region Serialized Fields

		[SerializeField] private float speed, minHeight = -2, maxHeight = 4;
		[SerializeField] private GameObject pole;

		#endregion

		#region Private Fields
		
		private Transform _transform;

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			ChoosePoleHeight();
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

		private void ChoosePoleHeight()
		{
			var pos = pole.transform.position;
			pos.y = Random.Range(minHeight, maxHeight);
			pole.transform.position = pos;
		}

		#endregion
	}
}