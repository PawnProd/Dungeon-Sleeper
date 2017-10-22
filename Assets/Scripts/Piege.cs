using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piege : MonoBehaviour {
    public GameObject player;
    public int dmgToPlayer;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            player.GetComponent<PlayerController>().SetHealth(dmgToPlayer);
        }
    }
}
