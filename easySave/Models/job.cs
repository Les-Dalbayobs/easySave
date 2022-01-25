﻿/// \file job.cs
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
        string name;
        DirectoryInfo folderSource;
        DirectoryInfo folderDestination;
        bool typeSave;
        #endregion

        #region properties
        public string Name { get => name; set => name = value; }
        public DirectoryInfo GetFolderSource()
        {
            return folderSource;
        }

        public void SetFolderSource(string value)
        {
            folderSource = new DirectoryInfo(value);
        }

        public DirectoryInfo GetFolderDestination()
        {
            return folderDestination;
        }

        public void SetFolderDestination(string value)
        {
            folderDestination = new DirectoryInfo(value);
        }

        public bool TypeSave { get => typeSave; set => typeSave = value; }
        #endregion

        #region constructors
        job(string nameJob,string pathSource,string pathDestination,bool typeJob)
        {
            this.name = nameJob;
            this.folderSource = new DirectoryInfo(pathSource);
            this.folderDestination = new DirectoryInfo(pathDestination);
            this.TypeSave = typeJob;
        }
        #endregion

        #region methodes

        #endregion

    }
}
