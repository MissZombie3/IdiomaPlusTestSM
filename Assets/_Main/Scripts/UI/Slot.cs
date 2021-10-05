using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    None,
    Sword,
    Apple
}

public class Slot : MonoBehaviour
{
    private ItemType type;
    private Sprite icon;

    public bool IsEmpty
    {
        get;
        private set;
    }

    private Image slotIcon;
    private PlayerMovement player;
    private CombatSystem combatSystem;
    private Health playerHealth;
    
    private void Awake()
    {
        slotIcon = transform.GetChild(0).GetComponent<Image>();
        IsEmpty = true;
        player = FindObjectOfType<PlayerMovement>();
        combatSystem = player.GetComponent<CombatSystem>();
        playerHealth = player.GetComponent<Health>();
    }

    public void UpdateSlot(Item item)
    {
        type = item.ItemType;
        icon = item.ItemIcon;
        IsEmpty = false;
        slotIcon.gameObject.SetActive(true);
        slotIcon.sprite = icon;
    }

    public void UseItem()
    {
        switch (type)
        {
            case ItemType.Sword:
                combatSystem.enabled = true;
                break;
            case ItemType.Apple:
                playerHealth.Heal(15);
                break;
        }
        RemoveFromInventory();
    }

    private void RemoveFromInventory()
    {
        type = ItemType.None;
        IsEmpty = true;
        slotIcon.gameObject.SetActive(false);
    }
}
