using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVictoire : MonoBehaviour {

    public GameObject bullePrincesse;
    public GameObject panelVictoire;

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
            bullePrincesse.SetActive(true);
            StartCoroutine(DelayBeforeEnding());
        }
    }

    IEnumerator DelayBeforeEnding()
    {
        yield return new WaitForSeconds(4);
        panelVictoire.SetActive(true);
    }
}
