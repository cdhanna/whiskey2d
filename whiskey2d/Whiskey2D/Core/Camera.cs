using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Whiskey2D.Core
{
    /// <summary>
    /// A Camera controls the position and zoom of the view into the game world. 
    /// </summary>
    [Serializable]
    public class Camera
    {

        /// <summary>
        /// Create a new camera with default values. 
        /// </summary>
        public Camera()
        {
            Size = new Vector(1280, 720);
            
            Position = Vector.Zero;
            ZoomMin = .1f;
            ZoomMax = 2.5f;
            
            Zoom = 1;
            buildTransform();
        }

        /// <summary>
        /// Convert a regular value into camera values by multiplying the camera's current zoom
        /// </summary>
        /// <param name="regular">a number</param>
        /// <returns>a number modified by the camera's zoom</returns>
        public float getCameraUnits(float regular)
        {
            return regular * Zoom;
        }

        /// <summary>
        /// Convert a regular vector into a camera vector by multiplying the camera's current zoom as a scalar on the vector
        /// </summary>
        /// <param name="regular">a vector</param>
        /// <returns>a vector modified by the camera's zoom</returns>
        public Vector getCameraUnits(Vector regular)
        {
            return new Vector(getCameraUnits(regular.X), getCameraUnits(regular.Y));
        }

        /// <summary>
        /// Convert a screen coordinate to a game coordinate
        /// </summary>
        /// <param name="screenCoord">A screen coordinate</param>
        /// <returns>A game coordinate</returns>
        public Vector getGameCoordinate(Vector screenCoord)
        {
            Vector converted = Vector2.Transform(screenCoord, buildBackwardsTransform());
            return converted;
        }

        /// <summary>
        /// Convert a game coordinate to a screen coordinate
        /// </summary>
        /// <param name="gameCoord">A game coordinate</param>
        /// <returns>a screen coordinate</returns>
        public Vector getScreenCoordinate(Vector gameCoord)
        {
            return Vector2.Transform(gameCoord, buildTransform());
        }

        /// <summary>
        /// Reset all of the camera's values
        /// </summary>
        /// <param name="position">The new position of the camera</param>
        /// <param name="zoom">The new zoom of the camera</param>
        public void reset(Vector position, float zoom)
        {
            Position = position;
            Zoom = zoom;
        }

        /// <summary>
        /// Reset all of the camera's values to their default values
        /// </summary>
        public void reset()
        {
            reset(Vector.Zero, 1);
        }

        /// <summary>
        /// get or set the screen size the camera is representing
        /// </summary>
        public Vector Size
        {
            get;
            set;
        }

        /// <summary>
        /// get or set the game coordinate in the center of the screen
        /// </summary>
        public Vector TruePosition
        {

            get
            {
                return round(getGameCoordinate(Size / 2));
            }

            set
            {
                
                origin = Vector.Zero;
                position = -value + Size/2;
                updateTransform();
            }
        }

        private Vector position;

        /// <summary>
        /// get or set the camera position
        /// </summary>
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
        /// <summary>
        /// get or set the camera origin 
        /// </summary>
        public Vector Origin
        {
            get
            {
                return origin;
            }
            set
            {
                

                origin = value;

                updateTransform();
            }
        }

        public void setOriginLockPosition(Vector nextOrigin)
        {
            Vector originNew = getGameCoordinate(nextOrigin);
            origin = originNew;
            updateTransform();
            originNew = getScreenCoordinate(origin);
            position += (nextOrigin - originNew);
            updateTransform();
        }


        private float zoom;
        /// <summary>
        /// get or set the camera zoom. The larger the zoom value, the more zoomed in the camera will be.
        /// </summary>
        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                zoom = MathHelper.Clamp(zoom, ZoomMin, ZoomMax);
                zoom = (float) Math.Round(zoom, 3);

                if (zoom < .001f) zoom = .001f;

                updateTransform();
            }
        }

        /// <summary>
        /// get or set the minimum value for the zoom value. 
        /// </summary>
        public float ZoomMin
        {
            get;
            set;
        }

        /// <summary>
        /// get or set the maximum value for the zoom value.
        /// </summary>
        public float ZoomMax
        {
            get;
            set;
        }


        [NonSerialized]
        private Matrix transform;

        /// <summary>
        /// get the transform matrix that has been created as a result of position, origin, zoom, and rotation
        /// </summary>
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

            ZoomMin = .5f;
            //Origin = new Vector(1280, 720) / 2f;
            Matrix t = Matrix.Identity

                 * Matrix.CreateTranslation(toVec3(Position))

            //* Matrix.CreateTranslation(toVec3(Origin))
          //  * Matrix.CreateScale(Zoom)
            * Matrix.CreateTranslation(toVec3(Origin))

           
            
            
            ;
            return t;
        }

        private Matrix buildBackwardsTransform()
        {
            Matrix t = Matrix.Identity
            * Matrix.CreateTranslation(toVec3(-position))
           // * Matrix.CreateTranslation(toVec3(origin))
            //* Matrix.CreateScale(1 / zoom)
            * Matrix.CreateTranslation(toVec3(origin))
            ;
            return t;
        }

    }
}
