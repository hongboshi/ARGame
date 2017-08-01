using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void Go()
    {
        Rigidbody rig = transform.GetComponentIfNoExist<Rigidbody>();
        rig.useGravity = false;
        
        rig.AddForce(Vector3.forward * 15f * Time.deltaTime, ForceMode.Impulse);
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
            Go();
	}
}
