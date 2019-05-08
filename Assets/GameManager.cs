using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string fileName;
    private CsvRead csvData;
    private int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        //csvData = GetComponent<CsvRead>;
        //csvData.csvRead(fileName);
        //for(int i=0; i < csvData.csvDatas.Count; i++)
        //{
        //    for(int j=0;j<csvData.csvDatas[i].Length;j++)
        //    {
        //        Debug.Log("csvDatas[" + i + "][" +j+"]["+ csvData.csvDatas[i][j]+"]");
        //    }
        //}//[j]はずっと0

    }

    // Update is called once per frame
    void Update()
    {
        //if (csvData.csvDatas.Count != frameCount)
        //{
        //    transform.rotation = Quaternion.Euler(xMag[frameCount], yMag[frameCount], zMag[frameCount]);
        //    frameCount++;
        //}
    }
}
