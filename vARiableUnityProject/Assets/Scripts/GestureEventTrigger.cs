using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class GestureEventTrigger : MonoBehaviour {

    public UnityEventArray[] SingleTapEventTriggerList;

    int currSingleTapEventTriggerListIdx = 0;

    void OnSingleTap()
    {
        if (SingleTapEventTriggerList.Length > 0)
        {
            foreach (var e in SingleTapEventTriggerList[currSingleTapEventTriggerListIdx].EventArray)
                e.Invoke();
            currSingleTapEventTriggerListIdx = (currSingleTapEventTriggerListIdx + 1) % SingleTapEventTriggerList.Length;
        }
    }
}

[Serializable]
public class UnityEventArray
{
    public UnityEvent[] EventArray;

}
