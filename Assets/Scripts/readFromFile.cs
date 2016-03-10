using SimpleJSON;
using UnityEngine;

public class readFromFile : MonoBehaviour {
	public string LoadJSONResourceFile(string path) {
		string filePath = path.Replace(".json", "");
		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
		return targetFile.text;
	}

}