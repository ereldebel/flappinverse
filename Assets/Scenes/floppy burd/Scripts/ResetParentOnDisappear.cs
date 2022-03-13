using UnityEngine;

namespace Scenes.floppy_burd.Scripts
{
	public class ResetParentOnDisappear : MonoBehaviour
	{
		private void OnBecameInvisible()
		{
			transform.parent.GetComponent<Obstacle>().ResetPosition();
		}
	}
}