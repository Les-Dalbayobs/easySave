/// \file job.cs
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
                else //Diferential
                {
                    copyDifferential(source, destination); //Launch backup

                    //compareDelete(this.pathSource, this.pathDestination);

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

        /// <summary>
        /// Method for making a differential backup
        /// </summary>
        /// <param name="source">Source DirectoryInfo</param>
        /// <param name="destination">Source DirectoryInfo</param>
        public void copyDifferential(DirectoryInfo source, DirectoryInfo destination)
        {
            DirectoryInfo[] folders = source.GetDirectories();
            
            Directory.CreateDirectory(destination.FullName);
            
            foreach (FileInfo file in source.GetFiles())
            {
                foreach (FileInfo fileDestination in destination.GetFiles())
                {
                    if(file.Name == fileDestination.Name && file.LastWriteTime > fileDestination.LastWriteTime)
                    {
                        Console.WriteLine("Copy : " + file.Name);
                        file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
                    }
                }

                try
                {
                    file.CopyTo(Path.Combine(destination.FullName, file.Name), false);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed : " + e.ToString());
                }

            }

            foreach (DirectoryInfo subFolder in folders)
            {
                DirectoryInfo destinationSubFolder = destination.CreateSubdirectory(subFolder.Name);
                copyDifferential(subFolder, destinationSubFolder);
            }
        }

        public void compareDelete(string source, string destination)
        {
            var targetFiles = Directory.GetFiles(destination, "*", SearchOption.AllDirectories);
            var notExists = targetFiles.Where(p => !File.Exists(p.Replace(source, destination))).ToList();

            foreach (var p in notExists)
            {
                try
                {
                    File.Delete(p);
                    Console.WriteLine("File delete : " + p);

                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed : " + e.ToString());
                }
            }
            /*var sourceFiles = Directory.EnumerateFiles(source, ".", SearchOption.AllDirectories);
            var targetFiles = Directory.EnumerateFiles(destination, ".", SearchOption.AllDirectories);

            // Makes path relatives so you can compare files in subdirectories
            sourceFiles = sourceFiles.Select(f => new Uri(f).MakeRelativeUri(source));
            targetFiles = targetFiles.Select(f => new Uri(f).MakeRelativeUri(destination));

            // Get files from targetDir that does not exist in sourceDir
            var filesToDelete = targetFiles.Except(sourceFiles);

            foreach (string file in filesToDelete)
            {
                try
                {
                    File.Delete(Path.Combine(destination, file));
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed : " + e.ToString());
                }
                
            }*/
        }

        #endregion

    }
}
