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
    MoveFlag MoveFlagObj = null;
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
        else
        {
            animation.SetAniamtionName(TDollAnimation.WAIT);
            if (MoveFlagObj != null)
            {
                Destroy(MoveFlagObj.gameObject);
                MoveFlagObj = null;
            }
        }
    }

    public void SetMoveTarget(Vector3 to)
    {
        MoveTarget = new Vector3(to.x, 0f, to.z);
        nowMoving = true;
    }

    public void ShotTo(Vector3 to)
    {
        Bullet obj = (Instantiate(bulletPrefab) as GameObject).GetComponent<Bullet>();
        obj.SetBullet(transform.localPosition, to, bulletSpeed);
        obj.SetParentUnit(this);
    }
}