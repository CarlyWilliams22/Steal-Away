using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraScript : MonoBehaviour
{
    private static readonly Color museumPlayerColor = new Color(75f / 255f, 75f / 255f, 75f / 255f);
    private static readonly Color vanPlayerColor = new Color(0.5f, 0.5f, 0.5f);
    private static readonly Color securityColor = new Color(200f / 255f, 200f / 255f, 200f / 255f);
    private static readonly Color minimapColor = new Color(1, 1, 1);

    public enum CameraType { Security, VanPlayer, MuseumPlayer, MiniMap };
    public CameraType type;
    public GameObject securityCameraLight;

    private Color originalAmbient;
    private Light[] allLights;
    private List<bool> allLightsOrigActive;

    private void Start()
    {
        allLights = FindObjectsOfType<Light>();
        allLightsOrigActive = new List<bool>();
    }

    private void OnPreCull()
    {
        originalAmbient = RenderSettings.ambientLight;
        switch (type)
        {
            case CameraType.Security:
                securityCameraLight.SetActive(true);
                RenderSettings.ambientLight = securityColor;
                break;
            case CameraType.MiniMap:
                DisableAllLights();
                securityCameraLight.SetActive(true);
                RenderSettings.ambientLight = minimapColor;
                break;
            case CameraType.VanPlayer:
                RenderSettings.ambientLight = vanPlayerColor;
                break;
            case CameraType.MuseumPlayer:
                RenderSettings.ambientLight = museumPlayerColor;
                break;
        }
    }

    private void OnPostRender()
    {
        RenderSettings.ambientLight = originalAmbient;

        switch (type)
        {
            case CameraType.Security:
                securityCameraLight.SetActive(false);
                break;
            case CameraType.MiniMap:
                ResetAllLights();
                securityCameraLight.SetActive(false);
                break;
        }
    }

    private void DisableAllLights()
    {
        allLightsOrigActive.Clear();
        foreach (Light light in allLights)
        {
            allLightsOrigActive.Add(light.gameObject.activeSelf);
            light.gameObject.SetActive(false);
        }
    }

    private void ResetAllLights()
    {
        for (int i = 0; i < allLights.Count(); i++) {
            allLights[i].gameObject.SetActive(allLightsOrigActive[i]);
        }
    }
}
