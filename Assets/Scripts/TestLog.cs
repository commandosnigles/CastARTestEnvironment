using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TestLog : MonoBehaviour {
	public enum Platform {Desktop, CastAR}
	public Platform TestingPlatform = Platform.Desktop;

	public static TestLog Instance;

	public int TestID;

	private int modelID;
	private bool modelLoaded;
	private float testStartTime;
	//File logFile = File.Create("testLog-" + TestID.ToString + ".txt");

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
	//	logFile = File.Create("testLog-" + TestID.ToString + ".txt");
	}

	public void StartTest() {
		testStartTime = Time.realtimeSinceStartup;
	}

	public void AddEntry (int modelID, float responseTime, bool correct) {
		LogEntry entry = new LogEntry();
		entry.ModelID = modelID;
		entry.ResponseTime = responseTime;
		entry.Correct = correct;
		Log.Add(entry);
	}

	public void Answer(bool correct) {
		AddEntry(modelID, Time.realtimeSinceStartup - testStartTime, correct);
	}

}
