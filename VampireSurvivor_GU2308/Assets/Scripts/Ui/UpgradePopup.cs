
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopup : MonoBehaviour
{
    public static UpgradePopup Instance;
    [SerializeField] private List<ItemInfoBase> itemInfos;
    [SerializeField] private UpgradeItemView upgradeItemViewPrefab;
    [SerializeField] private GameObject container;

    private bool hasSetup = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetupPopup()
    {
        if (!hasSetup)
        {
            hasSetup = true;
            foreach (var item in itemInfos)
            {
                var itemView = Instantiate(upgradeItemViewPrefab, container.transform);
                itemView.SetItemData(item);
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
