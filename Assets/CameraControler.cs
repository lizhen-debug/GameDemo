using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    Transform camera_transform;
    Vector3 cur_location;
    Quaternion cur_rotation;

    Vector3 look_at_rotation;

    float offset_x;
    float offset_y;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello unity");
        camera_transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        cur_location = camera_transform.position;
        cur_rotation = camera_transform.rotation;

        offset_x = Input.GetAxis("Mouse X");
        offset_y = Input.GetAxis("Mouse Y");

        look_at_rotation.x = offset_x;
        look_at_rotation.y = offset_y;

       

        if (Input.GetKey(KeyCode.W))
        {
            cur_location.z += (float)0.005;
            camera_transform.SetPositionAndRotation(cur_location, cur_rotation);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cur_location.z -= (float)0.005;
            camera_transform.SetPositionAndRotation(cur_location, cur_rotation);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cur_location.x -= (float)0.005;
            camera_transform.SetPositionAndRotation(cur_location, cur_rotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cur_location.x += (float)0.005;
            camera_transform.SetPositionAndRotation(cur_location, cur_rotation);
        }

    }
}
