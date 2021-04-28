using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public Dictionary<GameObject, int> cameras;

    private void Start()
    {
        cameras = new Dictionary<GameObject, int>();
    }

    private void OnEnable()
    {
        Messenger.AddListener<GameObject, bool>(GameEvent.MONITOR_VISIBLE, OnMonitorVisible);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<GameObject, bool>(GameEvent.MONITOR_VISIBLE, OnMonitorVisible);
    }

    private void OnMonitorVisible(GameObject camera, bool visible)
    {
        if (camera)
        {
            int count;
            if (cameras.TryGetValue(camera, out count))
            {
                cameras[camera] = visible ? count + 1 : count - 1;
            }
            else
            {
                cameras[camera] = visible ? 1 : 0;
            }
            camera.SetActive(cameras[camera] > 0);
        }
    }
}
