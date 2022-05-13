using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Texture2D cursorTexture;

    public void OnPointerEnter(PointerEventData eventData){
        Cursor.SetCursor(cursorTexture, Vector3.zero, CursorMode.ForceSoftware);
    }

    public void OnPointerExit(PointerEventData eventData){
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
    }
}
