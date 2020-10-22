using System;
using System.IO;

namespace TheaterApplication.Dal
{
    public class ScriptReader
    {
        public string ReadScript(string scriptName)
        {
            var path = $"{AppContext.BaseDirectory}/scripts/{scriptName}.sql";
            var result = File.ReadAllText(path);

            return result;
        }
    }
}
