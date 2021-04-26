using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VLookScript : MonoBehaviour
{
    float yAngle = 0;
    public GameObject head;
    bool currCharacter = false;
    public GameObject _camera;
    public GameObject playerM;
    private Camera cam;
    public Camera miniMapCamera;
    public LayerMask mapScreenLayer;
    public LayerMask floorLayer;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        cam = _camera.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SWITCH_PLAYER, Switch);
        Messenger.AddListener<bool>(GameEvent.PAUSE, OnPause);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, Switch);
        Messenger.RemoveListener<bool>(GameEvent.PAUSE, OnPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (currCharacter && !isPaused)
        {
            float xAngle = transform.eulerAngles.y + 5 * Input.GetAxis("Mouse X");
            yAngle -= 5 * Input.GetAxis("Mouse Y");
            yAngle = Mathf.Clamp(yAngle, -25, 32.75f);

            transform.eulerAngles = new Vector3(0, xAngle, 0);
            head.transform.localEulerAngles = new Vector3(yAngle, 0, 0);


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit monitorHit;
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out monitorHit, Mathf.Infinity, mapScreenLayer))
                {
                    Vector3 monitorPoint = monitorHit.collider.transform.InverseTransformPoint(monitorHit.point);

                    // scale
                    monitorPoint.x *= monitorHit.collider.transform.localScale.x;
                    monitorPoint.y *= monitorHit.collider.transform.localScale.y;
                    monitorPoint.z *= monitorHit.collider.transform.localScale.z;

                    // convert to 0..1, for some reason 0.8 is magic
                    const float range = 0.8f;
                    monitorPoint = new Vector3(ConvertRange(monitorPoint.x, -range, range, 0, 1), ConvertRange(monitorPoint.y, -range, range, 0, 1), ConvertRange(monitorPoint.z, -range, range, 0, 1));

                    // flip axis
                    monitorPoint.x = 1 - monitorPoint.x;
                    monitorPoint.z = 1 - monitorPoint.z;

                    // raycast into the game world
                    RaycastHit worldHit;
                    if (Physics.Raycast(miniMapCamera.ViewportPointToRay(new Vector3(monitorPoint.x, monitorPoint.z, 0)), out worldHit, Mathf.Infinity, floorLayer))
                    {
                        // open the closest door
                        DoorScript door = DoorManagerScript.Instance.ClosestDoorToPoint(worldHit.point);
                        door.isOpen = !door.isOpen;
                    }
                }
            }
        }
    }

    private void Switch()
    {
        currCharacter = !currCharacter;
        _camera.SetActive(currCharacter);
    }

    // https://stackoverflow.com/questions/929103/convert-a-number-range-to-another-range-maintaining-ratio
    private static float ConvertRange(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        return (((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin;
    }

    private void OnPause(bool pause)
    {
        isPaused = pause;
    }
}
