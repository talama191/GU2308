using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private float soundValue;
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnSliderChange(); });
    }

    private void OnSliderChange()
    {
        soundValue = slider.value;
    }

    public void SetSound(float value)
    {
        soundValue = value;
    }
}
