using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 


namespace SpaceMarauders.Utilities
{
    public static class MathHelper
    {
        public static float CurveAngle(float from, float to, float step)
        {
            if (step == 0)
            {
                return from;
            }

            if (from == to || step == 1) return to;

            Vector2 fromVector = new Vector2((float)Math.Cos(from), (float)Math.Sin(from));
            Vector2 toVector = new Vector2((float)Math.Cos(to), (float)Math.Sin(to));

            Vector2 currentVector = Slerp(fromVector, toVector, step);

            return (float)Math.Atan2(currentVector.Y, currentVector.X);
        }

        public static Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            double theta = Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            double sinTheta = Math.Sin(theta);
            return (float)(Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(Math.Sin(step * theta) / sinTheta) * to;
        }

        public static float RotationFromVector2(Vector2 v1, Vector2 v2)
        {
            Vector2 direction = v1 - v2;
            direction.Normalize();

            float angle = (float)Math.Atan2(direction.Y, direction.X);
            return angle; 
        }

        public static Vector2 Vec2ToEntitySpace(Vector2 offSet, Vector2 position, float rotation)
        {

            Matrix rotationMatrix = Matrix.CreateRotationZ(rotation);

            Vector2 _pos = offSet;

            // REMEMBER THAT THE POSITON = CENTER
            return _pos = (position) + Vector2.Transform(_pos, rotationMatrix);


        }

        public static Vector2 CenterOfImage(Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2); 
        }
    }
}
