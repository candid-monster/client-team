using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarketSlot : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item; //����� ������
    public Image itemImage;  // �������� �̹���

    private IMItemInfo theIMItemInfo;

    [SerializeField]
    private Inventory theInventory;

    void Start()
    {
        AddItem();
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
    }

    // ������ ���ο� ������ ���� �߰�
    public void AddItem()
    {
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
        }
        else
        {
            itemImage.sprite = null;
        }
    }

    //���� ����â
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 1)
        {
            if (item != null)
            {
                if(item.itemName != theIMItemInfo.GetItem())
                {
                    ShowToolTip(item, transform.position);
                }
                else if (item.itemName == theIMItemInfo.GetItem())
                {
                    if (theIMItemInfo.GetToolTipActive() == false)
                    {
                        ShowToolTip(item, transform.position);
                    }
                    else HideToolTip();
                }
            }
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theIMItemInfo.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theIMItemInfo.HideToolTip();
    }
}
