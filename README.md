# ⚙️ GearLog — Mod for The Long Dark

> **A tool for logging and analyzing gear items in The Long Dark game world.**

[![MelonLoader](https://img.shields.io/badge/MelonLoader-0.7+-blue.svg)](https://github.com/LavaGang/MelonLoader)
[![The Long Dark](https://img.shields.io/badge/Game-The%20Long%20Dark-orange.svg)](https://thelongdark.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

## 📋 Description

**GearLog** is a mod for The Long Dark that scans the current game scene and creates detailed JSON reports of all items found: their type, condition, quantity, and location. Perfect for:

- 🏠 Automatic inventory tracking at bases
- 🗂️ Analyzing loot and item spawning
- 🔍 Debugging your own mods
- 📊 Creating guides and wiki materials
- 🧪 Researching game mechanics

---

## ✨ Features

### 🎮 For Players
- 🔑 **Hotkeys**: Instantly scan the current location by pressing `Insert` (configurable)
- 🗑️ **Log Management**: Remove scene data from the session log with the `Delete` key
- 💾 **Auto-Save Binding**: Separate logs for each game session/save file
- ⚙️ **Flexible Settings**: Enable debug logging, filter destroyed items, etc.

### 🛠️ For Modders
- 📦 **Complete Item Info**: ID, displayName, type, condition, quantity
- 🗃️ **Structured Data**: JSON output with formatting and category grouping
- 🌐 **Cross-Platform**: Supports Windows, Linux, and macOS save paths

---

## 📦 Installation

### Requirements
- ✅ The Long Dark (Legitimate version)
- ✅ [MelonLoader 0.7+](https://github.com/LavaGang/MelonLoader/releases)
- ✅ Mod Dependencies:
  - `Il2Cpp` bindings
  - `Newtonsoft.Json`
  - `ModSettings` by sinai

### Installation Steps
1. Install [MelonLoader](https://github.com/LavaGang/MelonLoader#installation) for The Long Dark
2. Download the latest `GearLog.dll` from [Releases](../../releases)
3. Place the file in the `Mods/` folder:
   ```
   TheLongDark/
   └── Mods/
        └── GearLog.dll
   ```
4. Launch the game — the mod will load automatically

> 💡 The first launch will create the `Mods/GearLog/` folder for logs and settings.

---

## 🎮 Usage

### Quick Start
1. Load a save in **Survival** mode
2. Press `Insert` (default) to scan the current location
3. The log will be saved to:
   `Mods/GearLog/TLD_Log_Session_<SaveName>.json`

### Log Management
| Action | Key | Description |
|----------|---------|----------|
| 📸 Scan Location | `Insert` | Create/update report for the current scene |
| 🗑️ Delete Location | `Delete` | Remove scene data from the current session log |

### In-Game Settings
Open **Mod Settings** → **Gear Log**:

```
┌─ Controls ─────────────────────┐
│ [Generate Location Log]  Insert │ ← Scan Key
│ [Delete Location Log]    Delete │ ← Delete Key
├─ Advanced ─────────────────────┤
│ [ ] Enable Debug Logs          │ ← Detailed console output
│ [ ] Include Destroyed Items    │ ← Include items with 0% condition
└────────────────────────────────┘
```

---

## 📁 Output Data Structure

Example JSON log:
```json
{
  "SessionName": "sandbox1",
  "SessionStart": "2024-02-20 14:30:00",
  "LastUpdated": "2024-02-20 15:45:22",
  "Scenes": {
    "CampOffice": {
      "SceneName": "CampOffice",
      "ScanTime": "2024-02-20 15:45:22",
      "Food": [
        {
          "Id": "GEAR_CanOfBeans",
          "Name": "Can of Beans",
          "Type": "Food",
          "Condition": "85%",
          "Count": 2
        }
      ],
      "Tools": [
        {
          "Id": "GEAR_Hatchet",
          "Name": "Hatchet",
          "Type": "Tool",
          "Condition": "42%",
          "Count": 1
        }
      ]
    }
  }
}
```

### Item Categories
- 👕 `Clothing` — Clothing and accessories
- 🔥 `Firestarting` — Firestarting tools
- 💊 `FirstAid` — Medical items
- 🍲 `Food` — Food and water
- 🔧 `Tool` — Tools and weapons
- 📦 `Material` — Resources and crafting materials
- ❓ `Other` — Other items

---

## ❓ FAQ

**Q: Logs are not being created?**
A: Ensure you are in a game scene (not in the menu) and press `Insert`. Check the MelonLoader console for errors.

**Q: Where are logs stored?**
A: In the `Mods/GearLog/` folder with names like `TLD_Log_Session_<SaveName>.json`.

**Q: Does it scan containers?**
A: Yes, the mod automatically checks the contents of all active containers in the scene.

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add: AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📜 License

Distributed under the **MIT License**.
See [LICENSE](LICENSE) for more information.

> ⚠️ This mod is not affiliated with Hinterland Studio Inc. The Long Dark is a trademark of Hinterland Studio Inc.
