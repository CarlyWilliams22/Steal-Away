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
    }

    // Start is called before the first frame update
    void Start()
    {
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

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.OBTAINED_KEY_CARD, OnObtainedKeyCard);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.OBTAINED_KEY_CARD, OnObtainedKeyCard);
    }

    private void OnObtainedKeyCard()
    {
        playerHasKeyCard = true;
    }

}
