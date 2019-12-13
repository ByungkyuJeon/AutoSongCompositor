using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACompositor.src
{
    public static class FileManager
    {
        public static void SaveFile(List<Composition> _compositions, string _directory, string _name)
        {
            StringBuilder _content = new StringBuilder();

            // name
            _content.Append(_name);

            foreach (Composition _comp in _compositions)
            {
                // composition

            }
        }

        public static List<Composition> LoadFile(string _directory)
        {
            List<Composition> _result = new List<Composition>();


            return _result;
        }
    }
}
