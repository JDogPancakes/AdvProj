using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ValueBar : MonoBehaviour
{
    [SerializeField]
    private Slider valueSlider;
    [SerializeField]
    private float maxValue = -1;
    [SerializeField]
    private float currentValue = -1;


    private void Awake()
    {
        if (!valueSlider) {
            valueSlider = gameObject.GetComponent<Slider>();
        }

        //If Value info haven't been intialized, set it to default value.
        if (maxValue == -1)
        {
            maxValue = 1;
        }
        if (currentValue == -1)
        {
            currentValue = maxValue;
        }
    }

    private void LateUpdate()
    {
        valueSlider.value = currentValue;
    }

    /// <summary>
    ///  Set up player value info
    /// </summary>
    /// <param name="givenMaxValue"> The maximum value you want to set </param>
    /// <param name="givenCurrentValue">The current value </param>
    public void SetUpPlayerValue(float givenMaxValue,float givenCurrentValue) {
        SetMaxValue(givenMaxValue);
        SetCurrentValue(givenCurrentValue);
    }

    /// <summary>
    ///     Change the Maximum value
    /// </summary>
    /// <param name="givenMaxValue"> Value</param>
    public void SetMaxValue(float givenMaxValue) {
        maxValue = givenMaxValue;
        UpdateMaxValue_UI_Set(maxValue);
    }

    /// <summary>
    ///     Change the current value
    /// </summary>
    /// <param name="givenCurrentValue"> Value</param>
    public void SetCurrentValue(float givenCurrentValue) {
        currentValue = givenCurrentValue;
    }
    /// <summary>
    /// Decurease the current value by given float
    /// </summary>
    /// <param name="decreaseValue">The amount you want to decrease</param>
    public void DecreaseCurrentValue(float decreaseValue) {
        currentValue -= decreaseValue;
    }
    private void UpdateMaxValue_UI_Set(float givenMaxValue) {
        valueSlider.maxValue = givenMaxValue;
    }

    private void UpdateCurrentValue_UI_Set(float givenCurrentValue) {
        if (givenCurrentValue > maxValue) {
            Debug.Log("Change Current Value Refuse: Value overflow, set to default maximum.");
            givenCurrentValue = maxValue;
        }

        valueSlider.value = givenCurrentValue;
    }


}
