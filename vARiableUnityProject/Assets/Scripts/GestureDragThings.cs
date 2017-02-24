using UnityEngine;
using System.Collections;

public class GestureDragThings : MonoBehaviour {
    Vector3 deltaPos;
    Vector3 currFramePos;
    Vector3 old_pos;
    Vector3 mouse_move_dis;
    int flag = 0;
    void Start()
    {

    }
    void OnTapAndHold()
    {
        flag = 0;
    }
    void OnTapAndHoldEnd()
    {
    }
    void OnTapHoldAndDrag(Vector3 mouse_velocity)
    {
        if(flag ==0)
        {
            flag++;
        }
        Plane p = new Plane();
        //p.SetNormalAndPosition(Camera.main.transform.forward, transform.position);
        p.SetNormalAndPosition(transform.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (flag > 0 && p.Raycast(ray, out rayDistance))
        {
            mouse_move_dis = ray.GetPoint(rayDistance) - old_pos;
        }
        if (p.Raycast(ray, out rayDistance))
            old_pos = ray.GetPoint(rayDistance);
        if (transform.localPosition.x +
                Vector3.Dot(mouse_move_dis, this.transform.right.normalized) / 5 > - 0.004 &&

                transform.localPosition.x +
                Vector3.Dot(mouse_move_dis, this.transform.right.normalized) / 5 <= 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x +
                Vector3.Dot(mouse_move_dis, this.transform.right.normalized)/5, 
                transform.localPosition.y, transform.localPosition.z); 
        }
   //     else if(transform.localPosition.x > 0)
   //     {
   //         transform.localPosition = new Vector3(0f,
   //            transform.localPosition.y, transform.localPosition.z);
   //     }else if(transform.localPosition.x < -0.004)
   //     {
   //         transform.localPosition = new Vector3(-0.004f,
   //transform.localPosition.y, transform.localPosition.z);
   //     }

    }
    void OnTapHoldAndDragEnd()
    {

    }
}
