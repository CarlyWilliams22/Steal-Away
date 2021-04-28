using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraManagerScript : MonoBehaviour
{
    private Dictionary<GameObject, HashSet<GameObject>> cameras;
    public float disableDistance;
    public GameObject[] playerCameras;

    private void Start()
    {
        cameras = new Dictionary<GameObject, HashSet<GameObject>>();
    }

    private void Update()
    {
        foreach(KeyValuePair<GameObject, HashSet<GameObject>> entry in cameras)
        {
            entry.Key.SetActive(entry.Value.Any(monitor => playerCameras.Where(cam => cam.activeSelf).Any(player => Vector3.Distance(monitor.transform.position, player.transform.position) < disableDistance)));
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener<GameObject, GameObject, bool>(GameEvent.MONITOR_VISIBLE, OnMonitorVisible);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<GameObject, GameObject, bool>(GameEvent.MONITOR_VISIBLE, OnMonitorVisible);
    }

    private void OnMonitorVisible(GameObject camera, GameObject monitorScreen, bool visible)
    {
        if (camera)
        {
            HashSet<GameObject> monitors;
            if (!cameras.TryGetValue(camera, out monitors))
            {
                monitors = new HashSet<GameObject>();
                cameras[camera] = monitors;
            }

            if (visible)
            {
                monitors.Add(monitorScreen);
            }
            else
            {
                monitors.Remove(monitorScreen);
            }

            camera.SetActive(cameras[camera].Count > 0);
        }
    }
}
