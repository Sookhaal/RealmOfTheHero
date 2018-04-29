using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources {
	public class DialogBox : MonoBehaviour {
		public TMP_Text Text;
		public Image DialogImage;

		private void Awake() {
			DOTween.ToAlpha(() => Text.color, x => Text.color = x, 0f, 0f);
			DialogImage.DOFade(0f, 0f);
		}

		public IEnumerator ShowDialog() {
			DOTween.ToAlpha(() => Text.color, x => Text.color = x, 1f, .5f);
			DialogImage.DOFade(1f, .5f);
			Text.ForceMeshUpdate();
			var totalVisibleCharacters = Text.textInfo.characterCount;
			var visibleCount = 0;
			var counter = 0;
			while (visibleCount < totalVisibleCharacters) {
				visibleCount = counter % (totalVisibleCharacters + 1);
				Text.maxVisibleCharacters = visibleCount;
				counter++;
				yield return new WaitForSeconds(.01f);
			}
		}

		public IEnumerator HideDialog() {
			DOTween.ToAlpha(() => Text.color, x => Text.color = x, 0f, .5f);
			yield return DialogImage.DOFade(0f, .5f).WaitForCompletion();
		}
	}
}