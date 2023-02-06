using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCursor : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject cursor;

    public void OnSelect(BaseEventData data)
    {
        MoveCursor();
    }

    public void OnEnable()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            MoveCursor();
        }
    }

    private void MoveCursor()
    {
        Vector3 newPos = transform.position;
        newPos.x -= (GetComponent<RectTransform>().rect.xMax / 2) + cursor.GetComponent<RectTransform>().rect.xMax; //multiplication is arbitrary... for now
        cursor.transform.position = newPos;
    }

}