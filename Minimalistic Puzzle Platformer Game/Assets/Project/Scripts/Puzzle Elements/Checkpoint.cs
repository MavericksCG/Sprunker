using UnityEngine;
using System.Collections;
using Sprunker.Managing;

namespace Sprunker.PuzzleElements {
    public class Checkpoint : MonoBehaviour {

        private GameManager m;
        [SerializeField] private float waitTime;
        [SerializeField] private float destructionDelay;

        [SerializeField] private GameObject text;
        private Animator textAnim;

        [SerializeField] private AudioSource checkpointSFX;

        [HideInInspector] public bool hasSetCheckpoint = false;


        private void Awake () {
            m = FindObjectOfType<GameManager>();
            if (text != null) {
                textAnim = text.GetComponent<Animator>();
            }
        }


        private void OnTriggerEnter2D (Collider2D col) {
            if (col.CompareTag("Player")) {
                SetPosition(transform.position);
                StartCoroutine(DoAnimations());
                checkpointSFX.Play();
            }
        }


        private IEnumerator DoAnimations () {
            hasSetCheckpoint = true;

            if (text != null) {
                text.SetActive(true);
                textAnim.SetTrigger("Enter Frame");
            }

            yield return new WaitForSeconds(waitTime);

            textAnim.SetTrigger("Exit Frame");
            Destroy(text, destructionDelay);
        }


        private Vector3 SetPosition (Vector3 position) {
            m.lastRecordedPosition = position;
            return position;
        }

    }
}
