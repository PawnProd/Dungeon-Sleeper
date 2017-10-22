using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTuto : MonoBehaviour {

    public GameObject popupToShow;

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
            popupToShow.SetActive(true);
        }
    }
}
