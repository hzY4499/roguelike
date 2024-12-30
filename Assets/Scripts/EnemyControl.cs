using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    // Ԥ���Enemy_0����
    public GameObject enemy0Prefab;

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
            // ���ɵ���
            Instantiate(enemy0Prefab);
        }
    }
}
