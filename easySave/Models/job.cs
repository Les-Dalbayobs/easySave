/// \file job.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 0.1
/// \date 25/01/2022

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Models namespace
/// </summary>
namespace easySave.Models
{
    /// <summary>
    /// Class allowing the creation of jobs
    /// </summary>
    class job
    {
        #region attributes
        /// <summary>
        /// Stores the job name 
        /// </summary>
        string name;

        /// <summary>
        /// Stores the directoryInfo of the source folder
        /// </summary>
        DirectoryInfo folderSource;

        /// <summary>
        /// Stores the directoryInfo of the destination folder
        /// </summary>
        DirectoryInfo folderDestination;

        /// <summary>
        /// Stores the type of the backup job
        /// </summary>
        bool typeSave;
        #endregion

        #region properties
        /// <summary>
        /// Getter setter of the name property 
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Getter of the DirectoryInfo source
        /// </summary>
        /// <returns>DirectoryInfo source</returns>
        public DirectoryInfo GetFolderSource()
        {
            return folderSource;
        }

        /// <summary>
        /// Setter of the DirectoryInfo source
        /// </summary>
        /// <param name="value">Path source</param>
        public void SetFolderSource(string value)
        {
            folderSource = new DirectoryInfo(value);
        }

        /// <summary>
        /// Getter of the DirectoryInfo destination
        /// </summary>
        /// <returns>DirectoryInfo destination</returns>
        public DirectoryInfo GetFolderDestination()
        {
            return folderDestination;
        }

        /// <summary>
        /// Setter of the DirectoryInfo destination
        /// </summary>
        /// <param name="value">Path destination</param>
        public void SetFolderDestination(string value)
        {
            folderDestination = new DirectoryInfo(value);
        }

        /// <summary>
        /// Getter setter of the type property 
        /// </summary>
        public bool TypeSave { get => typeSave; set => typeSave = value; }
        #endregion

        #region constructors
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="nameJob">Name of the job</param>
        /// <param name="pathSource">Source folder path</param>
        /// <param name="pathDestination">Destination folder path</param>
        /// <param name="typeJob">Type of backup</param>
        job(string nameJob,string pathSource,string pathDestination,bool typeJob)
        {
            this.name = nameJob;
            this.folderSource = new DirectoryInfo(pathSource);
            this.folderDestination = new DirectoryInfo(pathDestination);
            this.typeSave = typeJob;
        }
        #endregion

        #region methodes

        #endregion

    }
}
