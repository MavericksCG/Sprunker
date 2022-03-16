using UnityEngine;
using Sprunker.Player;
using Sprunker.Managing;

namespace Sprunker.UserInterface {

    public class ReturnToCheckpoint : MonoBehaviour {

        private GameManager m;
        private Transform p;


        private PlayerController c;

        private void Start () {
            m = FindObjectOfType<GameManager>();
            p = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            c = FindObjectOfType<PlayerController>();

            Debug.Log(c.startPos);
        }

        public void SetPlayerPosition () {

            if (m.lastRecordedPosition == Vector2.zero)
                m.lastRecordedPosition = c.startPos;
            else
                p.position = m.lastRecordedPosition;
        }

    }

}
