using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Managers
{

    /// <summary>
    /// The LogManager allows messages to be sent to the log file, and to the screen. 
    /// </summary>
    public interface LogManager
    {

        /// <summary>
        /// Initializes the LogManager. This is when the log file is created.
        /// </summary>
        void init();

        /// <summary>
        /// Close the LogManager
        /// </summary>
        void close();

        /// <summary>
        /// Get the current log path
        /// </summary>
        /// <returns></returns>
        string getCurrentLogPath();

        /// <summary>
        /// Get the old log path
        /// </summary>
        /// <returns></returns>
        string getOldLogPath();


        /// <summary>
        /// updates the LogManager, so that input may be tracked
        /// </summary>
        void update();


        /// <summary>
        /// Writes a debug message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        void debug(string message);
        /// <summary>
        /// Writes an error message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        void error(string message);
        /// <summary>
        /// Writes a warning message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        void warning(string message);

        /// <summary>
        /// Writes a release message to the long file
        /// </summary>
        /// <param name="message">the message to give to the logger</param>
        void release(string message);


    }
}
