using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleStickBehaviour : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Vector2 target;
    private Vector2 home;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image joystickImage;
    private Vector3 input;

    void Start()
    {
        home = this.gameObject.transform.position;
        backgroundImage = this.GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform
                                                                    , eventData.position
                                                                    , eventData.pressEventCamera
                                                                    , out position))
        {
            position.x = (position.x / backgroundImage.rectTransform.sizeDelta.x);
            position.y = (position.y / backgroundImage.rectTransform.sizeDelta.y);

            input = new Vector3(position.x * 2 - 1, 0, position.y * 2 - 1);
            input = input.magnitude > 1.0f ? input.normalized : input;

            joystickImage.rectTransform.anchoredPosition =
                new Vector3(input.x * (backgroundImage.rectTransform.sizeDelta.x / 3)
                            , input.z * (backgroundImage.rectTransform.sizeDelta.y / 3));

            Debug.Log(input);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (input.x != 0)
        {
            return input.x;
        }
        else
        {
#if UNITY_EDITOR
            return Input.GetAxisRaw("Horizontal");
#else
			return Input.GetAxis("Horizontal");
#endif
        }
    }

    public float Vertical()
    {
		if (input.z != 0)
        {
            return input.z;
        }
        else
        {
#if UNITY_EDITOR
            return Input.GetAxisRaw("Vertical");
#else
			return Input.GetAxis("Vertical");
#endif
        }
    }
}
