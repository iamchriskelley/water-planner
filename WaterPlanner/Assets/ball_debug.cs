using UnityEngine;
using System.Collections;

public class ball_debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Debug.Log("ball: " + gameObject.GetComponent<Rigidbody>().velocity);
    }

    /*void Update()
    {
        var pos = gameObject.GetComponent<Transform>().position;
        Debug.DrawLine(pos, pos - new Vector3(0, 10000, 0), Color.yellow);
    }*/
}
