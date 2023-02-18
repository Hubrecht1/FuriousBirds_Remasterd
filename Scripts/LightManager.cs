using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
    [SerializeField] Light2D globalLight;
    float shakeTimer;
    float shakeTimerTotal;
    float startingIntensity;
    float lightIntensity;

    private void Awake()
    {
        Instance = this;
        globalLight = GetComponentInChildren<Light2D>();
        lightIntensity = globalLight.intensity;
    }

    public void Flash(float intensity, float time)
    {

        startingIntensity = intensity + globalLight.intensity;

        shakeTimerTotal = time;
        shakeTimer = time;

    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(startingIntensity, lightIntensity, 1 - (shakeTimer / shakeTimerTotal));


        }


    }
}
