using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f; // ����Ŭ�� ���� �ð� �Ӱ谪

    public Item.Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���

    [SerializeField]
    private TextMeshProUGUI text_Count;

    [SerializeField]
    private GameObject go_CountImage; //item count background image

    private Player thePlayer;
    private ItemInfo theItemInfo;

    private Rect baseRect;  // Inventory_Base �̹����� Rect ���� �޾� ��.

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item.Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage; //acess item image

        if (item.itemType != Item.ItemType.Equipment && item.itemType != Item.ItemType.ETC)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else if(item.itemType == Item.ItemType.Equipment) //weapon
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        else
        {

        }

        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
    
    private IEnumerator ResetDoubleClickFlag()
    {
        yield return new WaitForSeconds(doubleClickTimeThreshold);
        isDoubleClick = false;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.clickCount == 2) // ����Ŭ�� ����
        {
            isDoubleClick = true;
            StartCoroutine(ResetDoubleClickFlag());
        }
       
        if (isDoubleClick)
        {
            if(item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    if (thePlayer != null)
                    {
                        thePlayer.EquipItem(item.itemName);
                        
                    }
                }
                else
                {
                    //Debug.Log(item.itemName + "�� ����߽��ϴ�.");
                    SetSlotCount(-1);
                }
            }
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
            //Debug.Log(item);
            ShowToolTip(item, transform.position);
        }
    }

    // ���콺 Ŀ���� ���Կ��� ���� �� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        HideToolTip();
    }

    //���콺 �巡�װ� ���۵��� �� �߻��ϴ� �̺�Ʈ
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    //���콺 �巡�� ���� �� �߻��ϴ� �̺�Ʈ
    public void OnDrag(PointerEventData eventData)
    {
        HideToolTip();

        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    // ���콺 �巡�װ� ������ �� �߻��ϴ� �̺�Ʈ
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            Instantiate(DragSlot.instance.dragSlot.item,
                thePlayer.transform.position + thePlayer.transform.forward,
                Quaternion.identity);
            DragSlot.instance.dragSlot.ClearSlot();

        }

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // �ش� ���Կ� ���𰡰� ���콺 ��� ���� �� �߻��ϴ� �̺�Ʈ
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item.Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}