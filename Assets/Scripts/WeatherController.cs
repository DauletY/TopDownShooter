using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Material sky;
    public Light sun;
    private float _fullIntensity;
    private float _cloudValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _fullIntensity = sun.innerSpotAngle;
    }

    // Update is called once per frame
    void Update()
    {
        SetOvercast(_cloudValue);
        _cloudValue += .005f;
    }

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}
