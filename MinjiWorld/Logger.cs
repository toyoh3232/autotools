using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MinjiWorld
{
    public class Logger
    {
        private readonly object loggerLock = new object();

        private readonly TextWriter textWriter;

        public Logger(TextWriter writer)
        {
            textWriter = writer;
        }

        public void Log(string message)
        {
            lock (loggerLock)
            {
                textWriter.WriteLine($"{DateTime.Now:MM/dd HH:mm:ss} {message}");
            }
        } 
    }
}
