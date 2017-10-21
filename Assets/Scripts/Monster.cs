using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int playerDamage;
    public int pv = 1;
    public GameObject monster;
    public GameObject player;
    public string moveDirection;
    public float travelDistance;
  
    void Start()
    {

    }
    void Update()
    {
        MoveDirection();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "player")
        {
            player = col.gameObject;
            Attack();
        }
    }

    public void MonterDeath()
    {
        if (pv <= 0)
        {
            Destroy(monster.gameObject);
        }
    }

    public void Attack()
    {
        player.GetComponent<PlayerController>().SetHealth(1);
    }
    public void MoveDirection()
    {
        float targetPos;
        if(moveDirection == "Right")
        {
            targetPos = transform.position.x + travelDistance;

            if(transform.position.x < targetPos)
            {
                transform.Translate(Vector2.right);
            }
            else
            {
                moveDirection = "Left";
            }
        }
        else
        {
            targetPos = transform.position.x - travelDistance;

            if (transform.position.x > targetPos)
            {
                transform.Translate(Vector2.left);
            }
            else
            {
                moveDirection = "Right";
            }
        }

    }
}




    


