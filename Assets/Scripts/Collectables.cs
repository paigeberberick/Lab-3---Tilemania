using UnityEngine;
using UnityEngine.Tilemaps;

public class Collectable : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == null) return;

        // Remove any coin tiles inside the bounds of the player collider that touched
        Vector3Int min = tilemap.WorldToCell(other.bounds.min);
        Vector3Int max = tilemap.WorldToCell(other.bounds.max);

        for (int x = min.x; x <= max.x; x++)
        {
            for (int y = min.y; y <= max.y; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cell))
                {
                    tilemap.SetTile(cell, null);
                    Debug.Log("Coin collected at " + cell);
                }
            }
        }
    }
}