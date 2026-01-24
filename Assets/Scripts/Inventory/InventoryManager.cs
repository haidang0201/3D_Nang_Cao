
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static InventoryManager Instance { get; private set; }
    public Transform itemHolder;
    public GameObject itemPrefabs;
    public Toggle enable;
    void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        DisplayInventory();
    }
    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void DisplayInventory()
    {
        foreach (Transform item in itemHolder)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(itemPrefabs, itemHolder);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("ItemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemImage.sprite = item.imageItem;

            obj.GetComponent<ItemUIController>().SetItem(item);
        }
        EnableRemoveButton();
    }
    void EnableRemoveButton()
    {
        if (enable.isOn)
        {
            foreach (Transform item in itemHolder)
            {
                item.transform.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in itemHolder)
                {
                item.transform.Find("RemoveButton").gameObject.SetActive(false  );
                }
        }
        
    }
}
