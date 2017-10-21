using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : MonoBehaviour, IMonster
{
    public GameObject monsterObj;
    public int life;
    public int attackDmg;
    public MonsterType type;

    public WalkingMonster()
    {
        life = 1;
        attackDmg = 1;
        type = MonsterType.walking;
        monsterObj = new GameObject("WalkingMonster", typeof(SpriteRenderer));
        monsterObj.tag = "Monster";
        monsterObj.GetComponent<SpriteRenderer>().sprite = Resources.Load("WalkingMonster/Monster01", typeof(Sprite)) as Sprite;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {
        print("JE BOUGE");
    }

    public void Attack()
    {
        print("J'attaque");
    }

    public void TakeDamage()
    {
        print("AIE");
    }
}
