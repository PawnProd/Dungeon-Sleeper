using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    private IMonster monster;
    private MonsterFactory mFactory = new MonsterFactory();
    private float _timer = 0;

    private void Start()
    {
        monster = mFactory.SpawnMonster(MonsterType.walking);
        monster.SetSpawnPosition(transform.position);
    }

    private void Update()
    {
            monster.Move();
        
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
    void SetSpawnPosition(Vector2 position);
}
