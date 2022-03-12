using UnityEngine;
public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static void SetAnchorTop(this RectTransform rt)
    {
        rt.pivot = new Vector2(0.5f, 1);
        rt.anchorMin = new Vector2(0.5f, 1);
        rt.anchorMax = new Vector2(0.5f, 1);
    }
    public static void SetAnchorBottom(this RectTransform rt)
    {
        rt.pivot = new Vector2(0.5f, 0);
        rt.anchorMin = new Vector2(0.5f, 0);
        rt.anchorMax = new Vector2(0.5f, 0);
    }
    public static void SetAnchorLeft(this RectTransform rt)
    {
        rt.pivot = new Vector2(0, 0.5f);
        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(0, 0.5f);
    }
    public static void SetAnchorRigth(this RectTransform rt)
    {
        rt.pivot = new Vector2(1, 0.5f);
        rt.anchorMin = new Vector2(1, 0.5f);
        rt.anchorMax = new Vector2(1, 0.5f);
    }
    public static void SetAnchorMiddle(this RectTransform rt)
    {
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
    }
    public static void SetAnchorMin(this RectTransform rt, float x, float y)
    {
        rt.anchorMin = new Vector2(x, y);
    }
    public static void SetAnchorMax(this RectTransform rt, float x, float y)
    {
        rt.anchorMax = new Vector2(x, y);
    }
    public static void SetPivot(this RectTransform rt, float x, float y)
    {
        rt.pivot = new Vector2(x, y);
    }
    public static void SetStretchMiddle(this RectTransform rt)
    {
        rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchorMax = new Vector2(1, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
    }
    public static GameObject setPosition(this GameObject go, Vector2 pos)
    {
        go.GetComponent<RectTransform>().anchoredPosition = pos;
        return go;
    }

}