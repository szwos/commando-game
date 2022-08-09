using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSprite : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        List<Color> colors = new List<Color>();
        colors.Add(Color.white);//1
        colors.Add(Color.blue);//2
        colors.Add(Color.black);//3
        colors.Add(Color.red);//4
        colors.Add(Color.green);//5
        colors.Add(Color.cyan);//6
        colors.Add(Color.grey);//7
        colors.Add(Color.magenta);//8
        colors.Add(Color.yellow);//9
        colors.Add(Color.clear);//10

        SpriteRenderer spr = GetComponent<SpriteRenderer>();


       

        Texture2D newTex = new Texture2D(10, 512);
        newTex.filterMode = FilterMode.Point;
        VoronoiDiagram voronoiDiagram = new VoronoiDiagram(newTex.width, newTex.height, 10, false);


        for (int i = 0; i < newTex.width; i++)
        {
            for (int j = 0; j < newTex.height; j++)
            {
                    newTex.SetPixel(i, j, colors[voronoiDiagram.finalGrid[i, j] - 1]);   
            }
        }


        newTex.Apply();


        Sprite newSprite = Sprite.Create(newTex, new Rect(0.0f, 0.0f, newTex.width, newTex.height), new Vector2(0.5f, 0.5f), 100.0f);


        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        



}

}
