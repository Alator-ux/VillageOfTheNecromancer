using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPrefab;
    private GameObject tooltip;
    public void CreateTooltip(Vector2 position, string text)
    {
        DestroyTooltip();

        tooltip = Instantiate(tooltipPrefab, this.transform);
        var foreground = tooltip.transform.Find("Foreground");
        var tmp = foreground.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        tmp.text = text;

        var shift = tooltip.GetComponent<RectTransform>().rect.size / 2;
        tooltip.transform.position = position + shift;
    }
    public void DestroyTooltip()
    {
        Destroy(tooltip);
    }
}
