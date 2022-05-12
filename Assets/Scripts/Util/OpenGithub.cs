using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenGithub : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Texture2D cursorTexture;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Application.OpenURL ("https://github.com/"+name);
    }

    public void OnPointerEnter(PointerEventData eventData){
        Cursor.SetCursor(cursorTexture, Vector3.zero, CursorMode.ForceSoftware);
        this.gameObject.GetComponent<Outline>().effectColor = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData){
        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
        this.gameObject.GetComponent<Outline>().effectColor = Color.black;
    }

}
