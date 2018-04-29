using UnityEngine;

namespace Sources {
	[CreateAssetMenu(menuName = "Data/ActionData")]
	public class ActionData : ScriptableObject {
		public string UIText;
		public string[] TextPool;
		public float SelfConfidenceCost;
		public float ManaCost;
		public bool Unlocked;
		public bool DefaultUnlocked;
		public ActionType ActionType;
		public ActionElement ActionElement;

		private void OnEnable() {
			Unlocked = DefaultUnlocked;
		}
	}

	public enum ActionType {
		Attack,
		Spell,
		Item,
		RunAway
	}

	public enum ActionElement {
		None,
		Maso,
		Sarc,
		Sens
	}
}