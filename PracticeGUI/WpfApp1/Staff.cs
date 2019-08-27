using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace WpfApp1
{
    class Staff
    {
        public string Name { get; set; }
        public string photo { get; set; }

        public Staff()
        {
            this.photo = null;
            this.Name = null;
        }
        public Staff(string path)
        {
            string filename = Path.GetFileName(path);
            this.Name = filename.Split('.')[0];
            this.photo = path;
        }
    }
}
