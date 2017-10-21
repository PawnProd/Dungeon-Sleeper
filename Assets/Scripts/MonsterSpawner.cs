using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public List<IMonster> listMonster = new List<IMonster>();
    private MonsterFactory mFactory = new MonsterFactory();

    private void Start()
    {
        listMonster.Add(mFactory.SpawnMonster(MonsterType.walking));

        listMonster[0].Move();
        listMonster[0].Attack();
        listMonster[0].TakeDamage();
    }
}

public class MonsterFactory
{
    public IMonster SpawnMonster(MonsterType mType)
    {
        switch(mType)
        {
            case MonsterType.flying:
                return new FlyingMonster();
            case MonsterType.walking:
                return new WalkingMonster();
            default:
                return new WalkingMonster();
        }
    }
}

public enum MonsterType
{
    flying,
    walking
}

public interface IMonster
{
    void Move();
    void Attack();
    void TakeDamage();
}
