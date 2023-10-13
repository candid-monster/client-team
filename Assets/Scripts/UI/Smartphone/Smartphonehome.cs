using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Smartphonehome : MonoBehaviour, IPointerClickHandler
{
    private UISmartphone theUISmartphone;
    // Start is called before the first frame update
    void Start()
    {
        theUISmartphone = FindObjectOfType<UISmartphone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            theUISmartphone.OpenSmartphone();
        }
    }
}
