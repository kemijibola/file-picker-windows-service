using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Lite.Service
{
    public class DecryptViewModel
    {
        public string EncryptedFilePath { get; set; }
        public string OutputPath { get; set; }
    }

    public class DecryptResponseVM
    {
        public bool status { get; set; }
        public string Message { get; set; }
    }
}
