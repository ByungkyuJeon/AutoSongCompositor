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

            // 2. random
            random = new Random();
        }

        /// <summary>
        /// Generates core form
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Form GenerateCoreForm(Composition _composition)
        {
            Form _result = new Form();

            _result.Rhythm = GenerateCoreRhythm(_composition);

            _result.Chord = GenerateCoreChord(_composition);

            _result.Mellody = GenerateCoreMellody(_composition);

            return _result;
        }
    
        public Composition GenerateFull(Composition _composition)
        {
            // result comp.
            Composition _result = _composition;

            // 1. making core form
            _result.CoreForm = GenerateCoreForm(_composition);





            return _result;
        }

        /// <summary>
        /// Makes scale data
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public ScaleNote GenerateScale(Composition _composition)
        {
            // scale data
            ScaleNote _scaleNote = new ScaleNote();

            // 1. Generate core chord

            // 1.1. make scale
            _scaleNote.MakeScale(_composition.Setting.Scale, _composition.Setting.ScaleNote, _composition.Setting.ChordHeight);

            return _scaleNote;
        }

        /// <summary>
        /// Generates rhythm
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Rhythm GenerateCoreRhythm(Composition _composition)
        {
            Rhythm result = new Rhythm();

            result.MakeRhythm(_composition.Setting.LoopCount);

            return result;
        }

        /// <summary>
        /// Randomly generates core chord
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Chord GenerateCoreChord(Composition _composition)
        {
            Chord _result = new Chord();

            // 1. scale state check & make scale
            if (!_composition.CoreScale.IsScaled)
            {
                _composition.CoreScale = GenerateScale(_composition);
            }

            // 2. Makes diatonic chord mood list
            _result.ChordType = MakeReplList(_composition.Setting.ChordCount);

            // 3. Makes specific chords
            _result.FullChord = MakeSpecificChord(_result.ChordType, _composition.CoreScale.ScaleChord, _composition.Setting.BaseOctave);

            return _result;
        }

        /// <summary>
        /// Makes core mellody
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Mellody GenerateCoreMellody(Composition _composition)
        {
            Mellody _result = new Mellody();

            // avoid notes
            List<List<Note>> _avoid = new List<List<Note>>();

            // tension notes
            List<List<Note>> _tension = new List<List<Note>>();

            // chord tone notes
            List<List<Note>> _tone = new List<List<Note>>();

            // 1. scale state check & make scale
            if (!_composition.CoreScale.IsScaled)
            {
                _composition.CoreScale = GenerateScale(_composition);
            }

            // 2. prepare usable note list
            for (int _count = 0; _count < _composition.CoreForm.Chord.FullChord.Count; _count++)
            {
                _avoid.Add(new List<Note>());
                _tension.Add(new List<Note>());
                _tone.Add(new List<Note>());
            }

            // 3. make avoid, tension and chord tone note list
            foreach (List<Note> _iterChord in _composition.CoreForm.Chord.FullChord)
            {
                foreach (Note _iterNote in _iterChord)
                {
                    // chord tone
                    _tone[_composition.CoreForm.Chord.FullChord.IndexOf(_iterChord)].Add(GetPureNote(_iterNote));

                    // check if there is 3 avoid or tension notes
                    if (_tone[_composition.CoreForm.Chord.FullChord.IndexOf(_iterChord)].Count == _iterChord.Count)
                    {
                        break;
                    }

                    // 해당 코드 마지막 음
                    _composition.CoreScale.ValFirst = ((int)_iterChord[_iterChord.Count - 1] % 12);

                    // 해당 코드에서 n번째 3도 위 음의 인덱스(스케일에서의 인덱스)
                    _composition.CoreScale.ValSecond = (_composition.CoreScale.Notes.IndexOf((Note)_composition.CoreScale.ValFirst) + _iterChord.IndexOf(_iterNote)) % 7;

                    // avoid 
                    if ((int)_composition.CoreScale.Notes[_composition.CoreScale.ValSecond] - _composition.CoreScale.ValFirst == 1)
                    {
                        _avoid[_composition.CoreForm.Chord.FullChord.IndexOf(_iterChord)].Add(_composition.CoreScale.Notes[_composition.CoreScale.ValSecond]);
                    }
                    // tension
                    else
                    {
                        _tension[_composition.CoreForm.Chord.FullChord.IndexOf(_iterChord)].Add(_composition.CoreScale.Notes[_composition.CoreScale.ValSecond]);
                    }
                }
            }

            // count buffer
            int _countBuffer = 0;

            // random buffer
            int _ranBuffer;

            foreach(int _val in _composition.CoreForm.Rhythm.NoteTime)
            {
                _ranBuffer = random.Next() % 10;

                // tension : 30 %
                if (_ranBuffer <= 2)
                {
                    _result.FullMellody.Add(_tension[_countBuffer / 8][random.Next() % _tension[_countBuffer / 8].Count]);
                }
                // chord tone : 70%
                else
                {
                    _result.FullMellody.Add(_tone[_countBuffer / 8][random.Next() % _tone[_countBuffer / 8].Count]);
                }

                _countBuffer += _val;
            }

            return _result;
        }



        /// <summary>
        /// Randomly generates chord data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Chord GenerateChord(Composition _composition)
        {
            // TODO :: implement
            return new Chord();
        }

        /// <summary>
        /// Randomly generates mellody data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Mellody GenerateMellody(Composition _composition)
        {
            // TODO :: implement
            return new Mellody();
        }

        /// <summary>
        /// Randomly generates pattern data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Pattern GeneratePattern(Composition _composition)
        {
            // TODO :: implement
            return new Pattern();
        }

        /// <summary>
        /// Randomly generates rhythm data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Rhythm GenerateRhythm(Composition _composition)
        {
            // implement
            return new Rhythm();
        }

        /// <summary>
        /// Makes diatonic chord mood list
        /// </summary>
        /// <param name="ch_count"></param>
        /// <returns></returns>
        List<ReplChord> MakeReplList(int ch_count)
        {
            List<ReplChord> repls = new List<ReplChord>();
            List<ReplChord> rpelbuffer = new List<ReplChord>();
            List<int> rpelnum = new List<int>();
            bool passable = true;
            int count = 0;

            // 1. main chord form making
            if (ch_count == 4)
            {
                if (random.Next() % 2 == 0)
                {
                    repls.Add(ReplChord.Tonic);
                }
                else
                {
                    repls.Add(ReplChord.SubDominant);
                }
            }

            repls.Add(ReplChord.Dominant);
            repls.Add(ReplChord.SubDominant);
            repls.Add(ReplChord.Tonic);


            // 2. randomly sort repls
            for (int i = 0; i < repls.Count;)
            {
                passable = true;

                count = random.Next() % repls.Count;

                foreach (int iter in rpelnum)
                {
                    if (iter == count) { passable = false; break; }
                }
                if (!passable) { continue; }

                rpelnum.Add(random.Next() % repls.Count);
                rpelbuffer.Add(repls[i]);

                i++;
            }

            return rpelbuffer;  
        }

        /// <summary>
        /// Makes specific chord
        /// </summary>
        /// <param name="_repls"></param>
        /// <param name="_scaleChords"></param>
        /// <param name="_baseOctave"></param>
        /// <returns></returns>
        List<List<Note>> MakeSpecificChord(List<ReplChord> _repls, List<List<Note>> _scaleChords, int _baseOctave)
        {
            List<List<Note>> _result = new List<List<Note>>();

            // 1. chord mood making
            foreach (ReplChord iter in _repls)
            {
                // make the chord flow random 
                switch (iter)
                {
                    case (ReplChord.Tonic):

                        _result.Add(_scaleChords[(int)GetRandomRepl(iter)]);

                        break;

                    case (ReplChord.SubDominant):

                        _result.Add(_scaleChords[(int)GetRandomRepl(iter)]);

                        break;

                    case (ReplChord.Dominant):

                        _result.Add(_scaleChords[(int)GetRandomRepl(iter)]);

                        break;
                }
            }

            // 1.1 _result list copy to buffer
            List<List<Note>> _bufferList = new List<List<Note>>();
            List<Note> _subBuffer = new List<Note>();

            foreach (List<Note> iter in _result)
            {
                foreach (Note itt in iter)
                {
                    _subBuffer.Add(itt);
                }
                _bufferList.Add(_subBuffer);
                _subBuffer = new List<Note>();
            }

            // 2. specific chord making
            foreach (List<Note> iter in _result)
            {
                foreach (Note itt in iter)
                {
                    _bufferList[_result.IndexOf(iter)][iter.IndexOf(itt)] = GetOctaveNote(itt, _baseOctave);
                }
            }

            return _bufferList;
        }

        /// <summary>
        /// returns note that locates at parameter's octave
        /// </summary>
        /// <param name="note"></param>
        /// <param name="octave"></param>
        /// <returns></returns>
        Note GetOctaveNote(Note note, int octave)
        {
            // parameter varification
            if (octave < 1 || octave > 7)
            {
                return Note.NULL;
            }

            // calculating return
            return (Note)((int)note + 12 * octave);
        }

        /// <summary>
        /// returns replacable code by number
        /// </summary>
        /// <param name="rple"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        ReplChord GetRandomRepl(ReplChord _rple)
        {
            int _num = random.Next() % 2;

            switch (_rple)
            {
                case (ReplChord.Tonic):

                    if (_num == 0)
                    {
                        return ReplChord.Tonic;
                    }
                    else if (_num == 1)
                    {
                        return ReplChord.Tonic2;
                    }
                    else
                    {
                        return ReplChord.Tonic3;
                    }

                case (ReplChord.SubDominant):

                    if (_num == 0)
                    {
                        return ReplChord.SubDominant;
                    }
                    else
                    {
                        return ReplChord.SubDominant2;
                    }

                case (ReplChord.Dominant):

                    if (_num == 0)
                    {
                        return ReplChord.Dominant;
                    }
                    else
                    {
                        return ReplChord.Dominant2;
                    }
            }

            return _rple;
        }

        /// <summary>
        /// returns the note's base note
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        Note GetPureNote(Note note)
        {
            int buffer = (int)note;

            while (buffer > 12)
            {
                buffer -= 12;
            }

            return (Note)buffer;
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
