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

            // chord copy
            for(int _iter = 0; _iter < _origin.FullChord.Count; _iter++)
            {
                fullChord.Add(new List<Note>());

                for(int _subiter = 0; _subiter < _origin.FullChord[_iter].Count; _subiter++)
                {
                    fullChord[_iter].Add(_origin.FullChord[_iter][_subiter]);
                }
            }

            // type copy
            foreach(ReplChord _iter in _origin.ChordType)
            {
                chordType.Add(_iter);
            }
        }
    }
}
