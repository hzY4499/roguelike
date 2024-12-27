using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth; // �������ֵ
    public float currentHealth; // ��ǰ����ֵ
    public Slider healthSlider; // ������Slider���
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ������ֵ
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // ���������������ֵ
            healthSlider.value = currentHealth; // ���õ�ǰ��������ֵ
        }
    }
    void Update()
    {
        // ����������
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
    // �ܵ��˺�
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ����ֵ����
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // ��֤����ֵ��С��0

        // ���������������߼�
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    // ��������
    void Die()
    {
        // ���磬�������ʱ��ӡ��Ϣ��������Ҷ���
        Debug.Log("Player has died!");
        gameObject.SetActive(false);
    }
    // �����Enemy_0����ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����Enemy_0������ֵ��һ
        if (collision.gameObject.CompareTag("Enemy_0"))
        {
            TakeDamage(1f);
        }
    }
}
