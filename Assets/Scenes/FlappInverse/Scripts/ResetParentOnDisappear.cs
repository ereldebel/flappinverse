using UnityEngine;

namespace Scenes.FlappInverse.Scripts
{
	public class ResetParentOnDisappear : MonoBehaviour
	{
		private void OnBecameInvisible()
		{
			transform.parent.GetComponent<Obstacle>().ResetPosition();
		}
	}
}