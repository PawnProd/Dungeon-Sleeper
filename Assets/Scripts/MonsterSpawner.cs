using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject wMonsterPrefab;
    private IMonster monster;

    private void Start()
    {
        SpawnMonster(MonsterType.walking);
    }

    private void Update()
    {
        
    }

    public void SpawnMonster(MonsterType mType)
    {
        switch(mType)
        {
            case MonsterType.walking:
                Instantiate(wMonsterPrefab, transform.position, Quaternion.identity, transform);
                break;
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
