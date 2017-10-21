using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : MonoBehaviour, IMonster
{
    public GameObject monsterObj;
    public int life;
    public int attackDmg;
    public MonsterType type;

    private string _moveDirection;
    private float _travelDistance;
    private GameObject _target;

    public WalkingMonster()
    {
        life = 1;
        attackDmg = 1;
        type = MonsterType.walking;
        monsterObj = new GameObject("WalkingMonster", typeof(SpriteRenderer), typeof(Rigidbody2D));
        monsterObj.tag = "Monster";
        monsterObj.GetComponent<SpriteRenderer>().sprite = Resources.Load("WalkingMonster/Monster01", typeof(Sprite)) as Sprite;
        monsterObj.AddComponent<BoxCollider2D>();
        _moveDirection = "Left";
        _travelDistance = 3;
    }

	// Use this for initialization
	void Start () {
        Move();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {
        float targetPos;
        if (_moveDirection == "Right")
        {
            targetPos = monsterObj.transform.position.x + _travelDistance;

            if (monsterObj.transform.position.x < targetPos)
            {
                monsterObj.transform.Translate(Vector2.right);
            }
            else
            {
                _moveDirection = "Left";
            }
        }
        else
        {
            targetPos = monsterObj.transform.position.x - _travelDistance;
            print("Target "+targetPos);
            if (monsterObj.transform.position.x > targetPos)
            {
                print("JE BOUGE");
                monsterObj.transform.Translate(Vector2.left);
            }
            else
            {
                _moveDirection = "Right";
            }
        }
    }

    public void Attack()
    {
        if(_target != null)
        {
            _target.GetComponent<PlayerController>().SetHealth(attackDmg);
        }
    }

    public void TakeDamage()
    {
        if(_target != null)
        {
            life -= _target.GetComponent<PlayerController>().attackDmg;
            if (life <= 0)
            {
                Destroy(monsterObj);
                Destroy(this);
            }
        }

    }
}
