using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite itemIcon;

    public ItemType ItemType
    {
        get { return type; }
    }

    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }
}
