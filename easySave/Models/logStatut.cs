using System;
using System.Collections.Generic;
using System.Text;

namespace easySave.Models
{
    class logStatut
    {
        #region atributes
        string name;
        string sourceFilePath;
        string targetFilePath;
        string state;
        string totalFilesSize;
        string nbFilesLeftToDo;
        string progression;
        #endregion

        #region properties
        public string Name { get => name; set => name = value; }
        public string SourceFilePath { get => sourceFilePath; set => sourceFilePath = value; }
        public string TargetFilePath { get => targetFilePath; set => targetFilePath = value; }
        public string State { get => state; set => state = value; }
        public string TotalFilesSize { get => totalFilesSize; set => totalFilesSize = value; }
        public string NbFilesLeftToDo { get => nbFilesLeftToDo; set => nbFilesLeftToDo = value; }
        public string Progression { get => progression; set => progression = value; }
        #endregion

        #region constructor
        public logStatut()
        {

        }
        #endregion

        #region methods

        #endregion

    }
}
