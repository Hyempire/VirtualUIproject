using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DataPlotter : MonoBehaviour
{
    // Name of the input file, no extension
    public string inputfile;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    // Indices of colums to be assigned as a graph
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    // Full column names
    public string xName;
    public string yName;
    public string zName;

    // The prefab for the data points to be instantiated
    public GameObject PointPrefab;
    public GameObject PointHolder;

    // 아이콘들을 GameObjects 리스트로 받아오기
    public GameObject[] iconsList;
    public TextMeshProUGUI xLabel;
    public TextMeshProUGUI yLabel;
    public TextMeshProUGUI zLabel;

    private float RESCALE = 1f;
    float xMax;
    float yMax;
    float zMax;
    float xMin;
    float yMin;
    float zMin;

    // Start is called before the first frame update
    void Start()
    {
        // Set pointList to results of function Reader with argument input file
        pointList = CSVReader.Read(inputfile);
        // Log to console
        Debug.Log(pointList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);
        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + "colums in CSV.");
        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // 아이콘 리스트의 크기를, csv 파일의 데이터 개수와 같게 하고, 다르면 에러 출력
        Debug.Log(pointList.Count);
        Debug.Log(iconsList.Length);
        if (pointList.Count != iconsList.Length) throw new Exception("IconsList.Length and pointList.Count is different");

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];
        xLabel.text = xName;
        yLabel.text = yName;
        zLabel.text = zName;
        // Get maxes of each axis
        xMax = FindMaxValue(xName);
        yMax = FindMaxValue(yName);
        zMax = FindMaxValue(zName);
        // Get minimums of each axis
        xMin = FindMinValue(xName);
        yMin = FindMinValue(yName);
        zMin = FindMinValue(zName);

        //Sortfiles();
    }


    // Find Min and Max Value for normalizing positions
    private float FindMaxValue(string columnName)
    {
        // Set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);

        // Loop through Dictionary, overwrite existing maxValue if the new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }

        // Spit out the max value
        return maxValue;
    }
    private float FindMinValue(string columnName)
    {
        float minValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }

        // Spit out the min value
        return minValue;
    }

    public void Sortfiles()
    {
        // Loop through PointList
        for (var i = 0; i < pointList.Count; i++)
        {
            iconsList[i].GetComponent<Rigidbody>().drag = float.PositiveInfinity;
            iconsList[i].GetComponent<Rigidbody>().angularDrag = float.PositiveInfinity;
            iconsList[i].GetComponent<Rigidbody>().useGravity = false;

            // Get value in pointList at ith "row", in "column" Name
            // ToSingle() : convert string to float
            // Normalize position of the points : (i - min)/(max - min)
            float x = (System.Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);
            float y = (System.Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);
            float z = (System.Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

            // instantiate the prefab with coordinates defined above
            // Instantiate as gameobject variable so that it can be manipulated within loop
            // GameObject dataPoint = Instantiate(PointPrefab, new Vector3(x, y, z), Quaternion.identity);

            /* ===== dataPoint Instantiate 하는 거 대신에 아이콘 오브젝트를 좌표 평면에 위치시키는 코드 ===== */
            //GameObject dataPoint = Instantiate(iconsList[i]);
            //iconsList[i].transform.localScale = new Vector3(RESCALE, RESCALE, RESCALE);     

            // Make dataPoint child of PointHolder object
            iconsList[i].transform.parent = PointHolder.transform;

            // Relocate by parent's transform
            iconsList[i].transform.localPosition = new Vector3(x, y, z);    // prefab instantiate 안하고 있는 오브젝트 재배치하기

            // Assigns original values to dataPointName
            string dataPointName = pointList[i][xName] + " "
                                   + pointList[i][yName] + " "
                                   + pointList[i][zName];
            // Assigns name to the prefab
            iconsList[i].transform.name = dataPointName;
        }
    }

    public void ReleaseFilse()
    {
        for (var i = 0; i < pointList.Count; i++)
        {
            iconsList[i].GetComponent<Rigidbody>().drag = 0f;
            iconsList[i].GetComponent<Rigidbody>().angularDrag = 0f;
            iconsList[i].GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
