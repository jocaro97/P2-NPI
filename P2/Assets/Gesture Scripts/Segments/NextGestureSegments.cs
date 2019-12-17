using UnityEngine;
using System.Collections;
using Windows.Kinect;

namespace KinectSimpleGesture
{
   
    public class NextSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.HandRightState == HandState.Open
                &&
                skeleton.Joints[JointType.HandTipRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y < skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class NextSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (                
                skeleton.HandRightState == HandState.Open
                &&
                skeleton.Joints[JointType.HandTipRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y < skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class NextSegment3 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.HandRightState == HandState.Open
                &&
                skeleton.Joints[JointType.HandTipRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X - 0.05
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y < skeleton.Joints[JointType.SpineMid].Position.Y
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
