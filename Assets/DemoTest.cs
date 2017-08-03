using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
public class DemoTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        AppFacade.Ins.GetMgr<ResourceManager>().LoadAsset("Cube", (obj) => {
            //
        });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
