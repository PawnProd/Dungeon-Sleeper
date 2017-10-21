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
                DoPlayerAction();
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
        _nbPlayerAction = GenerateActionToPlayer();
    }

    // Effectue l'enchainement d'action programmé par le joueur
    public void DoPlayerAction()
    {
        while(_player.actionsList.Count != 0 && _nbPlayerAction != 0)
        {
            if(_player.actionsList[0] == "Right") // Déplacement à droite
            {
                _player.MoveRight();
            }
            else if (_player.actionsList[0] == "Left") // Déplacement à gauche
            {
                _player.MoveLeft();
            }
            else if (_player.actionsList[0] == "Jump") // Saut
            {
                _player.Jump();
            }
            // On passe à l'input suivant
            _player.actionsList.RemoveAt(0);
            --_nbPlayerAction;
        }
    }

}

public enum LevelState
{
    running,
    dreaming
}
