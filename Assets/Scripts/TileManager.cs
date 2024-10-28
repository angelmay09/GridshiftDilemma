using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public float tileSpacing = 15f;
    public int gridLength = 10;
    private GameObject player;
    private Tile[] allTiles;

    public GameObject levelCompletedPanel;
    public TMP_Text levelCompletedText;


    void Start()
    {
        GenerateTiles();
        player = GameObject.FindGameObjectWithTag("Player");
        levelCompletedPanel.SetActive(false);
    }

    void Update()
    {
        KeepPlayerInBounds();
        CheckLevelCompletion();
    }
    void GenerateTiles()
    {
        allTiles = new Tile[gridLength * gridLength];
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                Vector3 position = new Vector3(x * tileSpacing, 0, z * tileSpacing);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.type = (Tile.TileType)Random.Range(0, 3);
                allTiles[x * gridLength + z] = tileScript;

                Collider tileCollider = tile.GetComponent<Collider>();
                if (tileCollider != null)
                {
                    tileCollider.enabled = true;
                }
            }
        }
    }

    void KeepPlayerInBounds()
    {
        Vector3 playerPosition = player.transform.position;

        player.transform.position = new Vector3(
            Mathf.Clamp(playerPosition.x, 0, (gridLength - 1) * tileSpacing),
            playerPosition.y,
            Mathf.Clamp(playerPosition.z, 0, (gridLength - 1) * tileSpacing)
        );
    }

    void CheckLevelCompletion()
    {
        bool allBlueSteppedOn = true;

        foreach (var tile in allTiles)
        {
            if (tile.type == Tile.TileType.Blue && !tile.isSteppedOn)
            {
                allBlueSteppedOn = false;
                break;
            }
        }

        if (allBlueSteppedOn)
        {
            StartCoroutine(ShowLevelCompletedAndGoToLevelSelect());
        }
    }

    private IEnumerator ShowLevelCompletedAndGoToLevelSelect()
    {
        levelCompletedPanel.SetActive(true);
        levelCompletedText.text = "Level Completed!";
        yield return new WaitForSeconds(3f); 
        levelCompletedPanel.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }

    public void ResetProgress()
    {
        foreach (var tile in allTiles)
        {
            if (tile.type == Tile.TileType.Blue)
            {
                tile.ResetTile(); 
            }
        }
    }
}
