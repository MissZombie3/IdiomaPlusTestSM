using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    private Image myImage;
    private Health playerHealth;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        myImage.fillAmount = playerHealth.CurrentHealth / 100F;
    }
}
