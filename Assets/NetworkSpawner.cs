using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject Red;
    [SerializeField] private GameObject Blue;

    private void Start()
    {
        CmdSpawn(Red);
    }

    [Command]
    private void CmdSpawn(GameObject bot)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

        GameObject newBot = GameObject.Instantiate(bot);

        newBot.transform.position = spawnPosition;

        NetworkServer.Spawn(bot);
    }
}
