using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSyndra : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("DongerSphere").GetComponent<PlayerLevel>().LeveledUp += MoreSyndra_LeveledUp;
	}

    private void MoreSyndra_LeveledUp(object sender, System.EventArgs e)
    {
        gameObject.GetComponent<Spawner>().Interval /= 1.085f;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
