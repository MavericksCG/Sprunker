using UnityEngine;

public class CameraHandling : MonoBehaviour
{
    [Header("Camera Follow")]
    public float smoothing;

    private Vector3 referenceVelocity = Vector3.zero;
    public Vector3 followOffset;

    public Transform target;

    public bool lookAtTarget = false;


    private void LateUpdate () {

        if (target != null) {
            Vector3 targetPos = target.position + followOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref referenceVelocity, smoothing * Time.deltaTime);

            if (lookAtTarget) {
                transform.LookAt(target);
            }
        } else {
            Debug.Log("Player is null = True");
        }
    }
}