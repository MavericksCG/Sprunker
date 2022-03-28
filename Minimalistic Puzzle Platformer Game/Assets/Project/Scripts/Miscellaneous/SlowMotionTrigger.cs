using UnityEngine;

namespace Sprunker.Miscellaneous {
    
    public class SlowMotionTrigger : MonoBehaviour {

        [SerializeField] private KeyCode keyToPress;
        [SerializeField] private GameObject key;
        [SerializeField] private Animator keyAnim;

        [SerializeField] private float waitTime;


        private void OnTriggerEnter2D (Collider2D col) {
            if (col.CompareTag("Player")) {
                EnterSlowMotion();
            }
        }

        private void Update () {
            if (Input.GetKeyDown(keyToPress)) {
                ExitSlowMotion();
            }
        }

        private void ExitSlowMotion () {
            Time.timeScale = 1f;
            Debug.Log("Exited Slow Motion");
            Destroy(gameObject);
            keyAnim.SetTrigger("Exit");
            Destroy(key, waitTime);
        }

        private void EnterSlowMotion () {
            Time.timeScale = 0.1f;
            Debug.Log("Entered Slow Motion");
            key.SetActive(true);
            keyAnim.SetTrigger("Fill");
        }

    }

}
