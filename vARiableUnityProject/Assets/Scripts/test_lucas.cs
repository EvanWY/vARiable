using UnityEngine;
using System.Collections;

public class test_lucas : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log( distance_of_two_point_on_earth(new Vector2(10, 0), new Vector2(10, 1)));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    float distance_of_two_point_on_earth(Vector2 p1, Vector2 p2)
    {
        float delta_theta = Mathf.Sqrt(Mathf.Pow(p1.x - p2.x, 2) 
            + Mathf.Pow((p1.y - p2.y) * Mathf.Cos((p1.x+p2.x)/2 * Mathf.Deg2Rad), 2));
        Debug.Log(delta_theta);
        float delta_s = 6371 * (float)delta_theta * Mathf.Deg2Rad;
        return delta_s;
    }
}
