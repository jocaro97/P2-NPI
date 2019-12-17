using System;
using Windows.Kinect;
using UnityEngine;
using System.Collections;

namespace KinectSimpleGesture
{
    public interface IGestureSegment
    {
        GesturePartResult Update(Body skeleton);
    }

    public class Gesture
    {

        int _window_size;
        IGestureSegment[] _segments;
        int _currentSegment = 0;
        int _frameCount = 0;

        bool _motionController = false;
        string _type;

        public event EventHandler GestureRecognized;

        public Gesture(string type)
        {
            this._type = type;

            switch (type)
            {
                case "Next":
                    this._window_size = 20;
                    this._segments = new IGestureSegment[] { new NextSegment1(), new NextSegment2() , new NextSegment3()};
                    break;
                case "Prev":
                    this._window_size = 20;
                    this._segments = new IGestureSegment[] { new PrevSegment1(), new PrevSegment2() , new PrevSegment3()};
                    break;
                case "RotateLeft":
                    this._motionController = true;
                    this._segments = new IGestureSegment[] {new RotateLeftSegment1(), new RotateLeftSegment2()};
                    break;
                case "RotateRight":
                    this._motionController = true;
                    this._segments = new IGestureSegment[] {new RotateRightSegment1(), new RotateRightSegment2()};
                    break;               
                case "ZoomIn":
                    this._motionController = true;
                    this._segments = new IGestureSegment[] {new ZoomInSegment1(), new ZoomInSegment2()};
                    break;               
                case "ZoomOut":
                    this._motionController = true;
                    this._segments = new IGestureSegment[] {new ZoomInSegment1(), new ZoomInSegment2()};
                    break;
            }
        }

        public void Update(Body skeleton)
        {
            GesturePartResult result = _segments[_currentSegment].Update(skeleton);

            if (result == GesturePartResult.Succeeded)
            {   
                Debug.Log(_type + ": Segmento " + _currentSegment.ToString() + " reconocido");
                _currentSegment++;

                if(_motionController)
                {
                    if(_currentSegment == _segments.Length)
                    {
                        _currentSegment--;

                        if (GestureRecognized != null)
                        {
                            GestureRecognized(this, new EventArgs());
                        }
                    }                    
                }
                else
                {
                    if (_currentSegment == _segments.Length)
                    {                    
                        if (GestureRecognized != null)
                        {
                            GestureRecognized(this, new EventArgs());
                            Reset();
                        }
                    }
                    else
                    {
                        _frameCount = 0;
                    }
                }
            }
            else {
                Debug.Log(_type + ": Segmento " + _currentSegment.ToString() + " no reconocido");
                if (_frameCount == _window_size || (_currentSegment > 0 && _segments[_currentSegment -1].Update(skeleton) != GesturePartResult.Succeeded))
                {
                    // Debug.Log(_type + ": Segmento " + _currentSegment.ToString() + " fallido.");
                    Reset();
                }
                else
                {
                    // Debug.Log(_type + ": Segmento " + _currentSegment.ToString() + " no reconocido, frame_count: " + _frameCount);
                    _frameCount++;
                }
            } 
        }

        public void Reset()
        {
            _currentSegment = 0;
            _frameCount = 0;
        }
    }
}
