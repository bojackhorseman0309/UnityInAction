using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private TMP_Text[] itemLabels;

    [SerializeField] private TMP_Text curItemLabel;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button useButton;

    private string curItem;

    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();

        int len = itemIcons.Length;
        for (int i = 0; i < len; i++)
        {
            if (i < itemList.Count)
            {
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                Sprite sprite = Resources.Load<Sprite>($"Icons/{item}");
                itemIcons[i].sprite = sprite;
                itemLabels[i].SetNativeSize();

                int count = Managers.Inventory.GetItemCount(item);
                string message = $"x{count}";

                if (item == Managers.Inventory.equippedItem)
                {
                    message = "Equipped\n" + message;
                }

                itemLabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) => { OnItem(item); });

                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        if (!itemList.Contains(curItem))
        {
            curItem = null;
        }

        if (curItem == null)
        {
            curItemLabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else
        {
            curItemLabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if (curItem == "health")
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }

            curItemLabel.text = $"{curItem}";
        }
    }

    public void OnItem(string item)
    {
        curItem = item;
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(curItem);
        Refresh();
    }

    public void OnUse()
    {
        Managers.Inventory.ConsumeItem(curItem);
        if (curItem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }

        Refresh();
    }
}