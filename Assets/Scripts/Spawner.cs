using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Red;
    [SerializeField] private GameObject Blue;
    [SerializeField] private GameObject Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            Spawn(Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Spawn(Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Spawn(Player);
        }
    }

    private void Spawn(GameObject bot)
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0f;

        GameObject.Instantiate(bot, spawnPosition, Quaternion.identity, null);
    }
}
