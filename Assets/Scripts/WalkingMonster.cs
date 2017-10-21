using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : MonoBehaviour, IMonster
{
    public int life;
    public int attackDmg;
    public MonsterType type;

    private string _moveDirection;

    private float _travelDistance;
    private float _targetPos;
    private float _speed = 1f;

    private GameObject _target;


	// Use this for initialization
	void Start () {
        life = 1;
        attackDmg = 1;
        type = MonsterType.walking;
        SetupMonster();
        _moveDirection = "Left";
        _travelDistance = 3;
        _targetPos = transform.position.x - _travelDistance;
    }
	
	// Update is called once per frame
	void Update () {

       // Move();
    }

    public void Move()
    {
        if (_moveDirection == "Right")
        {       
            if (transform.position.x < _targetPos)
            {
                transform.position += Vector3.right * _speed * Time.deltaTime;
            }
            else
            {
                _moveDirection = "Left";
                _targetPos = transform.position.x - _travelDistance;
            }
        }
        else
        {
            if (transform.position.x > _targetPos)
            {
                transform.position += Vector3.left * _speed * Time.deltaTime;
            }
            else
            {
                _moveDirection = "Right";
                _targetPos = transform.position.x + _travelDistance;
            }
        }
    }

    public void SetupMonster()
    {
        gameObject.tag = "Monster";
        GetComponent<SpriteRenderer>().sprite = Resources.Load("WalkingMonster/Monster01", typeof(Sprite)) as Sprite;
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
            print("LA VIE "+life);
            if (life <= 0)
            {
                Destroy(this.gameObject);
            }
        }

    }

    public void SetSpawnPosition(Vector2 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
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
