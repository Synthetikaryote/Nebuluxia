using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
    Vector2 lastPlayerPos;
    float moveRate = 1f;
    Vector2 size;

    public void Setup(Transform parent, Canvas canvas)
    {
        var canvasRT = canvas.GetComponent<RectTransform>();
        size = canvasRT.sizeDelta;// new Vector2(canvasRT.sizeDelta.x * canvasRT.localScale.x, canvasRT.sizeDelta.y * canvasRT.localScale.y);
        var rt = GetComponent<RectTransform>();
        rt.SetParent(parent);
        rt.localScale = Vector3.one;
        Reposition();
    }

    void Reposition()
    {
        var rt = GetComponent<RectTransform>();
        var v = Ship.Instance.v;
        if (v == Vector2.zero)
            rt.anchoredPosition = new Vector3(Random.Range(0f, size.x), Random.Range(0f, size.y));
        else
        {
            var onLine = Random.Range(0f, size.x + size.y);
            var onHorizontal = onLine <= size.x;
            if (onHorizontal)
                rt.anchoredPosition = new Vector3(onLine, v.y < 0f ? 0f : size.y);
            else
                rt.anchoredPosition = new Vector3(v.x < 0f ? 0f : size.x, onLine - size.x);
        }

        rt.sizeDelta = Vector3.one * Random.Range(0.8f, 4f);
        moveRate = Random.Range(0.6f, 1.0f);
        lastPlayerPos = Ship.Instance.p;
    }

    // Update is called once per frame
    void LateUpdate () {
        var delta = Ship.Instance.p - lastPlayerPos;
        if (delta.sqrMagnitude > 0f)
        {
            var rt = GetComponent<RectTransform>();
            var p = rt.anchoredPosition;
            p -= delta * moveRate;
            rt.anchoredPosition = p;
            lastPlayerPos = Ship.Instance.p;

            if (p.x < 0f || p.x > Screen.width || p.y < 0f || p.y > Screen.height)
                Reposition();
        }
	}
}
