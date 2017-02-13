using UnityEngine;
using System.Collections;

public class InputStates_test2 : MonoBehaviour {

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
    public const int empty_state = 0;
    public const int state_gesture_tap = 1;
    public const int state_gesture_doubleTap = 2;
    public const int state_gesture_drag = 3;
    public const int state_gesture_tapAndHold = 4;
    public const int state_gesture_tapHoldAndDrag = 5;
    public const int state_gesture_pinch = 6;
    public const int state_gesture_spread = 7;
    public const int state_gesture_twoFingerDrag = 8;
    public const int state_gesture_twoFingerGestures = 9;
    public const int state_firstTapDown = 21;
    public const int state_secondTapDown = 22;
    public const int state_firstTapHold = 23;
    public const int state_secondTapHold = 24;

    public const int state_afterFirstTap = 25;

    [SerializeField]
    float gap_time, longest_single_tap_time, shortest_drag_distance;


    int current_state;
    float local_timer;
    Vector3 tap_down_position;
    Vector3 tap_down_position_2;
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
            case empty_state:
                if (Input.GetMouseButtonDown(0) && (local_timer > gap_time))
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(state_firstTapDown);
                }
                if (Input.GetMouseButtonDown(1) && Input.GetMouseButtonDown(0))
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(state_gesture_twoFingerGestures);
                }
                //else if (Input.GetMouseButtonDown(0) && (tap_count == 1 && local_timer <= gap_time))
                //{
                //    tap_down_position = Input.mousePosition;
                //    state_trans(state_secondTapDown);
                //}
                break;


            case state_gesture_tap:
                state_trans(empty_state);
                break;


            case state_gesture_doubleTap:
                //Debug.Log("reach_state_machine");
                state_trans(empty_state);
                break;


            case state_gesture_drag:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans(empty_state);
                }
                break;


            case state_gesture_tapAndHold:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans(empty_state);
                }
                break;


            case state_gesture_tapHoldAndDrag:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans(empty_state);
                }
                break;


            case state_gesture_pinch:
                state_trans(empty_state);
                break;


            case state_gesture_spread:
                state_trans( empty_state);
                break;


            case state_gesture_twoFingerDrag:
                if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
                {
                    state_trans(empty_state);
                }
                break;
            case state_gesture_twoFingerGestures:
                if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
                {
                    state_trans(empty_state);
                }
                break;


            case state_firstTapDown:
                if (local_timer > longest_single_tap_time)
                {
                    state_trans(state_firstTapHold);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    state_trans( state_afterFirstTap);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(state_gesture_twoFingerGestures);
                }
                else if ((Input.mousePosition - tap_down_position).magnitude > shortest_drag_distance)
                {
                    state_trans(state_gesture_drag);
                }
                break;


            case state_secondTapDown:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans( state_gesture_doubleTap);
                }
                break;


            case state_firstTapHold:
                if (Input.GetMouseButtonUp(0))
                {
                    state_trans( state_gesture_tapAndHold);
                }
                else if ((Input.mousePosition - tap_down_position).magnitude > shortest_drag_distance)
                {
                    state_trans( state_gesture_tapHoldAndDrag);
                }
                break;
            case state_secondTapHold:
                break;
            case state_afterFirstTap:
                if (local_timer >= gap_time)
                {
                    state_trans(state_gesture_tap);
                }
                else if (Input.GetMouseButtonDown(0) && local_timer <= gap_time)
                {
                    tap_down_position = Input.mousePosition;
                    state_trans(state_secondTapDown);
                }
                break;
            default:
                break;
        }
    }
    void state_trans( int new_state)
    {
        Debug.Log(new_state);
        current_state = new_state;
        local_timer = 0;
        int old_state = current_state;
        switch (old_state)
        {
            case empty_state:
                if (new_state == state_firstTapDown)
                {

                }
                if (new_state == state_secondTapDown)
                {

                }
                break;


            case state_gesture_tap:
                if (new_state == empty_state)
                {
                    //tap_count = 1;
                    // action

                }
                break;


            case state_gesture_doubleTap:
                if (new_state == empty_state)
                {
                    //Debug.Log("reach state_trans");

                }
                break;
            case state_gesture_drag:

                break;
            case state_gesture_tapAndHold:

                break;
            case state_gesture_tapHoldAndDrag:

                break;
            case state_gesture_pinch:

                break;
            case state_gesture_spread:

                break;
            case state_gesture_twoFingerDrag:

                break;
            case state_firstTapDown:
                break;
            case state_secondTapDown:
                break;
            case state_firstTapHold:
                break;
            case state_secondTapHold:
                break;
            default:
                break;
        }
    }
}
