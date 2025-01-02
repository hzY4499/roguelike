using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // Ԥ���Enemy����
    public GameObject[] enemyPrefabs;

    // ���ɵ��˼��ʱ��
    public float spawnInterval = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ���ɵ���
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<GameManager>().isOver) StopAllCoroutines();
    }

    // Э��ÿ��һ��ʱ������һ��Enemy_0
    private IEnumerator SpawnEnemy()
    {
        while (true) // ����ѭ��
        {
            // �ȴ�ָ��ʱ��
            yield return new WaitForSeconds(spawnInterval);
            int randomCount = Random.Range(0, 6);
            // ���ɵ���
            while (randomCount-- >= 0) Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform);
        }
    }
}
