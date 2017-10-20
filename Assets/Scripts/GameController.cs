using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int randomNbActionMin;
    public int randomNbActionMax;

    private LevelState levelState;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        switch(levelState)
        {
            case LevelState.running:
                //DO SOMETHING
                break;
            case LevelState.dreaming:
                //DO SOMETHING
                break;
        }
		
	}

    // Génère le nombre d'action donné au player
    public int GenerateActionToPlayer()
    {
        return Random.Range(randomNbActionMin, randomNbActionMax);
    }
}

public enum LevelState
{
    running,
    dreaming
}
