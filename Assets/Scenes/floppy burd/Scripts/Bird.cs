using UnityEngine;

namespace Scenes.floppy_burd.Scripts
{
	public class Bird : MonoBehaviour
	{
		#region Serialized Fields

		[SerializeField] private float jumpForce;
		[SerializeField] private AudioClip tapSound;
		[SerializeField] private Sprite idle, flap;

		#endregion

		#region Private Fields

		private Rigidbody2D _rigidbody2D;
		private AudioSource _audioSource;
		private SpriteRenderer _spriteRenderer;

		#endregion

		#region Function Events

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_audioSource = GetComponent<AudioSource>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) ||
			    (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began))
			{
				_rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				_audioSource.PlayOneShot(tapSound);
				_spriteRenderer.sprite = flap;
			}
			if (Input.GetKeyUp(KeyCode.Space) ||
			    (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended))
				_spriteRenderer.sprite = idle;
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
	}
}