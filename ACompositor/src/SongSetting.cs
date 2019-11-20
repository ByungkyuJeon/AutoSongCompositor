using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class SongSetting
    {
        /// <summary>
        /// Song jengre
        /// </summary>
        Jengre jengre;

        /// <summary>
        /// Song scale
        /// </summary>
        Scale scale;

        /// <summary>
        /// Base scale note
        /// </summary>
        Note scaleNote;

        /// <summary>
        /// Chord height
        /// </summary>
        int chordHeight;

        /// <summary>
        /// Number of chord in a node
        /// </summary>
        int chordCount;

        /// <summary>
        /// Pattern loop count
        /// </summary>
        int loopCount;

        /// <summary>
        /// Form type list
        /// </summary>
        List<FormType> forms;

        /// <summary>
        /// Variation type list
        /// </summary>
        List<Variation> variations;

        /// <summary>
        /// Song jengre
        /// </summary>
        public Jengre Jengre { get => jengre; set => jengre = value; }

        /// <summary>
        /// Song scale
        /// </summary>
        public Scale Scale { get => scale; set => scale = value; }

        /// <summary>
        /// Base scale note
        /// </summary>
        public Note ScaleNote { get => scaleNote; set => scaleNote = value; }

        /// <summary>
        /// Chord height
        /// </summary>
        public int ChordHeight { get => chordHeight; set => chordHeight = value; }

        /// <summary>
        /// Number of chord in a node
        /// </summary>
        public int ChordCount { get => chordCount; set => chordCount = value; }

        /// <summary>
        /// Base octave
        /// </summary>
        int baseOctave;

        /// <summary>
        /// Form type list
        /// </summary>
        public List<FormType> Forms { get => forms; set => forms = value; }

        /// <summary>
        /// Variation type list
        /// </summary>
        public List<Variation> Variations { get => variations; set => variations = value; }

        /// <summary>
        /// Base octave
        /// </summary>
        public int BaseOctave { get => baseOctave; set => baseOctave = value; }

        /// <summary>
        /// Pattern loop count
        /// </summary>
        public int LoopCount { get => loopCount; set => loopCount = value; }

        /// <summary>
        /// Initiates default window set
        /// </summary>
        public SongSetting()
        {
            forms = new List<FormType>();
            variations = new List<Variation>();
        }
    }
}
