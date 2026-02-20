using Il2Cpp;
using Il2CppRewired.Utils;
using Il2CppTLD.Gear;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;


namespace GearLog.Log.Entities
{
    public class ItemData
    {
        public string Id { get; private set; } = string.Empty;

        public string Name { get; private set; } = string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public GearType Type { get; private set; }

        public string Condition { get; private set; } = string.Empty;

        public int Count { get; set; }

        public ItemData(GearItem item)
        {
            Id = item.name;
            Name = _getItemName(item);
            Type = _getGearType(item);
            Condition = _getCondition(item);
            Count = _getItemCount(item);
        }

        private string _getItemName(GearItem item) =>
            !string.IsNullOrEmpty(item.DisplayName) ? item.DisplayName : item.name;

        private GearType _getGearType(GearItem item)
        {
            if (item.IsAnyGearType(GearType.Clothing)) return GearType.Clothing;
            if (item.IsAnyGearType(GearType.Firestarting)) return GearType.Firestarting;
            if (item.IsAnyGearType(GearType.FirstAid)) return GearType.FirstAid;
            if (item.IsAnyGearType(GearType.Food)) return GearType.Food;
            if (item.IsAnyGearType(GearType.Tool)) return GearType.Tool;
            if (item.IsAnyGearType(GearType.Material)) return GearType.Material;
            return GearType.Other;
        }

        private string _getCondition(GearItem item)
        {
            if (item.IsNullOrDestroyed())
            {
                return "0%";
            }

            float normalized = Mathf.Clamp01(item.GetNormalizedCondition());
            int percent = Mathf.RoundToInt(normalized * 100f);
            return $"{percent}%";
        }

        private int _getItemCount(GearItem item)
        {
            StackableItem stackable = item.GetComponent<StackableItem>();
            return stackable != null && stackable.m_Units > 0 ? stackable.m_Units : 1;
        }
    }
}
