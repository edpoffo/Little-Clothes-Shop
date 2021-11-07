using System;
using UnityEngine.Serialization;

namespace Tools.InventoryItemJson
{
    [Serializable]
    public class SerializableItem
    {
        public string itemName;
        public float itemValue;
        public string itemType;
        public string itemSpritePath;
        public int itemSpriteIndex;
        public float[] color;
        public string path;
        public string limitType;
    }
}