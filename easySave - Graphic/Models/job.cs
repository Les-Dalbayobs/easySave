using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

namespace easySave___Graphic.Models
{
    public static class Global
    {
        public static List<logSaveAdvancement> listSaveAdvancement;
    }

    /// <summary>
    /// Class allowing the creation of jobs
    /// </summary>
    class job
    {
        #region attributes

        logProgressSave logProgress = new logProgressSave();
        string jsonStringLogProgress;
        string pathFileLogProgress = @"C:\EasySave\Log\logProgressSave.json";
        string pathfolderLog;

        /// <summary>
        /// Create a new instance from logSaveAdvancement, named logSave
        /// </summary>
        logSaveAdvancement logSave = new logSaveAdvancement();
        // Create variable which stores json String for the Save Log
        string jsonStringLogSave;
        // Create variable which stores the path for the log save file
        string pathFileLogSave = @"C:\EasySave\Log\logSaveAdvancement.json";

        /// <summary>
        /// Initialize variable which stores number of files already copied
        /// </summary>
        int nbFilesCopied = 0;

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

        /// <summary>
        /// Creation serialize JsonSerializer to serialize objects or value types into JSON,
        /// and to deserialize JSON into objects or value types.
        /// </summary>
        JsonSerializer serializer = new JsonSerializer();

        #endregion

        #region properties
        /// <summary>
        /// Getter setter of the name attribute 
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Getter setter of the type attribute 
        /// </summary>
        /// 
        public bool Complete { get => typeSave; set => typeSave = value; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public bool Differential { get => !typeSave; set => typeSave = !value; }
        /// <summary>
        /// Getter setter of the pathSource attribute
        /// </summary>
        public string PathSource { get => pathSource; set => pathSource = value; }

        /// <summary>
        /// Getter setter of the pathSource attribute
        /// </summary>
        public string PathDestination { get => pathDestination; set => pathDestination = value; }

        /// <summary>
        /// 
        /// </summary>
        ///[JsonIgnore]
        ///public string EncryptionExtension { get => encryptionExtension; set => encryptionExtension = value; }

        public string GetPathFileLogProgress()
        {
            return pathFileLogProgress;
        }

        public void SetPathFileLogProgress(string folderLog)
        {
            this.pathfolderLog = folderLog;
            this.pathFileLogProgress = this.pathfolderLog + @"\logProgressSave.json";
            this.pathFileLogSave = this.pathfolderLog + @"\logSaveAdvancement.json";
        }
        #endregion

        #region constructors

        /// <summary>
        /// Constructor without specifying a parameter
        /// </summary>
        public job()
        {
            readLogAdvancement();
        }

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="nameJob">Name of the job</param>
        /// <param name="pathSource">Source folder path</param>
        /// <param name="pathDestination">Destination folder path</param>
        /// <param name="typeJob">Type of backup</param>
        public job(string nameJob, string source, string destination, bool typeJob)
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
        public bool copy(string encryptionExtension = null)
        {
            logSave.Name = this.Name;
            logSave.SourceFilePath = this.pathSource;
            logSave.TargetFilePath = this.pathDestination;
            logSave.TotalFilesToCopy = calculNbFiles(this.pathSource);
            logSave.TotalFilesSize = calculSizeFolder(this.pathSource);
            logSave.NbFilesLeftToDo = logSave.TotalFilesToCopy;
            nbFilesCopied = 0;

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

                    copyComplete(source, destination, encryptionExtension); //Launch backup

                    confirmSave = true; //Validate the backup
                }
                else //Differential
                {
                    copyDifferential(source, destination, encryptionExtension); //Launch backup

                    compareDelete(this.pathSource, this.pathDestination); //Delete non-existent files in the source

                    confirmSave = true;
                }
            }
            catch (Exception e)
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
        public Int64 calculSizeFolder(string pathSource)
        {
            Int64 size = 0; //Intialisation size of the repertory = 0

            DirectoryInfo folder = new DirectoryInfo(pathSource); //Create the DirectoryInfo of the path

            foreach (FileInfo file in folder.GetFiles()) //Keep all files 
            {
                size += file.Length; //Foreach files sizes, add in size variable
            }

            foreach (DirectoryInfo dir in folder.GetDirectories()) //Keep all folders
            {
                size += calculSizeFolder(dir.FullName);// Restart calculSize of the repertory
            }

            return size; //Return total of the size
        }

