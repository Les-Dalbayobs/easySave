﻿/// \file job.cs
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 0.1
/// \date 25/01/2022

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// Stores the type of the backup job,
        /// True = Complete,
        /// False = Differential
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

        /// <summary>
        /// Constructor without specifying a parameter
        /// </summary>
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
            this.name = nameJob; //Initializes the variable
            this.pathSource = source; //Initializes the variable
            this.pathDestination = destination; //Initializes the variable
            this.typeSave = typeJob; //Initializes the variable
        }

        #endregion

        #region methodes

        /// <summary>
        /// Method to start a backup
        /// </summary>
        /// <returns>Return if the backup is well done</returns>
        public bool copy()
        {
            bool confirmSave = false; //Confirmation of the backup execution - Set to false

            DirectoryInfo source = new DirectoryInfo(this.pathSource); //Create the DirectoryInfo of the source
            DirectoryInfo destination = new DirectoryInfo(this.pathDestination); //Create the DirectoryInfo of the destination

            //Try catch on the execution of the backup to avoid problems
            try
            {
                if (this.typeSave) //Complete
                {
                    //Verification that the directory exists
                    if (destination.Exists)
                    {
                        destination.Delete(true); //Delete the directory
                    }

                    copyComplete(source, destination); //Launch backup

                    confirmSave = true; //Validate the backup
                }
                else //Differential
                {
                    copyDifferential(source, destination); //Launch backup

                    compareDelete(this.pathSource, this.pathDestination); //Delete non-existent files in the source

                    confirmSave = true;
                }
            }
            catch
            {
                confirmSave = false; //Backup not performed
            }


            return confirmSave; //Returns whether the backup was performed
        }

        /// <summary>
        /// Method to check if a directory exists
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Bool - On the existence of the directory</returns>
        public bool verifExist(string path)
        {
            bool exist = false; //Variable who stock the boolean result

            DirectoryInfo source = new DirectoryInfo(path); //Create the DirectoryInfo of the path

            if (source.Exists) //To know if the source exist
            { 
                exist = true; //Update the variable with true
            }

            return exist; //Bool - On the existence of the directory
        }

        /// <summary>
        /// Method for calculating the total number of files in a folder and the subfolders
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>The total number of files in the folder and sub-folders </returns>
        public int calculNbFiles(string path)
        {
            int size = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length;

            return size;
        }

        /// <summary>
        /// Method to calculate the size in bytes of a folder
        /// </summary>
        /// <param name="pathSource">Directory path</param>
        /// <returns>Size in bytes</returns>
        public Int64 calculSize(string pathSource)
        {
            Int64 size = 0; //Intialisation size of the repertory = 0

            DirectoryInfo folder = new DirectoryInfo(pathSource); //Create the DirectoryInfo of the path

            foreach (FileInfo file in folder.GetFiles()) //Keep all files 
            {
                size += file.Length; //Foreach files sizes, add in size variable
            }

            foreach (DirectoryInfo dir in folder.GetDirectories()) //Keep all folders
            {
                size += calculSize(dir.FullName);// Restart calculSize of the repertory
            }

            return size; //Return total of the size
        }

        /// <summary>
        /// Method for making a full backup
        /// </summary>
        /// <param name="source">Source DirectoryInfo</param>
        /// <param name="destination">Destination DirectoryInfo</param>
        public void copyComplete(DirectoryInfo source, DirectoryInfo destination)
        {
            //Cache directories before we start copying
            DirectoryInfo[] folders = source.GetDirectories(); 
            
            //Create the destination directory
            Directory.CreateDirectory(destination.FullName); 

            //Copy all files in the folder
            foreach (FileInfo file in source.GetFiles())
            {
                //Copy the file to the target folder
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
            }

            //Search and enter the subfolders of the current folder
            foreach (DirectoryInfo subFolder in folders)
            {
                //Creates a sub-folder and saves this information in a DirectoryInfo
                DirectoryInfo destinationSubFolder = destination.CreateSubdirectory(subFolder.Name);
                22001
                //Start saving the new folder
                copyComplete(subFolder, destinationSubFolder);
            }
        }

        /// <summary>
        /// Method for making a differential backup
        /// </summary>
        /// <param name="source">Source DirectoryInfo</param>
        /// <param name="destination">Source DirectoryInfo</param>
        public void copyDifferential(DirectoryInfo source, DirectoryInfo destination)
        {
            //Cache directories before we start copying
            DirectoryInfo[] folders = source.GetDirectories();

            //Create the destination directory
            Directory.CreateDirectory(destination.FullName);

            //Copy iteration for all files in the folder
            foreach (FileInfo file in source.GetFiles())
            {
                //Now we look at all the files in the destination folder
                foreach (FileInfo fileDestination in destination.GetFiles())
                {
                    //If the name of the source file and the destination file are the same,
                    //as well as the date of modification of the source file superior to the destination file then it is copied.
                    if (file.Name == fileDestination.Name && file.LastWriteTime > fileDestination.LastWriteTime)
                    {
                        //Console.WriteLine("Copy : " + file.Name); //Test

                        //Try catch which will allow error handling if needed
                        try
                        {
                            //Copy the file to the target folder
                            file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
                        }
                        catch (Exception e)
                        {
                            //Console.WriteLine("The process failed : " + e.ToString()); //Displays the error
                        }

                    }
                }

                //Try catch which will allow error handling if needed
                try
                {
                    //Copy the file to the target folder only if it does not exist
                    file.CopyTo(Path.Combine(destination.FullName, file.Name), false);
                }
                catch (Exception e)
                {
                    //Console.WriteLine("The process failed : " + e.ToString()); //Displays the error
                }

            }

            //Search and enter the subfolders of the current folder
            foreach (DirectoryInfo subFolder in folders)
            {
                //Creates a sub-folder and saves this information in a DirectoryInfo
                DirectoryInfo destinationSubFolder = destination.CreateSubdirectory(subFolder.Name);

                //Start saving the new folder
                copyDifferential(subFolder, destinationSubFolder);
            }
        }

        /// <summary>
        /// Method to compare the source and destination to find the files to delete from the source.
        /// </summary>
        /// <param name="source">Source DirectoryInfo</param>
        /// <param name="destination">Source DirectoryInfo</param>
        public void compareDelete(string source, string destination)
        {
            //Lists all files in both folders and stores them in an enumeration list
            var sourceListPathFile = Directory.EnumerateFiles(source, ".", SearchOption.AllDirectories);
            var destinationListPathFile = Directory.EnumerateFiles(destination, ".", SearchOption.AllDirectories);

            //Create lists that will contain just the file names (not the full path)
            var sourceFiles = new List<string>();
            var destinationFiles = new List<string>();

            //Adds all file names to the lists
            foreach (string file in sourceListPathFile)
            {
                sourceFiles.Add(Path.GetFileName(file));
            }
            foreach (string file in destinationListPathFile)
            {
                destinationFiles.Add(Path.GetFileName(file));
            }

            //Creation of list of files delete from the source
            var filesToDelete = destinationFiles.Except(sourceFiles);

            //Deletes all files found
            foreach (string file in filesToDelete)
            {
                //Try catch which will allow error handling if needed
                try
                {
                    File.Delete(Path.Combine(destination, file));
                    //Console.WriteLine("File delete : " + file);

                }
                catch (Exception e)
                {
                    //Console.WriteLine("The process failed : " + e.ToString());
                }
                
            }


        }
            public int deleteJob(int jobNumber)
            {
                return 0;
            }

        

        #endregion

    }
}
