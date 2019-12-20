using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using KinectSimpleGesture;
using System;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;

public class BodySourceView : MonoBehaviour 
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject[] _items;
    public int _currentItem = 0;
    private List<Vector3> _originalPositions = new List<Vector3>();
    private List<Quaternion> _originalRotations = new List<Quaternion>();

    // private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private Gesture _NextGesture = new Gesture("Next");
    private Gesture _ZoomInGesture = new Gesture("ZoomIn");
    private Gesture _PrevGesture = new Gesture("Prev");
    private Gesture _ZoomOutGesture = new Gesture("ZoomOut");
    private Gesture _RotateLeftGesture = new Gesture("RotateLeft");
    private Gesture _RotateRightGesture = new Gesture("RotateRight");
    private Gesture _RotateUpGesture = new Gesture("RotateUp");
    private Gesture _RotateDownGesture = new Gesture("RotateDown");
    
    // private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    // {
    //     { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
    //     { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
    //     { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
    //     { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
    //     { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
    //     { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
    //     { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
    //     { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
    //     { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
    //     { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
    //     { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
    //     { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
    //     { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
    //     { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
    //     { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
    //     { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
    //     { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
    //     { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
    //     { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
    //     { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
    //     { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
    //     { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
    //     { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
    //     { Kinect.JointType.Neck, Kinect.JointType.Head },
    // };

    void Start()
    {
        _NextGesture.GestureRecognized += Next_GestureRecognized;
        _ZoomInGesture.GestureRecognized += ZoomIn_GestureRecognized;
        _PrevGesture.GestureRecognized += Prev_GestureRecognized;
        _ZoomOutGesture.GestureRecognized += ZoomOut_GestureRecognized;
        _RotateLeftGesture.GestureRecognized += RotateLeft_GestureRecognized;
        _RotateRightGesture.GestureRecognized += RotateRight_GestureRecognized;
        _RotateUpGesture.GestureRecognized += RotateUp_GestureRecognized;
        _RotateDownGesture.GestureRecognized += RotateDown_GestureRecognized;

        foreach (var it in _items)
        {
            _originalPositions.Add(it.transform.position);
            _originalRotations.Add(it.transform.rotation);
        }
    }

    void FixedUpdate () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();


        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                _NextGesture.Update(body);
                _PrevGesture.Update(body);
                _ZoomInGesture.Update(body);
                _ZoomOutGesture.Update(body);
                _RotateLeftGesture.Update(body);
                _RotateRightGesture.Update(body);
                _RotateUpGesture.Update(body);
                _RotateDownGesture.Update(body);

                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.material = BoneMaterial;
            lr.endWidth = 0.05f;
            lr.startWidth = 0.05f;
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.startColor = GetColorForState (sourceJoint.TrackingState);
                lr.endColor = GetColorForState (targetJoint.Value.TrackingState);   
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

    void Next_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Next gesture");
        _items[_currentItem].SetActive(false);
        _currentItem = (_currentItem + 1) % _items.Length;
        _items[_currentItem].transform.position = _originalPositions[_currentItem];
        _items[_currentItem].transform.rotation = _originalRotations[_currentItem];
        _items[_currentItem].SetActive(true); 
    }

    void Prev_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Prev Gesture");

        _items[_currentItem].SetActive(false);
        _currentItem = (_items.Length + _currentItem - 1) % _items.Length;
        _items[_currentItem].transform.position = _originalPositions[_currentItem];
        _items[_currentItem].transform.rotation = _originalRotations[_currentItem];
        _items[_currentItem].SetActive(true);
       
    }

    void RotateRight_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Rotate Right gesture");
        _items[_currentItem].transform.Rotate(Vector3.down * 25 * Time.deltaTime, Space.World);
    }

    void RotateLeft_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Rotate Left Gesture");
        _items[_currentItem].transform.Rotate(Vector3.up * 25 * Time.deltaTime, Space.World);
    }
    
    void RotateUp_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Rotate Up gesture");
        _items[_currentItem].transform.Rotate(Vector3.right * 25 * Time.deltaTime, Space.World);
    }

    void RotateDown_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Rotate Down Gesture");
        _items[_currentItem].transform.Rotate(Vector3.left * 25 * Time.deltaTime, Space.World);
    }

    void ZoomIn_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Zoom In Gesture");
        _items[_currentItem].transform.Translate(Vector3.back * Time.deltaTime, Space.World);
    }

    void ZoomOut_GestureRecognized(object sender, EventArgs e)
    {
        Debug.Log("Zoom Out Gesture");
        _items[_currentItem].transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
    }

}
