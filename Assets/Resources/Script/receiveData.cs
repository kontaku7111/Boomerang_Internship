using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;





public class receiveData : MonoBehaviour
{
    // These are variables stored sensor data
    public List<double> xAcc = new List<double>();
    public List<double> yAcc = new List<double>();
    public List<double> zAcc = new List<double>();
    public List<double> xGyro = new List<double>();
    public List<double> yGyro = new List<double>();
    public List<double> zGyro = new List<double>();
    public List<double> xMag = new List<double>();
    public List<double> yMag = new List<double>();
    public List<double> zMag = new List<double>();
    List<double> phi = new List<double>();
    List<double> theta = new List<double>();
    List<double> psi = new List<double>();

    public float norm = 0;

    //dataset
    public string acc_dataset; //dataset of accelerometer
    public string gyro_dataset; //dataset of accelerometer
    public string mag_dataset; //dataset of accelerometer
    public string euler_dataset;

    private CsvRead csvData;
    public GameObject Play_button;
    public GameObject RotationSpeed;
    public GameObject ThrowingSpeed;
    public GameObject BoomerangStatus;
    public GameObject Inclination;
    public changeText script_play;

    // whether bluetooth is used
    bool isBluetooth = false;
    public bool isPlay = false;
    public bool isFlight = false;
    public bool isThrow = false;

    private int frame=0;
    private int update_frequency = 1;
    public double maxSpeed = 0;
    private static int EoD = -99;

    // Start is called before the first frame update
    void Start()
    {
        RotationSpeed = GameObject.Find("RotationSpeed");
        BoomerangStatus = GameObject.Find("BoomerangStatus");
        Inclination = GameObject.Find("Inclination");
        ThrowingSpeed = GameObject.Find("ThrowingSpeed");
        if (isBluetooth)
        {   
            //setting to receive data sent from device
        }
        else
        {
            //Initialize
            csvData = gameObject.AddComponent<CsvRead>();
            Play_button = GameObject.Find("PlayButton");
            script_play = Play_button.GetComponent<changeText>();
            frame = 0;

            //read sensor data from csv file
            csvData.csvRead(acc_dataset);
            for (int i = 0; i < csvData.csvDatas.Count; i++)
            {
                //Debug.Log("csv data[0]: " + csvData.csvDatas[i][0]);
                //Debug.Log("[1]: "+csvData.csvDatas[i][1]);
                //Debug.Log("[2]: " + csvData.csvDatas[i][2]);
                xAcc.Add(double.Parse(csvData.csvDatas[i][0]));
                yAcc.Add(double.Parse(csvData.csvDatas[i][1]));
                zAcc.Add(double.Parse(csvData.csvDatas[i][2]));
            }
            csvData.clearData();
            csvData.csvRead(gyro_dataset);
            for (int i = 0; i < csvData.csvDatas.Count; i++)
            {
                //Debug.Log("csv data[0]: " + csvData.csvDatas[i][0]);
                //Debug.Log("[1]: "+csvData.csvDatas[i][1]);
                //Debug.Log("[2]: " + csvData.csvDatas[i][2]);
                xGyro.Add(double.Parse(csvData.csvDatas[i][0]));
                yGyro.Add(double.Parse(csvData.csvDatas[i][1]));
                zGyro.Add(double.Parse(csvData.csvDatas[i][2]));
            }
            csvData.clearData();
            csvData.csvRead(mag_dataset);
            for (int i = 0; i < csvData.csvDatas.Count; i++)
            {
                //Debug.Log("csv data[0]: " + csvData.csvDatas[i][0]);
                //Debug.Log("[1]: "+csvData.csvDatas[i][1]);
                //Debug.Log("[2]: " + csvData.csvDatas[i][2]);
                xMag.Add(double.Parse(csvData.csvDatas[i][0]));
                yMag.Add(double.Parse(csvData.csvDatas[i][1]));
                zMag.Add(double.Parse(csvData.csvDatas[i][2]));
            }
            csvData.clearData();
            csvData.csvRead(euler_dataset);
            for (int i = 0; i < csvData.csvDatas.Count; i++)
            {
                //Debug.Log("csv data[0]: " + csvData.csvDatas[i][0]);
                //Debug.Log("[1]: "+csvData.csvDatas[i][1]);
                //Debug.Log("[2]: " + csvData.csvDatas[i][2]);
                phi.Add(double.Parse(csvData.csvDatas[i][0]));
                theta.Add(double.Parse(csvData.csvDatas[i][1]));
                psi.Add(double.Parse(csvData.csvDatas[i][2]));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (true == isPlay)
        {
            if (EoD != frame)
            {
                //Debug.Log("phi: " + phi[frameCount]);
                //Debug.Log("theta: " + theta[frameCount]);
                //Debug.Log("psi: " + psi[frameCount]);
                //in Unity, ZXY euler, while ZYX euler in Matlab
                // in Unity 
                try
                {
                    norm = (float) (xAcc[frame] * xAcc[frame] + yAcc[frame] * yAcc[frame] + zAcc[frame] * zAcc[frame]);
                    if (Mathf.Sqrt(norm)>10)
                    {
                        // Flight
                        if (60 == update_frequency)
                        {
                            // Display rotation speed of boomerang
                            RotationSpeed.GetComponent<Text>().text = "Rotation speed: " + Mathf.Abs((float)zGyro[frame]) + "degree/s";
                            //update once in 6 frame
                            update_frequency = 1;
                        }
                        
                        // Display rotation speed of boomerang
                        RotationSpeed.GetComponent<Text>().text = "Rotation speed: " + Mathf.Abs((float)zGyro[frame]) + "degree/s";
                        
                        // Detect throwing speed
                        if (maxSpeed < Mathf.Abs((float)zGyro[frame]))
                        {
                            maxSpeed = zGyro[frame];
                        }else if(maxSpeed> Mathf.Abs((float)zGyro[frame]))
                        {
                            isThrow = true;
                            ThrowingSpeed.GetComponent<Text>().text = "Throwing speed: " + Mathf.Abs((float)maxSpeed) + "degree/s";
                        }

                        if (!isFlight)
                        {
                            isFlight = true;
                            // Display boomerang status;
                            BoomerangStatus.GetComponent<Text>().text = "Boomerang status: Flight";
                        }
                        update_frequency++;
                    }
                    else
                    {
                        // Holding boomerang
                        //rotate using phi, theta and psi caalculated by Madgwick
                        transform.rotation = Quaternion.Euler((float)-phi[frame], (float)-theta[frame], (float)-psi[frame]);
                        // Display inclination to horizontality
                        Inclination.GetComponent<Text>().text = "Inclination to horizontality: "+(int)Mathf.Abs((float)zMag[frame])+"°";
                        if (isFlight)
                        {
                            isFlight = false;
                            // Display boomerang status
                            BoomerangStatus.GetComponent<Text>().text = "Boomerang status: Holding";
                            // Initialize
                            maxSpeed = 0;
                        }
                    }
                }
                catch
                {
                    frame = EoD;
                    isPlay = false;
                    Play_button.GetComponentInChildren<Text>().text = "start";
                    Debug.Log("range out!!");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                if (EoD != frame)
                {
                    frame++;
                }
            }
        }
        else
        {

        }
    }

    //perform if a user push a play button
    public void clicPlayButton()
    {
        if (false == isPlay)
        { 
            if (EoD == frame)
            {
                frame = 0;
            }
            isPlay = true;
        }
        else
        {
            isPlay = false;
        }
    }

    //return isPlay value
    public bool get_isPlay()
    {
        Debug.Log("get_isPlay");
        return isPlay;
    }
}

