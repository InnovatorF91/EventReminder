using System;
using System.Windows;
using System.Windows.Media;

namespace EventReminder
{
	public partial class EventDetailsWindow : Window
	{
		/// <summary>
		/// 被選中的事件對象
		/// </summary>
		private EventObject _event;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="selectedEvent">被選中的事件對象</param>
		public EventDetailsWindow(EventObject selectedEvent)
		{
			InitializeComponent();
			_event = selectedEvent;
			// 初始化控件
			eventNameTextBox.Text = _event.Name;
			datePicker.SelectedDate = DateTime.Parse(_event.DateTime.ToString("yyyy/MM/dd"));
			timePicker.Text = _event.DateTime.ToString("HHmmss");
			// 更多初始化
			labelTextBox.Text = _event.Label;
		}

		/// <summary>
		/// 更改按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			// 更新事件的逻辑
			// 例如，保存更改到事件实例并更新数据源
			this.DialogResult = true;
			this.Close();
		}

		/// <summary>
		/// 刪除按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			// 删除事件的逻辑
			// 例如，从数据源中删除事件
			this.DialogResult = true;
			this.Close();
		}

		/// <summary>
		/// 取消按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			// 关闭窗口而不保存任何更改
			this.DialogResult = false;
			this.Close();
		}

		/// <summary>
		/// 備注欄在被選中時觸發的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void notesTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			if (notesTextBox.Text == "此處為備注")
			{
				notesTextBox.Text = string.Empty;
			}
		}

		/// <summary>
		/// 備注欄在失去選中狀態時觸發的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void notesTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(notesTextBox.Text))
			{
				notesTextBox.Text = "此處為備注";
				notesTextBox.FontStyle = FontStyles.Italic;
				notesTextBox.Foreground = Brushes.Gray;
			}
		}

		/// <summary>
		/// 是否是生日事件的CheckBox在沒有被勾選時發生的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void birthdayReminderCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{

		}

		/// <summary>
		/// 是否是生日事件的CheckBox在被勾選后的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void birthdayReminderCheckBox_Checked(object sender, RoutedEventArgs e)
		{

		}
	}
}

