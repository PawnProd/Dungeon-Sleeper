﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int randomNbActionMin; // Le nombre d'action minimum que peut effectuer le joueur
    public int randomNbActionMax; // Le nombre d'action maximum que peut effectuer le joueur
    public int delaySleeping; // Le temps de dodo

    public GameObject playerPrefab;
    public GameObject ghostPrefab;
    public GameObject cameraPlayer;

    public static LevelState _levelState;

    private PlayerController _player;
    private GhostController _ghost;

    private int _nbPlayerAction;


	// Use this for initialization
	void Start () {

        SetupGame();
        _levelState = LevelState.dreaming;
        _ghost = Instantiate(ghostPrefab).GetComponent<GhostController>();
        _player = Instantiate(playerPrefab).GetComponent<PlayerController>();
        _player.ghost = _ghost.gameObject;
        
    }
	
	// Update is called once per frame
	void Update () {

        switch(_levelState) // BOUCLE DE JEU
        {
            case LevelState.running:
                print("Running");
                _player.animator.SetBool("isSleep", false);
                cameraPlayer.transform.position = new Vector3(_player.transform.position.x, 0, -22);
                if (_player.actionsList.Count != 0 && _nbPlayerAction != 0)
                {
                    DoPlayerAction();
                }
                else
                {
                    _player.animator.SetBool("isWalking", false);
                    _player.animator.SetBool("isAttack", false);
                    _player.animator.SetBool("isSleep", true);
                    _nbPlayerAction = GenerateActionToPlayer();
                    RandomPhrase();
                    SetLevelState(LevelState.dreaming);
                }
                break;
            case LevelState.dreaming:
                print("Dreaming");
                cameraPlayer.transform.position = new Vector3(_ghost.transform.position.x, 0, -22);
                StartCoroutine(DelaySleeping());
                break;
            case LevelState.transition:
                print("Transition");
                StartCoroutine(DelayTransition());
                break;
        }
	}
    
    // Le temps de sommeil
    IEnumerator DelaySleeping()
    {
        yield return new WaitForSeconds(delaySleeping);
        SetLevelState(LevelState.transition);
    }

    // Le temps de sommeil
    IEnumerator DelayTransition()
    {
        yield return new WaitForSeconds(2);
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
        bool actionFinish = false;
        switch(_player.actionsList[0])
        {
            case "Right":
                actionFinish = _player.MoveRight();
                _player.animator.SetBool("isWalking", true);
                _player.transform.localScale = new Vector3(0.25f, 0.25f, 1);
                break;
            case "Left":
                actionFinish = _player.MoveLeft();
                _player.animator.SetBool("isWalking", true);
                _player.transform.localScale = new Vector3(-0.25f, 0.25f, 1);
                break;
            case "Jump":
                actionFinish = _player.Jump();
                break;
            case "Attack":
                _player.animator.SetBool("isWalking", false);
                _player.animator.SetBool("isAttack", true);
                actionFinish = _player.Attack(); 
                break;
        }
        if(actionFinish)
        {
            _player.actionsList.RemoveAt(0);
            --_nbPlayerAction;
        }

    }

}

public enum LevelState
{
    running,
    dreaming,
    transition
}
