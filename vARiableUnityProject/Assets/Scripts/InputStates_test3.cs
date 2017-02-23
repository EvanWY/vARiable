using UnityEngine;
using System.Collections;

public class InputStates_test3 : MonoBehaviour {

    [SerializeField]
    GameObject InteractObject;
    enum STATE
    {
        empty_state = 0,
        state_gesture_tap = 1,
        state_gesture_doubleTap = 2,
        state_gesture_drag = 3,
        state_gesture_tapAndHold = 4,
        state_gesture_tapHoldAndDrag = 5,
        state_gesture_pinch = 6,
        state_gesture_spread = 7,
        state_gesture_twoFingerDrag = 8,
        state_gesture_twoFingerGestures = 9,
        state_firstTapDown = 21,
        state_secondTapDown = 22,
        state_firstTapHold = 23,
        state_secondTapHold = 24,
        state_afterFirstTap = 25,

    }

    // Use this for initialization
    //public const int empty_state = 0;
    //public const int state_gesture_tap = 1;
    //public const int state_gesture_doubleTap = 2;
    //public const int state_gesture_drag = 3;
    //public const int state_gesture_tapAndHold = 4;
    //public const int state_gesture_tapHoldAndDrag = 5;
    //public const int state_gesture_pinch = 6;
    //public const int state_gesture_spread = 7;
    //public const int state_gesture_twoFingerDrag = 8;
    //public const int state_gesture_twoFingerGestures = 9;
    //public const int state_firstTapDown = 21;
    //public const int state_secondTapDown = 22;
    //public const int state_firstTapHold = 23;
    //public const int state_secondTapHold = 24;

    //public const int state_afterFirstTap = 25;

    [SerializeField]
    float gap_time, longest_single_tap_time, shortest_drag_distance;

    Vector3 old_mouse_pos;
    Vector3 mouse_pos;
    Vector3 mouse_velocity;

    STATE current_state;
    float local_timer;
    Vector3 tap_down_position;
    Vector3 tap_down_position_2;
    
