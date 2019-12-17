using UnityEngine;
using System.Collections;
using Windows.Kinect;

namespace KinectSimpleGesture
{
       public class RotateRightSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class RotateRightSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                (skeleton.Joints[JointType.HandRight].Position.X >= skeleton.Joints[JointType.ElbowRight].Position.X
                ||
                skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X)
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
    public class RotateRightSegment3 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X >= skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
