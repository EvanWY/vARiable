using UnityEngine;
using System.Collections;

public class GestureDragSuitCaseCover : MonoBehaviour
{

    Vector3 deltaPos;
    Vector3 currFramePos;
    Vector3 old_pos;
    Vector3 mouse_move_dis;
    float original_ang;
    Quaternion original_qua;
    int flag = 0;
    public int can_open;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTapHoldAndDrag(Vector3 mouse_velocity)
    {
        if (flag == 0)
        {
            flag++;
        }
        Plane p = new Plane();
        //p.SetNormalAndPosition(Camera.main.transform.forward, transform.position);
        p.SetNormalAndPosition(transform.parent.transform.forward, transform.parent.transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (flag > 0 && p.Raycast(ray, out rayDistance) && can_open == 2)
        {
            mouse_move_dis = ray.GetPoint(rayDistance) - old_pos;
        }
        if (p.Raycast(ray, out rayDistance))
        {
            old_pos = ray.GetPoint(rayDistance);
        }
        if (flag == 0)
        {
            mouse_move_dis = new Vector3(0, 0, 0);
        }
        //if (transform.localPosition.x +
        //        Vector3.Dot(mouse_move_dis, this.transform.right.normalized) / 5 > -0.004 &&

        //        transform.localPosition.x +
        //        Vector3.Dot(mouse_move_dis, this.transform.right.normalized) / 5 <= 0)
        //{
        //    transform.localPosition = new Vector3(transform.localPosition.x +
        //        Vector3.Dot(mouse_move_dis, this.transform.right.normalized) / 5,
        //        transform.localPosition.y, transform.localPosition.z);
        //}
        if (original_ang - Vector3.Dot(mouse_move_dis, this.transform.parent.transform.up.normalized) * 500 > -180&&
            original_ang - Vector3.Dot(mouse_move_dis, this.transform.parent.transform.up.normalized) * 500 < 0)
        {
            //Debug.Log(this.transform.parent.transform.right);
            transform.localEulerAngles = new Vector3(
                original_ang,
                0,
                0
                );
            original_ang -= Vector3.Dot(mouse_move_dis, this.transform.parent.transform.up.normalized) * 500;
            Debug.Log(original_ang);
            // transform.rotation = Quaternion.AngleAxis(original_ang, this.transform.parent.transform.right) * original_qua;

        }
    }
    void OnTapHoldAndDragEnd()
    {

    }
    void OnTapAndHold()
    {
        flag = 0;
    }
    void OnTapAndHoldEnd()
    {
    }
    void OnSingleTap()
    {

    }
}
