using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleViewModel : ViewModelBase
{
    private float sliderValue;
    public float SliderValue
    {
        get { return sliderValue; }
        set { Set<float>(ref sliderValue, value, "SliderValue"); }
    }

    public void OnSliderValueChanged(float newValue)
    {
        if (newValue != sliderValue)
        {
            this.SliderValue = newValue;
            Debug.Log($"NewValue:{newValue}");
        }
    }

    public void OnInputFieldChanged(string str)
    {
        this.SliderValue = int.Parse(str);

    }
}
