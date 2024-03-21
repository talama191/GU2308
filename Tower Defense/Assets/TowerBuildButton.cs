using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildButton : MonoBehaviour
{
    [SerializeField] private Image image;
    private FieldSlot slot;

    public void SetData(FieldSlot slot)
    {
        this.slot = slot;
    }

    public void BuildTower()
    {
        slot.BuildTower(image.sprite);
        AudioManager.Instance.PlayAudioClip();

        EventRegister.Instance.InvokeBuildTowerAction("");
    }
}
