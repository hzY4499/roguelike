using TMPro;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth; // �������ֵ
    public float currentHealth; // ��ǰ����ֵ
    public Slider healthSlider; // ������Slider���
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Animator animator;
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ������ֵ
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // ���������������ֵ
            healthSlider.value = currentHealth; // ���õ�ǰ��������ֵ
            healthText.text = currentHealth.ToString();
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
        animator.Play("GetDamage");
        healthText.text = currentHealth.ToString();
        // ���������������߼�
        if (currentHealth <= 0f)
        {

            Die();
        }
    }
    // ��������
    void Die()
    {
        currentHealth = 0;
        healthSlider.value = 0;
        // ���磬�������ʱ��ӡ��Ϣ��������Ҷ���
        Debug.Log("Player has died!");
        gameObject.SetActive(false);
    }
    // �����Enemy_0����ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����Enemy_0������ֵ��һ
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1f);
        }
    }
}
