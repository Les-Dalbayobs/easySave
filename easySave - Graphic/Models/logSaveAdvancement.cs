using System;
using System.Collections.Generic;
using System.Text;

namespace easySave___Graphic.Models
{
    public class logSaveAdvancement
    {
        #region attributes
        string name;
        string sourceFilePath;
        string targetFilePath;
        string state;
        int totalFilesToCopy;
        float totalFilesSize;
        int nbFilesLeftToDo;
        double progression;

        #endregion

        #region properties
        public string Name { get => name; set => name = value; }
        public string SourceFilePath { get => sourceFilePath; set => sourceFilePath = value; }
        public string TargetFilePath { get => targetFilePath; set => targetFilePath = value; }
        public string State { get => state; set => state = value; }
        public int TotalFilesToCopy { get => totalFilesToCopy; set => totalFilesToCopy = value; }
        public float TotalFilesSize { get => totalFilesSize; set => totalFilesSize = value; }
        public int NbFilesLeftToDo { get => nbFilesLeftToDo; set => nbFilesLeftToDo = value; }
        public double Progression { get => progression; set => progression = value; }

        #endregion

        #region constructor
        public logSaveAdvancement()
        {

        }
        #endregion

        #region methods



        #endregion
    }
}
