using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestLog : MonoBehaviour {
	public enum Platform {Desktop, CastAR}
	public Platform TestingPlatform = Platform.Desktop;

	public int TestID;

	public struct LogEntry {
		public int ModelID;
		public float ResponseTime;
		public bool Correct;
	}

	public List<LogEntry> Log = new List<LogEntry>();

	public void AddEntry (int modelID, float responseTime, bool correct) {
		LogEntry entry = new LogEntry();
		entry.ModelID = modelID;
		entry.ResponseTime = responseTime;
		entry.Correct = correct;
		Log.Add(entry);
	}

}
