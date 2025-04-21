# 📆 EventReminder

一款簡潔實用的桌面事件提醒工具，使用 C# 和 WPF (.NET Framework 4.8) 開發，支援本地 JSON 存儲，無需登入、無需連網，啟動即用，幫你記錄每日任務、重要行程與生日提醒。

![主畫面](./screenshots/main.png)

---

## 🧩 專案簡介

本專案為純 WPF 應用程式，未使用 MVVM 架構，適合入門級 C# 使用者了解事件儲存與視窗互動處理的基本流程。所有事件以 JSON 檔案方式儲存與讀取，操作直覺、開發輕便。

---

## ✨ 功能特色

- ✅ 主畫面顯示所有事件列表（事件名稱 / 時間 / 標籤）
- ✅ 雙擊事件可編輯詳細內容（名稱、時間、提醒方式、重複規則、描述）
- ✅ 支援「到點提醒」與「提前提醒」功能
- ✅ 事件可選擇是否「每年重複」（適用於生日）
- ✅ 資料儲存為本地 JSON，無需資料庫
- ✅ 支援事件新增、修改、刪除
- ✅ JSON 存檔路徑即時顯示（方便除錯與備份）

---

## 🖼️ 畫面預覽

| 主畫面 | 事件詳情 | 新增事件 |
|--------|----------|----------|
| ![95408ac2-d940-4f34-bbc9-40b10fbddbbf](https://github.com/user-attachments/assets/cff62fe3-2c33-43a3-a536-258bc02f748e) |![cff78f85-a684-4af1-b628-8e604b53acbd](https://github.com/user-attachments/assets/8587de1b-59fe-494b-ab92-1f6f8ad24898) | ![cddda999-ed31-44ef-82c3-267a9b32bc9c](https://github.com/user-attachments/assets/c725027f-23ff-4861-ab49-dd68b64155d8)|
---

## 🛠 技術細節

- **語言**：C#
- **框架**：.NET Framework 4.8
- **UI 技術**：WPF（非 MVVM 架構）
- **資料儲存**：使用 `Newtonsoft.Json` 套件進行序列化至本地 JSON 檔案
- **存儲範例**：
  ```json
{"Name":"\u9762\u8A66","PinyinName":"MianShi","DateTime":"2025-04-21T12:00:00","Label":"\u9762\u8A66","ReminderSetting":"\u5230\u9EDE\u63D0\u9192","RepeatSetting":"\u6C38\u4E0D","IsBirthday":false,"Note":"\u4ECA\u592912\uFF1A00\u9762\u8A66"}
