using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class TriggerSound : MonoBehaviour {

    public AudioClip clip;
    public AudioSource source;
    public PostProcessingProfile profile;
    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            source.clip = clip;
            source.Play();
            camera.GetComponent<PostProcessingBehaviour>().profile = profile;
        }
    }
}
