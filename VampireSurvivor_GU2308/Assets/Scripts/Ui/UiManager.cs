using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private UpgradePopup upgradePopup;
    public static UiManager Instance;
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
        slider.value = 1;
    }

    public void OpenUpgradePopup()
    {
        upgradePopup.gameObject.SetActive(true);
        upgradePopup.SetupPopup();
        Time.timeScale = 0;
    }

    public void UpdateHP(float hpPercent)
    {
        slider.value = hpPercent;
    }
    public void UpdateXP(float xpPercent)
    {
        xpSlider.value = xpPercent;
    }
}
