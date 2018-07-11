using UnityEngine;
using System.Collections;

public class UnitControlManager : MonoBehaviour
{
    public Transform UnitSelectedImage;
    public AttackLine attLine;
    TDollControl TDollSelected = null;
    string nowState = "";
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

        if(nowState == "AttackWait" && TDollSelected != null)
        {
            UpdateAttackWait();
        }
        else
        {
            attLine.gameObject.SetActive(false);
        }
    }

    void ClickUpdate()
    {
        switch (nowState)
        {
            case "ClickUnit":
                {
                    if (Input.GetMouseButton(0) == false)   nowState = "";
                    else                                    return;
                }
                break;
            case "ClickTerrian":
                {
                    if (Input.GetMouseButton(0) == false) nowState = "";
                }
                break;
            case "AttackWait":
                {
                    Vector3 pos;
                    if (GetTerrianClickPoint(out pos) == false) return;
                    if (TDollSelected != null) { TDollSelected.AimTo(pos); }
                    if (Input.GetMouseButtonDown(0)) { nowState = "Attacked"; TDollSelected.ShotTo(pos); return; }
                }
                break;
            case "Attacked":
                {
                    if (Input.GetMouseButton(0) == false) nowState = "";
                }
                return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (ClickMouseCheckUnitClickableLayer()) { nowState = "ClickUnit"; return; }
        }
        else if (Input.GetMouseButton(0))
        {
            if (ClickTerrian()) { nowState = "ClickTerrian"; return; }
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (TDollSelected != null) { nowState = "AttackWait"; return; }
        }
    }

    void UpdateAttackWait()
    {
        Vector3 point;
        if (GetTerrianClickPoint(out point))
        {
            if (TDollSelected != null)
            {
                //TDollSelected.SetMoveTarget(point);
                attLine.SetDest(TDollSelected.transform.localPosition, point);
                attLine.gameObject.SetActive(true);
            }
        }
    }

    bool GetTerrianClickPoint(out Vector3 pos)
    {
        string name = "Terrian";
        int layerMask = 1 << LayerMask.NameToLayer(name);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            pos = hit.point;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }

    bool ClickTerrian()
    {
        Vector3 point;
        if(GetTerrianClickPoint(out point))
        {
            if (TDollSelected != null)
            {
                TDollSelected.SetMoveTarget(point);
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
