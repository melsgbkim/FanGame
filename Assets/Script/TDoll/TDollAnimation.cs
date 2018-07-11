using UnityEngine;
using System.Collections;

using Spine;

public class TDollAnimation : MonoBehaviour {
    SkeletonAnimation motion;

    public const string MOVE = "move";
    public const string AIM = "attackAim";
    public const string ATTACK = "attack";
    public const string WAIT = "wait";

    // Use this for initialization
    void Start () {
        motion = GetComponent<SkeletonAnimation>();
        motion.AnimationName = "wait";

        motion.state.Start += StartDelegate;
        motion.state.End += EndDelegate;
        motion.state.Event += EventDelegate;
        motion.state.Complete += CompleteDelegate;
    }

    public void StartDelegate(Spine.AnimationState state, int trackIndex)
    {
        
    }
    public void EndDelegate(Spine.AnimationState state, int trackIndex)
    {

    }
    public void EventDelegate(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        if (motion.timeScale == 0) return;
        switch(e.Data.Name)
        {
            case "fire":
                {
                    FireEvent(e);
                    break;
                }
        }
    }
    public void CompleteDelegate(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        CompleteAttack();
    }



    public delegate void SpineEventDelegate(Spine.Event e);
    public SpineEventDelegate FireEvent;

    public delegate void SpineDelegate();
    public SpineDelegate CompleteAttack;

    // Update is called once per frame
    void Update () {
        
    }

    void SetAniNameWithNone(string value)
    {
        motion.AnimationName = "";
        motion.AnimationName = value;
    }

    void SetAniName(string value)
    {
        motion.AnimationName = value;
    }

    public void SetAniamtionName(string value)
    {
        bool aim = false;
        switch (value)
        {
            case AIM: aim = true; value = ATTACK; break;
        }
        
        switch (value)
        {
            case ATTACK: SetAniNameWithNone(value); break;
        }
        
        switch (value)
        {
            case MOVE: SetTimeScale(1); SetLoop(true); break;
            case WAIT: SetTimeScale(1); SetLoop(true); break;
            case ATTACK: SetTimeScale((aim ? 0 : 1)); SetLoop(false); break;
        }

        switch (value)
        {
            case MOVE: SetAniName(value); break;
            case WAIT: SetAniName(value); break;
        }

    }

    public void SetTimeScale(float val)
    {
        motion.timeScale = val;
    }

    public void SetLoop(bool val)
    {
        motion.loop = val;
    }



    public void FlipX(bool flip)
    {
        if (flip)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, 1);
        else
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }
}
