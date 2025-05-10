using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnOffAudio : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public Sprite OnImage;
    public Sprite OffImage;

    private Sprite currentImage;

    private void Start()
    {
        currentImage = OnImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentImage == OnImage)
        {
            gameObject.GetComponent<Button>().image.sprite = OffImage;
        }
        else if (currentImage == OffImage)
        {
            gameObject.GetComponent<Button>().image.sprite = OnImage;
        }
        else
        {
            Debug.Log($"Что-то пошло нетак. " +
                $"OnImage = {OnImage.name}, " +
                $"OffImage = {OffImage.name}," +
                $"Current Image = {currentImage.name}");
        }
        currentImage = gameObject.GetComponent<Button>().image.sprite;
    }
}