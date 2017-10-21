using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<string> actionsList = new List<string>();

    private bool _isMoving;

    public GameObject monster;

	void Start () {

	}
	

	void Update () {

        switch (GameController._levelState)
        {
            case LevelState.dreaming:
                if (Input.GetAxis("Horizontal") > 0 )
                {
                    print("MoveRight");
                    actionsList.Add("Right");
                }

                if (Input.GetAxis("Horizontal") < 0 )
                {
                    print("MoveLeft");
                    actionsList.Add("Left");
                }

                if (Input.GetButton("Jump"))
                {
                    print("Jump");
                    actionsList.Add("Jump");
                }

                if (Input.GetButton("Fire1"))
                {
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

    }

    public void OnTriggerEnter(Collider other)
    {

    }
}
