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
        /// Stores the path of the source folder
        /// </summary>
        string pathSource;

        /// <summary>
        /// Stores the path of the destination folder
        /// </summary>
        string pathDestination;

        /// <summary>
        /// Stores the type of the backup job
        /// </summary>
        bool typeSave;
        #endregion

        #region properties
        /// <summary>
        /// Getter setter of the name attribute 
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Getter setter of the type attribute 
        /// </summary>
        public bool TypeSave { get => typeSave; set => typeSave = value; }

        /// <summary>
        /// Getter setter of the pathSource attribute
        /// </summary>
        public string PathSource { get => pathSource; set => pathSource = value; }

        /// <summary>
        /// Getter setter of the pathSource attribute
        /// </summary>
        public string PathDestination { get => pathDestination; set => pathDestination = value; }
        #endregion

        #region constructors

        public job()
        {

        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="nameJob">Name of the job</param>
        /// <param name="pathSource">Source folder path</param>
        /// <param name="pathDestination">Destination folder path</param>
        /// <param name="typeJob">Type of backup</param>
        public job(string nameJob,string source,string destination,bool typeJob)
        {
            this.name = nameJob;
            this.pathSource = source;
            this.pathDestination = destination;
            this.typeSave = typeJob;
        }
        #endregion

        #region methodes
        public bool copy()
        {
            bool confirmSave = false;

            DirectoryInfo source = new DirectoryInfo(this.pathSource);
            DirectoryInfo destination = new DirectoryInfo(this.pathDestination);

            try
            {
                if (this.typeSave)
                {
                    if (destination.Exists)
                    {
                        destination.Delete(true);
                    }

                    copyComplete(source, destination);

                    confirmSave = true;
                }
                else
                {

                }
            }
            catch
            {
                confirmSave = false;
            }


            return confirmSave;
        }

        public bool verifExist()
        {
            bool exist = false;

            DirectoryInfo source = new DirectoryInfo(this.pathSource);

            if (source.Exists)
            {
                exist = true;
            }

            return exist;
        }

        public int calculNbFiles()
        {
            int size = Directory.GetFiles(this.pathSource, "*.*", SearchOption.AllDirectories).Length;

            return size;
        }

        public Int64 calculSize(string pathSource)
        {
            Int64 size = 0;

            DirectoryInfo folder = new DirectoryInfo(pathSource);

            foreach (FileInfo file in folder.GetFiles())
            {
                size += file.Length;
            }

            foreach (DirectoryInfo dir in folder.GetDirectories())
            {
                size += calculSize(dir.FullName);
            }

            return size;
        }

        public void copyComplete(DirectoryInfo source, DirectoryInfo destination)
        {
            DirectoryInfo[] folders = source.GetDirectories();

            Directory.CreateDirectory(destination.FullName);

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
            }

            foreach (DirectoryInfo subFolder in folders)
            {
                DirectoryInfo destinationSubFolder = destination.CreateSubdirectory(subFolder.Name);
                copyComplete(subFolder, destinationSubFolder);
            }
        }


        public void copyDifferential(DirectoryInfo source, DirectoryInfo destination)
        {
            

            /*DirectoryInfo[] folders = source.GetDirectories();
            
            Directory.CreateDirectory(destination.FullName);
            
            foreach (FileInfo file in source.GetFiles())
            {
                foreach (FileInfo fileDestination in destination.GetFiles())
                {
                    //Console.WriteLine(file.Name);
                    if(file.Name != fileDestination.Name && file.LastWriteTime < fileDestination.LastWriteTime)
                    {
                        Console.WriteLine("Copy : " + file.Name);
                        file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
                    }
                }
                
            }

            foreach (DirectoryInfo subFolder in folders)
            {
                DirectoryInfo destinationSubFolder = destination.CreateSubdirectory(subFolder.Name);
                copyFolderDifferential(subFolder, destinationSubFolder);
            }*/
        }


        #endregion

    }
}
