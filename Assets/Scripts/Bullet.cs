using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float aliveTime;
    private Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveTime < 0) Destroy(gameObject);
        aliveTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�����Ƿ���ǽ��
        if (collision.gameObject.CompareTag("Wall"))
        {
            // �����ӵ�
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && collision.GetComponent<Renderer>().enabled) // ȷ������������Ч���ᴥ��
        {
            Destroy(gameObject);
            int curDamage = GetDamage(out bool isCriticalHit);
            collision.gameObject.GetComponent<EnemyCollider>().SetDestroyReason(EnemyCollider.DestroyReason.PlayerAttack);
            collision.gameObject.GetComponent<EnemyCollider>().TakeDamage(curDamage, isCriticalHit);
        }
    }

    private int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if (player == null) return damage;

        if (Random.Range(0,101) <= player.criticalRate)
        {
            isCriticalHit = true;
            return (int)(damage * player.criticalDamage);
        }

        return damage;
    }
}
