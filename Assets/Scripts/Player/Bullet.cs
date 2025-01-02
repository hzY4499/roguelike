using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float aliveTime;
    [SerializeField] private AudioClip hitSound;  // 播放的音效文件
    private Player player;
    private AudioSource audioSource;  // 用于播放音效的AudioSource
    // Start is called before the first frame update
    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
        audioSource = GetComponent<AudioSource>();  // 获取当前子弹的AudioSource组件
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
        // 检查碰撞对象是否是墙体
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 销毁子弹
            Destroy(gameObject);
        }//与敌人碰撞
        if (collision.gameObject.CompareTag("Enemy") && collision.GetComponent<Renderer>().enabled) // 确保敌人生成特效不会触发
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
