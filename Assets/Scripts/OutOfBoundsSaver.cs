using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsSaver : MonoBehaviour {

    public float saverThreshold = -10f;
    public Vector3 saverLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y <= saverThreshold)
        {
            Debug.Log("yikes");
            gameObject.GetComponent<EntityStat>().Died();
            GameObject.Find("AnnouncerManage").GetComponent<AnnouncerManager>().ShowItselfSlain();
            gameObject.transform.position = saverLocation;
        }
	}
}
