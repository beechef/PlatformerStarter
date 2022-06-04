using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class CustomAnimationController : MonoBehaviour
{
    public new SkeletonAnimation animation;

    [SpineAnimation, SerializeField] protected List<string> immediateAnimations;

    private bool IsSamePreAnimation(string animationName)
    {
        TrackEntry track = animation.state.GetCurrent(0);
        if (track == null) return false;
        return track.Animation.Name.Equals(animationName) && !track.IsComplete;
    }

    private bool IsImmediateAnimation(string animationName)
    {
        foreach (var immediateAnimation in immediateAnimations)
        {
            if (immediateAnimation.Equals(animationName)) return true;
        }

        return false;
    }

    private bool IsCurrentAnimationImmediate()
    {
        TrackEntry track = animation.state.GetCurrent(0);
        if (track == null) return false;
        return IsImmediateAnimation(track.Animation.Name) && !track.IsComplete;
    }

    protected void SetAnimation(string animationName, bool loop, float timeScale)
    {
        if (!IsImmediateAnimation(animationName))
        {
            if (IsSamePreAnimation(animationName)) return;
            // if (IsCurrentAnimationImmediate()) return;
        }

        if (IsImmediateAnimation(animationName))
        {
            // animation.state.SetEmptyAnimation(1, 0);
            TrackEntry track = animation.state.SetAnimation(1, animationName, loop);
            track.MixDuration = 0;
            track.TimeScale = timeScale;
            track.Complete += entry =>
            {
                if (!track.Animation.Name.Equals("dead"))
                {
                    animation.state.SetEmptyAnimation(1, 0);
                }
            };
        }
        else
            animation.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
    }

    public void AddEvents(List<AnimationState.TrackEntryEventDelegate> events)
    {
        foreach (var e in events)
        {
            animation.state.Event += e;
        }
    }
}