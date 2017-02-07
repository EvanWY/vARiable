using UnityEngine;
using System.Collections;

public class single_tap : MonoBehaviour
{
    RaycastHit hit;
    int tap_flag;
    // Use this for initialization
    void Start()
    {
        tap_flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                if (true || hit.collider.tag == "can_be_single_tap")
                {
                    tap_flag = 1;
                    hit.collider.GetComponent<Animator>().SetBool("tap_flag",true);
                }
            }
        }

    }
}
