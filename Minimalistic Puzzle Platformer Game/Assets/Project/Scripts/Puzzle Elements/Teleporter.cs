using System;
using UnityEngine;
using Sprunker.Player;

namespace Sprunker.PuzzleElements {
	public class Teleporter : MonoBehaviour {

		private PlayerController c;
		public Transform destination;
		//[SerializeField] private bool killPlayer = false;

		private void Start() {
			c = FindObjectOfType<PlayerController>();
		}
		
		// private void OnCollisionEnter2D(Collision2D col) {
		// 	if (col.gameObject.CompareTag("Player") && killPlayer && c != null)
		// 		c.Die();
		// }

		public Transform GetDestination () {
			return destination;
		}
	}
}
