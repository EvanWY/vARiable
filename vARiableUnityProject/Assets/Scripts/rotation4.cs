using UnityEngine;
using System.Collections;

public class rotation4 : MonoBehaviour
{
    Vector3 rotation_axis;
    Vector3 parent_forward;
    Quaternion rotation_for_frame;
    float angle_rotate;
    [SerializeField]
    GameObject the_camera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void driven_rotating(Vector3 mouse_velocity)
    {
        Debug.Log(mouse_velocity);
        parent_forward = Vector3.Cross(this.transform.parent.forward,Vector3.Cross(the_camera.transform.forward, this.transform.parent.forward));

        rotation_axis = Vector3.Cross(mouse_velocity, parent_forward).normalized;
        //rotation_axis = this.transform.parent.forward;
        angle_rotate = mouse_velocity.magnitude/3 *Time.fixedDeltaTime;
        //angle_rotate = mouse_velocity.x / 3 * Time.fixedDeltaTime;

        rotation_for_frame = Quaternion.AngleAxis(angle_rotate, rotation_axis);
        this.transform.rotation = rotation_for_frame * this.transform.rotation;

    }
}
