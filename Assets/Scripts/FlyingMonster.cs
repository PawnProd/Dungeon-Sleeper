using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : MonoBehaviour, IMonster {

    public GameObject monsterObj;
    public int life;
    public int attackDmg;
    public MonsterType type;

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
