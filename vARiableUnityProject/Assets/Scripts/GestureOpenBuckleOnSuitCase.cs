using UnityEngine;
using System.Collections;

public class GestureOpenBuckleOnSuitCase : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnSingleTap(string name)
    {
        GetComponent<Animator>().SetInteger("isTagged", 1);
    }
}
