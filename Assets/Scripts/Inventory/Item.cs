
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "inventory/item")]

public class Item : ScriptableObject
{
    public string itemName;
    public int id;
    public int value;
    public Sprite imageItem;
    void Update()
    {

    }
}
