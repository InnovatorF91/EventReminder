using System;

namespace EventReminder
{
	public class EventObject
	{
		/// <summary>
		/// 事件名稱
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 事件發生時的年月日（月日）
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// 標簽
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// 提醒設置
		/// </summary>
		public string ReminderSetting { get; set; }

		/// <summary>
		/// 重複設置
		/// </summary>
		public string RepeatSetting { get; set; }

		/// <summary>
		/// 是否是生日
		/// </summary>
		public bool? IsBirthday { get; set; }

		/// <summary>
		/// 備注
		/// </summary>
		public string Note { get; set; }
	}
}