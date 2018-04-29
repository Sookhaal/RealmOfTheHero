using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sources {
	public class PlayerMovements : MonoBehaviour {
		private Transform _transform;
		public Image Snake1;
		public Image Snake2;
		public MovementTarget MovementTarget;
		public Animator EncounterAnimator;
		private PlayerStatsData _playerStatsData;
		[SerializeField]
		private GameObject _male;
		[SerializeField]
		private GameObject _female;

		[SerializeField]
		private RightWaifu _rightWaifu;

		[SerializeField]
		private GameObject _femaleBlacksmith;
		[SerializeField]
		private GameObject _maleBlacksmith;
		private bool _canMove;

		private void Awake() {
			_transform = GetComponent<Transform>();
			EncounterAnimator.gameObject.SetActive(true);
			_playerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
			switch (_playerStatsData.PlayerGenre) {
				case PlayerGenre.Male:
					_male.SetActive(true);
					MovementTarget.ViewAnimator = _male.GetComponent<Animator>();
					_femaleBlacksmith.SetActive(true);
					break;
				case PlayerGenre.Female:
					_female.SetActive(true);
					MovementTarget.ViewAnimator = _female.GetComponent<Animator>();
					_maleBlacksmith.SetActive(true);
					break;
			}

			StartCoroutine(Fix());
		}

		public IEnumerator ActivateEncounter(DatingEncounter encounter) {
			yield return new WaitUntil(() => encounter.DistanceFromPlayer.magnitude <= 0.1f);

			_rightWaifu.EnableCorrectWaifu(_playerStatsData);
			MovementTarget.MovementState = MovementState.InCombat;
			Snake1.DOFade(1f, 0f);
			Snake2.DOFade(1f, 0f);
			yield return Snake1.DOFillAmount(1f, 1f).SetEase(Ease.Linear).WaitForCompletion();
			yield return Snake2.DOFillAmount(1f, .5f).SetEase(Ease.Linear).WaitForCompletion();
			EncounterAnimator.SetTrigger("IntroEncounter");
			yield return new WaitForSeconds(2f);
			StartCoroutine(encounter.ShowIntroDialogs());
			yield return new WaitUntil(() => encounter.DatingEncounterData.IntroDone);
			EncounterAnimator.SetTrigger("IntroUI");
		}

		private IEnumerator Fix() {
			yield return new WaitForSeconds(.5f);
			_canMove = true;
		}

		private void Update() {
			if (!_canMove)
				return;
			_transform.DOMove(MovementTarget.TargetPosition, MovementTarget.Speed)
					  .SetEase(Ease.Linear);
		}

		public IEnumerator HideUI() {
			EncounterAnimator.SetTrigger("OutroUI");
			Snake1.DOFade(0f, 0f);
			Snake2.DOFade(0f, 0f);
			yield return new WaitForSeconds(1.5f);
		}

		public IEnumerator LeaveEncounter() {
			EncounterAnimator.SetTrigger("OutroEncounter");
			yield return new WaitForSeconds(1.5f);
			Snake1.fillAmount = 0f;
			Snake2.fillAmount = 0f;
			MovementTarget.MovementState = MovementState.Controlled;
		}
	}
}