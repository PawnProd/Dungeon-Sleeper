using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject wMonsterPrefab;
    public GameObject mMonsterPrefab;

    public List<GameObject> allWalkingMonsterPrefab;
    public List<GameObject> allMoveableMonsterPrefab;

    public MonsterType mTypeToSpawn;

    private GameObject monster;

    private void Start()
    {
        SpawnMonster(mTypeToSpawn);
    }

    private void Update()
    {
        
    }

    public void SpawnMonster(MonsterType mType)
    {
        switch(mType)
        {
            case MonsterType.walking:
                monster = Instantiate(allWalkingMonsterPrefab[Random.Range(0, allWalkingMonsterPrefab.Count)], transform.position, Quaternion.identity, transform);
                break;
            case MonsterType.moveable:
                monster = Instantiate(allMoveableMonsterPrefab[Random.Range(0, allWalkingMonsterPrefab.Count)], transform.position, Quaternion.identity, transform);
                break;
        }
    }
}

public enum MonsterType
{
    flying,
    walking,
    moveable
}

public interface IMonster
{
    void Move();
    void Attack();
    void TakeDamage();
    void SetSpawnPosition(Vector2 position);
}
