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

    private Rect baseRect;

    // Start is called before the first frame update
    void Start()
    {
        AddItem();
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

    // ������ ���ο� ������ ���� �߰�
    public void AddItem()
    {
        // itemImage�� sprite�� item�� �̹����� ������Ʈ�մϴ�.
        if (item != null)
        {
            itemImage.sprite = item.itemImage; // itemImage�� item�� �̹����� ����
        }
        else
        {
            // item�� null�̶�� �̹����� ����� �� �ֽ��ϴ�.
            itemImage.sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2) // ����Ŭ�� ����
        {
            if (item != null)
            {
                //Debug.Log(item.itemName + "�� �����߽��ϴ�.");
                theInventory.SetCoinText(item.itemValue);
            }
        }
        else if(eventData.clickCount == 1)
        {
            if (item != null)
            {
                if(item.itemName == theIMItemInfo.GetItem())
                {
                    HideToolTip();
                }
                ShowToolTip(item, transform.position);
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
