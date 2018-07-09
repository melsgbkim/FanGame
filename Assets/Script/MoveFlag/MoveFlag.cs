using UnityEngine;
using System.Collections;

public class MoveFlag : MonoBehaviour
{
    public MeshRenderer Line;
    public Transform Flag;

    public Vector3 FromPos;

    public float LineSpeed;
    float LineProgress;

    public float FloatingTime;
    public float FloatingLength;
    float FloatingProgressTime = 0f;
    // Use this for initialization
    /*void Start()
    {
        //Line.material.mainTextureOffset = new Vector2(10f, 1f);

        /*Line.material.mainTextureScale = new Vector2(3f, 1f);
        Line.material.mainTextureOffset = new Vector2(0.5f, 0f);
    }*/

    public void SetFrom(Vector3 from)
    {
        FromPos = from;
        Vector3 dest = transform.localPosition;

        Vector3 vec = (from - dest);
        Line.material.mainTextureScale = new Vector2(vec.magnitude, 1f);
        Line.transform.localPosition = vec * 0.5f;
        Line.transform.localScale = new Vector3(vec.magnitude, Line.transform.localScale.y, Line.transform.localScale.z);
        Line.transform.localRotation = Quaternion.Euler(90, -Mathf.Rad2Deg * (Mathf.Atan2((vec).z, (vec).x)),0);// * Quaternion.Euler(90, 0, 0);
    }

    public void SetDest(Vector3 from, Vector3 dest)
    {
        transform.localPosition = dest;
        SetFrom(from);
    }

    // Update is called once per frame
    void Update()
    {
        FloatingProgressTime += Time.deltaTime;
        if (FloatingProgressTime > FloatingTime)
            FloatingProgressTime -= FloatingTime;
        Flag.localPosition = new Vector3(Flag.localPosition.x,(Mathf.Sin(FloatingProgressTime / FloatingTime * 2 * Mathf.PI) + 1) * 0.5f * FloatingLength, Flag.localPosition.z);

        //        Line.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        LineProgress += Time.deltaTime * LineSpeed;
        if (LineProgress > 1f)
            LineProgress -= 1f;
        Line.material.mainTextureOffset = new Vector2(LineProgress, 0f);
    }
}
