using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIOrderWindow : MonoBehaviour
{
    public List<UIOrderInfo> OrderInfoList;
    public List<string> OrderSlotList;
    public List<UIOrderKeyInfo> OrderSlotKeyList;
    
    UIOrderSlot[] ChildArr;
    // Use this for initialization
    void Start()
    {
        ChildArr = transform.GetComponentsInChildren<UIOrderSlot>();
        int i = 0;
        foreach(UIOrderKeyInfo keyInfo in OrderSlotKeyList)
        {
            if (i >= ChildArr.Length)
                break;
            foreach (UIOrderInfo info in OrderInfoList)
            {
                if(info.name == keyInfo.name)
                {
                    ChildArr[i].SetInfoAndState(info,"ON");
                    ChildArr[i].thisKey = keyInfo.key;
                    i++;
                    break;
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
