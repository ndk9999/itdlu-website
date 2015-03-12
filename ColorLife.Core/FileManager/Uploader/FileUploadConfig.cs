
namespace ColorLife.Core.FileManager
{
    public class FileUploadConfig
    {
        private string _VirtualSavePath;
        public string VirtualSavePath
        {
            get { return _VirtualSavePath; }
            set
            {
                value = value.Replace("\\", "/");
                if (!value.EndsWith("/"))
                {
                    value += "/";
                }
                _VirtualSavePath = value;
            }
        }
        public bool OverwriteExistingFile { get; set; }
        public string AllowedExtensions { get; set; }

        public bool GenerateUniqueFileName { get; set; }
        /// <summary>
        /// Generate Folder Name by Date
        /// </summary>
        public bool GenerateDateFolder { get; set; }

        public double FileContentMaxLenght { get; set; }
        public bool AddTextOnImage { get; set; }
        /// <summary>
        /// If set True, Must config ImageWith and ImageHeight (Required)
        /// </summary>
        public bool IsResizeImage { get; set; }
        private int? _ImageWidth;
        public int ImageWidth
        {
            get { return (_ImageWidth != null) ? _ImageWidth.Value : 640; }
            set
            {
                _ImageWidth = value;
            }
        }
        private int? _ImageHeight;
        public int ImageHeight
        {
            get { return (_ImageHeight != null) ? _ImageHeight.Value : 480; }
            set { _ImageHeight = value; }
        }
    }
}
