using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private static readonly Color museumPlayerColor = new Color(50f / 255f, 50f / 255f, 50f / 255f);
    private static readonly Color vanPlayerColor = new Color(0.5f, 0.5f, 0.5f);
    private static readonly Color securityColor = new Color(200f / 255f, 200f / 255f, 200f / 255f);
    private static readonly Color minimapColor = new Color(1, 1, 1);

    public enum CameraType { Security, VanPlayer, MuseumPlayer, MiniMap };
    public CameraType type;
    public GameObject securityCameraLight;

    private Color originalAmbient;

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
            case CameraType.MiniMap:
                securityCameraLight.SetActive(false);
                break;
        }
    }
}
