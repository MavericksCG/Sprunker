using UnityEngine;
using Cinemachine;

namespace Sprunker.Player {
    public class PlayerDynamicCamera : MonoBehaviour {

        [Header("Assignables")] 
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private CinemachineVirtualCamera camera02;

        private PlayerController c;

        [Header("FOVs")] [SerializeField] private float sprintFOV;
        [SerializeField] private float walkFOV;
        [SerializeField] private float jumpFOV;
        [SerializeField] private float superJumpFOV;
        [SerializeField] private float dashFOV;

        [Header("Other")] [SerializeField] private float smoothing;
        private float vel = 0f;
        private float currentFOV;

        [SerializeField] private AnimationCurve fovCurve = new AnimationCurve();



        private void Start () {
            c = GetComponent<PlayerController>();
            currentFOV = cam.m_Lens.OrthographicSize;
        }

        private void Update () {

            // Horrifying code 0_0

            if (c.IsSprinting())
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, sprintFOV, ref vel, smoothing, smoothing);

            else
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);


            if (c.IsWalking())
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, walkFOV, ref vel, smoothing, smoothing);

            else
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);


            if (c.HasSuperJumped())
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, superJumpFOV, ref vel, smoothing, smoothing);

            else
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);


            if (c.HasDashed())
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, dashFOV, ref vel, smoothing, smoothing);

            else
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);


            if (c.HasJumped())
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, jumpFOV, ref vel, smoothing, smoothing);

            else
                cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, currentFOV, ref vel, smoothing, smoothing);

            // Updating Graph
            fovCurve.AddKey(Time.realtimeSinceStartup, cam.m_Lens.OrthographicSize);

        }


        private void OnTriggerEnter2D (Collider2D col) {
            if (col.CompareTag("Switch Virtual Camera 02")) {
                cam = camera02;
            }
        }

    }
}
