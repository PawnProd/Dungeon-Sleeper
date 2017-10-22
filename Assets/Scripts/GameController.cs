using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int randomNbActionMin; // Le nombre d'action minimum que peut effectuer le joueur
    public int randomNbActionMax; // Le nombre d'action maximum que peut effectuer le joueur
    public float delaySleeping; // Le temps de dodo

    public GameObject playerPrefab;
    public GameObject ghostPrefab;
    public GameObject cameraPlayer;
    public GameObject panelGameOver;

    public static LevelState _levelState;

    private PlayerController _player;
    private GhostController _ghost;

    private float _timer = 0;


	// Use this for initialization
	void Start () {
        _levelState = LevelState.dreaming;
        _ghost = Instantiate(ghostPrefab).GetComponent<GhostController>();
        _player = Instantiate(playerPrefab).GetComponent<PlayerController>();
        _player.ghost = _ghost.gameObject;
        _player.animator.SetBool("isSleep", true);
        SetupGame();

    }
	
	// Update is called once per frame
	void Update () {
        if(!_player.isDead)
        {
            switch (_levelState) // BOUCLE DE JEU
            {
                case LevelState.running:
                    print("Running");
                    _player.animator.SetBool("isSleep", false);
                    cameraPlayer.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -22);
                    if (_player.actionsList.Count != 0)
                    {
                        DoPlayerAction();
                    }
                    else
                    {
                        _player.animator.SetBool("isWalking", false);
                        _player.animator.SetBool("isAttack", false);
                        _player.animator.SetBool("isJumping", false);
                        _player.animator.SetBool("isSleep", true);
                        GenerateActionToPlayer();
                        RandomPhrase();
                        SetLevelState(LevelState.dreaming);
                    }
                    break;
                case LevelState.dreaming:
                    print("Dreaming");
                    cameraPlayer.transform.position = new Vector3(_ghost.transform.position.x, _ghost.transform.position.y, -22);
                    if(_player.actionsList.Count != 0)
                    {
                        StartCoroutine(DelaySleeping());
                    }
                    break;
                
            }
        }
        else
        {
            panelGameOver.SetActive(true);
        }
       
	}
    
    // Le temps de sommeil
    IEnumerator DelaySleeping()
    {
        yield return new WaitForSeconds(delaySleeping);
        SetLevelState(LevelState.running);
    }

    // Génère le nombre d'action donné au player
    public void GenerateActionToPlayer()
    {
        _player.nbActionMax =  Random.Range(randomNbActionMin, randomNbActionMax);
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

    public void RandomPhrase()
    {
        string phrase = _player.tiredList[Random.Range(0, _player.tiredList.Count - 1)];
        _player.SaySomething(phrase);
    }

    public void ReplaceScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
                _player.animator.SetBool("isWalking", false);
                _player.animator.SetBool("isJumping", true);
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
        }

    }


}

public enum LevelState
{
    running,
    dreaming,
}
