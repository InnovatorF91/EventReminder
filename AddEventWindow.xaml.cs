using EventReminder_WPF;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToolGood.Words;

namespace EventReminder
{
	public partial class AddEventWindow : Window
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AddEventWindow()
		{
			InitializeComponent();
			notesTextBox.Text = "請輸入備注";
			notesTextBox.FontStyle = FontStyles.Italic;
			notesTextBox.Foreground = Brushes.Gray;

			this.ResizeMode = ResizeMode.NoResize;
		}

		#region イベント

		/// <summary>
		/// 保存按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			// 保存新事件的逻辑
			// 例如，创建一个新的 Event 实例并将其保存到数据源


			this.DialogResult = CreateInfo();
			this.Close();
		}

		/// <summary>
		/// 取消按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			// 关闭窗口而不保存
			this.DialogResult = false;
			this.Close();
		}

		/// <summary>
		/// 備注欄在被關注時的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void NotesTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			notesTextBox.FontStyle = FontStyles.Normal;
			notesTextBox.Foreground = Brushes.Black;

			if (notesTextBox.Text == "請輸入備注")
			{
				notesTextBox.Text = string.Empty;
			}
		}

		/// <summary>
		/// 備注欄在失去關注時的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void NotesTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(notesTextBox.Text))
			{
				notesTextBox.Text = "請輸入備注";
				notesTextBox.FontStyle = FontStyles.Italic;
				notesTextBox.Foreground = Brushes.Gray;
			}
		}

		/// <summary>
		/// 是否是生日事件的CheckBox在被勾選后的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void birthdayReminderCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			timePicker.IsEnabled = false;
			labelTextBox.Text = "生日";
			labelTextBox.IsEnabled = false;

			if (datePicker.Template.FindName("PART_TextBox", datePicker) is TextBox textBox)
			{
				textBox.Text = datePicker.SelectedDate.HasValue ?
					datePicker.SelectedDate.Value.ToString("MM/dd") : "日付の選択";
			}
		}

		/// <summary>
		/// 是否是生日事件的CheckBox在沒有被勾選時發生的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void birthdayReminderCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			timePicker.IsEnabled = true;
			labelTextBox.Text = string.Empty;
			labelTextBox.IsEnabled = true;

			if (datePicker.Template.FindName("PART_TextBox", datePicker) is TextBox textBox)
			{
				textBox.Text = datePicker.SelectedDate.HasValue ?
					datePicker.SelectedDate.Value.ToString("yyyy/MM/dd") : "日付の選択";
			}
		}

		/// <summary>
		/// DatePicker在被選中時發生的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void datePicker_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ChangeTemplateFormat();
		}

		/// <summary>
		/// DataPicker在失去選中時發生的事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void datePicker_LostFocus(object sender, RoutedEventArgs e)
		{
			ChangeTemplateFormat();
		}
		#endregion

		#region プライベート　メソッド

		/// <summary>
		/// 創建新的事件信息
		/// </summary>
		/// <returns>result = true : 成功創建 / result = false : 創建失敗</returns>
		private bool CreateInfo()
		{
			var result = false;

			var eventName = eventNameTextBox.Text;
			var date = datePicker.SelectedDate.HasValue ? datePicker.SelectedDate.Value.ToString("yyyy/MM/dd") : null;
			var time = timePicker.Text;

			if (reminderComboBox.SelectedItem == null || repeatComboBox.SelectedItem == null)
			{
				MessageBox.Show("請選擇提醒設置或輸入設置。");

				return result;
			}

			var reminderSetting = reminderComboBox.SelectedItem.ToString().Replace(CommonConst.ReplaceWord, "");
			var repeatSetting = repeatComboBox.SelectedItem.ToString()
				.Replace(CommonConst.ReplaceWord, "");
			var isBirthday = birthdayReminderCheckBox.IsChecked;
			var laber = labelTextBox.Text;
			var note = notesTextBox.Text;

			if (isBirthday == false &&
				(string.IsNullOrEmpty(eventName)
				|| string.IsNullOrEmpty(laber)
				|| date == null))
			{
				MessageBox.Show("請輸入事件名稱或標籤或選擇您的日期。");

				return result;
			}
			else if (isBirthday == true && (string.IsNullOrEmpty(eventName)))
			{
				MessageBox.Show("請輸入生日對象的名稱。");

				return result;
			}

			var _newEvent = new EventObject
			{
				Name = eventName,
				DateTime = string.IsNullOrEmpty(time) ? DateTime.Parse(date) : DateTime.Parse(date + " " + time),
				ReminderSetting = reminderSetting,
				RepeatSetting = repeatSetting,
				IsBirthday = isBirthday,
				Label = laber,
				Note = note,
			};

			result = WriteToFile(_newEvent);

			return result;
		}

		/// <summary>
		/// 將新創建的事件信息寫入文件
		/// </summary>
		/// <param name="_event">事件對象</param>
		/// <returns>true : 保存成功 / false : 保存失敗</returns>
		private bool WriteToFile(EventObject _event)
		{
			var folderPath = Share.GetJsonFolderPath();

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			var dayInfo = _event.DateTime.ToString("yyyy-MM-dd");

			if (_event.IsBirthday == true)
			{
				dayInfo = _event.DateTime.ToString("MM-dd");
			}

			var regex = Share.GetRegexOfDocument();

			if (!regex.IsMatch(_event.Name))
			{
				_event.Name = WordsHelper.GetPinyin(_event.Name);
			}

			var stringBuilder = new StringBuilder().Append(_event.Name).Append("_").Append(dayInfo).Append(".json");

			var fileName = stringBuilder.ToString();

			var filePath = Path.Combine(folderPath, fileName);

			try
			{
				var jsonString = Share.SerializerData(_event);
				File.WriteAllText(filePath, jsonString);

				MessageBox.Show(fileName + "保存成功！");

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(fileName + "保存失敗！" + Environment.NewLine + ex.ToString());

				return false;
			}
		}

		/// <summary>
		/// 改變字符串格式
		/// 這裏指生日相關的CheckBox被勾選后，DatePicker中表示日期的格式改變為MM/dd
		/// </summary>
		private void ChangeTemplateFormat()
		{
			if (birthdayReminderCheckBox.IsChecked == true)
			{
				if (datePicker.Template.FindName("PART_TextBox", datePicker) is TextBox textBox)
				{
					textBox.Text = datePicker.SelectedDate.HasValue ? datePicker.SelectedDate.Value.ToString("MM/dd") : "日付の選択";
				}
			}
		}
		#endregion
	}
}

