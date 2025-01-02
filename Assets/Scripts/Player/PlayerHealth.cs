using TMPro;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth; // 最大生命值
    public float currentHealth; // 当前生命值
    public Slider healthSlider; // 生命条Slider组件
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private ParticleSystem passAwayParticles;

    public float recoveryTimes = 1f;

    [SerializeField] private Animator animator;
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        maxHealth = 30f;
        currentHealth = maxHealth; // 初始化生命值
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // 设置生命条的最大值
            healthSlider.value = currentHealth; // 设置当前生命条的值
            healthText.text = currentHealth.ToString();
        }
    }
    void Update()
    {
        // 更新生命条
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
    // 受到伤害
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 生命值减少
        animator.Play("GetDamage");
        healthText.text = currentHealth.ToString();
        // 这里可以添加死亡逻辑
        if (currentHealth <= 0f)
        {

            Die();
        }
    }
    // 回复血量
    public void Recovery(float recoveryHealth)
    {
        currentHealth += recoveryHealth * recoveryTimes;
        if (currentHealth >= maxHealth) currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }
    // 死亡处理
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
    // 检测与Enemy_0的碰撞并消失
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞的对象是Enemy，生命值减少
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5f);
        }
    }
}
