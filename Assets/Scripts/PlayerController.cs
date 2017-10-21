using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<string> actionsList = new List<string>();
    public List<string> tiredList = new List<string>();

    public GameObject monster;
    public GameObject ghost;

    public int attackDmg = 1;

    private string _facingDirection;

    public GameController gameController;

    public bool isDead = false;

    private bool _isMoving;

    private int _health = 1;

    private float _speed = 1;
    private float targetPos;


    void Start () {

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
        if (!_isMoving)
        {
            targetPos = transform.position.x + 2;
            _isMoving = true;
        }
        print(targetPos);
        if (transform.position.x < targetPos)
        {
            transform.Translate(2 * _speed * Time.deltaTime, 8 * _speed * Time.deltaTime, 0);
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

    public bool Attack()
    {
        if(monster != null)
        {
            monster.GetComponent<WalkingMonster>().TakeDamage();
        }

        return true;

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Monster")
        {
            monster = other.gameObject;
            if (actionsList.Count < 2 && actionsList[1] == "Attack")
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
