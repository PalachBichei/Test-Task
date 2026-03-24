using UnityEngine;

public class MobilePerformanceBootstrap : MonoBehaviour
{
    private void Awake()
    {
        int refreshRate = (int)Screen.currentResolution.refreshRateRatio.value;
        Application.targetFrameRate = refreshRate;
    }
}