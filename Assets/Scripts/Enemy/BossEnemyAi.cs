using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAi : EnemyAi
{
    [SerializeField] private List<BossStep> bossSteps;
    [SerializeField] private Transform shootPointsParent;
    protected int _bossStep = 0;
    private float _bossTimer = 1;
    protected override void Update()
    {
        if (_bossTimer > 0)
        {
            _bossTimer -= Time.deltaTime;
        }
        else
        {
            _bossTimer = bossSteps[_bossStep].stepDuration;
            OnStep();
            if (_bossStep < bossSteps.Count-1)
            {
                _bossStep += 1;
            }
            else
            {
                _bossStep = 0;
            }
        }
        base.Update();
    }
    protected virtual void OnStep()
    {
        if (bossSteps[_bossStep].stepSummons != null)
            //GameEventsManager.SummonRoundTrigger(bossSteps[_bossStep].stepSummons);
        foreach(Transform shootPoint in shootPointsParent)
        {
            shootPoint.gameObject.SetActive(false);
        }
        foreach(GameObject shootPoint in bossSteps[_bossStep].stepShooters)
        {
            shootPoint.SetActive(true);
        }
    }
}
