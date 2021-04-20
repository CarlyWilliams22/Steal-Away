using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MLookScript : MonoBehaviour
{

    float yAngle = 0;
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAngle = transform.eulerAngles.y + 5 * Input.GetAxis("Mouse X");
        yAngle -= 5 * Input.GetAxis("Mouse Y");
        yAngle = Mathf.Clamp(yAngle, -25, 50);

        transform.eulerAngles = new Vector3(0, xAngle, 0);
        head.transform.localEulerAngles = new Vector3(yAngle, 0, 0);
    }
}
