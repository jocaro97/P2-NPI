using UnityEngine;
using System.Collections;
using Windows.Kinect;

namespace KinectSimpleGesture
{
    public class PrevSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.HandLeftState == HandState.Open
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y < skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class PrevSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.HandLeftState == HandState.Open
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y < skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
