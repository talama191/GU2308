using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSetting : MonoBehaviour
{
    [SerializeField] private string value;
    [SerializeField] private int intValue;
    TMP_Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    public void OnDropdownChange()
    {
        value = dropdown.captionText.text;
        intValue = dropdown.value;
    }
}
