using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Points : MonoBehaviour {

    List<List<Vector2>> pointsFromImg;
    public bool isReady;
    void SetPoints(List<List<Vector2>> lst)
    {
        pointsFromImg = lst;
    }

    public IndexableCyclicalLinkedList<Vertex> convertToInxCyclLst(int withPoly)
    {
        IndexableCyclicalLinkedList<Vertex> lst;
        lst = new IndexableCyclicalLinkedList<Vertex>();
        for(int i=0;i<pointsFromImg[withPoly].Count;i++)
        {
            lst.AddLast(new Vertex(pointsFromImg[withPoly][i], i));
        }
        return lst;
    }


	// Use this for initialization



	void Start () {
        isReady = false;
        Save();
		List<List<Vector2>> listVertices2D = new List<List<Vector2>>();

		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		int parametrWidth = 1;
		int parametrHeight = 1;
		int width = sprite.sprite.texture.width / parametrWidth;
		int height = sprite.sprite.texture.height / parametrHeight;
		for (int i = 0; i < parametrWidth; i++) {
			for (int j = 0; j < parametrHeight; j++) {
				List<Vector2> top = new List<Vector2>();
				List<Vector2> down = new List<Vector2>();
				for (int x = 0; x < width; x++) {
					int count = 0;
					for (int y = 0; y < height; y++) {

						Color colorm1 = sprite.sprite.texture.GetPixel (x + i * width, y + j * height - 1);
						Color color = sprite.sprite.texture.GetPixel (x + i * width, y + j * height);

						if (y == 0 && color.r == 0) {
							Vector2 a = new Vector2 (x + i * width, y + j * height);
							top.Add (a);
							count++;
						}
						if (y == height-1 && color.r == 0) {
							Vector2 a = new Vector2 (x + i * width, y + j * height);
							down.Add (a);
							break;
						}
						if (color.r != colorm1.r ) {
							Vector2 a = new Vector2 (x + i * width, y + j * height);
							if (count == 0) {
								top.Add (a);
								count++;
							} else if (count == 1){
								down.Add (a);
								break;
							}
						}
					}
				}
				down.Reverse ();
				top.AddRange(down);
				listVertices2D.Add (top);
                SetPoints(listVertices2D);
                isReady = true;
			}
		}

		/* for (int i = 0; i < listVertices2D [0].Count; i++) {
			Debug.Log (listVertices2D [0][i]);
		} */
	}

    public void Save()
    {
        Texture2D newTexture = new Texture2D(100, 100, TextureFormat.ARGB32, false);

        for(int i=0;i<100; i++)
        {
            for(int j = 0; j < 100; j++)
            {
                int change = (int)Random.Range(0, 19);
                if (change < 10)
                {
                    newTexture.SetPixel(i, j, new Color(255, 255, 255));
                }
                else {
                    newTexture.SetPixel(i, j, new Color(0, 0, 0));
                }
            }
        }
        newTexture.Apply();
        byte[] bytes = newTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../Assets/plac"+".png", bytes);
        //Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
       
    }



    // Update is called once per frame
    void Update () {
		
	}
}

	