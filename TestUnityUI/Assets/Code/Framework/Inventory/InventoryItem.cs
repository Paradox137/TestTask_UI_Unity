using UnityEngine;

namespace Framework.Inventory
{
    public class InventoryItem
    {
        public string Title;
        public string Description;
        public Sprite Icon;

        public InventoryItem(string title, string description, Sprite icon)
        {
            Title = title;
            Description = description;
            Icon = icon;
        }
    }
}