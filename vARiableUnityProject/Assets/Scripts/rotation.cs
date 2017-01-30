using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour
{
    Quaternion initial_rot;
    Vector3 relative_coordinate_of_mouse;
    Vector3 old_relative_coordinate_of_mouse;
    Vector3 center_of_this_item;
    Vector3 rotation_axis;
    int image_height;
    Vector3 mouse_coordinate;
    RaycastHit hit;
    float angle_rotate;
    Quaternion rotation_for_frame;
    // Use this for initialization
    void Start()
    {
        //initial_rot.Set(Mathf.Sin(50), 0f, 0f, Mathf.Cos(50));
        //this.transform.rotation = initial_rot;

    }

    // Update is called once per frame
    void Update()
    {
        center_of_this_item = this.transform.position;
        old_relative_coordinate_of_mouse = relative_coordinate_of_mouse;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "rotation_controller")
            {
                relative_coordinate_of_mouse = hit.point - center_of_this_item;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (old_relative_coordinate_of_mouse != relative_coordinate_of_mouse)
            {
                rotation_axis = Vector3.Cross(old_relative_coordinate_of_mouse, relative_coordinate_of_mouse).normalized;
                angle_rotate = Mathf.Rad2Deg * Vector3.Cross(old_relative_coordinate_of_mouse.normalized, relative_coordinate_of_mouse.normalized).magnitude * 300f ;
                rotation_for_frame = Quaternion.AngleAxis(angle_rotate, rotation_axis);
                this.transform.rotation = rotation_for_frame * this.transform.rotation;
            }
        }

        if (!(Input.GetMouseButton(0)))
        {
            old_relative_coordinate_of_mouse = relative_coordinate_of_mouse;

        }
    }
}