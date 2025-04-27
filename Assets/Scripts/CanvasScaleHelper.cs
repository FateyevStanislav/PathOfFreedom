using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CanvasScaleHelper : MonoBehaviour
{
    void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = transform.Find("Main Camera").GetComponent<Camera>();
        var canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }
}
