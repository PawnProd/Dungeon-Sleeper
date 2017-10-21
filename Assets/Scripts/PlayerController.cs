using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<string> actionsList = new List<string>();

    private bool _isMoving;

    public GameObject monster;

    public int health;

	void Start () {

	}
	

	void Update () {

        switch (GameController._levelState)
        {
            case LevelState.dreaming:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    print("MoveRight");
                    actionsList.Add("Right");
                }

                if (Input.GetKeyDown(KeyCode.D))
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
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left);
    }

    public void Jump()
    {
        transform.Translate(1,2,0);
    }

    public void Attack()
    {
        //monster.GetComponent<Monster>().pv--;
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
        
    }
}
