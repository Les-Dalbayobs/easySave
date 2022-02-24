using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Windows.Threading;
using System.Windows;
using System.Threading;

namespace easySave___Graphic.Models
{
    public static class Global
    {
        public static List<logSaveAdvancement> listSaveAdvancement;
        public static bool pause = false;
        public static bool stop = false;
    }

    /// <summary>
    /// Class allowing the creation of jobs
    /// </summary>
    public class job
    {
        #region attributes
        static object lockReadOrWriteLog = new object();

        public static Mutex bigFile = new Mutex();

        static object lockPrioFinish = new object();
        public static int prioFinish = 0;


        logProgressSave logProgress = new logProgressSave();
        string jsonStringLogProgress;
        // Set the path for log progress file in JSON
        string pathFileLogProgress = @"C:\EasySave\Log\logProgressSave.json";
        // Set the path for the log progress file in XML
        string pathFileLogProgressXml = @"C:\EasySave\Log\logProgressSave.xml";
        // Set the folder for logs
        string pathfolderLog;

        /// <summary>
        /// Create a new instance from logSaveAdvancement, named logSave
        /// </summary>
        logSaveAdvancement logSave = new logSaveAdvancement();
        // Create variable which stores json String for the Save Log
        string jsonStringLogSave;
        // Create variable which stores the path for the log save file
        string pathFileLogSave = @"C:\EasySave\Log\logSaveAdvancement.json";
        // Create variable which stores the path for the state log file
        string pathFileLogSaveXml = @"C:\EasySave\Log\logSaveAdvancement.xml";

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
            lock (lockReadOrWriteLog)
            {
                readLogAdvancement();
            }
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
        public void updateLabel(System.Windows.Controls.Label label, string value)
        {
            if (label != null)
            {
                label.Content = value;
            }
        }

        public void updateProgressBar(System.Windows.Controls.ProgressBar progressBar, double value)
        {
            if (progressBar != null)
            {
                progressBar.Value = value;
            }
        }

        /// <summary>
        /// Method to start a backup
        /// </summary>
        /// <returns>Return if the backup is well done</returns>
        public bool copy(List<string> prioExtension = null, string encryptionExtension = null, System.Windows.Controls.ProgressBar progressBar = null, System.Windows.Controls.Label label = null)
        {
            //Log Advancement initialize/////////////////////////////////////////////////////////////////////////////////
            logSave.Name = this.Name;
            logSave.SourceFilePath = this.pathSource;
            logSave.TargetFilePath = this.pathDestination;
            logSave.TotalFilesToCopy = calculNbFiles(this.pathSource);
            logSave.TotalFilesSize = calculSizeFolder(this.pathSource);
            logSave.NbFilesLeftToDo = logSave.TotalFilesToCopy;
            nbFilesCopied = 0;
            lock (lockReadOrWriteLog)
            {
                readLogAdvancement();
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateProgressBar(progressBar, 0)), DispatcherPriority.ContextIdle);
             
            bool confirmSave = false; //Confirmation of the backup execution - Set to false

            DirectoryInfo source = new DirectoryInfo(this.pathSource); //Create the DirectoryInfo of the source
            DirectoryInfo destination = new DirectoryInfo(this.pathDestination); //Create the DirectoryInfo of the destination

            //Try catch on the execution of the backup to avoid problems
            try
            {
                //Complete////////////////////////////////////////////////////////////////////////////////////////////////
                if (this.typeSave) 
                {
                    //Verification that the directory exists
                    if (destination.Exists)
                    {
                        destination.Delete(true); //Delete the directory
                    }

                    copyComplete(source, destination, prioExtension, encryptionExtension, progressBar, label); //Launch backup

                    confirmSave = true; //Validate the backup
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                
                //Differential////////////////////////////////////////////////////////////////////////////////////////////
                else
                {
                    copyDifferential(source, destination, prioExtension, encryptionExtension, progressBar, label); //Launch backup

                    if (Global.stop == false)
                    {
                        compareDelete(this.pathSource, this.pathDestination); //Delete non-existent files in the source
                    }

                    confirmSave = true;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(label, "Finish")), DispatcherPriority.ContextIdle);

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

        public void copyFile(FileInfo file, bool overwrite, string destinationDirectory, string destinationFile, string encryptionExtension = null, System.Windows.Controls.ProgressBar progressBar = null, System.Windows.Controls.Label label = null)
        {

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(label, file.Name)), DispatcherPriority.ContextIdle);