        /// <summary>
        /// Method for making a full backup
        /// </summary>
        /// <param name="source">Source DirectoryInfo</param>
        /// <param name="destination">Destination DirectoryInfo</param>
        public void copyComplete(DirectoryInfo source, DirectoryInfo destination, string encryptionExtension = null)
        {
            //Cache directories before we start copying
            DirectoryInfo[] folders = source.GetDirectories();

            //Create the destination directory
            Directory.CreateDirectory(destination.FullName);

            //Copy all files in the folder
            foreach (FileInfo file in source.GetFiles())
            {
                // Create progress log
                logProgress.Name = this.Name;
                logProgress.FileSource = file.FullName;
                logProgress.FileTarget = Path.Combine(destination.FullName, file.Name);
                logProgress.DestPath = destination.FullName;
                logProgress.FileSize = file.Length.ToString();

                DateTime transferDelay = DateTime.Now;

                if (encryptionExtension != null && encryptionExtension != "" && file.Extension == encryptionExtension)
                {
                    int delayEncryption = encryption(file, destination);

                    logProgress.EncryptionTime = delayEncryption.ToString();

                    logProgress.FileTarget = Path.Combine(destination.FullName, Path.GetFileNameWithoutExtension(destination.FullName) + ".cry");
                }
                else
                {
                    //Copy the file to the target folder
                    file.CopyTo(Path.Combine(destination.FullName, file.Name), true);

                    logProgress.EncryptionTime = "0";
                }

                // Calculate number of files left to copy
                logSave.NbFilesLeftToDo--;
                // Calculate number of files copied;
                nbFilesCopied++;
                // Calculate progression of copy
                logSave.Progression = Math.Round(((double)nbFilesCopied / (double)logSave.TotalFilesToCopy * 100), 1);
                // Determine current state
                logSave.State = logSave.NbFilesLeftToDo == 0 ? "END" : "ACTIVE";

                TimeSpan timeSpan = DateTime.Now - transferDelay;

                // Calculate transfert Time
                logProgress.FileTransfertTime = timeSpan.ToString();

                // Add time to logProgress
                logProgress.SetTime();

                jsonStringLogProgress = JsonConvert.SerializeObject(logProgress, Formatting.Indented);

                using (StreamWriter writer = new StreamWriter(pathFileLogProgress, true))
                {
                    writer.WriteLine(jsonStringLogProgress);
                }

                readLogAdvancement();
                searchLogAdvancement();
                writeLogAdvancement();
            }

            //Search and enter the subfolders of the current folder
            foreach (DirectoryInfo subFolder in folders)
            {
                //Creates a sub-folder and saves this information in a DirectoryInfo
                DirectoryInfo destinationSubFolder = destination.CreateSubdirectory(subFolder.Name);

                //Start saving the new folder
                copyComplete(subFolder, destinationSubFolder);
            }
        }

        public void searchLogAdvancement()
        {
            int index = Global.listSaveAdvancement.FindIndex(logSave => logSave.Name == name);

            if (index >= 0)
                Global.listSaveAdvancement[index] = logSave;
            else
                Global.listSaveAdvancement.Add(logSave);
        }

