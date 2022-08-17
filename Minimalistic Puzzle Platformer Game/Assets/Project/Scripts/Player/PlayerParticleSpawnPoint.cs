using UnityEngine;

public class PlayerParticleSpawnPoint : MonoBehaviour {

    [SerializeField] private Transform playerTrans;
    [SerializeField] private Vector3 followOffset;

    private void Update() {
        if (playerTrans != null) {
            transform.position = playerTrans.position + followOffset;
        }
        else {
            Debug.LogWarning("<color=red>Property 'playerTrans(Player Transform)' is null!</color>");
        }
    }
}
