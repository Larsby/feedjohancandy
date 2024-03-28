using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTheRotationBaby : MonoBehaviour {
    Quaternion iniRot;
	// Use this for initialization
	void Start () {
        iniRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

    
          //  var rotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
            transform.rotation = iniRot;
       

    }
}
