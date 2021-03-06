﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	[SerializeField]
	private GameObject sceneCamera;
	public MatchSettings matchSettings;
	private static Dictionary<string, Player> players = new Dictionary<string, Player>();

	public static void RegisterPlayer(string netId, Player player) {
		string playerId = "Player " + netId;
		players.Add(playerId, player);
		player.transform.name = playerId;
	}

	public void SetSceneCameraActive (bool isActive)
	{
		if (sceneCamera == null)
			return;

		sceneCamera.SetActive(isActive);
	}

	void Awake ()
	{
		if (instance != null)
		{
			Debug.LogError("More than one GameManager in scene.");
		} else
		{
			instance = this;
		}
	}


	public static Player GetPlayer(string id) {
		return players[id];
	}

	public static void DeregisterPlayer(string id) {
		players.Remove(id);
	}

	void OnGUI ()
	{
	   GUILayout.BeginArea(new Rect(200, 200, 200, 500));
	   GUILayout.BeginVertical();

	   foreach (string _playerID in players.Keys)
	   {
	       GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
	   }

	   GUILayout.EndVertical();
	   GUILayout.EndArea();
	}
}