    GameObject ObjectTapAndHold;
    //Vector3 
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        state_machine();
        local_timer += Time.deltaTime;

    }
    void state_machine()
    {
        //Debug.Log("current state");
        //Debug.Log(current_state);
        switch (current_state)
        {
            case STATE.empty_state:
                if (Input.GetMouseButtonDown(0) && (local_timer > gap_time))
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(STATE.state_firstTapDown);
                }

                if (Application.isMobilePlatform)
                {
                    if (Input.touchCount >= 2)
                    {
                        tap_down_position = Input.mousePosition;
                        state_trans(STATE.state_gesture_twoFingerGestures);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(1) && Input.GetMouseButtonDown(0))
                    {
                        tap_down_position = Input.mousePosition;
                        state_trans(STATE.state_gesture_twoFingerGestures);
                    }
                }

                //else if (Input.GetMouseButtonDown(0) && (tap_count == 1 && local_timer <= gap_time))
                //{
                //    tap_down_position = Input.mousePosition;
                //    state_trans(state_secondTapDown);
                //}
                break;


            case STATE.state_gesture_tap:
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100f))
                    {
                        hit.collider.SendMessageUpwards("OnSingleTap", hit.collider.name);
                    }
                }
                state_trans(STATE.empty_state);
                break;


            case STATE.state_gesture_doubleTap:
                //Debug.Log("reach_state_machine");
                state_trans(STATE.empty_state);
                break;


            case STATE.state_gesture_drag:
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

                    if (Input.GetMouseButtonUp(0))
                    {
                        state_trans(STATE.empty_state);
                    }
                }
                break;


            case STATE.state_gesture_tapAndHold:

                if (Input.GetMouseButtonUp(0))
                {
                    state_trans(STATE.empty_state);
                }
                break;


            case STATE.state_gesture_tapHoldAndDrag:
                {
                    old_mouse_pos = mouse_pos;
                    mouse_pos = Input.mousePosition;
                    if (Input.GetMouseButton(0))
                    {
                        if (old_mouse_pos != mouse_pos)
                        {
                            mouse_velocity = (mouse_pos - old_mouse_pos) / Time.fixedDeltaTime;
                            ObjectTapAndHold.SendMessageUpwards("OnTapHoldAndDrag", mouse_velocity);
                        }
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        ObjectTapAndHold.SendMessageUpwards("OnTapHoldAndDragEnd");
                        ObjectTapAndHold = null;
                        state_trans(STATE.empty_state);
                    }
                }
                break;


            case STATE.state_gesture_pinch:
                state_trans(STATE.empty_state);
                break;


            case STATE.state_gesture_spread:
                state_trans(STATE.empty_state);
                break;


            case STATE.state_gesture_twoFingerDrag:
                if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
                {
                    state_trans(STATE.empty_state);
                }
                break;
            case STATE.state_gesture_twoFingerGestures:
                {
                    Touch touch0 = Input.GetTouch(0);
                    Touch touch1 = Input.GetTouch(1);

                    Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                    Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                    float prevDiff = (touch0PrevPos - touch1PrevPos).magnitude;
                    float currDiff = (touch0.position - touch1.position).magnitude;

                    float scale = currDiff / prevDiff;

                    InteractObject.GetComponent<ObjectAction>().DrivenScaling(scale);

                    if (Application.isMobilePlatform)
                    {
                        if (Input.touchCount < 2)
                        {
                            state_trans(STATE.empty_state);
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
                        {
                            state_trans(STATE.empty_state);
                        }
                    }
                }
                break;


            case STATE.state_firstTapDown:
                if (local_timer > longest_single_tap_time)
                {
                    state_trans(STATE.state_firstTapHold);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    state_trans(STATE.state_afterFirstTap);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(STATE.state_gesture_twoFingerGestures);
                }
                else if ((Input.mousePosition - tap_down_position).magnitude > shortest_drag_distance)
                {
                    mouse_pos = Input.mousePosition;
                    state_trans(STATE.state_gesture_drag);
                }
                break;


            case STATE.state_secondTapDown:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans(STATE.state_gesture_doubleTap);
                }
                break;


            case STATE.state_firstTapHold:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans(STATE.state_gesture_tapAndHold);
                }
                else if ((Input.mousePosition - tap_down_position).magnitude > shortest_drag_distance)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100f))
                    {
                        hit.collider.SendMessageUpwards("OnTapAndHold", hit.collider.name);
                        ObjectTapAndHold = hit.collider.gameObject;
                    }

                    state_trans(STATE.state_gesture_tapHoldAndDrag);
                }
                break;
            case STATE.state_secondTapHold:
                break;
            case STATE.state_afterFirstTap:
                if (local_timer >= gap_time)
                {
                    state_trans(STATE.state_gesture_tap);
                }
                else if (Input.GetMouseButtonDown(0) && local_timer <= gap_time)
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(STATE.state_secondTapDown);
                }
                break;
            default:
                break;
        }
    }
    void state_trans(STATE new_state)
    {
        Debug.Log(new_state);
        current_state = new_state;
        local_timer = 0;
        STATE old_state = current_state;
        switch (old_state)
        {
            case STATE.empty_state:
                if (new_state == STATE.state_firstTapDown)
                {

                }
                if (new_state == STATE.state_secondTapDown)
                {

                }
                break;


            case STATE.state_gesture_tap:
                if (new_state == STATE.empty_state)
                {
                    //tap_count = 1;
                    // action

                }
                break;


            case STATE.state_gesture_doubleTap:
                if (new_state == STATE.empty_state)
                {
                    //Debug.Log("reach state_trans");

                }
                break;
            case STATE.state_gesture_drag:

                break;
            case STATE.state_gesture_tapAndHold:

                break;
            case STATE.state_gesture_tapHoldAndDrag:

                break;
            case STATE.state_gesture_pinch:

                break;
            case STATE.state_gesture_spread:

                break;
            case STATE.state_gesture_twoFingerDrag:

                break;
            case STATE.state_firstTapDown:
                break;
            case STATE.state_secondTapDown:
                break;
            case STATE.state_firstTapHold:
                break;
            case STATE.state_secondTapHold:
                break;
            default:
                break;
        }
    }
}
