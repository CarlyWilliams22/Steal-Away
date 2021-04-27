using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MLookScript : MonoBehaviour
{

    float yAngle = 0;
    public GameObject head;
    bool currCharacter = true;
    private bool isPaused;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
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
            UpdateLook();
            UpdateClick();
        }
    }

    private void UpdateLook()
    {
        float xAngle = transform.eulerAngles.y + 5 * Input.GetAxis("Mouse X");
        yAngle -= 5 * Input.GetAxis("Mouse Y");
        yAngle = Mathf.Clamp(yAngle, -25, 32.75f);

        transform.eulerAngles = new Vector3(0, xAngle, 0);
        head.transform.localEulerAngles = new Vector3(yAngle, 0, 0);
    }

    private void UpdateClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                switch(hit.collider.gameObject.tag)
                {
                    case "DoorKeyPad":
                        hit.collider.GetComponentInParent<DoorScript>().OnPlayerClickKeypad();
                        break;
                    case "KeyCard":
                        Messenger.Broadcast(GameEvent.OBTAINED_KEY_CARD);
                        break;
                }
            }
        }
    }

    private void Switch()
    {
        currCharacter = !currCharacter;
    }

    private void OnPause(bool pause)
    {
        isPaused = pause;
    }
}
