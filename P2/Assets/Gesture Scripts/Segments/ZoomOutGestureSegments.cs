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
                skeleton.Joints[JointType.HandRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X 
                &&
                skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X 
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                &&
                skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y
                &&                 
                skeleton.Joints[JointType.HandRight].Position.Z > skeleton.Joints[JointType.ShoulderRight].Position.Z - 0.3
                &&
                skeleton.Joints[JointType.HandLeft].Position.Z > skeleton.Joints[JointType.ShoulderLeft].Position.Z - 0.3
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
                (
                skeleton.Joints[JointType.HandRight].Position.Z <= skeleton.Joints[JointType.ShoulderRight].Position.Z - 0.3
                &&
                skeleton.Joints[JointType.HandLeft].Position.Z >= skeleton.Joints[JointType.ShoulderLeft].Position.Z - 0.3
                )
                ||
                (
                skeleton.Joints[JointType.HandRight].Position.Z >= skeleton.Joints[JointType.ShoulderRight].Position.Z - 0.3
                &&
                skeleton.Joints[JointType.HandLeft].Position.Z <= skeleton.Joints[JointType.ShoulderLeft].Position.Z - 0.3)
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
