using UnityEngine;
using System.Collections;

public class ObjectAction : MonoBehaviour {
    Vector3 rotation_axis;
    Quaternion rotation_for_frame;
    float angle_rotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void DrivenRotating(Vector3 mouse_velocity)
    {
        Debug.Log(mouse_velocity);
        //rotation_axis = Vector3.Cross(mouse_velocity, this.transform.parent.transform.forward).normalized;
        rotation_axis = this.transform.parent.forward;
        //angle_rotate = mouse_velocity.magnitude/3 *Time.fixedDeltaTime;
        angle_rotate = mouse_velocity.x / 3 *Time.fixedDeltaTime;

        rotation_for_frame = Quaternion.AngleAxis(angle_rotate, rotation_axis);
        this.transform.rotation = rotation_for_frame * this.transform.rotation;

    }

    public void DrivenScaling(float scale)
    {
        transform.localScale *= scale;
    }
}
