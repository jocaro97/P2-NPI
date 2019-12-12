
using Microsoft.Kinect;

namespace KinectSimpleGesture
{
    public interface IGestureSegment
    {
        GesturePartResult Update(Skeleton skeleton);
    }

    public class NextSegment1 : IGestureSegment
    {
        public GesturePartResult Update(Skeleton skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }

    public class NextSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Skeleton skeleton)
        {
            if (
                skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ElbowRight].Position.X
                &&
                skeleton.Joints[JointType.HandTipRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed;
        }
    }
}
