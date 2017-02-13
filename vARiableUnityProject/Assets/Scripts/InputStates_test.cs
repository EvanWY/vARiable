
using UnityEngine;
using System.Collections;

public class InputStates_test : MonoBehaviour
{


    public const int empty_state = 0;
    public const int state_gesture_tap = 1;
    public const int state_gesture_doubleTap = 2;
    public const int state_gesture_drag = 3;
    public const int state_gesture_tapAndHold = 4;
    public const int state_gesture_tapHoldAndDrag = 5;
    public const int state_gesture_pinch = 6;
    public const int state_gesture_spread = 7;
    public const int state_gesture_twoFingerDrag = 8;
    public const int state_firstTapDown = 21;
    public const int state_secondTapDown = 22;
    public const int state_firstTapHold = 23;
    public const int state_secondTapHold = 24;

    [SerializeField]
    float gap_time,longest_single_tap_time, shortest_drag_distance;


    int current_state;
    float local_timer;
    int tap_count;
    Vector2 tap_down_position;
    Vector2 tap_down_position_2;
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
        Debug.Log("current state");
        Debug.Log(current_state);
        switch (current_state)
        {
            case empty_state:
                if (Input.touchCount == 1 && (tap_count == 0 || local_timer > gap_time))
                {
                    tap_down_position = Input.GetTouch(0).position;
                    state_trans(empty_state, state_firstTapDown);
                }
                else if (Input.touchCount == 1 && (tap_count == 1 && local_timer <= gap_time))
                {
                    tap_down_position = Input.GetTouch(0).position;
                    state_trans(empty_state, state_secondTapDown);
                }
                break;


            case state_gesture_tap:
                state_trans(state_gesture_tap, empty_state);
                break;


            case state_gesture_doubleTap:
                state_trans(state_gesture_doubleTap, empty_state);
                break;


            case state_gesture_drag:
                if (Input.touchCount == 0)
                {
                    state_trans(state_gesture_drag, empty_state);
                }
                break;


            case state_gesture_tapAndHold:
                if (Input.touchCount == 0)
                {
                    state_trans(state_gesture_tapAndHold, empty_state);
                }
                break;


            case state_gesture_tapHoldAndDrag:
                if (Input.touchCount == 0)
                {
                    state_trans(state_gesture_tapHoldAndDrag, empty_state);
                }
                break;


            case state_gesture_pinch:
                state_trans(state_gesture_pinch, empty_state);
                break;


            case state_gesture_spread:
                state_trans(state_gesture_spread, empty_state);
                break;


            case state_gesture_twoFingerDrag:
                state_trans(state_gesture_twoFingerDrag, empty_state);
                break;


            case state_firstTapDown:
                if (local_timer > longest_single_tap_time)
                {
                    state_trans(state_firstTapDown, state_firstTapHold);
                }
                else if (Input.touchCount == 0)
                {
                    state_trans(state_firstTapDown, state_gesture_tap);
                }
                else if ((Input.GetTouch(0).position-tap_down_position).magnitude > shortest_drag_distance)
                {
                    state_trans(state_firstTapDown, state_gesture_drag);
                }
                break;


            case state_secondTapDown:
                break;


            case state_firstTapHold:
                if(Input.touchCount == 0)
                {
                    state_trans(state_firstTapHold, state_gesture_tapAndHold);
                }
                else if((Input.GetTouch(0).position - tap_down_position).magnitude > shortest_drag_distance)
                {
                    state_trans(state_firstTapHold, state_gesture_tapHoldAndDrag);
                }
                break;
            case state_secondTapHold:
                break;
            default:
                break;
        }
    }
    void state_trans(int old_state, int new_state)
    {
        Debug.Log(new_state);
        current_state = new_state;
        local_timer = 0;
        switch (old_state)
        {
            case empty_state:
                if(new_state == state_firstTapDown)
                {
                    
                }
                if(new_state == state_secondTapDown)
                {

                }
                break;


            case state_gesture_tap:
                if (new_state == empty_state)
                {
                    tap_count = 1;
                    // action

                }
                break;


            case state_gesture_doubleTap:
                if (new_state == empty_state)
                {
                    tap_count = 0;
                    // action

                }
                break;
            case state_gesture_drag:
                tap_count = 0;

                break;
            case state_gesture_tapAndHold:
                tap_count = 0;

                break;
            case state_gesture_tapHoldAndDrag:
                tap_count = 0;

                break;
            case state_gesture_pinch:
                tap_count = 0;

                break;
            case state_gesture_spread:
                tap_count = 0;

                break;
            case state_gesture_twoFingerDrag:
                tap_count = 0;

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
