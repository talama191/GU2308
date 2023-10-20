using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    private Slider slider;
    private Image image;
    private void Start()
    {
        slider = GetComponent<Slider>();
        TestCoroutine.Instance.UpdatePlayerHP += UpdateHP;
    }

    public void UpdateHP(float hp)
    {
        slider.value = hp / 100f;
        // image.sprite=sprite;
    }
}
