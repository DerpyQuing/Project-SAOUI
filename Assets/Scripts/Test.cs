using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		readFromFile rff = new readFromFile();
		//var J = SimpleJSON.JSON.Parse(rff.LoadJSONResourceFile("ReferenceFiles/menu.json"));
		//Debug.Log(J["Menu"].);
		//JsonMapper.ToObject()
		//JsonData data = JsonMapper.ToObject(rff.LoadJSONResourceFile("ReferenceFiles/menu.json"));
		//Debug.Log((string)data["Menu"]);
		//PrintJson(rff.LoadJSONResourceFile("ReferenceFiles/test.json"));
		jk ();
	}
	



	public void jk () {
		readFromFile rff = new readFromFile();
		string json = rff.LoadJSONResourceFile("ReferenceFiles/test.json");

		JSONNode j = JSONNode.Parse(json);
		foreach (string k in j["Menu"]["Player"]["Equipment"]["Weapons"]["Simple Sword"].Keys){
			Debug.Log (k);
			Debug.Log(j["Menu"]["Player"]["Equipment"]["Weapons"]["Simple Sword"][k].AsInt);
		}


	}





}
