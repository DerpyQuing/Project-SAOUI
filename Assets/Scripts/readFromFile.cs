using SimpleJSON;
using UnityEngine;

public class ReadFromFile {
	public string LoadJSONResourceFile(string path) {
		string filePath = path.Replace(".json", "");
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		return targetFile.text;
	}
}