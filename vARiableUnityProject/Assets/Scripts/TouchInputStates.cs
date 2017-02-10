using UnityEngine;
using System.Collections;

public class TouchInputStates : MonoBehaviour {
    Vector3 old_mouse_pos;
    Vector3 mouse_pos;
    Vector3 mouse_velocity;
    [SerializeField]
    GameObject InteractObject;
    //Vector3 
	// Use this for initialization
	void Start () {
	    
	}

    int singleTapCounter = 0;

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

            InteractObject.GetComponent<ObjectAction>().DrivenScaling(scale);

            singleTapCounter = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            singleTapCounter++;

            if (singleTapCounter >= 10)
            {
                old_mouse_pos = mouse_pos;
                mouse_pos = Input.mousePosition;
                if (Input.GetMouseButton(0))
                {
                    if (old_mouse_pos != mouse_pos)
                    {
                        mouse_velocity = (mouse_pos - old_mouse_pos) / Time.fixedDeltaTime;
                        InteractObject.GetComponent<ObjectAction>().DrivenRotating(mouse_velocity);
                    }
                }
            }
            else
            {
                mouse_pos = Input.mousePosition;
            }
            
        }
        else
        {
            if (10 > singleTapCounter && singleTapCounter > 0)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    hit.collider.SendMessageUpwards("OnSingleTap", hit.collider.name);
                }
            }
            singleTapCounter = 0;
        }
    }
}
