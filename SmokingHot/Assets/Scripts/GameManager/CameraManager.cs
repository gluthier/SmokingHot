using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera sittingCamera;
    public CinemachineCamera playingCamera;
    
    public void SwitchToSittingCamera()
    {
        sittingCamera.Priority = 1;
        playingCamera.Priority = 0;
    }

    public void SwitchPlayingCamera()
    {
        sittingCamera.Priority = 0;
        playingCamera.Priority = 1;
    }
}
