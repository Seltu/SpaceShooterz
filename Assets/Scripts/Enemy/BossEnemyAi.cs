using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAi : EnemyAi
{
    [SerializeField] private int bossCycle = 1;
    [SerializeField] private List<BossStep> bossSteps;
    [SerializeField] private Transform shootPointsParent;
    protected int _bossStep = -1;
    private float _bossTimer = 1;
    private bool _summon = true;
    protected override void Update()
    {
        if (_bossTimer < 1)
        {
            _bossTimer += Time.deltaTime / 15;
        }
        else
        {
            if (_bossStep < bossCycle)
            {
                _bossStep += 1;
            }
            else
            {
                _bossStep = 0;
            }

            _bossTimer = 0;
            OnStep();
        }
        base.Update();
    }
    protected virtual void OnStep()
    {
        _summon = true;
        foreach(Transform shootPoint in shootPointsParent)
        {
            shootPoint.gameObject.SetActive(false);
        }
        foreach(GameObject shootPoint in bossSteps[_bossStep].stepShooters)
        {
            shootPoint.SetActive(true);
        }
    }

    public Round GetSummons()
    {
        return bossSteps[_bossStep].stepSummons;
    }
}
