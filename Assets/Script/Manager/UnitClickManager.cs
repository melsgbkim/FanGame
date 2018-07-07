using UnityEngine;
using System.Collections;

public class UnitClickManager : MonoBehaviour
{
    public Transform UnitSelectedImage;
    TDollControl TDollSelected = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickUpdate();
        if (UnitSelectedImage != null && TDollSelected != null)
        {
            UnitSelectedImage.localPosition = TDollSelected.transform.localPosition;
            UnitSelectedImage.gameObject.SetActive(true);
        }
        else if (UnitSelectedImage != null && TDollSelected == null)
        {
            UnitSelectedImage.gameObject.SetActive(false);
        }
    }

    void ClickUpdate()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (ClickMouseCheckUnitClickableLayer()) return;
           
        }

        if (Input.GetMouseButton(0))
        {
            if (ClickTerrian()) return;
        }
    }

    bool ClickTerrian()
    {
        string name = "Terrian";
        int layerMask = 1 << LayerMask.NameToLayer(name);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            print(hit.point);
            if(TDollSelected != null)
            {
                TDollSelected.SetMoveTarget(hit.point);
                return true;
            }
        }
        return false;
    }

    bool ClickMouseCheckUnitClickableLayer()
    {
        string name = "UnitClickable";
        int layerMask = 1 << LayerMask.NameToLayer(name);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return ClickUpdate(TDollSelected, hit);
        }
        return false;
    }

    bool ClickUpdate(TDollControl before, RaycastHit hit)
    { 
        switch(hit.collider.tag )
        {
            case "UnitClickable":
                {
                    TDollControl TDoll = hit.collider.transform.parent.GetComponent<TDollControl>();
                    if (TDoll == TDollSelected)
                        return false;
                    TDollSelected = TDoll;
                    print(TDoll.TDollName);
                }
                return true;
        }

        return false;
    }
}
