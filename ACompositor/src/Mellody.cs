using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Mellody
    {
        /// <summary>
        /// Full mellody
        /// </summary>
        List<Note> fullMellody;

        /// <summary>
        /// Full mellody
        /// </summary>
        public List<Note> FullMellody { get => fullMellody; set => fullMellody = value; }

        public Mellody()
        {
            fullMellody = new List<Note>();
        }
    }
}
