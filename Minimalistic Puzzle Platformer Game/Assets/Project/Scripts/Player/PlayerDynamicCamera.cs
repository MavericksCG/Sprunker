using UnityEngine;
using Cinemachine;

public class PlayerDynamicCamera : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera camera;
    private PlayerController c;

    [SerializeField] private float sprintFOV;
    [SerializeField] private float smoothing;
    private float vel = 0f;
    private float currentFOV;
    


    private void Start () {
        c = GetComponent<PlayerController>();
        currentFOV = camera.m_Lens.OrthographicSize;
        Debug.Log(currentFOV);
    }
    
    private void Update () {
        if (c.IsSprinting()) {
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, sprintFOV, ref vel, smoothing, smoothing);
        }
        else {
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);
        }
        
    }

}
