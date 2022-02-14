/// \file logProgressSave
/// \author Sheridan SHABANI
/// \author Steven LUCAS
/// \author Ahmed EL HARIRI
/// \version 1
/// \date 28/01/2022

using System;
using System.Collections.Generic;
using System.Text;

namespace easySave.Models
{
    class logProgressSave
    {
        #region attributes
        string name;
        string fileSource;
        string fileTarget;
        string destPath;
        string fileSize;
        string fileTransfertTime;
        string encyptiontime;
        string time;
        #endregion

        #region properties
        public string Name { get => name; set => name = value; }
        public string FileSource { get => fileSource; set => fileSource = value; }
        public string FileTarget { get => fileTarget; set => fileTarget = value; }
        public string DestPath { get => destPath; set => destPath = value; }
        public string FileSize { get => fileSize; set => fileSize = value; }
        public string FileTransfertTime { get => fileTransfertTime; set => fileTransfertTime = value; }
        public string Time { get => time; set => time = value; }
        public string Encyptiontime { get => encyptiontime; set => encyptiontime = value; }

        public string GetTime()
        {
            return time;
        }

        public void SetTime()
        {
            this.time = DateTime.Now.ToString("F");
        }

        #endregion

        #region constructor
        public logProgressSave()
        {
            
        }
        #endregion

        #region methods



        #endregion
    }
}
