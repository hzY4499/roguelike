using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // Ԥ���Enemy����
    public GameObject[] enemyPrefabs;

    // ���ɵ��˼��ʱ��
    public float spawnInterval = 3.0f;
    private float timer;
    private int maxNum;

    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ���ɵ���
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

    // Э��ÿ��һ��ʱ������һ��Enemy
    private IEnumerator SpawnEnemy()
    {
        while (true) // ����ѭ��
        {
            // �ȴ�ָ��ʱ��
            yield return new WaitForSeconds(spawnInterval);
            // �����������
            int randomCount = Random.Range(0, maxNum);
            // ���ɵ���
            while (randomCount-- >= 0) Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform);
        }
    }
}
