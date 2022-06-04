using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using AnimationState = Spine.AnimationState;
using Event = Spine.Event;

[RequireComponent(typeof(CharacterAnimationController))]
public class CharacterCombatController : CombatController
{
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected LayerMask arrowMask;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Transform blockPoint;
    private CharacterController characterController;
    private Rigidbody2D rig;
    private new CharacterAnimationController animation;
    private float timeScale = 1;

    private void Start()
    {
        animation = GetComponent<CharacterAnimationController>();
        statsController = GetComponent<StatsController>();
        rig = GetComponent<Rigidbody2D>();
        characterController = GetComponent<CharacterController>();
        coll = GetComponent<Collider2D>();
        ColliderDictionary.AddCombatController(coll, this);

        InitialEvents();
    }

    private void InitialEvents()
    {
        animation.AddEvents(new List<AnimationState.TrackEntryEventDelegate>()
        {
            AttackHandler,
            BlockHandler
        });
    }

    protected override void HitEnemy()
    {
        base.HitEnemy();
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, stats.AttackRange, enemyMask);
        if (enemies == null) return;
        foreach (var enemy in enemies)
        {
            ColliderDictionary.GetCombatController(enemy).TakeDamage(stats);
            // enemy.GetComponent<CombatController>().TakeDamage(stats);
        }
    }

    protected override void Block()
    {
        base.Block();
        Collider2D[] arrows = Physics2D.OverlapCircleAll(blockPoint.position, stats.BlockRange, arrowMask);
        if (arrows == null) return;
        foreach (var arrow in arrows)
        {
            ColliderDictionary.GetArrow(arrow).DestroyObject();
        }
    }

    public override void TakeDamage(Stats enemyStats)
    {
        base.TakeDamage(enemyStats);
        animation.ExecAnimation(stats.Health <= 0 ? CharacterAnimation.Dead : CharacterAnimation.Hit, timeScale);
        if (stats.Health <= 0)
        {
            StartCoroutine(DeadHandler());
        }
    }

    private void AttackHandler(TrackEntry trackEntry, Event e)
    {
        if (e.Data.Name.Equals("sword_attack"))
        {
            HitEnemy();
        }
    }

    private void BlockHandler(TrackEntry trackEntry, Event e)
    {
        if (e.Data.Name.Equals("shield_attack"))
        {
            Block();
        }
    }

    private IEnumerator DeadHandler()
    {
        characterController.enabled = false;
        // transform.localRotation = quaternion.Euler(0, 0, 90);
        rig.velocity = Vector2.zero;
        rig.isKinematic = true;
        coll.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public override void Dead()
    {
        animation.ExecAnimation(CharacterAnimation.Dead, 1);
        StartCoroutine(DeadHandler());
    }

    public override void Win()
    {
        Debug.Log("Victory!!!");
        animation.ExecAnimation(CharacterAnimation.Victory, 1.5f);
    }
    private void OnDestroy()
    {
        ColliderDictionary.RemoveCombatController(coll);
    }
}