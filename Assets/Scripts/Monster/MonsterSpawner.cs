using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //몬스터를 스폰스켜주는 스크립트

    public GameObject Monster = null;
    public GameObject boss = null;
    int xPos;
    int zPos;
    public int monsterCount;
    public int maxMonsterCount = 20; //최대로 소환될 몬스터수
    int spawnerSigt;
    float spawnInterval = 1.0f;
    

    /// <summary>
    /// 게임매니저에서 타워설치에서 전투모드로 갈때 몬스터 소환 코루틴을 실행시켜줄 함수
    /// </summary>
    /// <param name="Swap">true이면 스폰시작</param>
    public void StartSpawn(bool Swap)
    {
        if (Swap)
        {
            StartCoroutine(Spawner());
        }
    }
    /// <summary>
    /// 몬스터 스폰해주는 IEnumerator
    /// </summary>
    /// <returns>spawnInterval에 설정된 시간에 따라 소환됨 </returns>
    IEnumerator Spawner()
    {
        while (monsterCount<maxMonsterCount-1)
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
            Debug.Log("소환");
        }

        if (GameManager.INSTANCE.Wave == GameManager.INSTANCE.MaxWave)
        {

            spawnerSigt = Random.Range(0, 4);
            if (spawnerSigt == 0)
            {
                BossSpawn();
            }
            if (spawnerSigt == 1)
            {
                BossSpawn();
            }
            if (spawnerSigt == 2)
            {
                BossSpawn();
            }
            if (spawnerSigt == 3)
            {
                BossSpawn();
            }
        }
        

    }

    private void BossSpawn()
    {
        StartCoroutine(GameManager.INSTANCE.BossMasageOn());
        GameObject bossmons = Instantiate(boss, new Vector3(0, 0, -46), Quaternion.identity);
        monsterCount += 1;
        Debug.Log("보스소환");
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
