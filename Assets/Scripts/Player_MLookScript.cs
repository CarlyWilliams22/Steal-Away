using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player_MLookScript : MonoBehaviour
{

    float yAngle = 0;
    public GameObject head;
    bool currCharacter = true;
    private bool isPaused;
    private Camera _camera;
    private float mouseSensitivity;
    public float mouseSensitivityMin;
    public float mouseSensitivityMax;
    public float yClampMin;
    public float yClampMax;
    private AudioSource _audioSource;
    public AudioClip doorOpenErrorClip;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        mouseSensitivity = Prefs.GetFloat(Prefs.Property.MouseSensitivity);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SWITCH_PLAYER, Switch);
        Messenger.AddListener<bool>(GameEvent.PAUSE, OnPause);
        Messenger.AddListener<float>(GameEvent.MOUSE_SENSITIVITY_CHANGE, OnMouseSensitivityChange);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, Switch);
        Messenger.RemoveListener<bool>(GameEvent.PAUSE, OnPause);
        Messenger.RemoveListener<float>(GameEvent.MOUSE_SENSITIVITY_CHANGE, OnMouseSensitivityChange);
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
        float sensitivity = Mathf.Lerp(mouseSensitivityMin, mouseSensitivityMax, mouseSensitivity);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0));
        yAngle -= Input.GetAxis("Mouse Y") * sensitivity;
        yAngle = Mathf.Clamp(yAngle, yClampMin, yClampMax);
        head.transform.localEulerAngles = new Vector3(yAngle, 0, 0);
    }

    private void UpdateClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "DoorKeyPad":
                        DoorScript door = hit.collider.GetComponentInParent<DoorScript>();
                        door.OnPlayerClickKeypad();
                        if (!door.isOpen && !DoorManagerScript.Instance.playerHasKeyCard)
                        {
                            _audioSource.PlayOneShot(doorOpenErrorClip);
                        }
                        break;
                    case "KeyCard":
                        Messenger.Broadcast(GameEvent.OBTAINED_KEY_CARD);
                        break;
                    case "Stealable":
                        Messenger.Broadcast(GameEvent.PAINTING_STOLEN);
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

    private void OnMouseSensitivityChange(float value)
    {
        mouseSensitivity = value;
    }
}
