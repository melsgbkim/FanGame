using UnityEngine;
using System.Collections;
using System;

public class UIOrderSlot : MonoBehaviour
{
    UIOrderInfo myInfo;
    public MeshRenderer renderer;
    public KeyCode thisKey;

    float PushScaleProgress = 0f;
    public float PushScaleSpeed = 20f;

    string _State;
    public string State
    {
        set
        {
            if (myInfo == null) return;
            switch (value)
            {
                case "ON":  renderer.material = myInfo.On; break;
                case "OFF": renderer.material = myInfo.Off; break;
                case "":    renderer.material = myInfo.Empty; break;
                default: throw new Exception("UIOrderSlot SetState value is unknown. value : " + value);
            }
            _State = value;
        }

        get
        {
            return _State;
        }
    }
    public void SetInfoAndState(UIOrderInfo info,string value)
    {
        myInfo = info;
        State = value;
    }
    public void SetInfo(UIOrderInfo info)
    {
        myInfo = info;
    }
    // Use this for initialization
    void Start()
    {
        State = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(thisKey))  PushScaleProgress += Time.deltaTime * PushScaleSpeed;
        else                        PushScaleProgress -= Time.deltaTime * PushScaleSpeed;
        if (PushScaleProgress < 0f) PushScaleProgress = 0f;
        if (PushScaleProgress > 1f) PushScaleProgress = 1f;
        transform.localScale = new Vector3(0.3f, 0.3f, 1f) - new Vector3(0.05f, 0.05f, 0f) * PushScaleProgress;

    }
}
