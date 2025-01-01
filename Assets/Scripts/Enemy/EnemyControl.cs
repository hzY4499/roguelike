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

    }

    // Э��ÿ��һ��ʱ������һ��Enemy_0
    private IEnumerator SpawnEnemy()
    {
        while (true) // ����ѭ��
        {
            // �ȴ�ָ��ʱ��
            yield return new WaitForSeconds(spawnInterval);
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            // ���ɵ���
            Instantiate(enemyPrefabs[randomIndex], transform);
        }
    }
}
