using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources {
	public class InterfaceBar : MonoBehaviour {
		private PlayerStatsData _playerStatsData;
		private Slider _bar;
		[SerializeField]
		private BarType _barType;

		private void Awake() {
			_playerStatsData = Resources.Load<PlayerStatsData>("Data/PlayerStats");
			_bar = GetComponent<Slider>();
		}

		private void OnGUI() {
			switch (_barType) {
				case BarType.SelfConfidence:
					_bar.value = _playerStatsData.SelfConfidence / _playerStatsData.MaxSelfConfidence;
					break;
				case BarType.Mana:
					_bar.value = _playerStatsData.Mana / _playerStatsData.MaxMana;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public enum BarType {
		SelfConfidence,
		Mana
	}
}