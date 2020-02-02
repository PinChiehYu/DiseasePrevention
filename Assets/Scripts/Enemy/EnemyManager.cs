using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Fever, Dying, Nomask, Healthy, BT, Null }

[Serializable]
public class LaneData
{
    public EnemyType mainType;
    public GameObject prefab;
    public Vector3 spawnPoint;
    public Queue<EnemyController> enemyStack;
}

public class EnemyManager : MonoBehaviour
{

    public float spawningDuration = 0.25f;

    public List<LaneData> laneDataList;

    public GameObject prefabHealthy;
    public GameObject prefabBT;

    private int[] btLock;
    private int[] continuousLock;
    public int btBlockCount = 4;

    private bool isEnded = false;

    private Coroutine spawningCoro;

    private float currentTime = 0f;

    private void Start()
    {
        btLock = new int[3] { 0, 0, 0 };
        continuousLock = new int[3] { 0, 0, 0 };

        for (int lane = 0; lane < 3; lane++)
        {
            laneDataList[lane].enemyStack = new Queue<EnemyController>(10);
        }

        spawningCoro = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1f);

        while (!isEnded)
        {
            for (int lane = 0; lane < 3; lane++)
            {
                SpawnEnemy(lane);
            }

            yield return new WaitForSecondsRealtime(spawningDuration);
            currentTime += spawningDuration;
        }
    }

    private void SpawnEnemy(int lane)
    {
        GameObject tmp = null;
        EnemyType result = RandomSpawnFunc(laneDataList[lane].mainType);

        if (result.Equals(EnemyType.Null) || continuousLock[lane] == 2 || btLock[lane] > 0)
        {
            continuousLock[lane] = 0;
            btLock[lane]--;
            return;
        }

        if (result.Equals(laneDataList[lane].mainType))
        {
            tmp = Instantiate(laneDataList[lane].prefab, laneDataList[lane].spawnPoint, Quaternion.identity);
        }
        else if (result.Equals(EnemyType.Healthy))
        {
            tmp = Instantiate(prefabHealthy, laneDataList[lane].spawnPoint, Quaternion.identity);
        }
        else if (result.Equals(EnemyType.BT))
        {
            tmp = Instantiate(prefabBT, laneDataList[lane].spawnPoint, Quaternion.identity);
            btLock[lane] = btBlockCount;
        }

        continuousLock[lane]++;

        tmp.name = lane.ToString();
        tmp.GetComponent<EnemyController>().OnEnemyStopInteract += RemoveStack;
        laneDataList[lane].enemyStack.Enqueue(tmp.GetComponent<EnemyController>());
    }

    private void RemoveStack(int type)
    {
        if (laneDataList[type].enemyStack.Count > 0)
        {
            laneDataList[type].enemyStack.Dequeue();
        }
    }

    private EnemyType RandomSpawnFunc(EnemyType origin)
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        if (random <= 0.01f) return EnemyType.BT;
        else if (random <= 0.04f) return EnemyType.Healthy;
        else if (random <= 0.3f) return origin;
        else return EnemyType.Null;
    }

    public EnemyController GetMostFrontEnemyAtLine(EnemyType type)
    {
        if (laneDataList[(int)type].enemyStack.Count > 0)
        {
            return laneDataList[(int)type].enemyStack.Peek();
        }

        return null;
    }
}
