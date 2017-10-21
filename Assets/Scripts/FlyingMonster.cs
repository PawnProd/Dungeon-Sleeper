using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : MonoBehaviour, IMonster {

    public GameObject monsterObj;
    public int life;
    public int attackDmg;
    public MonsterType type;

    private string _moveDirection;
    private float _travelDistance;
    private GameObject _target;

    public FlyingMonster()
    {
        life = 1;
        attackDmg = 1;
        type = MonsterType.flying;
        monsterObj = new GameObject("FlyingMonster", typeof(SpriteRenderer));
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        float targetPos;
        if (_moveDirection == "Right")
        {
            targetPos = transform.position.x + _travelDistance;

            if (transform.position.x < targetPos)
            {
                transform.Translate(Vector2.right);
            }
            else
            {
                _moveDirection = "Left";
            }
        }
        else
        {
            targetPos = transform.position.x - _travelDistance;

            if (transform.position.x > targetPos)
            {
                transform.Translate(Vector2.left);
            }
            else
            {
                _moveDirection = "Right";
            }
        }
    }

    public void Attack()
    {
        _target.GetComponent<PlayerController>().SetHealth(attackDmg);
    }

    public void TakeDamage()
    {
        life -= _target.GetComponent<PlayerController>().attackDmg;
        if (life <= 0)
        {
            Destroy(monsterObj);
            Destroy(this);
        }
    }
}
