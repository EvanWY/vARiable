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

    // Update is called once per frame
    void FixedUpdate()
    {
        old_mouse_pos = mouse_pos;
        mouse_pos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            if (old_mouse_pos != mouse_pos)
            {
                mouse_velocity = (mouse_pos - old_mouse_pos) / Time.fixedDeltaTime;
                rotating_object.GetComponent<rotation3>().driven_rotating(mouse_velocity);
            }
        }
        //Debug.Log(mouse_velocity.magnitude);
        if (!(Input.GetMouseButton(0)))
        {
            old_mouse_pos = mouse_pos;

        }
    }
}
