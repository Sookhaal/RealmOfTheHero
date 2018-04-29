using UnityEngine;

namespace Sources {
	public class MenuButton : MonoBehaviour {
		[SerializeField]
		private Animator _introAnimator;
		public bool IntroStarted = true;

		private void Awake() {
			_introAnimator.SetTrigger("FirstFade");
		}

		private void OnMouseDown() {
			if (IntroStarted)
				return;
			_introAnimator.SetTrigger("StartIntro");
			IntroStarted = true;
		}
	}
}