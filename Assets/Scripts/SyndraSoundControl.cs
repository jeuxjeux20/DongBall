using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyndraSoundControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CheckChanged(bool b)
    {
        ControlDong.PlaySound = b;
    }
}
