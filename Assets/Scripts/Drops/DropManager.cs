using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [SerializeField] private ExpBall expBallPerfab;
    [SerializeField] private HealDrops healDropsPerfab;
    [SerializeField] private BoomDrops boomDropsPerfab;

    private ObjectPool<ExpBall> expBallPool;
    private ObjectPool<HealDrops> healDropsPool;
    private ObjectPool<BoomDrops> boomDropsPool;

    private void Awake()
    {
        EnemyCollider.onPassedAway += EnemyPassedAwayCallback;
        ExpBall.onCollected += ReleaseExpBall;
        HealDrops.onCollected += ReleaseHealDrops;
        BoomDrops.onCollected += ReleaseBoomDrops;
    }

    private void OnDestroy()
    {
        EnemyCollider.onPassedAway -= EnemyPassedAwayCallback;
        ExpBall.onCollected -= ReleaseExpBall;
        HealDrops.onCollected -= ReleaseHealDrops;
        BoomDrops.onCollected -= ReleaseBoomDrops;
    }
    // Start is called before the first frame update
    void Start()
    {
        expBallPool = new ObjectPool<ExpBall>(ExpBallCreateFunction, ExpBallActionOnGet, ExpBallActionOnRelease, ExpBallActionOnDestroy);
        healDropsPool = new ObjectPool<HealDrops>(HealDropsCreateFunction, HealDropsActionOnGet, HealDropsActionOnRelease, HealDropsActionOnDestroy);
        boomDropsPool = new ObjectPool<BoomDrops>(BoomDropsCreateFunction, BoomDropsActionOnGet, BoomDropsActionOnRelease, BoomDropsActionOnDestroy);
    }

    private ExpBall ExpBallCreateFunction() => Instantiate(expBallPerfab, transform);
    private void ExpBallActionOnGet(ExpBall expBall) => expBall.gameObject.SetActive(true);
    private void ExpBallActionOnRelease(ExpBall expBall) => expBall.gameObject.SetActive(false);
    private void ExpBallActionOnDestroy(ExpBall expBall) => Destroy(expBall.gameObject);
    
    private HealDrops HealDropsCreateFunction() => Instantiate(healDropsPerfab, transform);
    private void HealDropsActionOnGet(HealDrops healDrops) => healDrops.gameObject.SetActive(true);
    private void HealDropsActionOnRelease(HealDrops healDrops) => healDrops.gameObject.SetActive(false);
    private void HealDropsActionOnDestroy(HealDrops healDrops) => Destroy(healDrops.gameObject);

    private BoomDrops BoomDropsCreateFunction() => Instantiate(boomDropsPerfab, transform);
    private void BoomDropsActionOnGet(BoomDrops boomDrops) => boomDrops.gameObject.SetActive(true);
    private void BoomDropsActionOnRelease(BoomDrops boomDrops) => boomDrops.gameObject.SetActive(false);
    private void BoomDropsActionOnDestroy(BoomDrops boomDrops) => Destroy(boomDrops.gameObject);

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPassedAwayCallback(Vector2 enemyPosition)
    {
        bool shouldSpawnDrops = Random.Range(0, 101) <= 20;
        DropItems drops;
        if (shouldSpawnDrops)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    drops = healDropsPool.Get();
                    drops.transform.position = enemyPosition;
                    break;
                case 1:
                    drops = boomDropsPool.Get();
                    drops.transform.position = enemyPosition;
                    break;
            }
        }
        drops = expBallPool.Get();
        drops.transform.position = enemyPosition;
    }

    private void ReleaseExpBall(ExpBall expBall) => expBallPool.Release(expBall);
    private void ReleaseHealDrops(HealDrops healDrops) => healDropsPool.Release(healDrops);
    private void ReleaseBoomDrops(BoomDrops boomDrops)
    {
        boomDrops.boomRenderer.enabled = false;
        LeanTween.scale(boomDrops.boomCollider, 2 * Vector3.one, 1f)
                .setOnComplete(() =>
                {
                    // 重置缩放
                    boomDrops.boomCollider.transform.localScale = new Vector3(0, 0, 1);
                    boomDrops.boomRenderer.enabled = true;
                    // 释放对象回池
                    boomDropsPool.Release(boomDrops);
                });
    }
}
