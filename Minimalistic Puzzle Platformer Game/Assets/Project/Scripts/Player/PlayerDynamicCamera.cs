using UnityEngine;
using Cinemachine;

public class PlayerDynamicCamera : MonoBehaviour {

    [Header("Assignables")]
    [SerializeField] private CinemachineVirtualCamera camera;
    private PlayerController c;

    [Header("FOVs")]
    [SerializeField] private float sprintFOV;
    [SerializeField] private float walkFOV;
    [SerializeField] private float superJumpFOV;
    [SerializeField] private float dashFOV;
    
    [Header("Other")]
    [SerializeField] private float smoothing;
    private float vel = 0f;
    private float currentFOV;
    


    private void Start () {
        c = GetComponent<PlayerController>();
        currentFOV = camera.m_Lens.OrthographicSize;
        Debug.Log(currentFOV);
    }
    
    private void Update () {
        
        // Horrifying code 0_0
        
        if (c.IsSprinting()) 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, sprintFOV, ref vel, smoothing, smoothing);
        
        else 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);
        

        if (c.IsWalking()) 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, walkFOV, ref vel, smoothing, smoothing);
        
        else 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);
        

        if (c.HasSuperJumped()) 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, superJumpFOV, ref vel, smoothing, smoothing);
        
        else 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);
        

        if (c.HasDashed()) 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, dashFOV, ref vel, smoothing, smoothing);
        
        else 
            camera.m_Lens.OrthographicSize = Mathf.SmoothDamp(camera.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);
        
        
    }

}
