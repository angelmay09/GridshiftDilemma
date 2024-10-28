using UnityEngine;
public class Tile : MonoBehaviour
{
    public enum TileType { Red, Green, Blue }
    public TileType type;
    private Renderer tileRenderer;
    public bool isSteppedOn = false;
    void Start()
    {
        tileRenderer = GetComponent<Renderer>();
        SetTileColor();
    }

    // Update is called once per frame
    public void SetTileColor()
    {
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        tileRenderer.material = newMaterial;
        switch (type)
        {
            case TileType.Red:
                tileRenderer.material.color = Color.red;
                break;
            case TileType.Green:
                tileRenderer.material.color = Color.green;
                break;
            case TileType.Blue:
                tileRenderer.material.color = Color.blue;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSteppedOn = true;
            TileManager tileManager = FindObjectOfType<TileManager>();
            if (type == TileType.Red)
            {
                tileManager.ResetProgress();
            }
            else if (type == TileType.Blue)
            {
                tileRenderer.material.color = new Color32(55, 69, 107, 255);
            }
        }
    }

    public void ResetTile()
    {
        if (type == TileType.Blue)
        {
            tileRenderer.material.color = Color.blue; 
            isSteppedOn = false; 
        }
    }
}
