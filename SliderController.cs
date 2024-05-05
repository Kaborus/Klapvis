using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Text valueText;

    public void OnSliderChangedO(float value)
    {
        valueText.text = value.ToString();
    }
}
