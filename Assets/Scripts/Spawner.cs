using UnityEngine;

// Not usable at the moment :)
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Red;
    [SerializeField] private GameObject Blue;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Spawn(Red);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Spawn(Blue);
        }
    }

    private void Spawn(GameObject bot)
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0f;

        GameObject.Instantiate(bot, spawnPosition, Quaternion.identity, null);
    }
}
