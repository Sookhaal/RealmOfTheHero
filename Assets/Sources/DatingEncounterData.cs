using UnityEngine;

namespace Sources {
	[CreateAssetMenu(menuName = "Data/DatingEncounterData")]
	public class DatingEncounterData : ScriptableObject {
		public ActionElement Like;
		public ActionElement Neutral;
		public ActionElement Hate;

		public float Arouseness;
		public float DefaultArouseness;

		public bool EncounterCompleted;
		public bool CannotLose;

		// Lose fight at 4
		public int Failures;

		public float RequiredArouseness;

		public DialogTextData[] IntroDialogTexts;
		public DialogTextData[] WinDialogTexts;
		public DialogTextData[] LostDialogTexts;

		public DialogTextData[] LikeDialogTexts;
		public DialogTextData[] DislikeDialogTexts;
		public DialogTextData[] NeutralDialogTexts;

		public int CurrentDialogIndex;
		public bool IntroDone;
		public bool OutroDone;

		public EncounterCharacter EncounterCharacter;

		private void OnEnable() {
			Arouseness = DefaultArouseness;
			EncounterCompleted = false;
			IntroDone = false;
			OutroDone = false;
			CurrentDialogIndex = 0;
			Failures = 0;
		}
	}

	public enum EncounterCharacter {
		Orc,
		Priestess,
		RivalFemale,
		RivalMale
	}
}