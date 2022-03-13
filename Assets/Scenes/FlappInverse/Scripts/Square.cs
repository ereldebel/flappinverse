using System.Collections;
using UnityEngine;

namespace Scenes.FlappInverse.Scripts
{
	public class Square : MonoBehaviour
	{
		private enum Direction
		{
			Up,
			Down
		}

		#region Serialized Fields

		[SerializeField] private Sprite black, white;
		[SerializeField] private Direction flightDirection;
		[SerializeField] private float jumpForce, immunityOpaqueness = 0.5f;
		[SerializeField] private bool isBlack;
		[SerializeField] private AudioClip tapSound;

		#endregion

		#region Private Fields

		private Rigidbody2D _rigidbody2D;
		private SpriteRenderer _spriteRenderer;
		private Collider2D _collider2D;
		private AudioSource _audioSource;

		#endregion

		#region Constants

		private const int BlackLayer = 6, WhiteLayer = 7;

		#endregion

		#region Function Events

		private void Awake()
		{
			if (flightDirection == Direction.Down)
				jumpForce = -jumpForce;
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_collider2D = GetComponent<Collider2D>();
			_audioSource = GetComponent<AudioSource>();
		}

		private void Update()
		{
			if (!Input.GetKeyDown(KeyCode.Space) &&
			    (Input.touchCount == 0 || Input.GetTouch(Input.touchCount - 1).phase != TouchPhase.Began)) return;
			_rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			_rigidbody2D.AddTorque(jumpForce);
			if (isBlack)
				_audioSource.PlayOneShot(tapSound);
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			GameManager.EndGame();
		}

		private void OnBecameInvisible()
		{
			GameManager.EndGame();
		}

		#endregion

		#region Public Methods

		public void ChangeColor()
		{
			_collider2D.enabled = false;
			var color = _spriteRenderer.color;
			color.a = immunityOpaqueness;
			_spriteRenderer.color = color;
			Sprite newColor;
			if (isBlack)
			{
				newColor = white;
				gameObject.layer = WhiteLayer;
				isBlack = false;
			}
			else
			{
				newColor = black;
				gameObject.layer = BlackLayer;
				isBlack = true;
			}

			StartCoroutine(TimedImmunity(newColor));
		}

		#endregion

		#region Private Methods

		private IEnumerator TimedImmunity(Sprite sprite)
		{
			yield return new WaitForSeconds(1);
			_spriteRenderer.sprite = sprite;
			_collider2D.enabled = true;
			var color = _spriteRenderer.color;
			color.a = 1;
			_spriteRenderer.color = color;
		}

		#endregion
	}
}