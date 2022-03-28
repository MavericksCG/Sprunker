using UnityEngine;
using Cinemachine;

namespace Sprunker.Player {
    
    public class PlayerUtilities : MonoBehaviour {

        [Header("Audio Utils")]
        [SerializeField] private AudioSource spawn;


        [Header("Other Utils")]
        [SerializeField] private CinemachineVirtualCamera vcam02;
        [SerializeField] [Range(0f, 1f)] private float lerpSpeed;
        


        public void OnSpawn () {
            spawn.Play();
        }

        private void OnTriggerEnter2D (Collider2D col) {
            if (col.CompareTag("Switch Virtual Camera 02")) {
                vcam02.Follow = gameObject.transform;
                vcam02.m_Lens.OrthographicSize = 8.28f;
                
            }
        }
    }
    
}
