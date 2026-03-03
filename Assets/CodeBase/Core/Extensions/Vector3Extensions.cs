using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Core.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 WithY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }
        
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }

        public static Vector3 WithXY(this Vector3 vector, float x, float y)
        {
            return new Vector3(x, y, vector.z);
        }
    }
}