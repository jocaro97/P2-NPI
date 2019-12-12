
using Microsoft.Kinect;

namespace KinectSimpleGesture
{
    public interface IGestureSegment
    {
        GesturePartResult Update(Skeleton skeleton);
    }

    public class PrevSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Skeleton skeleton)
        {
            if (
                skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class PrevSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Skeleton skeleton)
        {
            if (
                skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ElbowLeft].Position.X
                &&
                skeleton.Joints[JointType.HandTipLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
