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

            chordType = new List<ReplChord>();
        }

        /// <summary>
        /// Deep copy
        /// </summary>
        /// <param name="_origin"></param>
        public void Copy(Chord _origin)
        {
            List<Note> _buffer = new List<Note>();

            // chord copy
            foreach(List<Note> _iter in _origin.FullChord)
            {
                foreach(Note _subiter in _iter)
                {
                    _buffer.Add(_subiter);
                }
                fullChord.Add(_buffer);
                _buffer.Clear();
            }

            // type copy
            foreach(ReplChord _iter in _origin.ChordType)
            {
                chordType.Add(_iter);
            }
        }
    }
}
