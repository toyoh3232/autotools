using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace CpyFcDel.NET
{
    public class DirectoryBindingStack : BindingList<string>
    {
        public int Push(string path)
        {
            path = path.ToUpper().TrimEnd('\\') + "\\";
            if (!base.Contains(path))
            {
                base.Insert(0, path);
                return 0;
            }
            return base.IndexOf(path);
        }
    }


    public class Options
    {
        public Options()
        {
            SetDefaults();
        }

        public DirectoryBindingStack SourceDirs { get; set; } = new DirectoryBindingStack();
        public DirectoryBindingStack TargetDirs { get; set; } = new DirectoryBindingStack();

        public bool IsWriteCacheOn { get; set; }
        public bool IsReadCacheOn { get; set; } 
        public int? LimitCount { get; set; } 
        public bool IsAutoExit { get; set; } 


        public void SetDefaults()
        {
            TargetDirs.Clear();
            SourceDirs.Clear();
            IsWriteCacheOn = true;
            IsReadCacheOn = true;
            LimitCount = null;
            IsAutoExit = false;
        }

        /*
         * commmand sourceDir TargetDir [/DW] [/DR] [/P:passes] [/AE]
         * default pattern: commmand sourceDir TargetDir
         */
        public void Parse(string[] args)
        {
            if (args.Length == 1) return;
            if (args.Length < 3) throw new Exception();
            var sourceDir = args[1];
            var targetDir = args[2];
            bool isWriteCacheOn = true;
            bool isReadCacheOn = true;
            int? limitCount = null;
            bool isAutoExit = false;

            var optionArgs = args.Select((v, i) => new { V = v, I = i }).Where(x => x.I >= 3).Select(x => x.V.ToUpper());
            var optionArgsList = optionArgs.ToList();

            if (optionArgs.Any(x => x == "/DW"))
            {
                isWriteCacheOn = false;
                optionArgsList = optionArgsList.Where(x => x != "/DW").ToList();
            }
            if (optionArgs.Any(x => x == "/DR"))
            {
                isReadCacheOn = false;
                optionArgsList = optionArgsList.Where(x => x != "/DR").ToList();
            }
            if (optionArgs.Any(x => x.StartsWith("/P:")))
            {
                try
                {
                    limitCount = int.Parse(optionArgs.Where(x => x.ToUpper().StartsWith("/P:")).First().Split(':')[1]);
                    optionArgsList = optionArgsList.Where(x => !x.StartsWith("/P:")).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
                if (optionArgs.Any(x => x.ToUpper().StartsWith("/P:")) && optionArgs.Any(x => x.ToUpper() == "/AE"))
                {
                    isAutoExit = true;
                    optionArgsList = optionArgsList.Where(x => x != "/AE").ToList();
                }
            }
            if (optionArgsList.Count > 0)
            {
                throw new Exception();
            }
            // add to the options if parse succeed
            this.IsAutoExit = isAutoExit;
            this.IsReadCacheOn = isReadCacheOn;
            this.IsWriteCacheOn = isWriteCacheOn;
            this.LimitCount = limitCount;
            this.SourceDirs.Push(sourceDir);
            this.SourceDirs.Push(targetDir);
        }

        public void Load()
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), name + ".xml");
            
            if (File.Exists(path))
            {
                var reader = new XmlSerializer(typeof(Options));
                var file = File.OpenRead(path);

                var options = (Options)reader.Deserialize(file);
                file.Close();

                this.IsAutoExit = options.IsAutoExit;
                this.IsReadCacheOn = options.IsReadCacheOn;
                this.IsWriteCacheOn = options.IsWriteCacheOn;
                this.LimitCount = options.LimitCount;
                this.SourceDirs = options.SourceDirs;
                this.TargetDirs = options.TargetDirs;
            }
        }
        
        public void Save()
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), name + ".xml");
            
            var writer = new XmlSerializer(typeof(Options)); 
            var file = File.Create(path);

            writer.Serialize(file, this);
            file.Close();
        }

    }
}
