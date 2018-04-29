using UnityEngine;

namespace Sources {
	public class LeftButtons : MonoBehaviour {
		[SerializeField]
		private GameObject _attacks;
		[SerializeField]
		private GameObject _spells;
		[SerializeField]
		private GameObject _items;
		[SerializeField]
		private GameObject _runAways;

		public void ShowAttacks() {
			HideEverything();
			_attacks.SetActive(true);
		}

		public void ShowSpells() {
			HideEverything();
			_spells.SetActive(true);
		}

		public void ShowItems() {
			HideEverything();
			_items.SetActive(true);
		}

		public void ShowRunAways() {
			HideEverything();
			_runAways.SetActive(true);
		}

		private void HideEverything() {
			_attacks.SetActive(false);
			_spells.SetActive(false);
			_items.SetActive(false);
			_runAways.SetActive(false);
		}
	}
}