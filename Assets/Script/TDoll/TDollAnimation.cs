using UnityEngine;
using System.Collections;

using Spine;

public class TDollAnimation : MonoBehaviour {
    SkeletonAnimation motion;

    public const string MOVE = "move";
    public const string ATTACK = "attack";
    public const string WAIT = "wait";

    // Use this for initialization
    void Start () {
        motion = GetComponent<SkeletonAnimation>();
        motion.AnimationName = "wait";
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) motion.AnimationName = "move";
        if (Input.GetKeyDown(KeyCode.W)) motion.AnimationName = "attack";
        if (Input.GetKeyDown(KeyCode.E)) motion.AnimationName = "wait";
    }

    public void SetAniamtionName(string value)
    {
        motion.AnimationName = value;
    }



    public void FlipX(bool flip)
    {
        if (flip)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, 1);
        else
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }
}
