using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltipPrefab;
    private GameObject tooltip;
    Vector2 shift;
    private void Start()
    {
        var tooltip = Instantiate(tooltipPrefab);
        shift = tooltip.GetComponent<RectTransform>().rect.size / 2;
        Destroy(tooltip);
    }
    public void CreateTooltip(Vector2 position, string text)
    {
        // TODO The tooltip should be created once per position
        if (tooltip != null && tooltip.transform.position.ToVector2() + shift == position)
        {
            return;
        }
        DestroyTooltip();

        tooltip = Instantiate(tooltipPrefab, this.transform);
        var foreground = tooltip.transform.Find("Foreground");
        var tmp = foreground.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        tmp.text = text;

        tooltip.transform.position = position + shift;
    }
    public void DestroyTooltip()
    {
        Destroy(tooltip);
    }
}
