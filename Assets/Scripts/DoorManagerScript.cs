using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerScript : MonoBehaviour
{
    private static DoorManagerScript _instance = null;
    private DoorScript[] doors;
    public bool playerHasKeyCard;

    public static DoorManagerScript Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        doors = FindObjectsOfType<DoorScript>();
    }

    public DoorScript ClosestDoorToPoint(Vector3 point)
    {
        DoorScript current = doors[0];
        foreach (DoorScript door in doors)
        {
            if (Vector3.Distance(door.transform.position, point) < Vector3.Distance(current.transform.position, point))
            {
                current = door;
            }
        }
        return current;
    }

    public float DistanceToClosestDoor(Vector3 point)
    {
        float minDist = float.MaxValue, dist;
        foreach (DoorScript door in doors)
        {
            dist = Vector3.Distance(door.transform.position, point);
            if (dist < minDist)
            {
                minDist = dist;
            }
        }
        return minDist;
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.OBTAINED_KEY_CARD, OnObtainedKeyCard);
        Messenger.AddListener(GameEvent.ALARM_SOUNDED, OnAlarmSounded);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.OBTAINED_KEY_CARD, OnObtainedKeyCard);
        Messenger.RemoveListener(GameEvent.ALARM_SOUNDED, OnAlarmSounded);
    }

    private void OnObtainedKeyCard()
    {
        playerHasKeyCard = true;
    }

    private void OnAlarmSounded()
    {
        foreach (DoorScript door in doors)
        {
            door.isOpen = false;
        }
    }

}
