using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources {
	public class ActionUIElement : MonoBehaviour {
		public ActionData ActionData;
		public TMP_Text Text;
		private PlayerStatsData _playerStatsData;
		public Button Button;

		private void Awake() {
			Button = GetComponent<Button>();
			_playerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
			Text.text = "";
			Button.interactable = false;
			StartCoroutine(UnlockAction());
		}

		private IEnumerator EnableAction() {
			yield return new WaitUntil(() => ActionData.Unlocked &&
											 _playerStatsData.Mana - ActionData.ManaCost >= 0f);
			Button.interactable = true;
			StartCoroutine(DisableAction());
		}

		private IEnumerator DisableAction() {
			yield return new WaitUntil(() => ActionData.Unlocked &&
											 _playerStatsData.Mana - ActionData.ManaCost < 0f);
			Button.interactable = false;
			StartCoroutine(EnableAction());
		}

		private IEnumerator UnlockAction() {
			yield return new WaitUntil(() => ActionData.Unlocked);
			Text.text = ActionData.UIText;
			Button.interactable = true;
			StartCoroutine(DisableAction());
		}

		public void DoAction() {
			_playerStatsData.Mana -= ActionData.ManaCost;
			_playerStatsData.SelfConfidence -= ActionData.SelfConfidenceCost;
			_playerStatsData.CurrentEncounter.ActionDialogDone = false;
			StartCoroutine(_playerStatsData.CurrentEncounter.ShowActionDialog(ActionData));

			if (ActionData.ActionElement == _playerStatsData.CurrentEncounter.DatingEncounterData.Like) {
				_playerStatsData.CurrentEncounter.DatingEncounterData.Arouseness += 25f;
				StartCoroutine(_playerStatsData.CurrentEncounter.ShowLikeDialog());
			}
			else if (ActionData.ActionElement == _playerStatsData.CurrentEncounter.DatingEncounterData.Hate) {
				_playerStatsData.CurrentEncounter.DatingEncounterData.Failures++;
				StartCoroutine(_playerStatsData.CurrentEncounter.ShowDislikeDialog());
			} else {
				StartCoroutine(_playerStatsData.CurrentEncounter.ShowNeutralDialog());
			}
		}
	}
}