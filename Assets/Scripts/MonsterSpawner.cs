using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject wMonsterPrefab;
    public GameObject mMonsterPrefab;

    public List<Sprite> allSpritesWalking;
    public List<RuntimeAnimatorController> allAnimatorWalking;

    public List<Sprite> allSpritesMoveable;
    public List<RuntimeAnimatorController> allAnimatorMoveable;

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
                monster = Instantiate(wMonsterPrefab, transform.position, Quaternion.identity, transform);
                int numMonster = Random.Range(0, allSpritesWalking.Count);
                monster.GetComponent<SpriteRenderer>().sprite = allSpritesWalking[numMonster];
                monster.GetComponent<Animator>().runtimeAnimatorController = allAnimatorWalking[numMonster];
                break;
            case MonsterType.moveable:
                monster = Instantiate(mMonsterPrefab, transform.position, Quaternion.identity, transform);
                int numMonster2 = Random.Range(0, allSpritesMoveable.Count);
                monster.GetComponent<SpriteRenderer>().sprite = allSpritesMoveable[numMonster2];
                monster.GetComponent<Animator>().runtimeAnimatorController = allAnimatorMoveable[numMonster2];
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
