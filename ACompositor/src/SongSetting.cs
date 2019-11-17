using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class SongSetting
    {
        Jengre jengre;

        Scale scale;

        Note scaleNote;

        int chordLength;

        int chordCount;

        List<FormType> forms;

        public Jengre Jengre { get => jengre; set => jengre = value; }

        public Scale Scale { get => scale; set => scale = value; }

        public Note ScaleNote { get => scaleNote; set => scaleNote = value; }

        public int ChordLength { get => chordLength; set => chordLength = value; }

        public int ChordCount { get => chordCount; set => chordCount = value; }

        public List<FormType> Forms { get => forms; set => forms = value; }
    }
}
