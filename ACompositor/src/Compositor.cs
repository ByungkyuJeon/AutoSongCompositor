using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Compositor
    {
        /// <summary>
        /// dictionary for code compositions
        /// </summary>
        Dictionary<ChordNote, Note[]> chordDic;

        /// <summary>
        /// Random
        /// </summary>
        Random random;

        /// <summary>
        /// Fundamental constructor - Preparing engine
        /// </summary>
        public Compositor()
        {
            // Initiation
            Initiate();
        }

        /// <summary>
        /// Engine Initiation
        /// </summary>
        public void Initiate()
        {
            // 1. make chord dictionary
            InitiateChordDic();

            // 2. 
        }

        /// <summary>
        /// Randomly generates chord data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Chord GenerateChord(Composition composition)
        {
            // TODO :: implement
            return new Chord();
        }

        /// <summary>
        /// Randomly generates mellody data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Mellody GenerateMellody(Composition composition)
        {
            // TODO :: implement
            return new Mellody();
        }

        /// <summary>
        /// Randomly generates pattern data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Pattern GeneratePattern(Composition composition)
        {
            // TODO :: implement
            return new Pattern();
        }

        /// <summary>
        /// Randomly generates rhythm data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Rhythm GenerateRhythm(Composition composition)
        {
            // implement
            return new Rhythm();
        }

        /// <summary>
        /// makes code dictionary data
        /// </summary>
        public void InitiateChordDic()
        {
            // C Codes
            chordDic.Add(ChordNote.C_Major, new Note[3] { Note.C, Note.E, Note.G });
            chordDic.Add(ChordNote.C_Minor, new Note[3] { Note.C, Note.Du, Note.G });
            chordDic.Add(ChordNote.C_Sus4, new Note[3] { Note.C, Note.F, Note.G });
            chordDic.Add(ChordNote.C_Dim, new Note[3] { Note.C, Note.Du, Note.Fu });
            chordDic.Add(ChordNote.C_M7, new Note[4] { Note.C, Note.E, Note.G, Note.Au });
            chordDic.Add(ChordNote.C_M6, new Note[4] { Note.C, Note.E, Note.G, Note.A });
            chordDic.Add(ChordNote.C_M2_9, new Note[4] { Note.C, Note.D, Note.E, Note.G });

            // D Codes
            chordDic.Add(ChordNote.D_Major, new Note[3] { Note.D, Note.Fu, Note.A });
            chordDic.Add(ChordNote.D_Minor, new Note[3] { Note.D, Note.F, Note.A });
            chordDic.Add(ChordNote.D_Sus4, new Note[3] { Note.D, Note.G, Note.A });
            chordDic.Add(ChordNote.D_Dim, new Note[3] { Note.D, Note.F, Note.Gu });
            chordDic.Add(ChordNote.D_M7, new Note[3] { Note.C, Note.Fu, Note.A });
            chordDic.Add(ChordNote.D_M6, new Note[4] { Note.D, Note.Fu, Note.A, Note.B });
            chordDic.Add(ChordNote.D_M2_9, new Note[4] { Note.D, Note.E, Note.Fu, Note.A });

            // E Codes
            chordDic.Add(ChordNote.E_Major, new Note[3] { Note.E, Note.Gu, Note.B });
            chordDic.Add(ChordNote.E_Minor, new Note[3] { Note.E, Note.G, Note.B });
            chordDic.Add(ChordNote.E_Sus4, new Note[3] { Note.E, Note.A, Note.B });
            chordDic.Add(ChordNote.E_Dim, new Note[3] { Note.E, Note.G, Note.Au });
            chordDic.Add(ChordNote.E_M7, new Note[4] { Note.E, Note.Gu, Note.B, Note.D });
            chordDic.Add(ChordNote.E_M6, new Note[4] { Note.E, Note.Gu, Note.B, Note.Cu });
            chordDic.Add(ChordNote.E_M2_9, new Note[4] { Note.E, Note.Fu, Note.Gu, Note.B });

            // F Codes
            chordDic.Add(ChordNote.F_Major, new Note[3] { Note.F, Note.A, Note.C });
            chordDic.Add(ChordNote.F_Minor, new Note[3] { Note.F, Note.Gu, Note.C });
            chordDic.Add(ChordNote.F_Sus4, new Note[3] { Note.F, Note.Au, Note.C });
            chordDic.Add(ChordNote.F_Dim, new Note[3] { Note.F, Note.Gu, Note.B });
            chordDic.Add(ChordNote.F_M7, new Note[4] { Note.F, Note.A, Note.C, Note.Du });
            chordDic.Add(ChordNote.F_M6, new Note[4] { Note.F, Note.A, Note.C, Note.D });
            chordDic.Add(ChordNote.F_M2_9, new Note[4] { Note.F, Note.G, Note.A, Note.C });

            // G Codes
            chordDic.Add(ChordNote.G_Major, new Note[3] { Note.G, Note.B, Note.D });
            chordDic.Add(ChordNote.G_Minor, new Note[3] { Note.G, Note.Au, Note.D });
            chordDic.Add(ChordNote.G_Sus4, new Note[3] { Note.G, Note.C, Note.D });
            chordDic.Add(ChordNote.G_Dim, new Note[3] { Note.G, Note.Au, Note.Cu });
            chordDic.Add(ChordNote.G_M7, new Note[4] { Note.G, Note.B, Note.D, Note.F });
            chordDic.Add(ChordNote.G_M6, new Note[4] { Note.G, Note.B, Note.D, Note.E });
            chordDic.Add(ChordNote.G_M2_9, new Note[4] { Note.G, Note.A, Note.B, Note.D });

            // A Codes
            chordDic.Add(ChordNote.A_Major, new Note[3] { Note.A, Note.Cu, Note.E });
            chordDic.Add(ChordNote.A_Minor, new Note[3] { Note.A, Note.C, Note.E });
            chordDic.Add(ChordNote.A_Sus4, new Note[3] { Note.A, Note.D, Note.E });
            chordDic.Add(ChordNote.A_Dim, new Note[3] { Note.A, Note.C, Note.Du });
            chordDic.Add(ChordNote.A_M7, new Note[4] { Note.A, Note.Cu, Note.E, Note.G });
            chordDic.Add(ChordNote.A_M6, new Note[4] { Note.A, Note.Cu, Note.E, Note.Fu });
            chordDic.Add(ChordNote.A_M2_9, new Note[4] { Note.A, Note.B, Note.Cu, Note.E });

            // B Codes
            chordDic.Add(ChordNote.B_Major, new Note[3] { Note.B, Note.Du, Note.Fu });
            chordDic.Add(ChordNote.B_Minor, new Note[3] { Note.B, Note.D, Note.Fu });
            chordDic.Add(ChordNote.B_Sus4, new Note[3] { Note.B, Note.E, Note.Fu });
            chordDic.Add(ChordNote.B_Dim, new Note[3] { Note.B, Note.D, Note.F });
            chordDic.Add(ChordNote.B_M7, new Note[4] { Note.B, Note.Du, Note.Fu, Note.A });
            chordDic.Add(ChordNote.B_M6, new Note[4] { Note.B, Note.Du, Note.Fu, Note.Gu });
            chordDic.Add(ChordNote.B_M2_9, new Note[4] { Note.B, Note.Cu, Note.Du, Note.Fu });
        }
    }
}
