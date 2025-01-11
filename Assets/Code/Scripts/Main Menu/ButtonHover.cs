using UnityEngine;
using UnityEngine.EventSystems;  // For UI events
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;  // The default background image
    public Sprite hoverSprite;   // The background image when hovered

    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogError("Button does not have an image component!");
        }
    }

    // Called when the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    // Called when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null && normalSprite != null)
        {
            buttonImage.sprite = normalSprite;
        }
    }
}
