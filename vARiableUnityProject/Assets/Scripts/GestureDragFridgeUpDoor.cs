using UnityEngine;
using System.Collections;

public class GestureDragFridgeUpDoor : MonoBehaviour {
    Vector3 deltaPos;
    Vector3 currFramePos;
    Vector3 old_pos;
    Vector3 mouse_move_dis;
    float original_ang;
    Quaternion original_qua;
    int flag = 0;
    void Start()
    {
        original_ang = 0;
        original_qua = this.transform.rotation;
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
        if (flag == 0)
        {
            flag++;
        }
        Plane p = new Plane();
        p.SetNormalAndPosition(transform.parent.transform.forward, transform.parent.transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (flag > 0 && p.Raycast(ray, out rayDistance))
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

        if (original_ang + Vector3.Dot(mouse_move_dis, this.transform.parent.transform.right.normalized) * 500 > -45 &&
            original_ang + Vector3.Dot(mouse_move_dis, this.transform.parent.transform.right.normalized) * 500 < 90)
        {
            //Debug.Log(this.transform.parent.transform.right);
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                original_ang,
                transform.localEulerAngles.z
                );
            original_ang += Vector3.Dot(mouse_move_dis, this.transform.parent.transform.right.normalized) * 500;
            Debug.Log(original_ang);
            // transform.rotation = Quaternion.AngleAxis(original_ang, this.transform.parent.transform.right) * original_qua;

        }
    }
    void OnTapHoldAndDragEnd()
    {

    }
}
