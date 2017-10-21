using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject wMonsterPrefab;
    public List<Sprite> allSprites;
    public List<RuntimeAnimatorController> allAnimator;
    private GameObject monster;

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
                monster = Instantiate(wMonsterPrefab, transform.position, Quaternion.identity, transform);
                int numMonster = Random.Range(0, allSprites.Count - 1);
                monster.GetComponent<SpriteRenderer>().sprite = allSprites[numMonster];
                monster.GetComponent<Animator>().runtimeAnimatorController = allAnimator[numMonster];
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
