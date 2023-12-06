using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSheet : MonoBehaviour
{
    [SerializeField] private List<TowerBuildButton> buildButtons;
    EventRegister evRegister;

    private void Awake()
    {
        evRegister = EventRegister.Instance;
    }

    private void OnEnable()
    {
        evRegister.OnBuildTowerAction += OnBuildTower;
    }

    private void OnDisable()
    {
        evRegister.OnBuildTowerAction -= OnBuildTower;
    }

    private void OnBuildTower(string s)
    {
        gameObject.SetActive(false);
    }

    public void SetSlot(FieldSlot slot)
    {
        foreach (var button in buildButtons)
        {
            button.SetData(slot);
        }
    }
}
