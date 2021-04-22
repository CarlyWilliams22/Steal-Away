using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraScript : MonoBehaviour
{
    float shadowDistance;
    private void OnPreRender()
    {
        shadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    private void OnPostRender()
    {
        QualitySettings.shadowDistance = shadowDistance;
    }
}
