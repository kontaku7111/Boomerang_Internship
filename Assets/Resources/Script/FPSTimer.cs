using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This scripit display FPS each frame

public class FPSTimer : MonoBehaviour
{
    public float FPS { get; private set; }

    static readonly float INTERVAL = 0.5f;
    float m_prev_time;
    int m_frame_count;

    void Start()
    {
        FPS = 60.0f;
        m_prev_time = 0.0f;
        m_frame_count = 0;
    }
    void Update()
    {
        ++m_frame_count;
        float diff_time = Time.realtimeSinceStartup - m_prev_time;
        if (INTERVAL > diff_time) { return; }
        FPS = ((float)m_frame_count / diff_time);
        m_frame_count = 0;
        m_prev_time = Time.realtimeSinceStartup;
        //change text attached game object
        this.GetComponent<Text>().text = "FPS: "+FPS ;

    }
}
