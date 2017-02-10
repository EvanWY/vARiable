using UnityEngine;
using System.Collections;

public class GestureAnimationTrigger : MonoBehaviour {

    public string[] SingleTapAnimTriggerList;
    
    int currSingleTapAnimTriggerListIdx = 0;

    void OnSingleTap()
    {
        if (SingleTapAnimTriggerList.Length > 0)
        {
            GetComponent<Animator>().SetTrigger(SingleTapAnimTriggerList[currSingleTapAnimTriggerListIdx]);
            currSingleTapAnimTriggerListIdx = (currSingleTapAnimTriggerListIdx + 1) % SingleTapAnimTriggerList.Length;
        }
    }
}
