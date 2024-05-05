using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
    public class Slot
    {
        public string itemName;
        public int count;
        public int max;
        public Sprite icon;

        public Slot()
        {
            itemName = "";
            count = 0;
            max = 100;
        }

        public bool IsEmpty
        {
            get
            {
                if (itemName == "" && count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool canAddItem(string itemName)
        {
            if (this.itemName == itemName && count < max)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;
            count++;
        }

        public void AddItem(string itemName, Sprite icon, int maxAllowed)
        {
            this.itemName = itemName;
            this.icon = icon;
            count++;
            this.max = maxAllowed;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;

                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }