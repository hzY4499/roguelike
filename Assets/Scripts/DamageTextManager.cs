using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] private DamageText damageTextPerfab;
    // Start is called before the first frame update
    private void Awake()
    {
        EnemyCollider.onDamageTaken += instantiateDamageText;
    }

    private void OnDestroy()
    {
        EnemyCollider.onDamageTaken -= instantiateDamageText;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void instantiateDamageText(int damage, Vector2 enemyPos)
    {
        Vector3 spawnPosition = enemyPos + Vector2.up * 0.2f;
        DamageText damageTextInstance = Instantiate(damageTextPerfab, spawnPosition, Quaternion.identity, transform);
        damageTextInstance.Animate(damage);
    }
}
