using Il2CppTLD.Gear;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GearLog.Mod.Entities
{
    public class SceneData
    {
        public string SceneName { get; private set; } = string.Empty;

        public DateTime ScanTime { get; private set; } = DateTime.Now;

        public List<ItemData> Clothing { get; private set; } = new();

        public List<ItemData> Firestarting { get; private set; } = new();

        public List<ItemData> FirstAid { get; private set; } = new();

        public List<ItemData> Food { get; private set; } = new();

        public List<ItemData> Tools { get; private set; } = new();

        public List<ItemData> Materials { get; private set; } = new();

        public List<ItemData> Other { get; private set; } = new();

        public SceneData(string sceneName)
        {
            SceneName = sceneName;
        }

        public void AddItem(ItemData item)
        {
            List<ItemData> targetList = _getListByType(item.Type);

            ItemData? existing = targetList.FirstOrDefault(i =>
                i.Id.Equals(item.Id, StringComparison.Ordinal));

            if (existing != null)
            {
                existing.Count += item.Count;
            }
            else
            {
                targetList.Add(item);
            }
        }

        private List<ItemData> _getListByType(GearType type) => type switch
        {
            GearType.Clothing => Clothing,
            GearType.Firestarting => Firestarting,
            GearType.FirstAid => FirstAid,
            GearType.Food => Food,
            GearType.Tool => Tools,
            GearType.Material => Materials,
            _ => Other
        };
    }
}
