using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{

    [Header("VARIABLES")]
    public CinemachineVirtualCamera mainVirtualCamera;

    public int desiredPriority;


    public static SwitchCamera instance;


    private void Awake () {
        instance = this;
    }


    public void SetPriority () {
        mainVirtualCamera.Priority = desiredPriority;
    }
}