using TMPro;
using UnityEngine;

public class UpgradeItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text itemValueText;

    private ItemInfoBase itemInfo;

    public void SetItemData(ItemInfoBase itemInfo)
    {
        this.itemInfo = itemInfo;
        nameText.text = itemInfo.ItemName;
        itemValueText.text = itemInfo.ValueType == UpgradeValueType.Flat ? $"+{itemInfo.Value}" : $"{itemInfo.Value}%";
    }

    public void UpgradePlayer()
    {
        PlayerStat.Instance.AddItem(itemInfo);
        UpgradePopup.Instance.Close();
    }
}