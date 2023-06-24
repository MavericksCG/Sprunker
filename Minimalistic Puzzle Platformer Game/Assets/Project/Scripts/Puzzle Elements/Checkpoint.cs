using System;
using UnityEngine;
using System.Collections;
using Sprunker.Managing;
using Sprunker.Player;

namespace Sprunker.PuzzleElements {
    public class Checkpoint : MonoBehaviour {

        private GameManager m;
        private PlayerController pc;
        
        [SerializeField] private float waitTime;
        [SerializeField] private float destructionDelay;

        [SerializeField] private GameObject prompt;
        private Animator promptAnim;

        [SerializeField] private AudioSource checkpointSFX;

        [HideInInspector] public bool hasSetCheckpoint = false;

        [SerializeField] private bool rechargeDash = false;
        [SerializeField] private bool rechargeSuperJump = false;
        [SerializeField] private bool hasCheckpoint = false;

        private void Awake () {
            m = FindObjectOfType<GameManager>();
            if (prompt != null) {
                promptAnim = prompt.GetComponent<Animator>();
            }
        }

        private void Start () {
            pc = FindObjectOfType<PlayerController>();
        }

        private void OnTriggerEnter2D (Collider2D col) {
            if (col.CompareTag("Player")) {
                SetPosition(transform.position);
                StartCoroutine(DoAnimations());

                hasCheckpoint = true;

                if (hasCheckpoint && rechargeDash)
                    pc.canDash = true;
                
                if (hasCheckpoint && rechargeSuperJump)
                    pc.canSuperJump = true;
                
                else if (!hasCheckpoint) {
                    pc.canDash = false;
                    pc.canSuperJump = false;
                }

                // Debug.Log("triggered collider : " + col.gameObject.name);
                
                // Play the checkpoint sound effect ONLY when it is actually assigned
                if (checkpointSFX != null)
                    checkpointSFX.Play();
            }
        }

        // private void OnTriggerExit2D(Collider2D col) {
        //     if (col.CompareTag("Player")) {
        //         hasCheckpoint = false;
        //     }
        // }


        private IEnumerator DoAnimations () {
            hasSetCheckpoint = true;

            if (prompt != null) {
                prompt.SetActive(true);
                promptAnim.SetTrigger("sizein");
                // Debug.Log("activation");
            }

            yield return new WaitForSeconds(waitTime);

            if (prompt != null) {
                promptAnim.SetTrigger("sizeout");
                
                // Wait before disabling the prompt
                yield return new WaitForSeconds(destructionDelay);
                
                // The destroy method won't really work here since if we destroy the prompt every single time after the animation completes then things would get messy later on.
                // Instead, we just disable the prompt after a certain amount of delay to avoid any future errors related to this.
                // Because there are going to be multiple checkpoints on each level so that would just give me a headache.
                prompt.SetActive(false);

                // Debug.Log("successfully completed");
            }
        }


        private Vector3 SetPosition (Vector3 position) {
            m.lastRecordedPosition = position;
            return position;
        }

    }
}
