using UnityEngine;
using System.Collections;
using Windows.Kinect;

namespace KinectSimpleGesture
{
  
    public class ZoomInSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X <= skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandRight].Position.Y - skeleton.Joints[JointType.HandLeft].Position.Y < 0.2
                &&
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.X >= skeleton.Joints[JointType.ElbowLeft].Position.X
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class ZoomInSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Body skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X
                &&                        
                skeleton.Joints[JointType.HandRight].Position.Y - skeleton.Joints[JointType.HandLeft].Position.Y < 0.2
                &&         
                skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                &&
                skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.SpineMid].Position.Y
                &&
                skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X
                )
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
