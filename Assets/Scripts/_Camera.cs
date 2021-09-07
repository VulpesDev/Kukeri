using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class _Camera : MonoBehaviour
{
    CinemachineVirtualCamera _cam;
    CinemachineBasicMultiChannelPerlin _perlin;
    void Start()
    {

        _cam = GetComponent<CinemachineVirtualCamera>();
        _perlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetCam();

    }

    void Update()
    {

    }

    public void CameraShake(float seconds, float frequency, float amplitude)
    {
        StartCoroutine(ICameraShake(seconds, frequency, amplitude));
    }
    IEnumerator ICameraShake(float seconds, float frequency, float amplitude)
    {
        _perlin.m_FrequencyGain = frequency;
        _perlin.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(seconds);
        ResetCam();

    }
    void ResetCam()
    {
        _perlin.m_FrequencyGain = 0;
        _perlin.m_AmplitudeGain = 0;
    }
}
