using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is moving a boomerang based on a dataset collected .


public class reproduceData : MonoBehaviour
{
    public string fileName1; //dataset1
    public string fileName2; //dataset2

    public bool isPlay = false; 
    public bool isDataset1 = false;
    public bool isDataset2 = false;

    private CsvRead csvData;
    public GameObject Play_button;
    public changeText script_play;

    List<double> phi = new List<double>();
    List<double> theta = new List<double>();
    List<double> psi = new List<double>();

    private int frameCount;
    private const int EndData = -99;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize
        csvData = gameObject.AddComponent<CsvRead>();
        Play_button = GameObject.Find("PlayButton");
        script_play = Play_button.GetComponent<changeText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (true==isPlay)
        {
            if (EndData != frameCount)
            {
                //Debug.Log("phi: " + phi[frameCount]);
                //Debug.Log("theta: " + theta[frameCount]);
                //Debug.Log("psi: " + psi[frameCount]);
                //in Unity, ZXY euler, while ZYX euler in Matlab
                // in Unity 
                try
                {
                    //rotate using phi, theta and psi caalculated by Madgwick
                    transform.rotation = Quaternion.Euler((float)-theta[frameCount], (float)-phi[frameCount], (float)-psi[frameCount]);
                }
                catch
                {
                    frameCount = EndData;
                    isPlay = false;
                    Play_button.GetComponentInChildren<Text>().text = "start";
                    Debug.Log("range out!!");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                if (EndData != frameCount)
                {
                    frameCount++;
                }
            }
        }
    }

    //perform if a user push a dataset1 button
    public void selectData1()
    {
        if (true == isDataset2 && false == isDataset1)
        {
            isDataset2 = false;
            csvData.clearData();
            phi.Clear();
            theta.Clear();
            psi.Clear();
            Debug.Log("data clear!");
        }
        else if (true == isDataset1)
        {
            return;
        }
        csvData.csvRead(fileName1);
        for (int i = 0; i < csvData.csvDatas.Count; i++)
        {
            //Debug.Log("csv data[0]: " + csvData.csvDatas[i][0]);
            //Debug.Log("[1]: "+csvData.csvDatas[i][1]);
            //Debug.Log("[2]: " + csvData.csvDatas[i][2]);
            phi.Add(double.Parse(csvData.csvDatas[i][0]));
            theta.Add(double.Parse(csvData.csvDatas[i][1]));
            psi.Add(double.Parse(csvData.csvDatas[i][2]));
        }
        isDataset1 = true;
    }

    //perform if a user push a dataset2 button
    public void selectData2()
    {
        if (true == isDataset1 && false == isDataset2)
        {
            isDataset1 = false;
            csvData.clearData();
            phi.Clear();
            theta.Clear();
            psi.Clear();
            Debug.Log("data clear!");
        }
        else if (true == isDataset2)
        {
            return;
        }
        csvData.csvRead(fileName2);
        for (int i = 0; i < csvData.csvDatas.Count; i++)
        {
            //Debug.Log("csv data[0]: " + csvData.csvDatas[i][0]);
            //Debug.Log("[1]: "+csvData.csvDatas[i][1]);
            //Debug.Log("[2]: " + csvData.csvDatas[i][2]);
            phi.Add(double.Parse(csvData.csvDatas[i][0]));
            theta.Add(double.Parse(csvData.csvDatas[i][1]));
            psi.Add(double.Parse(csvData.csvDatas[i][2]));
        }
        isDataset2 = true;
    }

    //perform if a user push a play button
    public void clicPlayButton()
    {
        if (true == isDataset1||true==isDataset2)
        {
            if (false == isPlay)
            {
                if (EndData == frameCount)
                {
                    frameCount = 0;
                }
                isPlay = true;
            }
            else
            {
                isPlay = false;
            }
        }
    }

    //return isPlay value
    public bool get_isPlay()
    {
        Debug.Log("get_isPlay");
        return isPlay;
    }

    //return a number which button selected
    public int get_whichSelectedData()
    {
        if (isDataset1)
        {
            return 1;
        }
        else if (isDataset2)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
