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
        string _type;

        public event EventHandler GestureRecognized;

        public Gesture(string type, int window_size)
        {
            this._type = type;
            this._window_size = window_size;

            switch (type)
            {
                case "Next":
                    _segments = new IGestureSegment[] {new NextSegment1(), new NextSegment2() };
                    break;
                case "RotateRight":
                    _segments = new IGestureSegment[] {new RotateRightSegment1(), new RotateRightSegment2()};
                    break;
                case "RotateLeft":
                    _segments = new IGestureSegment[] {new RotateLeftSegment1(), new RotateLeftSegment2()};
                    break;               
                case "Prev":
                    _segments = new IGestureSegment[] {new PrevSegment1(), new PrevSegment2()};
                    break;               
                case "ZoomIn":
                    _segments = new IGestureSegment[] {new ZoomInSegment1(), new ZoomInSegment2()};
                    break;
                case "ZoomOut":
                    _segments = new IGestureSegment[] {new ZoomOutSegment1(), new ZoomOutSegment2()};
                    break;
            }
        }

        public void Update(Body skeleton)
        {
            GesturePartResult result = _segments[_currentSegment].Update(skeleton);

            if (result == GesturePartResult.Succeeded)
            {
                Debug.Log(_type + ": Segmento " + _currentSegment.ToString() + " reconocido");
                if (_currentSegment + 1 < _segments.Length)
                {
                    _currentSegment++;
                    _frameCount = 0;
                }
                else
                {
                    if (GestureRecognized != null)
                    {
                        GestureRecognized(this, new EventArgs());
                        Reset();
                    }
                }
            }
            else {
                if (_frameCount == _window_size ||
                    (_currentSegment > 0 && _segments[_currentSegment -1].Update(skeleton) != GesturePartResult.Succeeded))
                    {
                        Debug.Log(_type + ": Segmento " + _currentSegment.ToString() + " fallido.");

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
