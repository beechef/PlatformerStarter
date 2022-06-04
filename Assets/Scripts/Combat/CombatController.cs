using System;
using UnityEngine;

[RequireComponent(typeof(StatsController))]
public class CombatController : MonoBehaviour
{
    protected StatsController statsController;

    protected Collider2D coll;

    protected Stats stats => statsController.GetStats();


    private void Start()
    {
        coll = GetComponent<Collider2D>();
        statsController = GetComponent<StatsController>();
        ColliderDictionary.AddCombatController(coll, this);
    }

    protected virtual void HitEnemy()
    {
  
    }

    protected virtual void Block()
    {

    }
    public virtual void TakeDamage(Stats enemyStats)
    {
        statsController.Hit(enemyStats);
    }

    public virtual void Dead()
    {
        
    }

    public virtual void Win()
    {
    }

    private void OnDestroy()
    {
        ColliderDictionary.RemoveCombatController(coll);
    }
}