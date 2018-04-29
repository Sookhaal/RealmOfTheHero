using UnityEngine;

namespace Sources {
	[ExecuteInEditMode]
	public class AutoSortOrder : MonoBehaviour {
		private Renderer _renderer;
		private Transform _transform;

		private void Awake() {
			_renderer = GetComponent<Renderer>();
			_transform = GetComponent<Transform>();
		}

		private void Update() {
			_renderer.sortingOrder = (int) -(_transform.position.y * 100f);
		}
	}
}