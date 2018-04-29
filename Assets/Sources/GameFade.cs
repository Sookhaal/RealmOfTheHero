using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources {
	public class GameFade : MonoBehaviour {
		private Image BlackFade;
		[SerializeField]
		private CanvasGroup _finalGroup;

		[SerializeField]
		private GameObject _heart1;
		[SerializeField]
		private GameObject _heart2;
		[SerializeField]
		private GameObject _heartMale;
		[SerializeField]
		private GameObject _heartFemale;
		private PlayerStatsData _playerStatsData;

		private void Awake() {
			BlackFade = GetComponent<Image>();
			BlackFade.DOFade(1f, 0f);
			StartCoroutine(Fade());
			StartCoroutine(WinGame());
			_playerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
		}

		private IEnumerator Fade() {
			yield return BlackFade.DOFade(0f, 2f).WaitForCompletion();
		}

		private IEnumerator WinGame() {
			yield return new WaitUntil(() => _heart1.activeInHierarchy && _heart2.activeInHierarchy);
			if (_playerStatsData.PlayerGenre == PlayerGenre.Female)
				yield return new WaitUntil(() => _heartMale.activeInHierarchy);
			else
				yield return new WaitUntil(() => _heartFemale.activeInHierarchy);

			yield return _finalGroup.DOFade(1f, 3f).WaitForCompletion();
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("MainMenu");
		}
	}
}