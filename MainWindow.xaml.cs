using EventReminder_WPF;
using System.IO;
using System.Windows;

namespace EventReminder
{
	public partial class MainWindow : Window
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			// 初始化事件列表
			LoadEvents();

			this.ResizeMode = ResizeMode.CanMinimize;
		}

		#region イベント

		/// <summary>
		/// 雙擊事件列表的事件，調出事件詳情頁面
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void EventListView_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			// 打開事件詳情頁面
			var selectedEvent = (EventObject)eventListView.SelectedItem;
			var detailsWindow = new EventDetailsWindow(selectedEvent);
			detailsWindow.ShowDialog();
		}

		/// <summary>
		/// 添加新事件按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void AddNewEvent_Click(object sender, RoutedEventArgs e)
		{
			// 打開添加新事件頁面
			var addEventWindow = new AddEventWindow();
			addEventWindow.ShowDialog();
		}

		/// <summary>
		/// 刷新按鈕事件
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void btnRefresh_Click(object sender, RoutedEventArgs e)
		{
			LoadEvents();
		}
		#endregion

		#region プライベート　メソッド

		/// <summary>
		/// 刷新列表
		/// </summary>
		private void LoadEvents()
		{
			// 這裡可以加載事件數據到EventListView
			var folderPath = Share.GetJsonFolderPath();

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			var files = Directory.GetFiles(folderPath);
			if (files.Length == 0)
			{
				MessageBox.Show("Date Folder is Empty.");
				return;
			}


		}
		#endregion
	}
}

