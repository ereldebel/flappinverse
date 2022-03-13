using UnityEngine;

namespace Scenes.Scripts
{
	public class ResetParentOnDisappear : MonoBehaviour
	{
		private void OnBecameInvisible()
		{
			transform.parent.GetComponent<Obstacle>().ResetPosition();
		}
	}
}