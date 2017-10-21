using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<string> actionsList = new List<string>();
    public List<string> tiredList = new List<string>();

    public GameObject monster;
    public GameObject ghost;

    public int attackDmg;

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
                    MoveRight();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("MoveLeft");
                    actionsList.Add("Left");
                    MoveLeft();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    print("Jump");
                    actionsList.Add("Jump");
                    Jump();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    print("Attack");
                    actionsList.Add("Attack");
                    Attack();
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

    public void Jump()
    {
        transform.Translate(2,2,0);
    }

    public void Attack()
    {
        if(!monster)
        {
            monster.GetComponent<Monster>().pv -= attackDmg;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        { 
            if( actionsList[0] == "Attack")
            {
                monster = other.gameObject;
                Attack();
            }
            else
            {
                monster.GetComponent<WalkingMonster>().Attack();
                print("you died");
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
