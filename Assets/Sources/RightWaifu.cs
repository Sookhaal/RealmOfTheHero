using System;
using UnityEngine;

namespace Sources {
	public class RightWaifu : MonoBehaviour {
		[SerializeField]
		private GameObject _orc;
		[SerializeField]
		private GameObject _priestess;
		[SerializeField]
		private GameObject _rival1Female;
		[SerializeField]
		private GameObject _rival1Male;

		public void EnableCorrectWaifu(PlayerStatsData playerStatsData) {
			_orc.SetActive(false);
			_priestess.SetActive(false);
			_rival1Female.SetActive(false);
			_rival1Male.SetActive(false);

			switch (playerStatsData.CurrentEncounter.DatingEncounterData.EncounterCharacter) {
				case EncounterCharacter.Orc:
					_orc.SetActive(true);
					break;
				case EncounterCharacter.Priestess:
					_priestess.SetActive(true);
					break;
				case EncounterCharacter.RivalFemale:
					_rival1Female.SetActive(true);
					break;
				case EncounterCharacter.RivalMale:
					_rival1Male.SetActive(true);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}