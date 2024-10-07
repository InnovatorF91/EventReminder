using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EventReminder_WPF
{
	static class Share
	{
		/// <summary>
		/// 得到json文件夾的地址
		/// </summary>
		/// <returns>json文件夾的地址</returns>
		public static string GetJsonFolderPath()
		{
			var parentFolderPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			var jsonfolder = CommonConst.JsonFolder;
			var folderPath = Path.Combine(parentFolderPath, jsonfolder);

			return folderPath;
		}

		/// <summary>
		/// 序列化信息
		/// </summary>
		/// <param name="info">信息</param>
		/// <returns>序列化后的信息</returns>
		public static string SerializerData<T>(T info)
		{
			return JsonSerializer.Serialize(info);
		}

		/// <summary>
		/// 對信息反序列化
		/// </summary>
		/// <param name="jsonString">讀取的字符串</param>
		/// <returns>反序列化后的信息</returns>
		public static T DeserializeData<T>(string jsonString)
		{
			return JsonSerializer.Deserialize<T>(jsonString);
		}

		/// <summary>
		/// 得到時間的書寫規範
		/// </summary>
		/// <returns>日期的書寫規範</returns>
		public static Regex GetRegexOfDate()
		{
			return new Regex("^[0-23]\\d{2}:[0-59]\\d{2}$");
		}

		/// <summary>
		/// 得到英語字符串的書寫規範
		/// </summary>
		/// <returns>英語字符串的書寫規範</returns>
		public static Regex GetRegexOfDocument()
		{
			return new Regex("^[A-Za-z]+$");
		}

		///// <summary>
		///// 得到所有當事人姓名
		///// </summary>
		//public static void GetAllInfo()
		//{
		//	var folderPath = Share.GetJsonFolderPath();

		//	var files = Directory.GetFiles(folderPath);
		//	if (files.Length == 0)
		//	{
		//		Console.WriteLine("Date Folder is Empty.");
		//		return;
		//	}

		//	foreach (var file in files)
		//	{
		//		var jsonString = File.ReadAllText(file);
		//		var birthDayInfo = Share.DeserializeData<BirthDayInfo>(jsonString);

		//		Console.WriteLine(birthDayInfo.CharaName);
		//	}
		//}
	}

}
