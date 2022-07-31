using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTiledBG : MonoBehaviour
{
    //TODO weighed random with possible selection in editor
    /*[System.Serializable]   
    public class TileWeightPair
    {
        GameObject tile;
        float weight;
    }
    public TileWeightPair[] tiles;
    */



    public Transform tileStartingPos;
    public int gridWidth = 1;
    public int gridHeight = 1;
    public bool startingPositionCentered = true;
    public bool disableParentSpriteRenderer = true;
    public string tilesSortingLayer = "Background";


    public GameObject[] tiles;

    


    private Vector2 tileSpacing;

    
    void Start()
    {
        tileSpacing = tiles[0].GetComponent<Renderer>().bounds.size;
        
        if(startingPositionCentered)
            tileStartingPos.position = new Vector2(-(tileSpacing.x * gridWidth / 2), -(tileSpacing.y * gridHeight / 2));


        for (int i = 0; i < gridHeight; i++)
        {
            for(int j = 0; j < gridWidth; j++)
            {
                int random = Random.Range(0, tiles.Length);

                GameObject tile = Instantiate(tiles[random], new Vector3(tileStartingPos.position.x + (j * tileSpacing.x), tileStartingPos.position.y + (i * tileSpacing.y)), Quaternion.identity) as GameObject;

                tile.GetComponent<SpriteRenderer>().sortingLayerName = tilesSortingLayer;

                tile.transform.parent = transform;
            }
        }

        if (disableParentSpriteRenderer)
            transform.GetComponent<SpriteRenderer>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
