using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CpyFcDel.NET
{
    class Options
    {
        private static Options ops;

        private Options(){ }

        public static Options Instance
        {
            get
            {
                if (ops == null)
                {
                    ops = new Options();
                }
                return ops;
            }
        }

        public BindingList<string> SourceDirs { get; set; } = new BindingList<string>();

        public BindingList<string> TargetDirs { get; set; } = new BindingList<string>();

        public bool IsWriteCacheOn { get; set; } = true;

        public bool IsReadCacheOn { get; set; } = true;

        public int? LimitCount { get; set; } = null;

        public bool IsAutoExit { get; set; } = false;

        public bool HasErrors { get; set; } = false;

        public string ErrorMessage { get; set; }

        public void Parse(string[] args)
        {
            switch(args.Length)
            {
                case 3:
                    ops.SourceDirs.Add(args[1]);
                    ops.TargetDirs.Add(args[2]);
                    break;
            }
        }
        
        public void Load()
        {
            // TODO
        }
        
        public void Save()
        {
            // TODO
        }
    }
}
