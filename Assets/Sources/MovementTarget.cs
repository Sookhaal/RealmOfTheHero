using UnityEngine;

namespace Sources {
	public class MovementTarget : MonoBehaviour {
		private Transform _transform;

		[SerializeField]
		private Transform[] _raycastOrigins;

		public Vector3 NewPosition;
		public Vector3 TargetPosition;
		public MovementState MovementState;
		public float Speed = 1f;
		public bool AdjustedCollisions;

		public Animator ViewAnimator;
		public Vector3 NewScale;

		private void Awake() {
			_transform = GetComponent<Transform>();
			NewPosition = _transform.position;
			MovementState = MovementState.Controlled;
			NewScale = ViewAnimator.GetComponent<Transform>().localScale;
		}

		private void Update() {
			if (AdjustedCollisions)
				return;
			switch (MovementState) {
				case MovementState.Controlled:
					if (Input.GetAxisRaw("Horizontal") > 0f)
						NewPosition.x = 1f;
					else if (Input.GetAxisRaw("Horizontal") < 0f)
						NewPosition.x = -1f;
					else
						NewPosition.x = 0f;

					if (Input.GetAxisRaw("Vertical") > 0f)
						NewPosition.y = 1f;
					else if (Input.GetAxisRaw("Vertical") < 0f)
						NewPosition.y = -1f;
					else
						NewPosition.y = 0f;

					if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f ||
						Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0f)
						ViewAnimator.SetFloat("Speed", 1f);
					else
						ViewAnimator.SetFloat("Speed", 0f);

					if (NewPosition.x > 0f)
						NewScale.x = -1f;
					else if (NewPosition.x < 0f)
						NewScale.x = 1f;
					ViewAnimator.GetComponent<Transform>().localScale = NewScale;
					AdjustWithCollisions();
					SetTargetPosition();
					break;
				case MovementState.Scripted:
					_transform.position = NewPosition;
					_transform.localPosition = _transform.localPosition.normalized;
					TargetPosition = _transform.position;
					if (_transform.localPosition.x > 0f)
						NewScale.x = -1f;
					if (_transform.localPosition.x < 0f)
						NewScale.x = 1f;
					ViewAnimator.GetComponent<Transform>().localScale = NewScale;
					ViewAnimator.SetFloat("Speed", 1f);
					break;
				case MovementState.InCombat:
					NewPosition.x = 0f;
					NewPosition.y = 0f;
					SetTargetPosition();
					ViewAnimator.SetFloat("Speed", 0f);
					break;
			}
		}

		private void AdjustWithCollisions() {
			foreach (var raycastOrigin in _raycastOrigins) {
				var r = Physics2D.Raycast(raycastOrigin.position,
										  NewPosition,
										  .5f);
				if (!r.collider)
					continue;
				var t = Vector3.Cross(r.normal, NewPosition);
				var d = Vector3.Cross(t, r.normal);
				d.Normalize();
				d *= NewPosition.magnitude;
				NewPosition.x = Mathf.Abs(NewPosition.x + r.normal.x) > 0.1f ? d.x : 0f;
				NewPosition.y = Mathf.Abs(NewPosition.y + r.normal.y) > 0.1f ? d.y : 0f;
				break;
			}
		}

		private void SetTargetPosition() {
			NewPosition.Normalize();
			_transform.localPosition = NewPosition;

			TargetPosition = _transform.position;
		}
	}

	public enum MovementState {
		Controlled,
		Scripted,
		InCombat
	}
}