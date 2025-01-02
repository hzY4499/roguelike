using TMPro;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth; // �������ֵ
    public float currentHealth; // ��ǰ����ֵ
    public Slider healthSlider; // ������Slider���
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private ParticleSystem passAwayParticles;

    public float recoveryTimes = 1f;

    [SerializeField] private Animator animator;
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        maxHealth = 30f;
        currentHealth = maxHealth; // ��ʼ������ֵ
        if (healthSlider != null)
        {
            healthSlider.maxValue = (int) maxHealth; // ���������������ֵ
            healthSlider.value = (int) currentHealth; // ���õ�ǰ��������ֵ
            healthText.text = currentHealth.ToString();
        }
    }
    void Update()
    {
        // ����������
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
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
    // �ظ�Ѫ��
    public void Recovery(float recoveryHealth)
    {
        currentHealth += recoveryHealth * recoveryTimes;
        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }
    // ��������
    void Die()
    {
        currentHealth = 0;
        healthSlider.value = 0;
        passAwayParticles.transform.parent = null;
        passAwayParticles.Play();
        Destroy(gameObject);
        gameManager.GameOver();
        //gameObject.SetActive(false);
    }
    // �����Enemy_0����ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����Enemy������ֵ����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5f);
        }
    }
}
