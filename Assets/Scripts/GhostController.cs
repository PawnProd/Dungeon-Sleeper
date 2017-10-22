using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    public List<string> actionList = new List<string>();

    private float _speed = 2;
    private float targetPos;

    private Animator _animator;

    private string _facingDirection;
    private bool _isMoving;
    private float _delay;
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(actionList.Count != 0)
        {
            DoPlayerAction();
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

        if(GameController._levelState == LevelState.running)
        {
            _animator.SetBool("isWalking", false);
        }
	}

    public bool MoveRightGhost()
    {
        if (!_isMoving)
        {
            targetPos = transform.position.x + 1;
            _isMoving = true;
        }

        if (transform.position.x < targetPos)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _facingDirection = "right";
            return false;
        }

        else
        {
            _facingDirection = "right";
            _isMoving = false;
            return true;
        }

    }

    public bool MoveLeftGhost()
    {
        if (!_isMoving)
        {
            targetPos = transform.position.x - 1;
            _isMoving = true;
        }

        if (transform.position.x > targetPos)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _facingDirection = "left";
            return false;
        }

        else
        {
            _facingDirection = "left";
            _isMoving = false;
            return true;
        }

    }

    // Effectue la liste d'action des players
    public void DoPlayerAction()
    {
        bool actionFinish = false;
        switch (actionList[0])
        {
            case "Right":
                _animator.SetBool("isWalking", true);
                transform.localScale = new Vector3(0.25f, 0.25f, 1);
                actionFinish = MoveRightGhost();
                break;
            case "Left":
                _animator.SetBool("isWalking", true);
                transform.localScale = new Vector3(-0.25f, 0.25f, 1);
                actionFinish = MoveLeftGhost();
                break;
            case "Jump":
                _animator.SetBool("isJumping", true);
                actionFinish = JumpGhost();
                break;
            case "Attack":
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isAttack", true);
                actionFinish = AttackGhost();
                break;
        }
        if (actionFinish)
        {
            actionList.RemoveAt(0);
        }

    }

    public bool JumpGhost()
    {
        _delay += Time.deltaTime;
        if (_delay >= 1)
        {
            _animator.SetBool("isJumping", false);
            if (!_isMoving)
            {
                if (_facingDirection == "right")
                {
                    targetPos = transform.position.x + 2;
                }
                else
                {
                    targetPos = transform.position.x - 2;
                }
                _isMoving = true;
            }
            print(targetPos);
            if (_facingDirection == "right" && transform.position.x < targetPos)
            {
                transform.Translate(1 * _speed * Time.deltaTime, 4 * _speed * Time.deltaTime, 0);
                return false;
            }
            else if (_facingDirection == "left" && transform.position.x > targetPos)
            {
                transform.Translate(-1 * _speed * Time.deltaTime, 4 * _speed * Time.deltaTime, 0);
                return false;
            }

            else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Personnage_Idle"))
            {
                _isMoving = false;
                _delay = 0;
                return true;
            }
        }

        return false;
    }


    public bool AttackGhost()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Personnage_Fight"))
        {
            _animator.SetBool("isAttack", false);
            return true;
        }

        return false;
    }

}
