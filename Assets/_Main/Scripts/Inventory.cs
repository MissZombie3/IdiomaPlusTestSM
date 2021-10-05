using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Slot[] slots;

    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject inventoryPanel;

    private void Awake()
    {
        slots = new Slot[slotContainer.childCount];

        for (int i = 0; i < slots.Length; i++)
            slots[i] = slotContainer.GetChild(i).GetComponent<Slot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
            AddItem(other.GetComponent<Item>());
    }

    private void AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].UpdateSlot(item);
                item.gameObject.SetActive(false);
                break;
            }
        }
    }
}
