using UnityEngine;

namespace Sources {
	[CreateAssetMenu(menuName = "Data/DialogText")]
	public class DialogTextData : ScriptableObject {
		public string Text;
		public Side Side;
		public bool Italic;
	}

	public enum Side {
		Left,
		Right
	}
}