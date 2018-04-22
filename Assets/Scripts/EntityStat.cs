using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityStat : MonoBehaviour {

    [HideInInspector]
    private int deaths = 0;
    [HideInInspector]
    private int kills = 0;
    [HideInInspector]
    private int assists = 0;

    public bool mainPlayer = true;

    // Use this for initialization
    void Start () {
		if (mainPlayer)
        {
            GameObject.Find("KDA").GetComponent<Text>().text = "0/0/0";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Died()
    {
        deaths++;
        UpdateText();
    }
    public void KilledSomeone()
    {
        kills++;
        UpdateText();
    }
    public void AssistedOnAKill()
    {
        assists++;
        UpdateText();
    }
    private void UpdateText()
    {
        GameObject.Find("KDA").GetComponent<Text>().text = string.Join("/", new string[] { kills.ToString(), deaths.ToString(), assists.ToString() });
    }
}
