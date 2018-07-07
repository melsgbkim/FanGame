using UnityEngine;
using System.Collections;

public class TDollControl : MonoBehaviour
{
    public string TDollName = "";
    public float MoveSpeed = 10f;
    public TDollAnimation animation;

    Vector3 MoveTarget;
    Rigidbody rigidbody;
    bool nowMoving;

    public void Start()
    {
        MoveTarget = transform.localPosition;
        rigidbody = GetComponent<Rigidbody>();
        nowMoving = false;
    }

    public void FixedUpdate()
    {
        if(nowMoving)FixedMovingUpdate();
    }

    void FixedMovingUpdate()
    {
        Vector3 vec = (MoveTarget - transform.localPosition);
        float moveDistance = MoveSpeed * Time.fixedDeltaTime;
        if (vec.magnitude < moveDistance)
        {
            moveDistance = vec.magnitude;
            nowMoving = false;
        }
        animation.FlipX(vec.x < 0);
        rigidbody.MovePosition(transform.localPosition + vec.normalized * moveDistance);
        print(rigidbody.velocity);
    }

    public void Update()
    {
        if (nowMoving) animation.SetAniamtionName(TDollAnimation.MOVE);
        else animation.SetAniamtionName(TDollAnimation.WAIT);
    }

    public void SetMoveTarget(Vector3 to)
    {
        MoveTarget = new Vector3(to.x, 0f, to.z);
        nowMoving = true;
    }
}