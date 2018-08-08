using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentsToDisable;
    [HideInInspector]
    //public GameObject playerUIInstance;

    void Start()
    {
        // Disable components that should only be
        // active on the player that we control
        if (!isLocalPlayer)
        {
            DisableComponents();
            //AssignRemoteLayer();
        }
        else
        {
            // Disable player graphics for local player
            //SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            // Create PlayerUI
            //playerUIInstance = Instantiate(playerUIPrefab);
            //playerUIInstance.name = playerUIPrefab.name;

            // Configure PlayerUI
            //PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            // if (ui == null)
            // 	Debug.LogError("No PlayerUI component on PlayerUI prefab.");
            // ui.SetPlayer(GetComponent<Player>());

            GetComponent<Player>().SetupPlayer();

            string _username = "Loading...";
            _username = transform.name;

            CmdSetUsername(transform.name, _username);
        }
    }

    [Command]
    void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username + " has joined!");
            player.username = username;
        }
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    private void RegisterPlayer()
    {
        string id = GetComponent<NetworkIdentity>().netId.ToString();
        var player = GetComponent<Player>();
        GameManager.RegisterPlayer(id, player);
    }

    void OnDisable()
    {
        //Destroy(playerUIInstance);

        if (isLocalPlayer)
            GameManager.instance.SetSceneCameraActive(true);

        GameManager.DeregisterPlayer(transform.name);
    }


}
