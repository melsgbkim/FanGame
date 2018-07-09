using UnityEngine;
using System.Collections;

public class AttackLine : MonoBehaviour
{
    public MeshRenderer Line;
    public Vector3 FromPos;

    public float LineSpeed;
    float LineProgress;

    public void SetFrom(Vector3 from)
    {
        FromPos = from;
        Vector3 dest = transform.localPosition;

        Vector3 vec = (from - dest);
        Line.material.mainTextureScale = new Vector2(vec.magnitude, 1f);
        Line.transform.localPosition = vec * 0.5f;
        Line.transform.localScale = new Vector3(vec.magnitude, Line.transform.localScale.y, Line.transform.localScale.z);
        Line.transform.localRotation = Quaternion.Euler(90, -Mathf.Rad2Deg * (Mathf.Atan2((vec).z, (vec).x)), 0);// * Quaternion.Euler(90, 0, 0);
        transform.localPosition = new Vector3(transform.localPosition.x, 1f, transform.localPosition.z);
    }

    public void SetDest(Vector3 from, Vector3 dest)
    {
        dest = new Vector3(dest.x, 0f, dest.z);
        transform.localPosition = dest;
        SetFrom(from);
       
    }

    // Update is called once per frame
    void Update()
    {
        LineProgress += Time.deltaTime * LineSpeed;
        if (LineProgress > 1f)
            LineProgress -= 1f;
        Line.material.mainTextureOffset = new Vector2(LineProgress, 0f);
    }
}
