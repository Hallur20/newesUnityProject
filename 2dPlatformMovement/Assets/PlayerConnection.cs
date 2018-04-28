using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerConnection : NetworkBehaviour {

    public GameObject Player;
    public GameObject ui;

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
	}

    public string stringToEdit = "";
    bool hasBeenPressed = false; //boolean for allowing gui to spawn gui stuff
    void OnGUI() //ongui has an update listener so hasbeenpressed is being observed all the time
    {
        if (!isLocalPlayer) {
            return;
        }
        if (!hasBeenPressed) {
            stringToEdit = GUI.TextField(new Rect(80, 10, 200, 20), stringToEdit, 25); //stringtoedit has to be assigned to the textfield because else we cannot edit it in the game
            if (GUI.Button(new Rect(80, 70, 200, 20), "Start the game"))
            {
                if (!stringToEdit.Equals("")) //we check if the inputfield is not empty, then we are allowed to spawn player.
                {
                    CmdSpawnMyPlayer();
                    CmdChangePlayerName(stringToEdit);
                    hasBeenPressed = true; //now gui cannot spawn gui stuff and is removed

                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }


        if (Player.name == null)
        {
            Debug.Log(Player.name);
            Debug.Log("test");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            string n = stringToEdit;
            CmdChangePlayerName(n); //if something goes wrong with giving playerconnection a name, press p
        }

    }
    [Command]
    void CmdSpawnMyPlayer()
    {
        GameObject go = Instantiate(Player);
        NetworkServer.SpawnWithClientAuthority(go,connectionToClient);
        Debug.Log(stringToEdit);
    }
    [SyncVar(hook = "OnPlayerNameChanged")] //syncvar = change value in command method, then let the hook rpc method do it's thing
    public string PlayerName = "Anonymous";

    [Command]
    void CmdChangePlayerName(string n)
    {
        PlayerName = n; //syncvar also updates other clients with new name
    }

    void OnPlayerNameChanged(string newName)
    {
        PlayerName = newName;
        gameObject.name = newName;
    }
}
