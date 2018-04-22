using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeisendongAdvertisment : MonoBehaviour {

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}

    private void OnMouseDown() // When sign clicked btw
    {
        System.Diagnostics.Process.Start("https://www.twitch.tv/heisendongna");
    }
}
