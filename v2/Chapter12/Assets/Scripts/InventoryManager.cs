using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class InventoryManager : MonoBehaviour, IGameManager
    {
        private Dictionary<string, int> _items;
        private NetworkService _network;
        public ManagerStatus status { get; private set; }

        public string equippedItem { get; private set; }

        public void Startup(NetworkService service)
        {
            Debug.Log("Inventory manager starting...");
            _network = service;


            UpdateData(new Dictionary<string, int>());
            status = ManagerStatus.Started;
        }

        public void UpdateData(Dictionary<string, int> items)
        {
            _items = items;
        }

        public Dictionary<string, int> GetData()
        {
            return _items;
        }

        public bool ConsumeItem(string name)
        {
            if (_items.ContainsKey(name))
            {
                _items[name]--;
                if (_items[name] == 0)
                {
                    _items.Remove(name);
                }
            }
            else
            {
                Debug.Log("Cannot consume " + name);
                return false;
            }

            DisplayItems();
            return true;
        }

        private void DisplayItems()
        {
            string itemDisplay = "Items: ";

            foreach (KeyValuePair<string, int> item in _items)
            {
                itemDisplay += item.Key + "(" + item.Value + ") ";
            }

            Debug.Log(itemDisplay);
        }

        public void AddItem(string name)
        {
            if (_items.ContainsKey(name))
            {
                _items[name] += 1;
            }
            else
            {
                _items[name] = 1;
            }

            DisplayItems();
        }

        public List<string> GetItemList()
        {
            List<string> list = new List<string>(_items.Keys);
            return list;
        }

        public int GetItemCount(string name)
        {
            if (_items.ContainsKey(name))
            {
                return _items[name];
            }

            return 0;
        }

        public bool EquipItem(string name)
        {
            if (_items.ContainsKey(name) && equippedItem != name)
            {
                equippedItem = name;
                Debug.Log("Equipped " + name);
                return true;
            }

            equippedItem = null;
            Debug.Log("Unequipped");
            return false;
        }
    }
}