using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI killCountText;

    [SerializeField] PlayerController playerController;

    private void Update()
    {
        RefreshHealthSlider();
        RefreshKillCount();
    }

    private void RefreshHealthSlider()
    {
        healthSlider.maxValue = playerController.healthSystem.GetHealthMax();
        healthSlider.value = playerController.healthSystem.GetHealth();
    }

    private void RefreshKillCount()
    {
        killCountText.text = playerController.killCount.ToString();
    }
}
