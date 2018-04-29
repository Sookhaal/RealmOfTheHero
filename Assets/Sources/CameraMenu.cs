using DG.Tweening;
using UnityEngine;

namespace Sources {
	public class CameraMenu : MonoBehaviour {
		[SerializeField]
		[Range(0f, 10f)]
		private float _moveSpeed = 10f;

		[SerializeField]
		[Range(1f, 25f)]
		private float _parallaxFactorX = 10f;

		[SerializeField]
		[Range(1f, 25f)]
		private float _parallaxFactorY = 5f;

		private Transform _transform;
		private Vector3 _newPos;
		private Vector3 _startingPos;
		private Camera _camera;

		private void Awake() {
			_camera = GetComponent<Camera>();
			_transform = GetComponent<Transform>();
			_newPos = _transform.position;
			_startingPos = _transform.position;
		}

		private void Update() {
			_newPos.x = _startingPos.x + (_camera.ScreenToViewportPoint(Input.mousePosition).x - .5f) / 2f * _parallaxFactorX;
			_newPos.y = _startingPos.y + (_camera.ScreenToViewportPoint(Input.mousePosition).y - .5f) / 2f * _parallaxFactorY;

			_transform.DOLocalMove(_newPos, _moveSpeed);
		}
	}
}