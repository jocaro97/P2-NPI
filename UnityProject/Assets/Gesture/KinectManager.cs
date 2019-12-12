using KinectSimpleGesture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectManager : MonoBehaviour
{
    private KinectSensor sensor;
    private MultiSourceFrame reader;
    
    // Start is called before the first frame update
    static KinectSimpleGesture.NextSegment1 nextsegment1 = new NextSegment1();
    static KinectSimpleGesture.NextSegment2 nextsegment2 = new NextSegment2();

    static KinectSimpleGesture.IGestureSegment[] segmentos = { nextsegment1, nextsegment2 };
    static KinectSimpleGesture.Gesture change = new Gesture(segmentos, 50);
    void Start()
    {
      
        this.sensor = KinectSensor.GetDefault();
        if (sensor != null)
        {
            /*
            reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color |
                                             FrameSourceTypes.Depth |
                                             FrameSourceTypes.Infrared |
                                             FrameSourceTypes.Body);
            reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            */
            change.GestureRecognized += Gesture_GestureRecognized;

            this.sensor.Open();
        }

        Console.ReadKey();
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
    static void Gesture_GestureRecognized(object sender, EventArgs e)
    {
        Console.WriteLine("You just waved!");
    }

}
