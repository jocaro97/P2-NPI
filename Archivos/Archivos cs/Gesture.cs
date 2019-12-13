using Microsoft.Kinect;
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

        public Gesture(IGestureSegment[] segments, int window_size)
        {
            _window_size = window_size;
            _segments = segments;
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
