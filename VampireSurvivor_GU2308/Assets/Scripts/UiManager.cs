
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
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

    public void UpdateHP(float hpPercent)
    {
        slider.value = hpPercent;
    }
}