using UnityEngine;
using System.Collections;
using LitJson;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		readFromFile rff = new readFromFile();
		//var J = SimpleJSON.JSON.Parse(rff.LoadJSONResourceFile("ReferenceFiles/menu.json"));
		//Debug.Log(J["Menu"].);
		//JsonMapper.ToObject()
		//JsonData data = JsonMapper.ToObject(rff.LoadJSONResourceFile("ReferenceFiles/menu.json"));
		//Debug.Log((string)data["Menu"]);
		PrintJson(rff.LoadJSONResourceFile("ReferenceFiles/test.json"));
	}
	
	public static void PrintJson(string json) {
		JsonReader reader = new JsonReader(json);
		JsonData data = JsonMapper.ToObject(reader);

		Debug.Log(data["name"]);
		Debug.Log(data[0]);
	
	

		// The Read() method returns false when there's nothing else to read
		while (reader.Read()) {
			string type = reader.Value != null ?
				reader.Value.GetType().ToString() : "";

			Debug.Log (reader.Token + ", " + reader.Value);
			if(reader.Token.ToString().Equals("PropertyName")) {
				Debug.Log("Hi");
			}
		}
	}
}
