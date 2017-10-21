using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int randomNbActionMin; // Le nombre d'action minimum que peut effectuer le joueur
    public int randomNbActionMax; // Le nombre d'action maximum que peut effectuer le joueur
    public int delaySleeping; // Le temps de dodo

    public GameObject playerPrefab;

    public static LevelState _levelState;

    private PlayerController _player;

    private int _nbPlayerAction;

    private float _timer = 0;


	// Use this for initialization
	void Start () {

        SetupGame();
        _levelState = LevelState.dreaming;
        _player = Instantiate(playerPrefab).GetComponent<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {

        switch(_levelState) // BOUCLE DE JEU
        {
            case LevelState.running:
                print("Running");
                _timer += Time.deltaTime;
                if(_timer >= 1)
                {
                    print(_player.actionsList.Count);
                    if(_player.actionsList.Count != 0 && _nbPlayerAction != 0)
                    {
                        DoPlayerAction();
                    }
                    else
                    {
                        RandomPhrase();
                        SetLevelState(LevelState.dreaming);
                    }
                    
                    _timer = 0;

                }
                break;
            case LevelState.dreaming:
                print("Dreaming");
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
        _nbPlayerAction = GenerateActionToPlayer();
    }

    public void RandomPhrase()
    {
        string phrase = _player.tiredList[Random.Range(0, _player.tiredList.Count - 1)];
        _player.SaySomething(phrase);
    }

    // Effectue la liste d'action des players
    public void DoPlayerAction()
    {
        switch(_player.actionsList[0])
        {
            case "Right":
                _player.MoveRight();
                break;
            case "Left":
                _player.MoveLeft();
                break;
            case "Jump":
                _player.Jump();
                break;
        }

        _player.actionsList.RemoveAt(0);
        --_nbPlayerAction;
    }

}

public enum LevelState
{
    running,
    dreaming
}
