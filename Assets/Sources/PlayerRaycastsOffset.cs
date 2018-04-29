using DG.Tweening;
using UnityEngine;

namespace Sources {
	public class PlayerRaycastsOffset : MonoBehaviour {
		public MovementTarget MovementTarget;
		private Transform _transform;

		private void Awake() {
			_transform = GetComponent<Transform>();
		}

		private void Update() {
			if (MovementTarget.NewPosition.magnitude <= 0.1f)
				return;
			var angle = Mathf.Atan2(MovementTarget.NewPosition.y, MovementTarget.NewPosition.x) *
						Mathf.Rad2Deg;
			var rot = Quaternion.AngleAxis(angle, Vector3.forward);

			_transform.DORotate(rot.eulerAngles, 0f);
		}
	}
}