using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int randomNbActionMin;
    public int randomNbActionMax;

    public int delaySleeping;

    private LevelState _levelState;

	// Use this for initialization
	void Start () {
        SetupGame();
	}
	
	// Update is called once per frame
	void Update () {

        switch(_levelState) // BOUCLE DE JEU
        {
            case LevelState.running:
                //DO SOMETHING
                break;
            case LevelState.dreaming:
                StartCoroutine(DelaySleeping());
                break;
        }
		
	}
    
    // Le temps de sommeil
    IEnumerator DelaySleeping()
    {
        yield return new WaitForSeconds(delaySleeping);
        SetLevelState(LevelState.running);
    }

    // Génère le nombre d'action donné au player
    public int GenerateActionToPlayer()
    {
        return Random.Range(randomNbActionMin, randomNbActionMax);
    }

    // Change l'état du level 
    public void SetLevelState(LevelState newState)
    {
        _levelState = newState;
    }

    // Setup les différents paramètre de la partie
    public void SetupGame()
    {
        GenerateActionToPlayer();
    }

}

public enum LevelState
{
    running,
    dreaming
}
