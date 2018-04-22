using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public GameObject barForeground;
    [Range(0.0f, 1.0f)]
    public float fill;

	// Use this for initialization
	void Start () {
        Image component = barForeground.GetComponent<Image>();
        component.fillMethod = Image.FillMethod.Horizontal;
        component.fillAmount = fill;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetProgress(float fill)
    {
        this.fill = fill;
        Image component = barForeground.GetComponent<Image>();
        component.fillAmount = this.fill;
    }
}
