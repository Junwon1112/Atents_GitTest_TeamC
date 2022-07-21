using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Monster = null;
    public int xPos;
    public int zPos;
    public int monsterCount;
    public int maxMonsterCount = 20;
    public int spawnerSigt;
    public float spawnInterval = 0.1f;

    private void Start()
    {
        StartCoroutine(MonsterSpawn());
    }

    IEnumerator MonsterSpawn()
    {
        spawnerSigt = Random.Range(0, 4);

        while ( monsterCount < maxMonsterCount)
        {
            if (spawnerSigt == 0)
            {
                xPos = Random.Range(-46, 46);
                zPos = Random.Range(-46, -45);
                //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

                yield return new WaitForSeconds(spawnInterval);
                GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
                monsterCount += 1;
            }
            if (spawnerSigt == 1)
            {
                xPos = Random.Range(-46, -45);
                zPos = Random.Range(46, -46);
                //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

                yield return new WaitForSeconds(spawnInterval);
                GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
                monsterCount += 1;
            }
            if (spawnerSigt == 2)
            {
                xPos = Random.Range(-46, 46);
                zPos = Random.Range(46, 45);
                //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

                yield return new WaitForSeconds(spawnInterval);
                GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
                monsterCount += 1;
            }
            if (spawnerSigt == 3)
            {
                xPos = Random.Range(45, 46);
                zPos = Random.Range(46, -46);
                //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

                yield return new WaitForSeconds(spawnInterval);
                GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
                monsterCount += 1;
            }
        }
    }
}
