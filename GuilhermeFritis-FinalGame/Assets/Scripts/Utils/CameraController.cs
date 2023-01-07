using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public InputActionReference input;
    public CinemachineFreeLook cinemachine;

    private float yValue;
    private float xValue;

    void Start()
    {
        input.asset.Enable();

        GetCameraSpeed();

        SetInputs();
    }

    private void GetCameraSpeed()
    {
        yValue = cinemachine.m_YAxis.m_MaxSpeed;
        xValue = cinemachine.m_XAxis.m_MaxSpeed;

        cinemachine.m_YAxis.m_MaxSpeed = 0;
        cinemachine.m_XAxis.m_MaxSpeed = 0;
    }

    private void SetInputs()
    {
        input.action.performed += ctx => AllowCameraControl();
        input.action.canceled += ctx => DisableCameraControl();
    }

    private void AllowCameraControl()
    {
        cinemachine.m_YAxis.m_MaxSpeed = yValue;
        cinemachine.m_XAxis.m_MaxSpeed = xValue;
    }

    private void DisableCameraControl()
    {
        cinemachine.m_YAxis.m_MaxSpeed = 0;
        cinemachine.m_XAxis.m_MaxSpeed = 0;
    }
}
