using UnityEngine;
using System.Collections;
using Windows.Kinect;

namespace KinectSimpleGesture
{
  
    public class ZoomOutSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.Z > skeleton.Joints[JointType.ElbowRight].Position.Z - 0.2
                &&
                skeleton.Joints[JointType.HandRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandLeft].Position.Z > skeleton.Joints[JointType.ElbowLeft].Position.Z - 0.2
                &&
                skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class ZoomOutSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z - 0.2
                &&
                skeleton.Joints[JointType.HandRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z - 0.2
                &&
                skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}