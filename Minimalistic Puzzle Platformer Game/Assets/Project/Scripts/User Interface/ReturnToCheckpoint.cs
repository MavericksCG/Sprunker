using UnityEngine;
using Sprunker.Player;
using Sprunker.Managing;
using UnityEngine.SceneManagement;

namespace Sprunker.UserInterface {

    public class ReturnToCheckpoint : MonoBehaviour {

        [Header("References")]
        private GameManager m;
        [SerializeField] private Transform p;
        private PlayerController c;

        [SerializeField] private AudioSource checkpointRespawnAudio;
        
        
        private void Start () {
            m = FindObjectOfType<GameManager>();
            // p = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            c = FindObjectOfType<PlayerController>();

            // Debug.Log(c.startPos);
        }

        public void SetPlayerPosition () {

            // Activating, Deactivating
            gameObject.SetActive(false);
            p.gameObject.SetActive(true);
            
            m.goUI.SetActive(false);
            m.indi.SetActive(true);
            m.dindi.SetActive(true);
            m.pmObj.SetActive(false);
            
            // Play the checkpoint respawn audio if the value is not null
            if (checkpointRespawnAudio != null) 
                checkpointRespawnAudio.Play();
            

            // sip sip gulp gulp
            // Just doing stuff, changing position, etc.
            p.position = m.lastRecordedPosition;
        }

    }

}
