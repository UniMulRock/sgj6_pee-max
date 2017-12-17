using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconDropObject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int Index;

	[SerializeField]
    private Image iconImage;
    private Sprite nowSprite;

    void Start()
    {
        nowSprite = null;

		var list = PeeMax.System.GameManager.Instance.SelectedCommands;
		var cmdObj = (list.Count > Index) ? list [Index] : null;
		if (cmdObj != null) {
			Image droppedImage = cmdObj.GetComponent<Image>();
			iconImage.sprite = droppedImage.sprite;
			iconImage.color = Vector4.one * 0.6f;
		}
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;

        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
		iconImage.color = Vector4.one;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (pointerEventData.pointerDrag == null) return;
        iconImage.sprite = nowSprite;
        if (nowSprite == null)
            iconImage.color = Vector4.zero;
        else
            iconImage.color = Vector4.one;
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        nowSprite = droppedImage.sprite;
        iconImage.color = Vector4.one;

		PeeMax.System.GameManager.Instance.InsertSelectedCommand (pointerEventData.pointerDrag, Index);
    }
}
