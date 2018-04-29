using UnityEngine;

namespace Sources {
	[ExecuteInEditMode]
	public class EncounterGraphicsScaler : MonoBehaviour {
		[SerializeField]
		private float _baseScale;
		private RectTransform _rectTransform;
		private Vector3 _newScale;
		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
			_newScale.x = _baseScale;
			_newScale.y = _baseScale;
			_newScale.z = 1f;
		}

		private void Update() {
			_newScale.x = _baseScale * (Camera.main.pixelWidth/ 1920f);
			_newScale.y = _baseScale * (Camera.main.pixelWidth/ 1920f);
			_rectTransform.localScale = _newScale;
		}
	}
}