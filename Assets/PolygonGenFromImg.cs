using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonCWSetOfPoints
{
    public List<Vector2> ListOfPoints;
    public PolygonCWSetOfPoints()
    {
        ListOfPoints = new List<Vector2>();
    }
}


public class PolygonGenFromImg : MonoBehaviour
{
    Texture2D _texture2d; 	// Use this for initialization
    Dictionary<int, PolygonCWSetOfPoints> _idxToPolygonDict;
    Vector2 _cursor;
    public Transform PointPrefab;
    public Transform SimplyPolygonPrefab;
    public Transform ParentOfPolygons;
    public int GridOfCuttingSize = 3;
	void Start ()
    {
        _texture2d = GetComponent<SpriteRenderer>().sprite.texture;
        _idxToPolygonDict = new Dictionary<int, PolygonCWSetOfPoints>();
        int widthOfGrid;
        int heightOfGrid;
        widthOfGrid = Mathf.CeilToInt(_texture2d.width / GridOfCuttingSize);
        heightOfGrid = Mathf.CeilToInt(_texture2d.height / GridOfCuttingSize);
        for(int i= 0;i<widthOfGrid; i++)
        {
            for(int j=0;j<heightOfGrid;j++)
            {
                _idxToPolygonDict[j * widthOfGrid + i] = new PolygonCWSetOfPoints();
            }
        }
        
        
        for(int i=0;i<widthOfGrid;i++)
        {
            for(int j=0;j< heightOfGrid; j++)
            {
                SetUpPolygonCWSet(i, j, GridOfCuttingSize, _idxToPolygonDict[j * widthOfGrid + i]);
            }
        }

        Debug.Log(_idxToPolygonDict.Count);


        MakeSimplePolygons(_idxToPolygonDict, widthOfGrid, heightOfGrid);

    }

    void GetPointsObjAndSetUpSimplyPolygon(Vector2 []vec2, Transform simplyPolygon)
    {
        Transform[] objArray;
        objArray = new Transform[vec2.Length];
        for(int i=0;i< vec2.Length;i++)
        {
            objArray[i] = Instantiate(PointPrefab, new Vector3(vec2[i].x,vec2[i].y,0), transform.rotation, simplyPolygon);
        }
        System.Array.Reverse(objArray);
        simplyPolygon.GetComponent<Triangulator>().points =  objArray;
    }

    void MakeSimplePolygons(Dictionary<int,PolygonCWSetOfPoints> idxToPolyg, int widthOfGrid,int heightOfGrid)
    {
        List<Transform> PointsTransformTmpList;
        PointsTransformTmpList = new List<Transform>();


        for (int i = 0; i < widthOfGrid; i++)
        {
            for (int j = 0; j < heightOfGrid; j++)
            {
                if (idxToPolyg[j * widthOfGrid + i].ListOfPoints.Count != 0)
                {
                    Transform obj = Instantiate(SimplyPolygonPrefab, new Vector3(0, 0, 0), transform.rotation, ParentOfPolygons);
                    obj.name = string.Format("{0},{1}", i, j);
                    GetPointsObjAndSetUpSimplyPolygon(idxToPolyg[j * widthOfGrid + i].ListOfPoints.ToArray(), obj);
                }
            }
        }
    }

    void SetUpPolygonCWSet(int x,int y,int gridOfCuttingSize,PolygonCWSetOfPoints polygonCwSet)
    {
        Vector2 gridCursor;
        Vector2 gridStepCursor;
        gridCursor = new Vector2(-0.5f, -0.5f);
        gridCursor += new Vector2(x * gridOfCuttingSize, y * gridOfCuttingSize);
        gridStepCursor = gridCursor;
        
        if(y * 33 + x == 175)
        {
            Debug.Log("error?");
        }
        

        //up side 
        if(_texture2d.GetPixel((int)gridCursor.x,(int)gridCursor.y).grayscale < 0.5f)
        {
            polygonCwSet.ListOfPoints.Add(gridCursor);
        }
        for(int i=0;i<gridOfCuttingSize;i++)
        {
            gridStepCursor += new Vector2(1f, 0f);
            if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale != _texture2d.GetPixel((int)gridStepCursor.x, (int)gridStepCursor.y).grayscale)
            {
                polygonCwSet.ListOfPoints.Add(gridCursor + new Vector2(0.5f, 0f));
            }
            gridCursor = gridStepCursor;         
        }
        if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale < 0.5f)
        {
            polygonCwSet.ListOfPoints.Add(gridCursor);
        }

        //right side
        for (int i = 0; i < gridOfCuttingSize; i++)
        {
            gridStepCursor += new Vector2(0f, -1f);
            if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale != _texture2d.GetPixel((int)gridStepCursor.x, (int)gridStepCursor.y).grayscale)
            {
                polygonCwSet.ListOfPoints.Add(gridCursor + new Vector2(0.0f, -0.5f));
            }
            gridCursor = gridStepCursor;
        }
        if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale < 0.5f)
        {
            polygonCwSet.ListOfPoints.Add(gridCursor);
        }

        //button side
        for (int i = 0; i < gridOfCuttingSize; i++)
        {
            gridStepCursor += new Vector2(-1f, 0f);
            if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale != _texture2d.GetPixel((int)gridStepCursor.x, (int)gridStepCursor.y).grayscale)
            {
                polygonCwSet.ListOfPoints.Add(gridCursor + new Vector2(-0.5f, 0f));
            }
            gridCursor = gridStepCursor;
        }
        if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale < 0.5f)
        {
            polygonCwSet.ListOfPoints.Add(gridCursor);
        }


        //left side
        for (int i = 0; i < gridOfCuttingSize; i++)
        {
            gridStepCursor += new Vector2(0f, 1f);
            if (_texture2d.GetPixel((int)gridCursor.x, (int)gridCursor.y).grayscale != _texture2d.GetPixel((int)gridStepCursor.x, (int)gridStepCursor.y).grayscale)
            {
                polygonCwSet.ListOfPoints.Add(gridCursor + new Vector2(0.0f, 0.5f));
            }
            gridCursor = gridStepCursor;
        }



    }


    // Update is called once per frame
    void Update ()
    {
		
	}
}
