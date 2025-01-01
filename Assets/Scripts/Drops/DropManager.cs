using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [SerializeField] private ExpBall expBallPerfab;
    [SerializeField] private HealDrops healDropsPerfab;

    private ObjectPool<ExpBall> expBallPool;
    private ObjectPool<HealDrops> healDropsPool;

    private void Awake()
    {
        EnemyCollider.onPassedAway += EnemyPassedAwayCallback;
        ExpBall.onCollected += ReleaseExpBall;
        HealDrops.onCollected += ReleaseHealDrops;
    }

    private void OnDestroy()
    {
        EnemyCollider.onPassedAway -= EnemyPassedAwayCallback;
        ExpBall.onCollected -= ReleaseExpBall;
        HealDrops.onCollected -= ReleaseHealDrops;
    }
    // Start is called before the first frame update
    void Start()
    {
        expBallPool = new ObjectPool<ExpBall>(ExpBallCreateFunction, ExpBallActionOnGet, ExpBallActionOnRelease, ExpBallActionOnDestroy);
        healDropsPool = new ObjectPool<HealDrops>(HealDropsCreateFunction, HealDropsActionOnGet, HealDropsActionOnRelease, HealDropsActionOnDestroy);
    }

    private ExpBall ExpBallCreateFunction() => Instantiate(expBallPerfab, transform);
    private void ExpBallActionOnGet(ExpBall expBall) => expBall.gameObject.SetActive(true);
    private void ExpBallActionOnRelease(ExpBall expBall) => expBall.gameObject.SetActive(false);
    private void ExpBallActionOnDestroy(ExpBall expBall) => Destroy(expBall.gameObject);
    
    private HealDrops HealDropsCreateFunction() => Instantiate(healDropsPerfab, transform);
    private void HealDropsActionOnGet(HealDrops healDrops) => healDrops.gameObject.SetActive(true);
    private void HealDropsActionOnRelease(HealDrops healDrops) => healDrops.gameObject.SetActive(false);
    private void HealDropsActionOnDestroy(HealDrops healDrops) => Destroy(healDrops.gameObject);

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPassedAwayCallback(Vector2 enemyPosition)
    {
        bool shouldSpawnDrops = Random.Range(0, 101) <= 20;
        DropItems drops = shouldSpawnDrops ? healDropsPool.Get() : expBallPool.Get();

        Instantiate(drops, enemyPosition, Quaternion.identity, transform);
    }

    private void ReleaseExpBall(ExpBall expBall) => expBallPool.Release(expBall);
    private void ReleaseHealDrops(HealDrops healDrops) => healDropsPool.Release(healDrops);
}
