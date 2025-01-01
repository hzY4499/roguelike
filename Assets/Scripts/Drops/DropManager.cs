using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private ExpBall expBallPerfab;

    private void Awake()
    {
        EnemyCollider.onPassedAway += EnemyPassedAwayCallback;
    }

    private void OnDestroy()
    {
        EnemyCollider.onPassedAway -= EnemyPassedAwayCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPassedAwayCallback(Vector2 enemyPosition)
    {
        Instantiate(expBallPerfab, enemyPosition, Quaternion.identity, transform);
    }
}
