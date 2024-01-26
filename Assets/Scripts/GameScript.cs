using UnityEngine;
using UnityEngine.SceneManagement;
public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private TilesScript[] tiles;
    private int emptySpaceIndex = 15;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel;
    void Start()
    {
        _camera = Camera.main;
        Shuffle();
        endPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 10)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    TilesScript thisTile = hit.transform.GetComponent<TilesScript>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                    
                }
            }
        }
        if (!_isFinished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null) 
                {
                if (a.inRightPlace)
                    correctTiles++;
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
      
                _isFinished = true;
                endPanel.SetActive(true);
            }
        }
    }

 // choose the scene to continue
   public void Continue()
   {
        SceneManager.LoadSceneAsync("Combat1");
   }

    public void Shuffle()
    {
        if (emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }

        // Perform a small number of random swaps
        int numberOfSwaps = 30;

        for (int i = 0; i < numberOfSwaps; i++)
        {
            int randomIndex1 = Random.Range(0, 15);
            int randomIndex2 = Random.Range(0, 15);

            // Swap the target positions of two tiles
            var tempPos = tiles[randomIndex1].targetPosition;
            tiles[randomIndex1].targetPosition = tiles[randomIndex2].targetPosition;
            tiles[randomIndex2].targetPosition = tempPos;

            // Swap the tiles in the array
            var tempTile = tiles[randomIndex1];
            tiles[randomIndex1] = tiles[randomIndex2];
            tiles[randomIndex2] = tempTile;
        }
    }
    public int findIndex(TilesScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] !=null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0;i < tiles.Length;i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }
}

