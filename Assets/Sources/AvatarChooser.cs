using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources {
	public class AvatarChooser : MonoBehaviour {
		public AvatarChooser Other;
		public bool CanClick = true;
		public PlayerGenre AvatarGenre;
		public PlayerStatsData PlayerStatsData;
		public Image BlackFade;

		private void Awake() {
			PlayerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
		}

		private void OnMouseDown() {
			if (!CanClick)
				return;
			Other.CanClick = false;
			PlayerStatsData.PlayerGenre = AvatarGenre;
			StartCoroutine(LoadScene());
		}

		private IEnumerator LoadScene() {
			yield return BlackFade.DOFade(1f, 2f).WaitForCompletion();
			SceneManager.LoadScene("GameScreen");
		}
	}
}