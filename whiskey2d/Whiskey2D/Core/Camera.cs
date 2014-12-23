using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    [Serializable]
    public class Camera
    {


        public Camera()
        {
            Size = new Vector(1280, 720);
            Position = Vector.Zero;
            Zoom = 1;
        }


        public float getCameraUnits(float regular)
        {
            return regular * Zoom;
        }
        public Vector getCameraUnits(Vector regular)
        {
            return new Vector(getCameraUnits(regular.X), getCameraUnits(regular.Y));
        }

        public Vector getGameCoordinate(Vector screenCoord)
        {
            Vector converted = Vector2.Transform(screenCoord, buildBackwardsTransform());
            return converted;
        }
        public Vector getScreenCoordinate(Vector gameCoord)
        {
            return Vector2.Transform(gameCoord, buildTransform());
        }

        public void reset(Vector position, float zoom, float rotation)
        {
            Position = position;
            Zoom = zoom;
            Rotation = rotation;
        }
        public void reset()
        {
            reset(Vector.Zero, 1, 0);
        }

        public Vector Size
        {
            get;
            set;
        }

        public Vector TruePosition
        {

            get
            {
                return round(getGameCoordinate(Size / 2));
            }

            set
            {
                float oldZoom = Zoom;
                origin = Vector.Zero;
                position = -value + Size/2;
                updateTransform();
            }
        }

        private Vector position;
        public Vector Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                updateTransform();
            }
        }

        private Vector origin;
        public Vector Origin
        {
            get
            {
                return origin;
            }
            set
            {


                //Vector2 originNew = GetGameLocation(screenLocation);
                //_origin = originNew;
                //UpdateTranslation();
                //originNew = GetScreenLocation(_origin);
                //_position += (screenLocation - originNew);
                //UpdateTranslation();

                Vector originNew = getGameCoordinate(value);
                origin = originNew;
                updateTransform();
                originNew = getScreenCoordinate(origin);
                position += (value - originNew);
                updateTransform();


                //origin = value;
                //updateTransform();
                
            }
        }

        private float zoom;
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                zoom = (float) Math.Round(zoom, 3);
                if (zoom < .001f) zoom = .001f;

                updateTransform();
            }
        }

        float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                updateTransform();
            }
        }

        [NonSerialized]
        private Matrix transform;


        public Matrix TranformMatrix
        {
            get
            {
                if (transform == null)
                    transform = buildTransform();

                return transform;
            }
        }


        private Vector3 toVec3(Vector vec)
        {
            return new Vector3(vec.X, vec.Y, 0);
        }

        private void updateTransform()
        {
            transform = buildTransform();
        }

        private Vector2 round(Vector2 v2)
        {
            return new Vector2((float)Math.Round((double)v2.X), (float)Math.Round((double)v2.Y));
        }

        private Matrix buildTransform()
        {


            //position = round(position);
            //origin = round(origin);
            //zoom = (float)Math.Round((float)zoom, 1);

            Matrix t = Matrix.Identity
            
                
            

            * Matrix.CreateTranslation(toVec3(-origin))
            //t *= Matrix.CreateRotationZ(Rotation);
            * Matrix.CreateScale(zoom)

            * Matrix.CreateTranslation(toVec3(position))
            * Matrix.CreateTranslation(toVec3(origin))

            
            ;
            
           
            

            return t;
        }

        private Matrix buildBackwardsTransform()
        {

            //position = round(position);
            //origin = round(origin);
            //zoom = (float)Math.Round((float)zoom, 1);

            Matrix t = Matrix.Identity

            * Matrix.CreateTranslation(toVec3(-position))
            * Matrix.CreateTranslation(toVec3(-origin))
            
            //t *= Matrix.CreateRotationZ(-Rotation);
            * Matrix.CreateScale(1 / zoom)
            
            * Matrix.CreateTranslation(toVec3(origin))

            
            
            ;

            

            return t;
        }

    }
}
