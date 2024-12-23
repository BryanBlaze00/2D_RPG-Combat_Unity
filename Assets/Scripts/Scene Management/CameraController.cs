using Unity.Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera cinemachineCamera;

        protected override void Awake()
    {
        base.Awake();
    }

    public void SetPlayerCameraFollow()
    {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        cinemachineCamera.Follow = Player.Instance.transform;
    }
}
