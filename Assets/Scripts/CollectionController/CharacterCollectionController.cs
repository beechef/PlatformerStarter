using System.Collections.Generic;
using Spine;
using UnityEngine;
using AnimationState = Spine.AnimationState;

[RequireComponent(typeof(CharacterAnimationController))]
public class CharacterCollectionController : CollectionController
{
    private StatsController statsController;
    private new CharacterAnimationController animation;
    private float healthValue, attackSpeedValue;

    private void Start()
    {
        statsController = GetComponent<StatsController>();
        animation = GetComponent<CharacterAnimationController>();
        InitialEvents();
    }

    private void InitialEvents()
    {
        animation.AddEvents(new List<AnimationState.TrackEntryEventDelegate>()
        {
            HealHandler,
            AttackSpeedHandler
        });
    }

    private void HealHandler(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name.Equals("buff_2"))
        {
            statsController.Heal(healthValue);
        }
    }

    private void AttackSpeedHandler(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name.Equals("buff_1"))
        {
            statsController.AttackSpeed(attackSpeedValue);
        }
    }

    public override void Heal(float health)
    {
        base.Heal(health);
        healthValue = health;
        animation.ExecAnimation(CharacterAnimation.BuffHeal, 1);
    }

    public override void AttackSpeed(float attackSpeed)
    {
        base.AttackSpeed(attackSpeed);
        attackSpeedValue = attackSpeed;
        animation.ExecAnimation(CharacterAnimation.BuffAttackSpeed, 1);
    }
}