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


        private float positionSpeed = .5f;
        private Vector positionVelocity = Vector.Zero;
        private Vector targetPosition = Vector.Zero;
        private Vector position;
        private Vector origin;
        private float zoomSpring = .5f;
        private float zoomFriction = .5f;
        private float zoomVelocity = 0;
        private float zoomAcceleration = 0;
        private float targetZoom = 1;
        private float zoom;

        [NonSerialized]
        private Matrix transform;

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
            reset();
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
        /// Gets or Sets the target zoom level of the Camera. The Camera will try to interpolate to the target zoom value using a spring model.
        /// The amount that the camera zoom changes will be determined by the zoom force, (ZoomTarget - Zoom) * ZoomSpring.
        /// The force will then be inversly dapended by ZoomFriction.
        /// </summary>
        public float TargetZoom { get { return targetZoom; } set { targetZoom = (value); } }

        /// <summary>
        /// Gets or Sets the zoom springyness. The ZoomSpring controls the spring constant value used in the zoom calculations
        /// </summary>
        public float ZoomSpring { get { return zoomSpring; } set { zoomSpring = value; } }

        /// <summary>
        /// Gets or Sets the zoom friction. The ZoomFriction controls the friction value used in the zoom calculations
        /// </summary>
        public float ZoomFriction { get { return zoomFriction; } set { zoomFriction = value; } }

        /// <summary>
        /// Gets or Sets the target position of the Camera. The Camera will try to interpolate to the target position value using a linear model.
        /// The amount that the camera position changes will be determined by (PositionTarget - Position).Unit * PositionSpeed.
        /// </summary>
        public Vector TargetPosition { get { return targetPosition; } set { targetPosition = (value); } }
        
        /// <summary>
        /// Gets or sets the PositionSpeed. PositionSpeed controls how fast the Camera moves towards the target position.
        /// </summary>
        public float PositionSpeed { get { return positionSpeed; } set { positionSpeed = value; } }

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
                Position = -value + Size/2;
                updateTransform();
            }
        }


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
                position = (value);
               
                updateTransform();
            }
        }

        
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

                Vector val = (value);
                Vector originNew = (getGameCoordinate(val));
                origin = originNew;
                updateTransform();
                originNew = (getScreenCoordinate(origin));
                TargetPosition += (val - originNew);
                Position += (val - originNew);
                
            }
        }


        
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

            ZoomMin = .1f;
            Matrix t = Matrix.Identity
                * Matrix.CreateTranslation(toVec3(-Origin))
                * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
                * Matrix.CreateTranslation(toVec3(Origin))
                * Matrix.CreateTranslation(toVec3(Position))
                ;

            return t;
        }

        private Matrix buildBackwardsTransform()
        {
            Matrix t = Matrix.Invert(buildTransform());
            return t;
        }


        /// <summary>
        /// Center the camera around the given spot. This happens instantly.
        /// </summary>
        /// <param name="spot">The spot to center around, in GameCoordinates</param>
        public void center(Vector spot)
        {
            Origin = getScreenCoordinate(spot);
            Position = -spot + Size / 2;
        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// </summary>
        /// <param name="spot">The spot to center around, in GameCoordinates</param>
        public void follow(Vector spot)
        {
            Origin = getScreenCoordinate(spot);
            targetPosition = -spot + Size / 2;
        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// </summary>
        /// <param name="gob">The GameObject to center around</param>
        public void follow(GameObject gob)
        {
            follow(gob.Position);
        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// Using this function instead of regular follow, will ask the Camera to try and keep the view restricted to the 
        /// given constraints. If the camera is zoomed too far out, then this function is worthless. However, if the camera is zoomed in,
        /// then it will not move past the constraint values. This is useful for having a camera track a GameObject, but not revealing anything
        /// outside of the room or level segment that the gameobject is in.
        /// </summary>
        /// <param name="gob">The GameObject to center around</param>
        /// <param name="left">The left bound, in GameCoordinates</param>
        /// <param name="top">The top bound, in GameCoordinates</param>
        /// <param name="right">The right bound, in GameCoordinates</param>
        /// <param name="bottom">THe bottam bound, in GameCoordinates</param>
        public void followClamped(GameObject gob, float left, float top, float right, float bottom)
        {
            followClamped(gob.Position, left, top, right, bottom);
        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// Using this function instead of regular follow, will ask the Camera to try and keep the view restricted to the 
        /// given constraints. If the camera is zoomed too far out, then this function is worthless. However, if the camera is zoomed in,
        /// then it will not move past the constraint values. This is useful for having a camera track a GameObject, but not revealing anything
        /// outside of the room or level segment that the gameobject is in.
        /// </summary>
        /// <param name="gob">The GameObject to center around</param>
        /// <param name="left">The left bound, in GameCoordinates</param>
        /// <param name="right">The right bound, in GameCoordinates</param>
        public void followClampedX(GameObject gob, float left, float right)
        {
            followClampedX(gob.Position, left, right);
        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// Using this function instead of regular follow, will ask the Camera to try and keep the view restricted to the 
        /// given constraints. If the camera is zoomed too far out, then this function is worthless. However, if the camera is zoomed in,
        /// then it will not move past the constraint values. This is useful for having a camera track a GameObject, but not revealing anything
        /// outside of the room or level segment that the gameobject is in.
        /// </summary>
        /// <param name="gob">The GameObject to center around</param>
        /// <param name="top">The top bound, in GameCoordinates</param>
        /// <param name="bottom">THe bottam bound, in GameCoordinates</param>
        public void followClampedY(GameObject gob, float top, float bottom)
        {
            followClampedY(gob.Position, top, bottom);
        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// Using this function instead of regular follow, will ask the Camera to try and keep the view restricted to the 
        /// given constraints. If the camera is zoomed too far out, then this function is worthless. However, if the camera is zoomed in,
        /// then it will not move past the constraint values. This is useful for having a camera track a GameObject, but not revealing anything
        /// outside of the room or level segment that the gameobject is in.
        /// </summary>
        /// <param name="clampSpot">The spot to center around, in GameCoordinates</param>
        /// <param name="left">The left bound, in GameCoordinates</param>
        /// <param name="right">The right bound, in GameCoordinates</param>
        public void followClampedX(Vector clampSpot, float left, float right)
        {
            float invZoom = 1 / Zoom;

            right -= invZoom * (Size.X / 2);
            left += invZoom * (Size.X / 2);

            if (left < right)
            {
                clampSpot = new Vector(
                    MathHelper.Clamp(clampSpot.X, left, right),
                    clampSpot.Y);
            }
            else
            {
                clampSpot.X = (left + right) / 2;
            }
            follow(clampSpot);

        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// Using this function instead of regular follow, will ask the Camera to try and keep the view restricted to the 
        /// given constraints. If the camera is zoomed too far out, then this function is worthless. However, if the camera is zoomed in,
        /// then it will not move past the constraint values. This is useful for having a camera track a GameObject, but not revealing anything
        /// outside of the room or level segment that the gameobject is in.
        /// </summary>
        /// <param name="clampSpot">The spot to center around, in GameCoordinates</param>
        /// <param name="top">The top bound, in GameCoordinates</param>
        /// <param name="bottom">THe bottam bound, in GameCoordinates</param>
        public void followClampedY(Vector clampSpot, float top, float bottom)
        {
            float invZoom = 1 / Zoom;

            top += invZoom * (Size.Y / 2);
            bottom -= invZoom * (Size.Y / 2);


            if (top < bottom)
            {
                clampSpot = new Vector(
                        clampSpot.X,
                        MathHelper.Clamp(clampSpot.Y, top, bottom));
            }
            else
            {
                clampSpot.Y = (top + bottom) / 2;
            }
            follow(clampSpot);

        }

        /// <summary>
        /// Track the given spot. Calling this function will ask the Camera to interpolate to the target position.
        /// Using this function instead of regular follow, will ask the Camera to try and keep the view restricted to the 
        /// given constraints. If the camera is zoomed too far out, then this function is worthless. However, if the camera is zoomed in,
        /// then it will not move past the constraint values. This is useful for having a camera track a GameObject, but not revealing anything
        /// outside of the room or level segment that the gameobject is in.
        /// </summary>
        /// <param name="clampSpot">The spot to center around, in GameCoordinates</param>
        /// <param name="left">The left bound, in GameCoordinates</param>
        /// <param name="top">The top bound, in GameCoordinates</param>
        /// <param name="right">The right bound, in GameCoordinates</param>
        /// <param name="bottom">THe bottam bound, in GameCoordinates</param>
        public void followClamped(Vector clampSpot, float left, float top, float right, float bottom)
        {
            Vector camTopLeft = getGameCoordinate(Size/2);

            float invZoom = 1 / Zoom;

            right -= invZoom * (Size.X / 2);
            left += invZoom * (Size.X / 2);
            top += invZoom * (Size.Y / 2);
            bottom -= invZoom * (Size.Y / 2);

            
            if (left < right)
            {
                clampSpot = new Vector(
                    MathHelper.Clamp(clampSpot.X, left, right),
                    clampSpot.Y);
            }
            else
            {
                clampSpot.X = (left + right) / 2;
            }

            if (top < bottom)
            {
                clampSpot = new Vector(
                        clampSpot.X,
                        MathHelper.Clamp(clampSpot.Y, top, bottom));
            }
            else
            {
                clampSpot.Y = (top + bottom) / 2;
            }

            follow(clampSpot);
           
        }


        
        /// <summary>
        /// Updates the Camera
        /// </summary>
        public void update()
        {


            zoomVelocity *= ZoomFriction;
            zoomAcceleration = ZoomSpring * (targetZoom - Zoom);
            zoomVelocity += zoomAcceleration;

            positionVelocity = Math.Min(PositionSpeed, (targetPosition - Position).Length) * (targetPosition - Position).UnitSafe;
            
            Position += positionVelocity;
            Zoom += zoomVelocity;

            updateTransform();

        }

        

    }
}