            //Log Progress update///////////////////////////////////////////////////////////////////
            // Create progress log
            logProgress.Name = this.Name;
            logProgress.FileSource = file.FullName;
            logProgress.FileTarget = destinationFile;
            logProgress.DestPath = destinationDirectory;
            logProgress.FileSize = file.Length.ToString();
            DateTime transferDelay = DateTime.Now;
            ///////////////////////////////////////////////////////////////////////////////////////

            //Copy or encryption///////////////////////////////////////////////////////////////////
            if (encryptionExtension != null && encryptionExtension != "" && file.Extension == encryptionExtension)
            {
                int delayEncryption = encryption(file, destinationDirectory);

                logProgress.EncryptionTime = delayEncryption.ToString();

                logProgress.FileTarget = destinationDirectory + @"\" + Path.GetFileNameWithoutExtension(destinationFile) + ".cry";
            }
            else
            {
                try
                {
                    //Copy the file to the target folder
                    file.CopyTo(destinationFile, overwrite);
                }
                catch
                {
                    FileInfo fileDestination = new FileInfo(destinationFile);

                    if (fileDestination.Exists && file.Name == fileDestination.Name && file.LastWriteTime > fileDestination.LastWriteTime)
                    {
                        file.CopyTo(destinationFile, true);
                    }
                }

                logProgress.EncryptionTime = "0";
            }
            ///////////////////////////////////////////////////////////////////////////////////////

