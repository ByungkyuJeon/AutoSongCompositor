using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Chord
    {
        /// <summary>
        /// Core chord 
        /// </summary>
        List<List<Note>> coreChord;

        /// <summary>
        /// Core chord
        /// </summary>
        public List<List<Note>> CoreChord { get => coreChord; set => coreChord = value; }
    }
}
