using UnityEngine;

namespace Sprunker.Player {
    
    public class PlayerUtilities : MonoBehaviour {

        [Header("Audio Utils")]
        [SerializeField] private AudioSource spawn;
        
        
        public void OnSpawn () {
            spawn.Play();
        }
        
    }
    
}
