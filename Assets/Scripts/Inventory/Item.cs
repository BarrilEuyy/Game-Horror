using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemSprite;
    public GameObject inspectItem;
    public Vector3 worldScale;
    public Vector3 inspectScale;
    public Button actionButton;
}
