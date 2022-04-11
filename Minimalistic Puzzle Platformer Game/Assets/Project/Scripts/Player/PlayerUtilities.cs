using UnityEngine;
using Cinemachine;

namespace Sprunker.Player {
    
    public class PlayerUtilities : MonoBehaviour {

        [SerializeField] private GameObject player;
        private Vector3 savedPos;

        public void Spawn (AudioSource spawn) {
            spawn.Play();
        }

        public void SetPosition (Transform posTransform, Vector3 pos) {
            savedPos = posTransform.position;
            // Add some styling
            Debug.Log("<b><color=lightblue>Saved Transform's Coordinates</color></b> : " + savedPos, player);
            posTransform.position = pos;
        }

        public void ResetPosition (Transform transformToReset) {
            transformToReset.position = savedPos;
        }

    }
    
}
