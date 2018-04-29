using System;
using System.Collections;
using Spine.Unity;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources {
	public class DatingEncounter : MonoBehaviour {
		private PlayerMovements _playerMovements;
		public Transform TargetTransform;
		public Transform PlayerTransform;
		public Vector3 DistanceFromPlayer;
		public DatingEncounterData DatingEncounterData;
		private PlayerStatsData _playerStatsData;
		[SerializeField]
		private DialogBox _leftDialog;
		[SerializeField]
		private DialogBox _rightDialog;

		private Coroutine _lose;
		private Coroutine _win;

		[SerializeField]
		private GameObject _uiBlocker;
		public bool ActionDialogDone;

		public SkeletonGraphic WaifuSkeleton;
		public GameObject Hearts;

		private void Awake() {
			_playerMovements = FindObjectOfType<PlayerMovements>();
			PlayerTransform = _playerMovements.GetComponent<Transform>();
			_playerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
		}

		private void Update() {
			DistanceFromPlayer = PlayerTransform.position - TargetTransform.position;
		}

		private void OnTriggerEnter2D(Collider2D c) {
			if (DatingEncounterData.EncounterCompleted)
				return;
			if (!c.CompareTag("Player"))
				return;
			_playerMovements.MovementTarget.MovementState = MovementState.Scripted;
			_playerMovements.MovementTarget.NewPosition.x = TargetTransform.position.x;
			_playerMovements.MovementTarget.NewPosition.y = TargetTransform.position.y;
			_playerStatsData.CurrentEncounter = this;

			StartCoroutine(_playerMovements.ActivateEncounter(this));
			_lose = StartCoroutine(LoseFight());
			_win = StartCoroutine(WinFight());
		}

		private IEnumerator LoseFight() {
			if (DatingEncounterData)
				yield break;
			yield return new WaitUntil(() => DatingEncounterData.Failures >= 4);
			StopCoroutine(_win);

			yield return _playerMovements.HideUI();
			DatingEncounterData.CurrentDialogIndex = 0;
			yield return ShowLostDialogs();
			DatingEncounterData.Failures = 0;
			DatingEncounterData.IntroDone = false;
			DatingEncounterData.CurrentDialogIndex = 0;
			_playerStatsData.CurrentEncounter = null;
			StartCoroutine(_playerMovements.LeaveEncounter());
		}

		private IEnumerator WinFight() {
			yield return new WaitUntil(() => DatingEncounterData.Arouseness >=
											 DatingEncounterData.RequiredArouseness);
			try {
				StopCoroutine(_lose);
			} catch (Exception e) { }

			yield return _playerMovements.HideUI();
			DatingEncounterData.CurrentDialogIndex = 0;
			yield return ShowWinDialogs();
			DatingEncounterData.Failures = 0;
			DatingEncounterData.IntroDone = false;
			DatingEncounterData.CurrentDialogIndex = 0;
			DatingEncounterData.EncounterCompleted = true;
			_playerStatsData.CurrentEncounter = null;
			StartCoroutine(_playerMovements.LeaveEncounter());
			Hearts.SetActive(true);
		}

		public IEnumerator ShowIntroDialogs() {
			DatingEncounterData.OutroDone = false;
			while (DatingEncounterData.CurrentDialogIndex < DatingEncounterData.IntroDialogTexts.Length) {
				var currentText = DatingEncounterData.IntroDialogTexts[DatingEncounterData.CurrentDialogIndex];
				if (currentText.Italic) {
					_leftDialog.Text.fontStyle = FontStyles.Italic;
					_rightDialog.Text.fontStyle = FontStyles.Italic;
				}

				_leftDialog.Text.text = currentText.Text;
				_rightDialog.Text.text = currentText.Text;

				yield return UpdateDialog(currentText);
			}

			DatingEncounterData.IntroDone = true;
		}

		public IEnumerator ShowWinDialogs() {
			yield return new WaitUntil(() => ActionDialogDone);
			while (DatingEncounterData.CurrentDialogIndex < DatingEncounterData.WinDialogTexts.Length) {
				var currentText = DatingEncounterData.WinDialogTexts[DatingEncounterData.CurrentDialogIndex];
				if (currentText.Italic) {
					_leftDialog.Text.fontStyle = FontStyles.Italic;
					_rightDialog.Text.fontStyle = FontStyles.Italic;
				}

				_leftDialog.Text.text = currentText.Text;
				_rightDialog.Text.text = currentText.Text;

				yield return UpdateDialog(currentText);
			}

			DatingEncounterData.OutroDone = true;
		}

		public IEnumerator ShowLostDialogs() {
			yield return new WaitUntil(() => ActionDialogDone);
			while (DatingEncounterData.CurrentDialogIndex < DatingEncounterData.LostDialogTexts.Length) {
				var currentText = DatingEncounterData.LostDialogTexts[DatingEncounterData.CurrentDialogIndex];
				if (currentText.Italic) {
					_leftDialog.Text.fontStyle = FontStyles.Italic;
					_rightDialog.Text.fontStyle = FontStyles.Italic;
				}

				_leftDialog.Text.text = currentText.Text;
				_rightDialog.Text.text = currentText.Text;

				yield return UpdateDialog(currentText);
			}

			DatingEncounterData.OutroDone = true;
		}

		public IEnumerator ShowLikeDialog() {
			yield return new WaitUntil(() => ActionDialogDone);
			WaifuSkeleton.AnimationState.SetAnimation(0, "horny", true);
			var rng = Random.Range(0, DatingEncounterData.LikeDialogTexts.Length - 1);
			var currentText = DatingEncounterData.LikeDialogTexts[rng];
			if (currentText.Italic) {
				_leftDialog.Text.fontStyle = FontStyles.Italic;
				_rightDialog.Text.fontStyle = FontStyles.Italic;
			}

			_leftDialog.Text.text = currentText.Text;
			_rightDialog.Text.text = currentText.Text;
			yield return UpdateDialog(currentText, false);

			_uiBlocker.SetActive(false);
		}

		public IEnumerator ShowDislikeDialog() {
			yield return new WaitUntil(() => ActionDialogDone);
			WaifuSkeleton.AnimationState.SetAnimation(0, "sad", true);
			var rng = Random.Range(0, DatingEncounterData.DislikeDialogTexts.Length - 1);
			var currentText = DatingEncounterData.DislikeDialogTexts[rng];
			if (currentText.Italic) {
				_leftDialog.Text.fontStyle = FontStyles.Italic;
				_rightDialog.Text.fontStyle = FontStyles.Italic;
			}

			_leftDialog.Text.text = currentText.Text;
			_rightDialog.Text.text = currentText.Text;
			yield return UpdateDialog(currentText, false);

			_uiBlocker.SetActive(false);
		}

		public IEnumerator ShowNeutralDialog() {
			yield return new WaitUntil(() => ActionDialogDone);
			WaifuSkeleton.AnimationState.SetAnimation(0, "idle", true);
			var rng = Random.Range(0, DatingEncounterData.NeutralDialogTexts.Length - 1);
			var currentText = DatingEncounterData.NeutralDialogTexts[rng];
			if (currentText.Italic) {
				_leftDialog.Text.fontStyle = FontStyles.Italic;
				_rightDialog.Text.fontStyle = FontStyles.Italic;
			}

			_leftDialog.Text.text = currentText.Text;
			_rightDialog.Text.text = currentText.Text;
			yield return UpdateDialog(currentText, false);

			_uiBlocker.SetActive(false);
		}

		public IEnumerator ShowActionDialog(ActionData actionData) {
			_uiBlocker.SetActive(true);

			var rng = Random.Range(0, actionData.TextPool.Length - 1);
			var currentText = actionData.TextPool[rng];

			_leftDialog.Text.text = currentText;
			yield return _leftDialog.ShowDialog();
			yield return new WaitForSeconds(1f);
			yield return _leftDialog.HideDialog();
			ActionDialogDone = true;
		}

		private IEnumerator UpdateDialog(DialogTextData currentText, bool increment = true) {
			switch (currentText.Side) {
				case Side.Left:
					yield return _leftDialog.ShowDialog();
					break;
				case Side.Right:
					yield return _rightDialog.ShowDialog();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			yield return new WaitForSeconds(1f);

			switch (currentText.Side) {
				case Side.Left:
					yield return _leftDialog.HideDialog();
					break;
				case Side.Right:
					yield return _rightDialog.HideDialog();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (increment)
				DatingEncounterData.CurrentDialogIndex++;
		}
	}
}