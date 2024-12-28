using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // 预设的Enemy_0对象
    public GameObject enemy0Prefab;

    // 生成敌人间隔时间
    public float spawnInterval = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 开始生成敌人
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 协程每隔一段时间生成一个Enemy_0
    private IEnumerator SpawnEnemy()
    {
        while (true) // 无限循环
        {
            // 生成敌人
            Instantiate(enemy0Prefab);

            // 等待指定时间
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
