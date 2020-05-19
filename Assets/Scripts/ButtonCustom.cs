using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonCustom : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header("Colours")]
    public Color baseColor;
    public Color hoverColor;
    Text text;
    [Header("Events")]
    public UnityEvent OnClick;
    bool toggle;

    void Update()
    {
        if(toggle==true)
        {
            OnClick.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = baseColor;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        toggle = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        toggle = false;
    }

}


