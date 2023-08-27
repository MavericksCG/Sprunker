using System;
using UnityEngine;

namespace Sprunker.PuzzleElements {

    public class NegativeGravityArea : MonoBehaviour {
        
        [Header("Variables")]
        [Range(0f, -5f), SerializeField] private float alteredGravScale;
        private Rigidbody2D prb;

        private void Start() {
            prb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                prb.gravityScale = alteredGravScale;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                prb.gravityScale = 1f;
            }
        }
    }

}
