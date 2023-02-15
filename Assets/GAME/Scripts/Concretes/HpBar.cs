using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    [SerializeField] Image frontEndImage;

    public void SetFillRate(float fillRate)
    {
        frontEndImage.fillAmount = fillRate;
    }
}
