using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDManager : MonoBehaviour
{
    [SerializeField] private Image mask;
    private float originalMaskSize;

    private void Start()
    {
        originalMaskSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalMaskSize * value);
    }
}
