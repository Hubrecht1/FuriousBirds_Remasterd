using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugInfo;
    float[] frameDeltaTimeArray;
    int lastFrameIndex;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }
    private void Update()
    {

        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;

        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        debugInfo.text = $"Fps: {Mathf.RoundToInt(calculateFPS()).ToString()} \n{SystemInfo.deviceModel} {SystemInfo.processorType}\n{SystemInfo.graphicsDeviceName} {SystemInfo.graphicsDeviceType}";



    }

    float calculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;

    }

}