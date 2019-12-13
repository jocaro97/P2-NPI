using KinectSimpleGesture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectManager : MonoBehaviour
{
    private KinectSensor sensor;
    private BodyFrameReader reader;
    private Body[] _Data = null;

    // Start is called before the first frame update
    static KinectSimpleGesture.NextSegment1 nextsegment1 = new NextSegment1();
    static KinectSimpleGesture.NextSegment2 nextsegment2 = new NextSegment2();

    static KinectSimpleGesture.IGestureSegment[] segmentos = { nextsegment1, nextsegment2 };
    static KinectSimpleGesture.Gesture change = new Gesture(segmentos, 50);
    void Start()
    {
      
        sensor = KinectSensor.GetDefault();
        if (sensor != null)
        {
            reader = sensor.BodyFrameSource.OpenReader();
            
            //reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            
            change.GestureRecognized += Gesture_GestureRecognized;
            if (!sensor.IsOpen)
            {
                sensor.Open();
            }
            
        }

        Console.ReadKey();
    }
    /*
    static void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
    {
        var reference = e.FrameReference.AcquireFrame();
        using (var frame = reference.BodyFrameReference.AcquireFrame()) 
        {
            if (frame != null)
            {
                Body[] bodies = new Body[frame.BodyFrameSource.BodyCount];

                frame.GetAndRefreshBodyData(bodies);


                foreach (var body in bodies)
                {
                    if (body != null)
                    {
                        change.Update(body);
                    }
                }
            }
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (reader != null)
        {
            var frame = reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[sensor.BodyFrameSource.BodyCount];
                    
                }

                frame.GetAndRefreshBodyData(_Data);
                foreach(var body in _Data)
                {
                    if(body != null)
                    {
                        change.Update(body);
                    }
                }

                frame.Dispose();
                frame = null;
            }
        }

    }
    static void Gesture_GestureRecognized(object sender, EventArgs e)
    {
        Console.WriteLine("You just CHAGE DE SCENE!");
    }

    void OnApplicationQuit()
    {
        if (reader != null)
        {
            reader.Dispose();
            reader = null;
        }

        if (sensor != null)
        {
            if (sensor.IsOpen)
            {
                sensor.Close();
            }

            sensor = null;
        }
    }

}
