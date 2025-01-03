using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // 预设的Enemy对象
    public GameObject[] enemyPrefabs;

    // 生成敌人间隔时间
    public float spawnInterval = 3.0f;
    private float timer;
    private int maxNum;

    // Start is called before the first frame update
    void Start()
    {
        // 开始生成敌人
        timer = 0f;
        maxNum = 3;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 50f)
        {
            maxNum++;
            spawnInterval -= 0.2f;
            timer = 0f;
        }
        if (FindAnyObjectByType<GameManager>().isOver) StopAllCoroutines();
    }

    // 协程每隔一段时间生成一个Enemy
    private IEnumerator SpawnEnemy()
    {
        while (true) // 无限循环
        {
            // 等待指定时间
            yield return new WaitForSeconds(spawnInterval);
            // 生成数量随机
            int randomCount = Random.Range(0, maxNum);
            // 生成敌人
            while (randomCount-- >= 0) Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform);
        }
    }
}
