using System.Diagnostics;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace UnitySublime
{
	class OpenWithSublimeText
	{
		static readonly string applicationPath = @"C:\Program Files\Sublime Text 3\sublime_text.exe";

		[MenuItem("Assets/Open with Sublime Text", priority = -1000)]
		static void Open()
		{
			string filePath = GetSelectedFilePath();
			Process.Start(applicationPath, filePath);
		}

		[MenuItem("Assets/Open with Sublime Text", validate = true)]
		static bool OpenValidate()
		{
			return !string.IsNullOrEmpty(GetSelectedFilePath());
		}

		static string GetSelectedFilePath()
		{
			//An object must be selected
			if (Selection.activeObject == null)
			{
				return null;
			}
			//Selected must be an asset
			string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (string.IsNullOrEmpty(assetPath))
			{
				return null;
			}
			//Selected must be a file
			string fullPath = Application.dataPath + assetPath.Substring(6);
			if (!File.Exists(fullPath))
			{
				return null;
			}
			return fullPath;
		}
	}
}