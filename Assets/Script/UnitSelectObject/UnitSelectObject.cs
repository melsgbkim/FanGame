using UnityEngine;
using System.Collections;

public class UnitSelectObject : MonoBehaviour
{
    public Transform Head;

    public float FloatingTime;
    public float FloatingLength;
    float FloatingProgressTime = 0f;

    // Update is called once per frame
    void Update()
    {
        FloatingProgressTime += Time.deltaTime;
        if (FloatingProgressTime > FloatingTime)
            FloatingProgressTime -= FloatingTime;
        Head.localPosition = new Vector3(Head.localPosition.x, (Mathf.Sin(FloatingProgressTime / FloatingTime * 2 * Mathf.PI) + 1) * 0.5f * FloatingLength, Head.localPosition.z);
    }
}
