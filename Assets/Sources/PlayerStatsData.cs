using UnityEngine;

namespace Sources {
	[CreateAssetMenu(menuName = "Data/PlayerStats")]
	public class PlayerStatsData : ScriptableObject {
		public float SelfConfidence;
		public float MaxSelfConfidence;
		public float DefaultMaxSelfConfidence;
		public float Mana;
		public float MaxMana;
		public float DefaultMaxMana;

		public DatingEncounter CurrentEncounter;

		public PlayerGenre PlayerGenre;

		private void OnEnable() {
			MaxSelfConfidence = DefaultMaxSelfConfidence;
			SelfConfidence = MaxSelfConfidence;
			MaxMana = DefaultMaxMana;
			Mana = MaxMana;
			CurrentEncounter = null;
		}
	}
}