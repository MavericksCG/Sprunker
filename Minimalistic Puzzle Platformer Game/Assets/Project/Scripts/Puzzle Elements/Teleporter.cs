using UnityEngine;

namespace Sprunker.PuzzleElements {
	public class Teleporter : MonoBehaviour {

		public Transform destination;

		public Transform GetDestination () {
			return destination;
		}
	}
}
