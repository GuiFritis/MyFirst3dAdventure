using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;
    private float _shakeDuration;
    public CinemachineBasicMultiChannelPerlin cmBasicMultiChanel;

    private void OnValidate() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cmBasicMultiChanel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amp, float frequency, float duration)
    {
        cmBasicMultiChanel.m_AmplitudeGain = amp;
        cmBasicMultiChanel.m_FrequencyGain = frequency;

        _shakeDuration = duration;
    }

    private void Update() {
        if(_shakeDuration > 0)
        {
            _shakeDuration -= Time.deltaTime;
        }
        else 
        {           
            cmBasicMultiChanel.m_AmplitudeGain = 0f;
            cmBasicMultiChanel.m_FrequencyGain = 0f;
        }
    }
}
