using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarketSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item.Item item; // ȹ���� ������
    public Image itemImage;  // �������� �̹���

    private ItemInfo theItemInfo;

    private Rect baseRect;

    // Start is called before the first frame update
    void Start()
    {
        AddItem();
        theItemInfo = FindObjectOfType<ItemInfo>();
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

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theItemInfo.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theItemInfo.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            ShowToolTip(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }
}
