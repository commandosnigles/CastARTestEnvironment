using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TestLog : MonoBehaviour {
	public enum Platform {Desktop, CastAR}
	public Platform TestingPlatform = Platform.Desktop;

	public static TestLog Instance;

	public static int TestID;
	public bool Oculus = true;
	public int ModelCount = 20;
	private List <int> modelOrder = new List<int>();
	private int modelListPosition = 0;

	private int currentModelID;
	private GameObject currentModel;
	private bool modelLoaded;
	private float testStartTime;
	private static string directory = Application.persistentDataPath;
//	FileStream logFile = File.Create(directory + "/TestLog_" + TestID.ToString ());

	public struct LogEntry {
		public int ModelID;
		public float ResponseTime;
		public bool Correct;
	}

	public List<LogEntry> Log = new List<LogEntry>();


	void Awake() {
		if (!Instance)
			Instance = this;
		else Destroy(this);
		PopulateModelOrder();
	//	logFile = File.Create("testLog-" + TestID.ToString + ".txt");
	}

	void Start(){
		LoadModel();
	}
	[RPC]
	public void LoadModel(){
		testStartTime = Time.realtimeSinceStartup;
		if (currentModel) {
			GameObject.Destroy (currentModel);
			modelListPosition++;
		}
		currentModelID = modelOrder[modelListPosition];

		currentModel = GameObject.Instantiate(Resources.Load("TestModels/TestModel_" + currentModelID.ToString ()),
		                                      Vector3.zero, Quaternion.identity) as GameObject;

		currentModel.name = "TestModel_" + currentModelID.ToString ();
	}

	public void AddEntry (int modelID, float responseTime, bool correct) {
		LogEntry entry = new LogEntry();
		entry.ModelID = modelID;
		entry.ResponseTime = responseTime;
		entry.Correct = correct;
		Log.Add(entry);
//		if (logFile.CanWrite){
//			logFile.BeginWrite();
//			logFile.write
//		}
	}

	public void Answer(bool correct) {
		AddEntry(currentModelID, Time.realtimeSinceStartup - testStartTime, correct);
		Debug.Log("Correct Response: " + correct);
		networkView.RPC ("LoadModel", RPCMode.AllBuffered);
	}

	void PopulateModelOrder () {
		if (TestID % 2 == 0 ^ Oculus) {
			for (int i = 0; i < ModelCount/2; i++) {
				modelOrder.Add(i);
			}
		} 
		else {
			for (int i = ModelCount/2; i < ModelCount; i++) {
				modelOrder.Add(i);
			}
		}
	}

}
