using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f; // ����Ŭ�� ���� �ð� �Ӱ谪

    public Item.Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage; //item count background image

    private Player thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
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

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else //weapon
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
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
}