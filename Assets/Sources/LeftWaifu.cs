using UnityEngine;

namespace Sources {
	public class LeftWaifu : MonoBehaviour {
		private PlayerStatsData _playerStatsData;

		[SerializeField]
		private GameObject _male;
		[SerializeField]
		private GameObject _female;

		private void Awake() {
			_playerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
			switch (_playerStatsData.PlayerGenre) {
				case PlayerGenre.Male:
					_male.SetActive(true);
					break;
				case PlayerGenre.Female:
					_female.SetActive(true);
					break;
			}
		}
	}

	public enum PlayerGenre {
		Male,
		Female
	}
}