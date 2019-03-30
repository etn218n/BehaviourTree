using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject victim;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0f;

            GameObject.Instantiate(victim, spawnPosition, Quaternion.identity, null);
        }
    }
}
