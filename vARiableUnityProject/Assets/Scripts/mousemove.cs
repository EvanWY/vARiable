using UnityEngine;
using System.Collections;

public class mousemove : MonoBehaviour {
    Vector3 old_mouse_pos;
    Vector3 mouse_pos;
    Vector3 mouse_velocity;
    [SerializeField]
    GameObject rotating_object;
    //Vector3 
	// Use this for initialization
	void Start () {
	    
	}

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevDiff = (touch0PrevPos - touch1PrevPos).magnitude;
            float currDiff = (touch0.position - touch1.position).magnitude;

            float scale = currDiff / prevDiff;

            rotating_object.GetComponent<ObjectAction>().DrivenScaling(scale);
        }
        else if (Input.GetMouseButton(0))
        {
            old_mouse_pos = mouse_pos;
            mouse_pos = Input.mousePosition;
            if (Input.GetMouseButton(0))
            {
                if (old_mouse_pos != mouse_pos)
                {
                    mouse_velocity = (mouse_pos - old_mouse_pos) / Time.fixedDeltaTime;
                    rotating_object.GetComponent<ObjectAction>().DrivenRotating(mouse_velocity);
                }
            }
            //Debug.Log(mouse_velocity.magnitude);
            if (!(Input.GetMouseButton(0)))
            {
                old_mouse_pos = mouse_pos;

            }
        }
    }
}
