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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
