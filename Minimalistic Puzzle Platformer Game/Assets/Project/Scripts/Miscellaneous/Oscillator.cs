using UnityEngine;

public class Oscillator : MonoBehaviour {
    
    [Header("References and Variables")]
    [SerializeField] private Transform target;
    private Vector3 currentPos;

    [SerializeField] private bool oscillate;

    [SerializeField] [Range(0f, 1f)] private float speed;

    private void Start () {
        currentPos = transform.position;
        Debug.Log(currentPos);
    }

    private void Update () {
        while (oscillate) {
            transform.position = Vector3.Lerp(transform.position, target.position, speed);

            if (transform.position == target.position) {
                transform.position = Vector3.Lerp(transform.position, currentPos, speed);
            }

            if (transform.position == currentPos) {
                transform.position = Vector3.Lerp(transform.position, target.position, speed);
            }
        }
    }

}
