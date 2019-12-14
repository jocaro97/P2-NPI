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

        public event EventHandler GestureRecognized;

        public Gesture(string type)
        {
            switch (type)
            {
                case "Next":
                    _window_size = 50;
                    _segments = new IGestureSegment[] {new NextSegment1(), new NextSegment2() };
                    break;
                case "RotateRight":
                    _window_size = 50;
                    _segments = new IGestureSegment[] {new RotateRightSegment1(), new RotateRightSegment2()};
                    break;
                case "RotateLeft":
                    _window_size = 50;
                    _segments = new IGestureSegment[] {new RotateLeftSegment1(), new RotateLeftSegment2()};
                    break;               
                case "Prev":
                    _window_size = 50;
                    _segments = new IGestureSegment[] {new PrevSegment1(), new PrevSegment2()};
                    break;               
                case "ZoomIn":
                    _window_size = 50;
                    _segments = new IGestureSegment[] {new ZoomInSegment1(), new ZoomInSegment2()};
                    break;
                case "ZoomOut":
                    _window_size = 50;
                    _segments = new IGestureSegment[] {new ZoomOutSegment1(), new ZoomOutSegment2()};
                    break
            }
        }

        public void Update(Body skeleton)
        {
            GesturePartResult result = _segments[_currentSegment].Update(skeleton);

            if (result == GesturePartResult.Succeeded)
            {
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
            else if (result == GesturePartResult.Failed || _frameCount == _window_size)
            {
                Reset();
            }
            else
            {
                _frameCount++;
            }
        }

        public void Reset()
        {
            _currentSegment = 0;
            _frameCount = 0;
        }
    }
}
