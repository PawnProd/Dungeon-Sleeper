using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<string> actionsList = new List<string>();
    public List<string> tiredList = new List<string>();

    public GameObject monster;    

    public int attackDmg;

    public string facingDirection;

    private bool _isMoving;

    private int _health;


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
                    Attack();
                }
                break;
        }
        
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right);
        facingDirection = "right";
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left);
        facingDirection = "left";
    }

    public void Jump()
    {
        transform.Translate(1,2,0);
    }

    public void Attack()
    {
        if (facingDirection == "left")
        {

        }

        else
        {

        }
        //monster.GetComponent<Monster>().pv -= attackDmg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            monster = other.gameObject;
            Attack();
        }
    }

    public void Death()
    {
        print("YOU DIED!!");
    }

    public void SaySomething(string text)
    {
        print("I said something!");
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