        public void writeLogAdvancement()
        {
            jsonStringLogSave = JsonConvert.SerializeObject(Global.listSaveAdvancement, Formatting.Indented);

            using (var streamWriter = new StreamWriter(pathFileLogSave))
            {
                //Initializes a new instance of the JsonTextWriter class using the specified TextWriter.
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonWriter, JsonConvert.DeserializeObject(jsonStringLogSave));
                }
            }
        }

        public void readLogAdvancement()
        {
            if (!File.Exists(pathFileLogSave))
            {
                File.Create(pathFileLogSave).Close();
            }

            if (File.Exists(pathFileLogSave))
            {
                easySave.Models.Global.listSaveAdvancement = new List<easySave.Models.logSaveAdvancement>();

                //StreamReader instance to read text from a file
                using (var streamReader = new StreamReader(pathFileLogSave))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        Global.listSaveAdvancement = serializer.Deserialize<List<logSaveAdvancement>>(jsonReader);
                    }
                }
            }

            if (easySave.Models.Global.listSaveAdvancement == null)
            {
                easySave.Models.Global.listSaveAdvancement = new List<easySave.Models.logSaveAdvancement>();
            }
        }


        /// <summary>
        /// Method for making a differential backup
        /// </summary>
        /// <param name="source">Source DirectoryInfo</param>
        /// <param name="destination">Source DirectoryInfo</param>
        public void copyDifferential(DirectoryInfo source, DirectoryInfo destination, string encryptionExtension = null)
        {
            //Cache directories before we start copying
            DirectoryInfo[] folders = source.GetDirectories();

            //Create the destination directory
            Directory.CreateDirectory(destination.FullName);

            compareDelete(source.FullName, destination.FullName);

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
                        //Try catch which will allow error handling if needed
                        try
                        {
                            logProgress.Name = this.Name;
                            logProgress.FileSource = file.FullName;
                            logProgress.FileTarget = Path.Combine(destination.FullName, file.Name);
                            logProgress.DestPath = destination.FullName;
                            logProgress.FileSize = file.Length.ToString();

                            DateTime transferDelay = DateTime.Now;

                            if (encryptionExtension != null && encryptionExtension != "" && file.Extension == encryptionExtension)
                            {
                                int delayEncryption = encryption(file, destination);

                                logProgress.EncryptionTime = delayEncryption.ToString();

                                logProgress.FileTarget = Path.Combine(destination.FullName, Path.GetFileNameWithoutExtension(destination.FullName) + ".cry");

                            }
                            else
                            {
                                //Copy the file to the target folder
                                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);

                                logProgress.EncryptionTime = "0";
                            }

                            //Copy the file to the target folder
                            file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
                            // Calculate number of files left to copy
                            logSave.NbFilesLeftToDo--;
                            // Calculate number of files copied;
                            nbFilesCopied++;
                            // Calculate progression of copy
                            logSave.Progression = Math.Round(((double)nbFilesCopied / (double)logSave.TotalFilesToCopy * 100), 1);
                            // Determine current state
                            logSave.State = logSave.NbFilesLeftToDo == 0 ? "END" : "ACTIVE";
                            TimeSpan timeSpan = DateTime.Now - transferDelay;

                            logProgress.FileTransfertTime = timeSpan.ToString();

                            logProgress.SetTime();

                            jsonStringLogProgress = JsonConvert.SerializeObject(logProgress, Formatting.Indented);
                            jsonStringLogSave = JsonConvert.SerializeObject(logSave, Formatting.Indented);

                            using (StreamWriter writer = new StreamWriter(pathFileLogProgress, true))
                            {
                                writer.WriteLine(jsonStringLogProgress);
                            }

                            readLogAdvancement();
                            searchLogAdvancement();
                            writeLogAdvancement();
                        }
                        catch (Exception e)
                        {
                            //Console.WriteLine("The process failed : " + e.ToString()); //Displays the error
                        }
                    }
                }

                //Try catch which allow error handling if needed
                try
                {
                    logProgress.Name = this.Name;
                    logProgress.FileSource = file.FullName;
                    logProgress.FileTarget = Path.Combine(destination.FullName, file.Name);
                    logProgress.DestPath = destination.FullName;
                    logProgress.FileSize = file.Length.ToString();

                    DateTime transferDelay = DateTime.Now;

                    if (encryptionExtension != null && encryptionExtension != "" && file.Extension == encryptionExtension)
                    {
                        int delayEncryption = encryption(file, destination);

                        logProgress.EncryptionTime = delayEncryption.ToString();

                        logProgress.FileTarget = Path.Combine(destination.FullName, Path.GetFileNameWithoutExtension(destination.FullName) + ".cry");
                    }
                    else
                    {
                        //Copy the file to the target folder
                        file.CopyTo(Path.Combine(destination.FullName, file.Name), true);

                        logProgress.EncryptionTime = "0";
                    }

                    //Copy the file to the target folder only if it does not exist
                    file.CopyTo(Path.Combine(destination.FullName, file.Name), false);
                    // Calculate number of files left to copy
                    logSave.NbFilesLeftToDo--;
                    // Calculate number of files copied;
                    nbFilesCopied++;
                    // Calculate progression of copy
                    logSave.Progression = Math.Round(((double)nbFilesCopied / (double)logSave.TotalFilesToCopy * 100), 1);
                    // Determine current state
                    logSave.State = logSave.NbFilesLeftToDo == 0 ? "END" : "ACTIVE";
                    TimeSpan timeSpan = DateTime.Now - transferDelay;

                    logProgress.FileTransfertTime = timeSpan.ToString();

                    logProgress.SetTime();

                    jsonStringLogProgress = JsonConvert.SerializeObject(logProgress, Formatting.Indented);
                    jsonStringLogSave = JsonConvert.SerializeObject(logSave, Formatting.Indented);


                    using (StreamWriter writer = new StreamWriter(pathFileLogProgress, true))
                    {
                        writer.WriteLine(jsonStringLogProgress);
                    }

                    readLogAdvancement();
                    searchLogAdvancement();
                    writeLogAdvancement();
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
                    if (!file.Contains(".cry"))
                    {
                        File.Delete(Path.Combine(destination, file));
                    }
                    //Console.WriteLine("File delete : " + file);

                }
                catch (Exception e)
                {
                    //Console.WriteLine("The process failed : " + e.ToString());
                }
            }
        }

        public override string ToString()
        {
            return $"Name : {this.name}\n" +
                $"Source : {this.pathSource}\n" +
                $"Destination : {this.pathDestination}\n" +
                $"Type : {this.typeSave}\n";
        }

        public bool verifCreateDestination()
        {
            bool verif = false;

            try
            {
                DirectoryInfo destination = new DirectoryInfo(this.pathDestination);
                Directory.CreateDirectory(destination.FullName);

                if (destination.Exists)
                {
                    verif = true;
                }
            }
            catch
            {
                verif = false;
            }

            return verif;
        }

        public int encryption(FileInfo fileSource, DirectoryInfo destination)
        {
            string pathCryptoSoft = @"C:\Program Files (x86)\CryptoSoft\CryptoSoft.exe";

            string path = destination.FullName + @"\" + Path.GetFileNameWithoutExtension(fileSource.FullName) + ".cry";

            var e = Process.Start(pathCryptoSoft, "\"" + fileSource.FullName + "\"" + " " + "\"" + path + "\"");

            e.WaitForExit();

            int returnEncryption = e.ExitCode;

            return returnEncryption;
        }
        #endregion
    }
}