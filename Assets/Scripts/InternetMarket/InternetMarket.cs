using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarket : MonoBehaviour
{
    public static bool internetmarketActivated = false;  // �κ��丮 Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.

    [SerializeField]
    private GameObject go_InternetMarketBase; // Inventory_Base �̹���
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot���� �θ��� Grid Setting 
    [SerializeField]
    private TextMeshProUGUI text_Coin;

    private IMItemInfo theIMItemInfo;
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
        theInventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInternetMarket();
        Coin();
    }

    private void TryOpenInternetMarket()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            internetmarketActivated = !internetmarketActivated;

            if (internetmarketActivated)
                OpenInternetMarket();
            else
                CloseInternetMarket();

        }
    }

    private void OpenInternetMarket()
    {
        go_InternetMarketBase.SetActive(true);
    }

    private void CloseInternetMarket()
    {
        go_InternetMarketBase.SetActive(false);
        theIMItemInfo.HideToolTip();
    }

    private void Coin()
    {
        if (theInventory != null)
        {
            text_Coin.text = theInventory.GetCoinText();
        }
    }
}
