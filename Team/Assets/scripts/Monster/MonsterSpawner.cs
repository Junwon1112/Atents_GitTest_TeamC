using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Monster = null;
    int xPos;
    int zPos;
    public int monsterCount;
    public int maxMonsterCount = 20;
    int spawnerSigt;
    float spawnInterval = 1.0f;
    //-46 -10
    public void StartSpawn(bool Swap)
    {
        if (Swap)
        {
            StartCoroutine(Spawner());
        }
    }

    IEnumerator Spawner()
    {
        while (monsterCount<maxMonsterCount)
        {
            yield return new WaitForSeconds(spawnInterval);
            spawnerSigt = Random.Range(0, 4);
            if(spawnerSigt== 0)
            {
                GameObject mons = Instantiate(Monster, new Vector3(46, 0, 0), Quaternion.identity);
                monsterCount += 1;
            }
            if (spawnerSigt == 1)
            {
                GameObject mons = Instantiate(Monster, new Vector3(-46, 0, -10), Quaternion.identity);
                monsterCount += 1;
            }
            if (spawnerSigt == 2)
            {
                GameObject mons = Instantiate(Monster, new Vector3(0, 0, 46), Quaternion.identity);
                monsterCount += 1;
            }
            if (spawnerSigt == 3)
            {
                GameObject mons = Instantiate(Monster, new Vector3(0, 0, -46), Quaternion.identity);
                monsterCount += 1;
            }
            Debug.Log("¼ÒÈ¯");
        }

    }

    //IEnumerator MonsterSpawn()
    //{
    //    spawnerSigt = Random.Range(0, 4);

    //    while ( monsterCount < maxMonsterCount)
    //    {
    //        if (spawnerSigt == 0)
    //        {
    //            xPos = Random.Range(-46, 46);
    //            zPos = Random.Range(-46, -45);
    //            //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

    //            yield return new WaitForSeconds(spawnInterval);
    //            GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
    //            monsterCount += 1;
    //        }
    //        if (spawnerSigt == 1)
    //        {
    //            xPos = Random.Range(-46, -45);
    //            zPos = Random.Range(46, -46);
    //            //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

    //            yield return new WaitForSeconds(spawnInterval);
    //            GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
    //            monsterCount += 1;
    //        }
    //        if (spawnerSigt == 2)
    //        {
    //            xPos = Random.Range(-46, 46);
    //            zPos = Random.Range(46, 45);
    //            //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

    //            yield return new WaitForSeconds(spawnInterval);
    //            GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
    //            monsterCount += 1;
    //        }
    //        if (spawnerSigt == 3)
    //        {
    //            xPos = Random.Range(45, 46);
    //            zPos = Random.Range(46, -46);
    //            //Instantiate(monster, new Vector3(xPos, 0, zPos), Quaternion.identity);

    //            yield return new WaitForSeconds(spawnInterval);
    //            GameObject mons = Instantiate(Monster, new Vector3(xPos, 0, zPos), Quaternion.identity);
    //            monsterCount += 1;
    //        }
    //    }
    //}
}