            //Log Advancement update///////////////////////////////////////////////////////////////
            // Calculate number of files left to copy
            logSave.NbFilesLeftToDo--;
            // Calculate number of files copied;
            nbFilesCopied++;
            // Calculate progression of copy
            logSave.Progression = Math.Round(((double)nbFilesCopied / (double)logSave.TotalFilesToCopy * 100), 1);
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateProgressBar(progressBar, logSave.Progression)), DispatcherPriority.ContextIdle);
            //System.Windows.Forms.Application.DoEvents();
            // Determine current state
            logSave.State = logSave.NbFilesLeftToDo == 0 ? "END" : "ACTIVE";
            //////////////////////////////////////////////////////////////////////////////////////

            //Log Progress update/////////////////////////////////////////////////////////////////
            // Calculate transfert Time
            TimeSpan timeSpan = DateTime.Now - transferDelay;
            logProgress.FileTransfertTime = timeSpan.ToString();

            // Add time to logProgress
            logProgress.SetTime();
            //////////////////////////////////////////////////////////////////////////////////////
            
            lock (lockReadOrWriteLog)
            {
                if (Properties.Settings.Default.typeLog == "json")
                {
                    //Log Progress JSON///////////////////////////////////////////////////////////////////
                    jsonStringLogProgress = JsonConvert.SerializeObject(logProgress, Formatting.Indented);

                    using (StreamWriter writer = new StreamWriter(pathFileLogProgress, true))
                    {
                        writer.WriteLine(jsonStringLogProgress);
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    //Log Progress XML/////////////////////////////////////////////////////////////////////
                    writeXmlLogProgress();
                    ///////////////////////////////////////////////////////////////////////////////////////
                }

                //Log advancement//////////////////////////////////////////////////////////////////////
                searchLogAdvancement();
                writeLogAdvancement();
                ///////////////////////////////////////////////////////////////////////////////////////
            }

        }

        public void copyListFiles(List<string> sourceList, bool overwrite, List<string> prioList = null, string encryptionExtension = null, System.Windows.Controls.ProgressBar progressBar = null, System.Windows.Controls.Label label = null)
        {
            if (prioList != null)
            {
                lock (lockPrioFinish)
                {
                    prioFinish++;
                }

                for (int i = 0; i < prioList.Count;)
                {
                    while (Global.pause == true)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(label, "Pause")), DispatcherPriority.ContextIdle);
                        Thread.Sleep(1000);
                    }

                    if (Global.stop == true)
                    {
                        break;
                    }

                    string pathFile = prioList[i].Replace(this.pathSource, string.Empty);
                    string destinationFile = this.pathDestination + pathFile;

                    FileInfo fileSource = new FileInfo(prioList[i]);

                    string directoryDestination = Path.GetDirectoryName(destinationFile);
                    Directory.CreateDirectory(directoryDestination);

                    if (fileSource.Length > (Properties.Settings.Default.bigSize * 1024) && Properties.Settings.Default.bigSize != 0)
                    {
                        if (bigFile.WaitOne(1000))
                        {
                            copyFile(fileSource, overwrite, directoryDestination, destinationFile, encryptionExtension, progressBar, label);

                            sourceList.Remove(prioList[i]);
                            prioList.Remove(prioList[i]);

                            bigFile.ReleaseMutex();
                        }
                    }
                    else
                    {
                        copyFile(fileSource, overwrite, directoryDestination, destinationFile, encryptionExtension, progressBar, label);

                        sourceList.Remove(prioList[i]);
                        prioList.Remove(prioList[i]);
                    }
                }

                lock (lockPrioFinish)
                {
                    prioFinish--;
                }
            }
            
            for (int i = 0; i < sourceList.Count;)
            {
                while (Global.pause == true)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(label, "Pause")), DispatcherPriority.ContextIdle);
                    Thread.Sleep(1000);
                }

                while (prioFinish != 0)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => updateLabel(label, "Wait")), DispatcherPriority.ContextIdle);
                    Thread.Sleep(1000);
                }

                if (Global.stop == true)
                {
                    break;
                }

                FileInfo fileSource = new FileInfo(sourceList[i]);

                string pathFile = sourceList[i].Replace(this.PathSource, string.Empty);
                string destinationFile = this.pathDestination + pathFile;

                string directoryDestination = Path.GetDirectoryName(destinationFile);
                Directory.CreateDirectory(directoryDestination);

                if (fileSource.Length > (Properties.Settings.Default.bigSize * 1024) && Properties.Settings.Default.bigSize != 0)
                {
                    if (bigFile.WaitOne(1000))
                    {
                        copyFile(fileSource, overwrite, directoryDestination, destinationFile, encryptionExtension, progressBar, label);

                        sourceList.Remove(sourceList[i]);

                        bigFile.ReleaseMutex();
                    }
                }
                else
                {
                    copyFile(fileSource, overwrite, directoryDestination, destinationFile, encryptionExtension, progressBar, label);

                    sourceList.Remove(sourceList[i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFullName"></param>
        /// <param name="listPrioExtensions"></param>
        /// <returns></returns>
        public List<string> createListPrio(string sourceFullName, List<string> listPrioExtensions = null)
        {
            if (listPrioExtensions != null)
            {
                List<string> fullPrio = new List<string>();
                
                foreach (string onePrio in listPrioExtensions)
                {
                    fullPrio.AddRange(Directory.EnumerateFiles(sourceFullName, "*." + onePrio, SearchOption.AllDirectories).ToList<string>());
                }

                return fullPrio;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="listPrioExtensions"></param>
        /// <param name="encryptionExtension"></param>
        /// <param name="progressBar"></param>
        /// <param name="label"></param>
        public void copyComplete(DirectoryInfo source, DirectoryInfo destination,List<string> listPrioExtensions = null, string encryptionExtension = null, System.Windows.Controls.ProgressBar progressBar = null, System.Windows.Controls.Label label = null)
        {
            List<string> sourceListPathFiles = Directory.EnumerateFiles(source.FullName, ".", SearchOption.AllDirectories).ToList<string>();

            if (listPrioExtensions != null)
            {
                List<string> listFilesPrio = createListPrio(source.FullName, listPrioExtensions);

                copyListFiles(sourceListPathFiles, true, listFilesPrio, encryptionExtension, progressBar, label);
            }
            else
            {
                copyListFiles(sourceListPathFiles, true, listPrioExtensions, encryptionExtension, progressBar, label);
            }
        }

        /// <summary>
        /// Method to write in XML log progress file
        /// </summary>
        public void writeXmlLogProgress()
        {
            if (!File.Exists(pathFileLogProgressXml))
            {
                File.Create(pathFileLogProgressXml).Close();
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(logProgressSave));
            TextWriter writerXml = new StreamWriter(pathFileLogProgressXml, true);
            xmlSerializer.Serialize(writerXml, logProgress);
            writerXml.Close();
        }

        /// <summary>
        /// Method to search into a state log file  
        /// </summary>
        public void searchLogAdvancement()
        {
            lock (Global.listSaveAdvancement)
            {
                int index = Global.listSaveAdvancement.FindIndex(logSave => logSave.Name == name);

                if (index >= 0)
                Global.listSaveAdvancement[index] = logSave;
                else
                Global.listSaveAdvancement.Add(logSave);
            }
        }

        /// <summary>
        /// Method to write into state log files
        /// </summary>
        public void writeLogAdvancement()
        {
            if (Properties.Settings.Default.typeLog == "json")
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
            else
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<logSaveAdvancement>));
                using (var streamWriter = new StreamWriter(pathFileLogSaveXml))
                {
                    xml.Serialize(streamWriter, Global.listSaveAdvancement);
                }
            }
        }

        /// <summary>
        /// Method to read into the logs 
        /// </summary>
        public void readLogAdvancement()
        {
            if (Properties.Settings.Default.typeLog == "json")
            {
                if (!File.Exists(pathFileLogSave))
                {
                    File.Create(pathFileLogSave).Close();
                }
                else
                {
                    using (var streamReader = new StreamReader(pathFileLogSave))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            Global.listSaveAdvancement = serializer.Deserialize<List<logSaveAdvancement>>(jsonReader);
                        }
                    }
                }

                if (Global.listSaveAdvancement == null)
                {
                    Global.listSaveAdvancement = new List<logSaveAdvancement>();
                }
            }
            else
            {
                if (!File.Exists(pathFileLogSaveXml))
                {
                    File.Create(pathFileLogSaveXml).Close();
                }
                else
                {
                    try
                    {

                        var doc = new System.Xml.XmlDocument();
                        doc.Load(pathFileLogSaveXml);

                        XmlSerializer xml = new XmlSerializer(typeof(List<logSaveAdvancement>));
                        using (var stream = new FileStream(pathFileLogSaveXml, FileMode.Open))
                        {
                            Global.listSaveAdvancement = (List<logSaveAdvancement>)xml.Deserialize(stream);
                        }
                        
                    }
                    catch (System.Xml.XmlException e)
                    {
                        Global.listSaveAdvancement = new List<logSaveAdvancement>();
                    }
                    
                }
            }
        }

        /// <summary>
        /// Method to make a differential save
        /// </summary>
        /// <param name="source">Source path</param>
        /// <param name="destination">Destination path</param>
        /// <param name="listPrioExtensions">List of priorized files</param>
        /// <param name="encryptionExtension">Encryption extension</param>
        /// <param name="progressBar">Progress bars</param>
        /// <param name="label">Labels</param>
        public void copyDifferential(DirectoryInfo source, DirectoryInfo destination, List<string> listPrioExtensions = null, string encryptionExtension = null, System.Windows.Controls.ProgressBar progressBar = null, System.Windows.Controls.Label label = null)
        {
            // List of all source files extensions
            List<string> sourceListPathFiles = Directory.EnumerateFiles(source.FullName, ".", SearchOption.AllDirectories).ToList<string>();

            // Check if the list of priorization is empty
            if (listPrioExtensions != null)
            {
                // If not, creates a list of priorities, with file full name and list all the extensions prioritized
                List<string> listFilesPrio = createListPrio(source.FullName, listPrioExtensions);

                // Then copy all the list, taking all prios in consideration
                copyListFiles(sourceListPathFiles, false, listFilesPrio, encryptionExtension, progressBar, label);
            }
            else
            {
                // If not, copy directly
                copyListFiles(sourceListPathFiles, false, listPrioExtensions, encryptionExtension, progressBar, label);
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
                while (Global.pause == true)
                {
                    Thread.Sleep(1000);
                }

                //Try catch which will allow error handling if needed
                try
                {
                    if (!file.Contains(".cry"))
                    {
                        File.Delete(Path.Combine(destination, file));
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine("The process failed : " + e.ToString());
                }
            }
        }

        /// <summary>
        /// Method which overrites the included ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            /// Change all the default parameters
            return $"Name : {this.name}\n" +
                $"Source : {this.pathSource}\n" +
                $"Destination : {this.pathDestination}\n" +
                $"Type : {this.typeSave}\n";
        }

        /// <summary>
        /// Method to verify if the destination exists during creation
        /// </summary>
        /// <returns></returns>
        public bool verifCreateDestination()
        {
            // Initialize the verification variable at false
            bool verif = false;

            /// Try catch to verify if the destination exists
            try
            {
                // Create the directory at the destination path
                DirectoryInfo destination = new DirectoryInfo(this.pathDestination);
                Directory.CreateDirectory(destination.FullName);

                // If the path exists, return true
                if (destination.Exists)
                {
                    verif = true;
                }
            }
            // If not, return false
                catch
                {
                    verif = false;
                }
            return verif;
        }

        /// <summary>
        /// Method to encrypt specific files during a save
        /// </summary>
        /// <param name="fileSource">Source of the file</param>
        /// <param name="destination">Destination of the copy</param>
        /// <returns></returns>
        public int encryption(FileInfo fileSource, string destinationDirectory)
        {
            // Set the path for Cryptosoft software
            string pathCryptoSoft = @"C:\Program Files (x86)\CryptoSoft\CryptoSoft.exe";

            // Set the destination path for encrypted files
            string path = destinationDirectory + @"\" + Path.GetFileNameWithoutExtension(fileSource.FullName) + ".cry";

            // Start the process of encryption
            var e = Process.Start(pathCryptoSoft, "\"" + fileSource.FullName + "\"" + " " + "\"" + path + "\"");

            e.WaitForExit();

            int returnEncryption = e.ExitCode;

            return returnEncryption;
        }
        #endregion
    }
}