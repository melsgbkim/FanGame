using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Transform LinePos;
    public Transform LineRot;
    public Transform LineScale;

    public Vector3 FromPos;
    public Vector3 BeforeVec3;
    public bool startBullet = false;

    public float BulletSpeed;
    float LineProgress;

    TDollControl parentTDoll;
    public float lifeTime;

    const float PosY = 1f;

    public void Start()
    {
        BeforeVec3 = transform.localPosition;
        lifeTime = 10f;
    }

    public void FixedUpdate()
    {
        if(startBullet)
        {
            SetDest(BeforeVec3, transform.localPosition);
            BeforeVec3 = transform.localPosition;
        }
    }

    void SetFrom(Vector3 from)
    {
        FromPos = from;
        Vector3 dest = transform.localPosition;

        Vector3 vec = rigidbody.velocity * Time.fixedDeltaTime;
        LinePos.transform.localPosition = vec * 0.5f;
        LineScale.localScale = new Vector3(LineScale.localScale.x, vec.magnitude*0.5f, LineScale.localScale.z);
        LineRot.localRotation = Quaternion.Euler(0, -Mathf.Rad2Deg * (Mathf.Atan2((vec).z, (vec).x)), 0);// * Quaternion.Euler(90, 0, 0);
    }

    void SetDest(Vector2 from, Vector2 dest)
    {
        SetFrom(new Vector3(from.x, PosY, from.y));
    }

    public void SetBullet(Vector2 from, Vector2 dest, float speed)
    {
        BeforeVec3 = transform.localPosition = new Vector3(from.x, PosY, from.y);
        BulletSpeed = speed;
        Vector3 vec = (new Vector3(dest.x, PosY, dest.y) - new Vector3(from.x, PosY, from.y));
        rigidbody.velocity = vec.normalized * BulletSpeed;
        startBullet = true;
    }

    public void SetBullet(Vector3 from, Vector3 dest, float speed)
    {
        SetBullet(new Vector2(from.x, from.z), new Vector2(dest.x, dest.z),speed);
    }
    public void SetParentUnit(TDollControl TDoll)
    {
        parentTDoll = TDoll;
        //parentTDoll
    }


    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
            SetBullet(new Vector2(-50, 0), new Vector2(1,0),50);*/
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0f)
        {
            Destroy(gameObject);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "UnitCollision" && other.transform.parent != null && other.transform.parent.gameObject != parentTDoll.gameObject)
        {
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject);
            print("Delete");
        }
    }
}
