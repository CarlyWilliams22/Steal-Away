using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VLookScript : MonoBehaviour
{
    float yAngle = 0;
    public GameObject head;
    bool currCharacter = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SWITCH_PLAYER, Switch);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, Switch);
    }

    // Update is called once per frame
    void Update()
    {
        if (currCharacter)
        {
            float xAngle = transform.eulerAngles.y + 5 * Input.GetAxis("Mouse X");
            yAngle -= 5 * Input.GetAxis("Mouse Y");
            yAngle = Mathf.Clamp(yAngle, -25, 32.75f);

            transform.eulerAngles = new Vector3(0, xAngle, 0);
            head.transform.localEulerAngles = new Vector3(yAngle, 0, 0);

        }


    }

    private void Switch()
    {
        currCharacter = !currCharacter;
    }
}
