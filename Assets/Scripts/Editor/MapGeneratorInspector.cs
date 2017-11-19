using System;
using System.Collections;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorInspector : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		if (GUILayout.Button("Generate"))
		{
			GameObject.Find("MapGenerator").GetComponent<MapGenerator>().Generate();
		}
		
		if (GUILayout.Button("Clear map"))
		{
			GameObject.Find("MapGenerator").GetComponent<MapGenerator>().ClearMap();
		}
	}
}
