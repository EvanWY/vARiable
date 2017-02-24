using UnityEngine;
using System.Collections;

public class GesturePushRotate : MonoBehaviour {

	void OnTapAndHold()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    void OnTapAndHoldEnd()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    Vector3 deltaEular;
    Vector3 currFrameEular;
    void OnTapHoldAndDrag()
    {
        deltaEular = transform.localEulerAngles - currFrameEular;
        currFrameEular = transform.localEulerAngles;

        GetComponent<Rigidbody>().isKinematic = true;
        Plane p = new Plane();
        p.SetNormalAndPosition(transform.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (p.Raycast(ray, out rayDistance))
            transform.LookAt(ray.GetPoint(rayDistance));
    }

    void OnTapHoldAndDragEnd()
    {


        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().angularVelocity = deltaEular / Time.deltaTime;
    }
}
