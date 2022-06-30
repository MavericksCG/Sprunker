using UnityEngine;
using Sprunker.Managing;

namespace Sprunker.Miscellaneous {

    public class EnableObjectTrigger : MonoBehaviour {

        [SerializeField] private GameObject objectToEnable;

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Player")) {
                GameManager.instance.EnableObj(objectToEnable);
            }
        }
        
    }

}
