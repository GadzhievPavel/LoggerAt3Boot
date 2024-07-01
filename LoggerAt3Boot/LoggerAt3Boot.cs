using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAt3Boot
{
    /// <summary>
    /// Производит логирование в файл
    /// </summary>
    public class LoggerAt3Boot
    {
        private String path;
        private String extension = ".txt";
        private String errorPath = "Error";
        private String infoPath = "Info";
        private String warningPath = "warning";
        private int limitSize = 1024 * 1024 * 1;

        /// <summary>
        /// Создаем объект класса, подготавливаем папки для логов
        /// </summary>
        /// <param name="basepath">корневой путь для логов</param>
        public LoggerAt3Boot(string basepath)
        {
            this.path = basepath;
            errorPath = path + "\\" + DateTime.Now.ToShortDateString() + "\\" +
                errorPath + "\\";
            if (!Directory.Exists(errorPath))
            {
                Directory.CreateDirectory(errorPath);
            }
            infoPath = path + "\\" + DateTime.Now.ToShortDateString() + "\\" +
                infoPath + "\\";
            if (!Directory.Exists(infoPath))
            {
                Directory.CreateDirectory(infoPath);
            }
            warningPath = path + "\\" + DateTime.Now.ToShortDateString() + "\\" +
                warningPath + "\\";
            if (!Directory.Exists(warningPath))
            {
                Directory.CreateDirectory(warningPath);
            }
        }

        /// <summary>
        /// Максимальный размер файла в байтах 
        /// </summary>
        /// <param name="size"></param>
        public void setLimitSize(int size)
        {
            limitSize = size;
        }

        public void error(string message)
        {
            var fileInfo = getLastFile(errorPath);
            String logPath;
            if (fileInfo is null)
            {
                logPath = errorPath + "\\" + DateTime.Now.Ticks + extension;
            }
            else
            {
                logPath = fileInfo.FullName;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[ERROR] ");
            sb.Append(DateTime.Now);
            sb.Append(' ');
            sb.AppendLine(message);
            write(sb.ToString(), logPath);
        }

        public void info(string message)
        {
            var fileInfo = getLastFile(infoPath);
            String logPath;
            if (fileInfo is null)
            {
                logPath = infoPath + "\\" + DateTime.Now.Ticks + extension;
            }
            else
            {
                logPath = fileInfo.FullName;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[INFO] ");
            sb.Append(DateTime.Now);
            sb.Append(' ');
            sb.AppendLine(message);
            write(sb.ToString(), logPath);
        }

        public void warning(string message)
        {
            var fileInfo = getLastFile(warningPath);
            String logPath;
            if (fileInfo is null)
            {
                logPath = warningPath + "\\" + DateTime.Now.Ticks + extension;
            }
            else
            {
                logPath = fileInfo.FullName;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[WARN] ");
            sb.Append(DateTime.Now);
            sb.Append(' ');
            sb.AppendLine(message);
            write(sb.ToString(), logPath);
        }

        private void write(string message, string path)
        {
            using (StreamWriter logWriter = new StreamWriter(path, append: true))
            {
                logWriter.WriteLine(message);
            }
        }

        private FileInfo getLastFile(String path)
        {
            var files = Directory.GetFiles(path);
            var listFiles = new List<FileInfo>();
            foreach (var file in files)
            {
                var infoFile = new FileInfo(file);
                listFiles.Add(infoFile);
            }
            if (!listFiles.Any())
            {
                return null;
            }
            var lastFile = listFiles.OrderBy(info => info.LastWriteTime).FirstOrDefault();
            return lastFile.Length < limitSize?lastFile: null;
        }
    }
}
