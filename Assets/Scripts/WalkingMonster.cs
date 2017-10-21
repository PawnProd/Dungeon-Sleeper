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
    private float _targetPos;
    private float _speed = 1f;

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
        _targetPos = monsterObj.transform.position.x - _travelDistance;
    }

    public GameObject MonsterObj
    {
        get { return monsterObj; }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void Move()
    {
        if (_moveDirection == "Right")
        {       
            if (monsterObj.transform.position.x < _targetPos)
            {
                monsterObj.transform.position += Vector3.right * _speed * Time.deltaTime;
            }
            else
            {
                _moveDirection = "Left";
                _targetPos = monsterObj.transform.position.x - _travelDistance;
            }
        }
        else
        {
            if (monsterObj.transform.position.x > _targetPos)
            {
                monsterObj.transform.position += Vector3.left * _speed * Time.deltaTime;
            }
            else
            {
                _moveDirection = "Right";
                _targetPos = monsterObj.transform.position.x + _travelDistance;
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

    public void SetSpawnPosition(Vector2 position)
    {
        monsterObj.transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _target = other.gameObject;
            if(GameController._levelState == LevelState.dreaming)
            {
                Attack();
            }
        }
    }
}
