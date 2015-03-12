using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ColorLife.Core.FileManager
{
    public class FTP
    {
        public string UserName = "";
        public string Password = "";
        public bool KeepAlive = false;
        public bool UseSSL = true;
        private string m_FTPSite = "";
        public string FTPSite
        {
            get { return m_FTPSite; }
            set
            {
                m_FTPSite = value;
                if (!m_FTPSite.EndsWith("/")) m_FTPSite += "/";
            }
        }
        private string m_CurDir = "";
        public string CurrentDirectory
        {
            get { return m_CurDir; }
            set
            {
                m_CurDir = value;
                if (!m_CurDir.EndsWith("/") && m_CurDir != "")
                    m_CurDir += "/";
                m_CurDir = m_CurDir.TrimStart("/".ToCharArray());
            }
        }

        public FTP() { }
        public FTP(string sFTPSite, string sUserName,
                   string sPassword)
        {
            UserName = sUserName;
            Password = sPassword;
            FTPSite = sFTPSite;
        }

        public static bool ValidateServerCertificate(object sender,
           X509Certificate certificate, X509Chain chain,
           SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors ==
               SslPolicyErrors.RemoteCertificateChainErrors)
            {
                return false;
            }
            else if (sslPolicyErrors ==
             SslPolicyErrors.RemoteCertificateNameMismatch)
            {
                System.Security.Policy.Zone z =
                   System.Security.Policy.Zone.CreateFromUrl
                   (((HttpWebRequest)sender).RequestUri.ToString());
                if (z.SecurityZone ==
                   System.Security.SecurityZone.Intranet ||
                   z.SecurityZone ==
                   System.Security.SecurityZone.MyComputer)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public List<string> GetFileList(string CurDirectory,
           string StartsWith, string EndsWith)
        {
            CurrentDirectory = CurDirectory;
            return GetFileList(StartsWith, EndsWith);
        }
        public List<string> GetFileList(string StartsWith,
                                        string EndsWith)
        {
            FtpWebRequest oFTP =
               (FtpWebRequest)FtpWebRequest.Create(FTPSite +
               CurrentDirectory);
            //oFTP.EnableSsl = true;
            oFTP.Credentials = new NetworkCredential(UserName,
                                                      Password);
            oFTP.KeepAlive = KeepAlive;
            oFTP.EnableSsl = UseSSL;
            // Validate the server certificate with
            // ServerCertificateValidationCallBack
            if (UseSSL) ServicePointManager.
               ServerCertificateValidationCallback =
               new RemoteCertificateValidationCallback
               (ValidateServerCertificate);
            //System.Security.Cryptography.X509Certificates.
            //X509Certificate oCert = new System.Security.Cryptography.
            //X509Certificates.X509Certificate();
            //oFTP.ClientCertificates.Add(oCert);

            oFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response =
               (FtpWebResponse)oFTP.GetResponse();
            StreamReader sr =
               new StreamReader(response.GetResponseStream());
            string str = sr.ReadLine();
            List<string> oList = new List<string>();
            while (str != null)
            {
                if (str.StartsWith(StartsWith) &&
                   str.EndsWith(EndsWith)) oList.Add(str);
                str = sr.ReadLine();
            }
            sr.Close();
            response.Close();
            oFTP = null;

            return oList;
        }

        public bool GetFile(string Name, string DestFile)
        {
            //1. Create a request: must be in ftp://hostname format,
            //   not just ftp.myhost.com
            FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
               Create(FTPSite + CurrentDirectory + Name);
            //oFTP.EnableSsl = true;
            //2. Set credentials
            oFTP.Credentials = new NetworkCredential(UserName,
                                                     Password);
            //Define the action required (in this case, download
            //                            a file)
            oFTP.Method = WebRequestMethods.Ftp.DownloadFile;

            //3. Settings
            oFTP.KeepAlive = KeepAlive;
            oFTP.EnableSsl = UseSSL;
            // Validate the server certificate with
            // ServerCertificateValidationCallBack
            if (UseSSL) ServicePointManager.
               ServerCertificateValidationCallback = new
               RemoteCertificateValidationCallback
               (ValidateServerCertificate);
            //we want a binary transfer, not textual data
            oFTP.UseBinary = true;

            //4. If we were using a method that uploads data;
            //   for example, UploadFile, we would open the
            //   ftp.GetRequestStream here an send the data

            //5. Get the response to the Ftp request and the
            //   associated stream
            FtpWebResponse response =
               (FtpWebResponse)oFTP.GetResponse();
            Stream responseStream = response.GetResponseStream();
            //loop to read & write to file
            FileStream fs = new FileStream(DestFile, FileMode.Create);
            Byte[] buffer = new Byte[2047];
            int read = 1;
            while (read != 0)
            {
                read = responseStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, read);
            }    //see Note(1)
            responseStream.Close();
            fs.Flush();
            fs.Close();
            responseStream.Close();
            response.Close();
            oFTP = null;

            return true;
        }

        public bool UploadFile(FileInfo oFile)
        {
            FtpWebRequest ftpRequest;
            FtpWebResponse ftpResponse;

            try
            {
                //Settings required to establish a connection with
                //the server
                ftpRequest = (FtpWebRequest)FtpWebRequest.
                   Create(FTPSite + CurrentDirectory + oFile.Name);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Proxy = null;
                ftpRequest.UseBinary = true;
                ftpRequest.Credentials =
                   new NetworkCredential(UserName, Password);
                ftpRequest.KeepAlive = KeepAlive;
                ftpRequest.EnableSsl = UseSSL;
                // Validate the server certificate with
                // ServerCertificateValidationCallBack
                if (UseSSL) ServicePointManager.
                   ServerCertificateValidationCallback = new
                      RemoteCertificateValidationCallback
                      (ValidateServerCertificate);

                //Selection of file to be uploaded
                byte[] fileContents = new byte[oFile.Length];

                //will destroy the object immediately after being used
                using (FileStream fr = oFile.OpenRead())
                {
                    fr.Read(fileContents, 0,
                    Convert.ToInt32(oFile.Length));
                }
                using (Stream writer = ftpRequest.GetRequestStream())
                {
                    writer.Write(fileContents, 0, fileContents.Length);
                }
                //Gets the FtpWebResponse of the uploading operation
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                //Display response
                //Response.Write(ftpResponse.StatusDescription);

                ftpResponse.Close();
                ftpRequest = null;

                return true;
            }
            catch //(WebException webex)
            {
                return false;
                //this.Message = webex.ToString();
            }
        }

        public bool DeleteFile(string Name)
        {
            //1. Create a request: must be in ftp://hostname format,
            //   not just ftp.myhost.com
            FtpWebRequest oFTP = (FtpWebRequest)FtpWebRequest.
               Create(FTPSite + CurrentDirectory + Name);
            //oFTP.EnableSsl = true;
            //2. Set credentials
            oFTP.Credentials = new NetworkCredential(UserName, Password);
            //Define the action required (in this case, download a file)
            oFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            //3. Settings
            oFTP.KeepAlive = KeepAlive;
            oFTP.EnableSsl = UseSSL;
            // Validate the server certificate with
            // ServerCertificateValidationCallBack
            if (UseSSL) ServicePointManager.
               ServerCertificateValidationCallback = new
               RemoteCertificateValidationCallback
               (ValidateServerCertificate);
            //we want a binary transfer, not textual data
            oFTP.UseBinary = true;

            //4. If we were using a method that uploads data;
            //   for example, UploadFile, we would open the
            // ftp.GetRequestStream here an send the data

            //5. Get the response to the Ftp request and the associated
            //   stream
            FtpWebResponse response = (FtpWebResponse)oFTP.GetResponse();
            FtpStatusCode oStat = response.StatusCode;
            response.Close();
            oFTP = null;

            return true;
        } // DeleteFile()
    }
}
