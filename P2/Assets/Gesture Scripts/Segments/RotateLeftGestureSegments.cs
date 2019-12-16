using UnityEngine;
using System.Collections;
using Windows.Kinect;

namespace KinectSimpleGesture
{
  
    public class RotateLeftSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class RotateLeftSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
    public class RotateLeftSegment3 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.SpineMid].Position.X
                &&
                skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

}
