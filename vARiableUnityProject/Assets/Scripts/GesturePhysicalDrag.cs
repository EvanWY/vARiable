using UnityEngine;
using System.Collections;

public class GesturePhysicalDrag : MonoBehaviour {

	void OnTapAndHold()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    void OnTapAndHoldEnd()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    Vector3 deltaPos;
    Vector3 currFramePos;
    void OnTapHoldAndDrag()
    {
        deltaPos = transform.position - currFramePos;
        currFramePos = transform.position;

        GetComponent<Rigidbody>().isKinematic = true;
        Plane p = new Plane();
        p.SetNormalAndPosition(Camera.main.transform.forward, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (p.Raycast(ray, out rayDistance))
            transform.position = ray.GetPoint(rayDistance);
    }

    void OnTapHoldAndDragEnd()
    {


        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = deltaPos / Time.deltaTime;
    }
}
