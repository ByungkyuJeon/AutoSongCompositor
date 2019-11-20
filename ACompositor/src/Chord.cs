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
        /// Full chord
        /// </summary>
        List<List<Note>> fullChord;

        /// <summary>
        /// Reple type of chord
        /// </summary>
        List<ReplChord> chordType;

        /// <summary>
        /// Full chord
        /// </summary>
        public List<List<Note>> FullChord { get => fullChord; set => fullChord = value; }

        /// <summary>
        /// Reple type of chord
        /// </summary>
        public List<ReplChord> ChordType { get => chordType; set => chordType = value; }

        public Chord()
        {
            fullChord = new List<List<Note>>();
        }
    }
}
