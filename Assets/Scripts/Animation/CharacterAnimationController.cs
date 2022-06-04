using Spine.Unity;
using UnityEngine;

public class CharacterAnimationController : CustomAnimationController
{

    [SpineAnimation, SerializeField] private string animIdle;
    [SpineAnimation, SerializeField] private string animWalk;
    [SpineAnimation, SerializeField] private string animSprint;
    [SpineAnimation, SerializeField] private string animJump;
    [SpineAnimation, SerializeField] private string animAttack;
    [SpineAnimation, SerializeField] private string animBlock;
    [SpineAnimation, SerializeField] private string animHit;
    [SpineAnimation, SerializeField] private string animDead;
    [SpineAnimation, SerializeField] private string animBuffHeal;
    [SpineAnimation, SerializeField] private string animBuffAttackSpeed;
    [SpineAnimation, SerializeField] private string animVictory;
    

    [SpineAnimation, SerializeField] public string currentAnimation;
    
    public void ExecAnimation(CharacterAnimation anim, float timeScale)
    {
        currentAnimation = anim switch
        {
            CharacterAnimation.Idle => animIdle,
            CharacterAnimation.Walk => animWalk,
            CharacterAnimation.Sprint => animSprint,
            CharacterAnimation.Jump => animJump,
            CharacterAnimation.Attack => animAttack,
            CharacterAnimation.Block => animBlock,
            CharacterAnimation.Hit => animHit,
            CharacterAnimation.Dead => animDead,
            CharacterAnimation.BuffHeal => animBuffHeal,
            CharacterAnimation.BuffAttackSpeed => animBuffAttackSpeed,
            CharacterAnimation.Victory => animVictory,
            _ => currentAnimation
        };

        SetAnimation(currentAnimation, false, timeScale);
    }
}

public enum CharacterAnimation
{
    Idle,
    Walk,
    Sprint,
    Jump,
    Attack,
    Block,
    Hit,
    Dead,
    BuffHeal,
    BuffAttackSpeed,
    Victory,
}