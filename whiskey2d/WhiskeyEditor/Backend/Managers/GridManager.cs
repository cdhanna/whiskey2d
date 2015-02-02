using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;

namespace WhiskeyEditor.Backend.Managers
{

    /// <summary>
    /// The grid manager keeps track of the editor debug grid. 
    /// </summary>
    class GridManager
    {

        private static GridManager instance = new GridManager();
        private GridManager() 
        {
            setGridSize(128, 128);
        }

        /// <summary>
        /// The single instance of the GridManager.
        /// </summary>
        public static GridManager Instance { get { return instance; } }

        /// <summary>
        /// get or set the width of each cell
        /// </summary>
        public int GridSizeX { get; set; }

        /// <summary>
        /// get or set the height of each cell
        /// </summary>
        public int GridSizeY { get; set; }

        /// <summary>
        /// get or set the width and height of each cell
        /// </summary>
        public Vector GridSize
        {
            get { return new Vector(GridSizeX, GridSizeY); }
            set
            {
                GridSizeX = (int)value.X;
                GridSizeY = (int)value.Y;
            }
        }


        public void increase()
        {
            setGridSize(GridSizeX * 2, GridSizeY * 2);

        }

        public void decrease()
        {
            setGridSize(Math.Max(2, GridSizeX / 2), Math.Max(2, GridSizeY / 2));
        }

        /// <summary>
        /// set the grid size
        /// </summary>
        /// <param name="x">the width of the cells</param>
        /// <param name="y">the height of the cells</param>
        public void setGridSize(int x, int y)
        {
            GridSizeX = x;
            GridSizeY = y;
        }

        /// <summary>
        /// set the grid size
        /// </summary>
        /// <param name="size">the width and height of the cells</param>
        public void setGridSize(int size)
        {
            setGridSize(size, size);
        }

        /// <summary>
        /// snap a value in the x dimension. 
        /// This effectively truncates the given value
        /// </summary>
        /// <param name="sample">the value to snap</param>
        /// <returns>the snapped value</returns>
        public float snapX(float sample)
        {
            return (float)Math.Floor(sample / GridSizeX) * GridSizeX;
        }

        /// <summary>
        /// snap a value in the y dimension
        /// This effectively truncates the given value
        /// </summary>
        /// <param name="sample">the value to snap</param>
        /// <returns>the snapped value</returns>
        public float snapY(float sample)
        {
            return sample - (sample % GridSizeY);
        }

        /// <summary>
        /// snap a value in the x and y dimensions
        /// </summary>
        /// <param name="sample">the value to snap</param>
        /// <returns>the snapped value</returns>
        public Vector snap(Vector sample)
        {
            return new Vector(snapX(sample.X), snapY(sample.Y));
        }

        /// <summary>
        /// snap a value in the x dimension
        /// this works like regular snap, except that instead of truncating the value, it is rounded to the closest grid line
        /// </summary>
        /// <param name="sample">the value to snap</param>
        /// <returns>the snapped value</returns>
        public float snapRoundX(float sample)
        {
            return snapX(sample + GridSizeX / 2);
        }

        /// <summary>
        /// snap a value in the y dimension
        /// this works like regular snap, except that instead of truncating the value, it is rounded to the closest grid line
        /// </summary>
        /// <param name="sample">the value to snap</param>
        /// <returns>the snapped value</returns>
        public float snapRoundY(float sample)
        {
            return snapY(sample + GridSizeY / 2);
        }

        /// <summary>
        /// snap a value in the x and y dimensions
        /// this works like regular snap, except that instead of truncating the value, it is rounded to the closest grid line
        /// </summary>
        /// <param name="sample">the value to snap</param>
        /// <returns>the snapped value</returns>
        public Vector snapRound(Vector sample)
        {
            return new Vector(snapRoundX(sample.X), snapRoundY(sample.Y));
        }

        //public Vector snapCenter(Vector sample)
        //{
        //    return (GridSize / 2) + snap(sample);
        //}


    }
}
