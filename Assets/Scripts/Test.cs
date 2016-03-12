using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Test : MonoBehaviour {

	public GameObject tcube;

	// Use this for initialization
	void Start () {
		readFromFile rff = new readFromFile();
		//var J = SimpleJSON.JSON.Parse(rff.LoadJSONResourceFile("ReferenceFiles/menu.json"));
		//Debug.Log(J["Menu"].);
		//JsonMapper.ToObject()
		//JsonData data = JsonMapper.ToObject(rff.LoadJSONResourceFile("ReferenceFiles/menu.json"));
		//Debug.Log((string)data["Menu"]);
		//PrintJson(rff.LoadJSONResourceFile("ReferenceFiles/test.json"));

		//createTestItems ();
		maine();
	}
	



	public void test () {
		readFromFile rff = new readFromFile();
		string json = rff.LoadJSONResourceFile("ReferenceFiles/test.json");

		JSONNode j = JSONNode.Parse(json);


		for(int i = 0; i < j["Menu"].Count; i++) {
			Debug.Log(j["Menu"][i]["_baseIconPath"].ToString());
			Debug.Log(j["Menu"][i]["_activeIconPath"].ToString());
			Debug.Log(j["Menu"][i]["_text"].ToString());
			Debug.Log(j["Menu"][i]["_hasChildren"].AsBool);
			Debug.Log("-------------------------");
		}

	}

	public void createTestItems() {
		readFromFile rff = new readFromFile();
		string json = rff.LoadJSONResourceFile("ReferenceFiles/test.json");
		JSONNode j = JSONNode.Parse(json);
		JSONNode k = j["Menu"];

		for(int i = 0; i < j["Menu"].Count; i++) {
			GameObject.Instantiate(tcube);
			if(j["Menu"][i]["_hasChildren"].AsBool) {

			}
		}
	}



	public void maine() {
		readFromFile rff = new readFromFile();
		string menu = rff.LoadJSONResourceFile("ReferenceFiles/test.json");
		JSONNode jsonNode = JSONNode.Parse(menu);
		testRecursizeness(jsonNode["Menu"]);
	}

	// To quote the infamous natty-ice, "Fuck the club up,"
	public bool testRecursizeness(JSONNode jsonNode) {
		for(int i = 0; i < jsonNode.Count; i++) {
			if(jsonNode[i]["_hasChildren"] != null) {
				createTestItems (jsonNode[i]);
				if(jsonNode[i]["_hasChildren"].AsBool) {
					testRecursizeness(jsonNode[i]); 
				}
			}
		}
		return false;
	}

	public void createTestItems (JSONNode jsonNode) {
		Debug.Log(jsonNode["_text"]);
	}




}
