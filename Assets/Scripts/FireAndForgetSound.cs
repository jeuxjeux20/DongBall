using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAndForgetSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play(AudioClip audioClip)
    {
        var audio = gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip);
        StartCoroutine(WaitForDying());
    }
    IEnumerator WaitForDying()
    {
        yield return new WaitUntil(() => !gameObject.GetComponent<AudioSource>().isPlaying);
        GameObject.Destroy(gameObject);
    }
}
