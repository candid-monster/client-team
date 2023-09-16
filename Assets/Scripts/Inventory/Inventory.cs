using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;  // �κ��丮 Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base �̹���
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 

    [SerializeField]
    private TextMeshProUGUI text_Coin;

    private Slot[] slots;  // ���Ե� �迭

    private ItemInfo theItemInfo;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        theItemInfo = FindObjectOfType<ItemInfo>();

    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();

        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        theItemInfo.HideToolTip();
    }

    public void AcquireItem(Item.Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType && Item.ItemType.ETC != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ����
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }else if (Item.ItemType.ETC == _item.itemType)
        {
            text_Coin.text = (_count * 1000000000).ToString();
        }
        if(Item.ItemType.ETC != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
                    return;
                }
            }
        }
    }

    public string GetCoinText()
    {
        return text_Coin.text;
    }
}