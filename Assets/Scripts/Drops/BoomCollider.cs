using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.scale(gameObject, Vector3.one, 1f).setOnComplete(() => Destroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.GetComponent<Renderer>().enabled) // 确保敌人生成特效不会触发
        {
            collision.gameObject.GetComponent<EnemyCollider>().PassAway();
        }
    }
}
