using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float aliveTime;
    [SerializeField] private AudioClip hitSound;  // ���ŵ���Ч�ļ�
    private Player player;
    private AudioSource audioSource;  // ���ڲ�����Ч��AudioSource
    // Start is called before the first frame update
    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
        audioSource = GetComponent<AudioSource>();  // ��ȡ��ǰ�ӵ���AudioSource���
    }
    void Start()
    {
        
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }
    public void SetAliveTime(float newAliveTime)
    {
        aliveTime = newAliveTime;
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
        }//�������ײ
        if (collision.gameObject.CompareTag("Enemy") && collision.GetComponent<Renderer>().enabled) // ȷ������������Ч���ᴥ��
        {
            audioSource.transform.parent = null;
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            Destroy(gameObject);

            int curDamage = GetDamage(out bool isCriticalHit);
            collision.gameObject.GetComponent<EnemyCollider>().SetDestroyReason(EnemyCollider.DestroyReason.PlayerAttack);
            collision.gameObject.GetComponent<EnemyCollider>().TakeDamage(curDamage, isCriticalHit);
        }
    }

    private int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if (player == null) return (int)damage;

        if (Random.Range(0,101) <= player.criticalRate)
        {
            isCriticalHit = true;
            return (int)(damage * player.criticalDamage);
        }

        return (int)damage;
    }
}
