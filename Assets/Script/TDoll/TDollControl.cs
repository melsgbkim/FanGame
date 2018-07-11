using UnityEngine;
using System.Collections;

public class TDollControl : MonoBehaviour
{
    public string TDollName = "";
    public float MoveSpeed = 10f;
    public TDollAnimation animation;
    public MoveFlag MoveFlagPrefab;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    Vector3 MoveTarget;
    Vector3 AimTarget;
    Vector3 ShotTarget;

    MoveFlag MoveFlagObj = null;
    Rigidbody rigidbody;
    bool nowMoving = false;
    bool nowAttackMotion = false;

    public void Start()
    {
        MoveTarget = transform.localPosition;
        rigidbody = GetComponent<Rigidbody>();

        animation.FireEvent = SpineEventFire;
        animation.CompleteAttack = SpineCompleteAttack;
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
        if (nowMoving)
        {
            animation.FlipX(vec.x < 0);
            rigidbody.MovePosition(transform.localPosition + vec.normalized * moveDistance);
        }
    }

    public void Update()
    {
        if (nowMoving)
        {
            animation.SetAniamtionName(TDollAnimation.MOVE);
            if (MoveFlagObj == null)
            {
                MoveFlagObj = Instantiate(MoveFlagPrefab) as MoveFlag;
                MoveFlagObj.SetDest(transform.localPosition, MoveTarget);
            }
            else
            {
                MoveFlagObj.SetDest(transform.localPosition, MoveTarget);
            }
        }
        else if(nowAttackMotion)
        {
            animation.FlipX((AimTarget.x - transform.localPosition.x) < 0);
        }
        else
        {
            animation.SetAniamtionName(TDollAnimation.WAIT);
            DestroyMoveFlag();
        }
    }

    void DestroyMoveFlag()
    {
        if (MoveFlagObj != null)
        {
            Destroy(MoveFlagObj.gameObject);
            MoveFlagObj = null;
        }
    }

    public void SetMoveTarget(Vector3 to)
    {
        MoveTarget = new Vector3(to.x, 0f, to.z);
        nowMoving = true;
    }

    public void AimTo(Vector3 to)
    {
        if(nowMoving)
            MoveTarget = transform.localPosition;
        AimTarget = to;
        nowAttackMotion = true;
        DestroyMoveFlag();
        animation.SetAniamtionName(TDollAnimation.AIM);
    }

    public void ShotTo(Vector3 to)
    {
        ShotTarget = to;
        animation.SetAniamtionName(TDollAnimation.ATTACK);
        //animation.SetTimeScale(1);
    }

    public void SpineEventFire(Spine.Event e)
    {
        Bullet obj = (Instantiate(bulletPrefab) as GameObject).GetComponent<Bullet>();
        obj.SetBullet(transform.localPosition, ShotTarget, bulletSpeed);
        obj.SetParentUnit(this);
    }

    public void SpineCompleteAttack()
    {
        nowAttackMotion = false;
    }
}