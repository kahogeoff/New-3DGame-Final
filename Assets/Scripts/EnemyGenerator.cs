using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject FasterEnemy;
    public GameObject HeavyEnemy;
    public int EnemyThresHold;
    public int WaveNum;
    public int EnemyNumPerWave;
    public int EnemyNumNow;
    public float TimeCounter;
    public bool PassChapter;

    private GameObject[] SpawnPointList;
    public int WaveCounter;
    private int SpawnPosNum;
    private bool firstWave;

    // Use this for initialization
    void Start()
    {
        SpawnPointList = GameObject.FindGameObjectsWithTag("Spawn");
        SpawnPosNum = SpawnPointList.Length;
        WaveCounter = 0;
        TimeCounter = 10.0f;
        firstWave = true;
        EnemyNumNow = GameObject.FindGameObjectsWithTag("Enemy").Length;
        PassChapter = false;
        //GameObject.FindGameObjectsWithTag("Enemy").Length
    }

    // Update is called once per frame
    void Update()
    {
        EnemyNumNow = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (firstWave)
        {
            TimeCounter -= Time.deltaTime;
            if (TimeCounter <= 0.0f)
            {
                SpawnEnemy();
                WaveCounter++;
                TimeCounter = 5.0f;
                
                firstWave = false;
            }
        }
        else
        {
            if (EnemyNumNow <= 0)
            {
                if (WaveCounter < WaveNum)
                {
                    TimeCounter -= Time.deltaTime;
                    if (TimeCounter <= 0.0f)
                    {
                        SpawnEnemy();
                        WaveCounter++;
                        TimeCounter = 5.0f;
                        EnemyNumPerWave += 1;
                        EnemyThresHold += 90;
                    }
                }
                else
                {
                    //pass successfully
                    PassChapter = true;
                }
            }
        }


    }

    private void SpawnEnemy()
    {
        GameObject tmp_Enemy =new GameObject();
        //Random.seed = System.Guid.NewGuid().GetHashCode();
        for (int i = 0; i < EnemyNumPerWave; i++)
        {
            //tmp_Enemy = Instantiate(FasterEnemy) as GameObject;
            if (Random.Range(0, 100) > EnemyThresHold)
            {
                tmp_Enemy = Instantiate(FasterEnemy) as GameObject;
            }

            else if (Random.Range(0, 100) <= EnemyThresHold)
            {
                tmp_Enemy = Instantiate(HeavyEnemy) as GameObject;
            }

            int k = Random.Range(0, SpawnPosNum - 1);
            tmp_Enemy.transform.position = SpawnPointList[k].transform.position;
            tmp_Enemy.SetActive(false);
            tmp_Enemy.SetActive(true);
            tmp_Enemy.SendMessage("SetTarget");
        }

    }
}
