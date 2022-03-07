using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraUDLRZ : MonoBehaviour
{
    float scanspeed = 0.2f;
    float zoomSpeed = 4.0f;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "PlaqueAssay")
        {
            zoomSpeed = 40f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float updown = Input.GetAxis("Vertical") * scanspeed;
        float leftright = Input.GetAxis("Horizontal") * scanspeed;

        transform.Translate(-1 * leftright, -1 * updown, 0);


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * zoomSpeed);

    }
}

