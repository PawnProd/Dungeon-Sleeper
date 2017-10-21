using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public List<string> actionsList = new List<string>();
    public List<string> tiredList = new List<string>();

    public GameObject monster;
    public GameObject ghost;

    public Animator animator;

    public int attackDmg = 1;

    private string _facingDirection;

    public GameController gameController;

    public bool isDead = false;

    private bool _isMoving;

    private int _health = 1;

    private float _speed = 1;
    private float targetPos;
    private float _delay = 0;


    void Start () {
        animator = GetComponent<Animator>();
    }
	

	void Update () {

        switch (GameController._levelState)
        {
            case LevelState.dreaming:

                ghost.SetActive(true);

                if (Input.GetKeyDown(KeyCode.D))
                {
                    print("MoveRight");
                    actionsList.Add("Right");
                    ghost.GetComponent<GhostController>().actionList.Add("Right");
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("MoveLeft");
                    actionsList.Add("Left");
                    ghost.GetComponent<GhostController>().actionList.Add("Left");
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    print("Jump");
                    actionsList.Add("Jump");
                    ghost.GetComponent<GhostController>().actionList.Add("Jump");
                }

                if (Input.GetMouseButtonDown(0))
                {
                    print("Attack");
                    actionsList.Add("Attack");
                    ghost.GetComponent<GhostController>().actionList.Add("Attack");
                }
                break;
        }

        if (_health<=0)
        {
            Death();
        }
        
    }

    public bool MoveRight()
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

    public bool MoveLeft()
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

    public bool Jump()
    {
        _delay += Time.deltaTime;
        print(_delay);
        if (_delay >= 2)
        {
            animator.SetBool("isJumping", false);
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
                transform.Translate(2 * _speed * Time.deltaTime, 8 * _speed * Time.deltaTime, 0);
                return false;
            }
            else if (_facingDirection == "left" && transform.position.x > targetPos)
            {
                transform.Translate(-2 * _speed * Time.deltaTime, 8 * _speed * Time.deltaTime, 0);
                return false;
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Personnage_Idle"))
            {
                _isMoving = false;
                _delay = 0;
                return true;
            }
        }

        return false;
    }

    public bool Attack()
    {
        if(monster != null)
        {
            monster.GetComponent<WalkingMonster>().TakeDamage();
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Finish_Fight"))
        {
            return true;
        }

        return false;

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Monster")
        {
            monster = other.gameObject;
            if (actionsList.Count > 2 && actionsList[1] == "Attack")
            {
                Attack();
            }
            else
            {
                monster.GetComponent<WalkingMonster>().Attack();
            }
        }
    }

    public void Death()
    {
        print("YOU DIED!!");
        animator.SetBool("isDied", true);
        isDead = true;
    }

    public void SaySomething(string text)
    {
        print(text);
    }

    
                                                       ////////////////////             \\\\\\\\\\\\\\\\\\\
                                             //////////////////////////////   GETTERS   \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    public int GetHealth()
    {
        return _health;
    }

                                                  ////////////////////             \\\\\\\\\\\\\\\\\\\
                                        //////////////////////////////   SETTERS   \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    public void SetHealth(int damage)
    {
        _health -= damage;
    }
}
