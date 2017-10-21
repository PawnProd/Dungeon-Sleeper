using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<string> actionsList = new List<string>();
    public List<string> tiredList = new List<string>();

    public GameObject monster;    

    public int attackDmg;

    private string _facingDirection;

    public GameController gameController;

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
                if (Input.GetKeyDown(KeyCode.D))
                {
                    print("MoveRight");
                    actionsList.Add("Right");
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("MoveLeft");
                    actionsList.Add("Left");
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    print("Jump");
                    actionsList.Add("Jump");
                }

                if (Input.GetMouseButtonDown(1))
                {
                    print("Attack");
                    actionsList.Add("Jump");
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
        targetPos = transform.position.x + 1;

        if (transform.position.x < targetPos)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _facingDirection = "right";
            return false;
        }

        else
        {
            _facingDirection = "right";
            return true;
        }

    }

    public bool MoveLeft()
    {
        targetPos = transform.position.x - 1;

        if (transform.position.x > targetPos)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _facingDirection = "left";
            return false;
        }

        else
        {
            _facingDirection = "left";
            return true;
        }

    }

    public void Jump()
    {
        transform.Translate(2,2,0);
    }

    public void Attack()
    {
        monster.GetComponent<Monster>().pv -= attackDmg;
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
            }
        }
    }

    public void Death()
    {
        print("YOU DIED!!");
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
