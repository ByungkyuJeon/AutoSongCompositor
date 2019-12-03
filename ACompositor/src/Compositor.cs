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
    
        public Composition GenerateFull(Composition _composition)
        {
            // result comp.
            Composition _result = _composition;

            // 1. making core form
            _result.CoreForm = GenerateCoreForm(_composition);

            // 2. make forms based on core form
            foreach (Variation _vari in _composition.Setting.Variations)
            {
                // 2.1 check variation and variate
                switch (_vari)
                {
                    case (Variation.Origin):

                        _result.Forms.Add(_result.CoreForm);
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Type = _composition.Setting.Forms[_composition.Setting.Variations.IndexOf(_vari)];
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Variation = _vari;

                        break;

                    case (Variation.Shrink):

                        _result.Forms.Add(VariateShrink(_result.CoreForm));
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Type = _composition.Setting.Forms[_composition.Setting.Variations.IndexOf(_vari)];
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Variation = _vari;

                        break;

                    case (Variation.Extend):

                        _result.Forms.Add(VariateExtend(_result.CoreForm, _result.CoreScale));
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Type = _composition.Setting.Forms[_composition.Setting.Variations.IndexOf(_vari)];
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Variation = _vari;

                        break;

                    case (Variation.Newition):

                        _result.Forms.Add(VariateNew(_composition));
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Type = _composition.Setting.Forms[_composition.Setting.Variations.IndexOf(_vari)];
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Variation = _vari;

                        break;

                    case (Variation.Octaviation):

                        _result.Forms.Add(VariateOctave(_result.CoreForm));
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Type = _composition.Setting.Forms[_composition.Setting.Variations.IndexOf(_vari)];
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Variation = _vari;

                        break;

                    case (Variation.Tailing):

                        _result.Forms.Add(VariateTail(_result.CoreForm, _result.CoreScale));
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Type = _composition.Setting.Forms[_composition.Setting.Variations.IndexOf(_vari)];
                        _result.Forms[_composition.Setting.Variations.IndexOf(_vari)].Variation = _vari;

                        break;
                }
            }         

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

            // 1.1. make scale
            _scaleNote.MakeScale(_composition.Setting.Scale, _composition.Setting.ScaleNote, _composition.Setting.ChordHeight);

            return _scaleNote;
        }

        /// <summary>
        /// Generates core form
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Form GenerateCoreForm(Composition _composition)
        {
            Form _result = new Form();

            Composition _compBuffer = _composition;

            _compBuffer.CoreForm = _result;

            _result.Rhythm = GenerateRhythm(_compBuffer);

            _result.Chord = GenerateChord(_compBuffer);

            _result.Mellody = GenerateMellody(_compBuffer);

            return _result;
        }

        /// <summary>
        /// Generates rhythm
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Rhythm GenerateRhythm(Composition _composition)
        {
            Rhythm result = new Rhythm();

            result.MakeRhythm(_composition.Setting.LoopCount, _composition.Setting.ChordCount, 2);

            return result;
        }

        /// <summary>
        /// Randomly generates core chord
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Chord GenerateChord(Composition _composition)
        {
            Chord _result = new Chord();

            // 1. scale state check & make scale
            if (!_composition.CoreScale.IsScaled)
            {
                _composition.CoreScale = GenerateScale(_composition);
            }

            // 2. Makes diatonic chord mood list
            _result.ChordType = MakeReplList(_composition.Setting.ChordCount, 2);

            // 3. Makes specific chords
            _result.FullChord = MakeSpecificChord(_result.ChordType, _composition.CoreScale.ScaleChord, _composition.Setting.BaseOctave);

            return _result;
        }

        /// <summary>
        /// Makes core mellody
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        public Mellody GenerateMellody(Composition _composition)
        {
            Mellody _result = new Mellody();

            // avoid notes
            List<List<Note>> _avoid;

            // tension notes
            List<List<Note>> _tension;

            // chord tone notes
            List<List<Note>> _tone;

            // 1. scale state check & make scale
            if (!_composition.CoreScale.IsScaled)
            {
                _composition.CoreScale = GenerateScale(_composition);
            }

            // 2. make mellody tone list
            _avoid = GetMellToneList(MellTone.Avoid, _composition.CoreForm, _composition.CoreScale);
            _tension = GetMellToneList(MellTone.Tension, _composition.CoreForm, _composition.CoreScale);
            _tone = GetMellToneList(MellTone.Tone, _composition.CoreForm, _composition.CoreScale);

            // count buffer
            int _countBuffer = 0;

            // random buffer
            int _ranBuffer;

            // 3. make specific mellody notes
            foreach(int _val in _composition.CoreForm.Rhythm.NoteTime)
            {
                _ranBuffer = random.Next() % 10;

                // tension : 30 %
                if (_ranBuffer <= 2)
                {
                    _result.FullMellody.Add(GetOctaveNote(_tension[_countBuffer / 8][random.Next() % _tension[_countBuffer / 8].Count], _composition.Setting.BaseOctave));
                }
                // chord tone : 70%
                else
                {
                    _result.FullMellody.Add(GetOctaveNote(_tone[_countBuffer / 8][random.Next() % _tone[_countBuffer / 8].Count], _composition.Setting.BaseOctave));
                }

                _countBuffer += _val;
            }

            return _result;
        }

        /// <summary>
        /// Randomly generates rhythm data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Rhythm GenerateRhythm(Form _form)
        {
            // implement
            return new Rhythm();
        }

        /// <summary>
        /// Randomly generates chord data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Chord GenerateChord(Form _form)
        {
            Chord _result = new Chord();

            // TODO :: implement
            return new Chord();
        }

        /// <summary>
        /// Randomly generates mellody data
        /// </summary>
        /// <param name="composition"></param>
        /// <returns></returns>
        public Mellody GenerateMellody(Form _form)
        {
            // TODO :: implement
            return new Mellody();
        }

        /// <summary>
        /// Shrink mellodies, rhythms in the form
        /// </summary>
        /// <param name="_origin"></param>
        /// <returns></returns>
        Form VariateShrink(Form _origin)
        {
            Form _result = new Form();

            // deep copy the form
            _result.Copy(_origin);

            // variate
            foreach(int _iter in _origin.Rhythm.NoteTime)
            {
                // check the item index
                if(_origin.Rhythm.NoteTime.IndexOf(_iter) % 2 == 1 && _origin.Rhythm.NoteTime.IndexOf(_iter) != _origin.Rhythm.NoteTime.Count - 1)
                {
                    // change original list
                    // 1. change value
                    // rhythm (time value)
                    _result.Rhythm.NoteTime[_origin.Rhythm.NoteTime.IndexOf(_iter) - 1] += _result.Rhythm.NoteTime[_origin.Rhythm.NoteTime.IndexOf(_iter)];

                    // 2. delete value
                    // rhythm
                    _result.Rhythm.NoteTime.RemoveAt(_origin.Rhythm.NoteTime.IndexOf(_iter));
                    // mellody
                    _result.Mellody.FullMellody.RemoveAt(_origin.Rhythm.NoteTime.IndexOf(_iter));
                }
            }

            return _result;
        }

        /// <summary>
        /// Variate tail randomly
        /// </summary>
        /// <param name="_origin"></param>
        /// <param name="_scale"></param>
        /// <returns></returns>
        Form VariateTail(Form _origin, ScaleNote _scale)
        {
            Form _result = new Form();

            // avoid notes
            List<List<Note>> _avoid;

            // tension notes
            List<List<Note>> _tension;

            // chord tone notes
            List<List<Note>> _tone;

            // deep copy the form
            _result.Copy(_origin);

            // 1. make mellody tone list
            _avoid = GetMellToneList(MellTone.Avoid, _origin, _scale);
            _tension = GetMellToneList(MellTone.Tension, _origin, _scale);
            _tone = GetMellToneList(MellTone.Tone, _origin, _scale);

            // count buffer
            int _countBuffer = 0;

            // random buffer
            int _ranBuffer;

            // tail buffer
            int _tailBuffer = 0;

            // index buffer
            int _indexBuffer = 0;

            // 1. calculate full time
            foreach (int _iter in _origin.Rhythm.NoteTime)
            {
                _tailBuffer += _iter;
            }

            // 75% of value
            _tailBuffer = _tailBuffer - (_tailBuffer / 4);

            // 2. variate
            foreach (int _iter in _origin.Rhythm.NoteTime)
            {
                // check the item time value
                if (_countBuffer > _tailBuffer)
                {
                    _indexBuffer = _origin.Rhythm.NoteTime.IndexOf(_iter);

                    break;

                }

                _countBuffer += _iter;
            }

            // reset mellodies
            _result.Mellody.FullMellody.Clear();

            // refresh count buffer
            _countBuffer = 0;

            // 3. make new mellody notes
            foreach (int _iter in _origin.Rhythm.NoteTime)
            {
                // check index : first 75% notes to be same / last 25% notes to be new
                if (_origin.Rhythm.NoteTime.IndexOf(_iter) < _indexBuffer)
                {
                    _result.Mellody.FullMellody.Add(_origin.Mellody.FullMellody[_origin.Rhythm.NoteTime.IndexOf(_iter)]);
                }
                else
                {
                    _ranBuffer = random.Next() % 10;

                    // tension : 30 %
                    if (_ranBuffer <= 2)
                    {
                        _result.Mellody.FullMellody.Add(_tension[_countBuffer / 8][random.Next() % _tension[_countBuffer / 8].Count]);
                    }
                    // chord tone : 70%
                    else
                    {
                        _result.Mellody.FullMellody.Add(_tone[_countBuffer / 8][random.Next() % _tone[_countBuffer / 8].Count]);
                    }

                }

                _countBuffer += _iter;
            }

            return _result;
        }

        /// <summary>
        /// Make new rhythm and mellody
        /// </summary>
        /// <param name="_composition"></param>
        /// <returns></returns>
        Form VariateNew(Composition _composition)
        {
            Form _result = new Form();

            // deep copy the form
            _result.Copy(_composition.CoreForm);

            _result.Rhythm = GenerateRhythm(_composition);

            _result.Mellody = GenerateMellody(_composition);

            return _result;
        }

        /// <summary>
        /// Variate octave randomly
        /// </summary>
        /// <param name="_origin"></param>
        /// <returns></returns>
        Form VariateOctave(Form _origin)
        {
            // result data
            Form _result = new Form();

            // deep copy the form
            _result.Copy(_origin);

            // octave data
            int _oct = 0;

            // octave down
            if (random.Next() % 2 == 0)
            {
                _oct = -12;
            }
            // octave up
            else
            {
                _oct = 12;
            }

            // apply variation
            foreach (Note _iter in _origin.Mellody.FullMellody)
            {
                if (_iter != Note.NULL)
                {
                    _result.Mellody.FullMellody[_origin.Mellody.FullMellody.IndexOf(_iter)] = (Note)(_iter + _oct);
                }
            }

            return _result;
        }

        /// <summary>
        /// Extends mellodies, rhythm in the form
        /// </summary>
        /// <param name="_origin"></param>
        /// <param name="_scale"></param>
        /// <returns></returns>
        Form VariateExtend(Form _origin, ScaleNote _scale)
        {
            Form _result = new Form();

            // avoid notes
            List<List<Note>> _avoid;

            // tension notes
            List<List<Note>> _tension;

            // chord tone notes
            List<List<Note>> _tone;

            // deep copy the form
            _result.Copy(_origin);

            // 1. make mellody tone list
            _avoid = GetMellToneList(MellTone.Avoid, _origin, _scale);
            _tension = GetMellToneList(MellTone.Tension, _origin, _scale);
            _tone = GetMellToneList(MellTone.Tone, _origin, _scale);

            // count buffer
            int _countBuffer = 0;

            // random buffer
            int _ranBuffer;

            // 2. variate
            foreach (int _iter in _origin.Rhythm.NoteTime)
            {
                // check the item index
                if (_iter != 1)
                {
                    _ranBuffer = random.Next() % 10;

                    _result.Rhythm.NoteTime[_origin.Rhythm.NoteTime.IndexOf(_iter)] /= 2;
                    _result.Rhythm.NoteTime.Insert(_origin.Rhythm.NoteTime.IndexOf(_iter), _iter / 2);

                    // tension : 30 %
                    if (_ranBuffer <= 2)
                    {
                        _result.Mellody.FullMellody.Insert(_origin.Rhythm.NoteTime.IndexOf(_iter),
                            _tension[_countBuffer / 8][random.Next() % _tension[_countBuffer / 8].Count]);
                    }
                    // chord tone : 70%
                    else
                    {
                        _result.Mellody.FullMellody.Insert(_origin.Rhythm.NoteTime.IndexOf(_iter),
                            _tone[_countBuffer / 8][random.Next() % _tone[_countBuffer / 8].Count]);
                    }

                }

                _countBuffer += _iter;
            }

            return _result;
        }

        List<List<Note>> GetMellToneList(MellTone _mellTone, Form _form, ScaleNote _scale)
        {
            // avoid notes
            List<List<Note>> _avoid = new List<List<Note>>();

            // tension notes
            List<List<Note>> _tension = new List<List<Note>>();

            // chord tone notes
            List<List<Note>> _tone = new List<List<Note>>();

            int _lastNoteBuffer;
            int _thirdNoteBuffer;

            // 1. prepare usable note list
            for (int _count = 0; _count < _form.Chord.FullChord.Count; _count++)
            {
                _avoid.Add(new List<Note>());
                _tension.Add(new List<Note>());
                _tone.Add(new List<Note>());
            }

            // copy chord as pure
            List<List<Note>> _chordBuffer = new List<List<Note>>();
            List<Note> _subBuffer = new List<Note>();

            foreach(List<Note> _iterChord in _form.Chord.FullChord)
            {
                foreach(Note _iterNote in _iterChord)
                {
                    _subBuffer.Add(GetPureNote(_iterNote));
                }
                _chordBuffer.Add(CopyNoteList(_subBuffer));
                _subBuffer.Clear();
            }

            // 2. make avoid, tension and chord tone note list
            foreach (List<Note> _iterChord in _chordBuffer)
            {
                foreach (Note _iterNote in _iterChord)
                {
                    // chord tone
                    _tone[_chordBuffer.IndexOf(_iterChord)].Add(GetPureNote(_iterNote));

                    // check if there is 3 avoid or tension notes
                    if (_tone[_chordBuffer.IndexOf(_iterChord)].Count == _iterChord.Count)
                    {
                        break;
                    }

                    // last note in the chord
                    _lastNoteBuffer = ((int)_iterChord[_iterChord.Count - 1] % 12);

                    // 해당 코드에서 n번째 3도 위 음의 인덱스(스케일에서의 인덱스)
                    _thirdNoteBuffer = (_scale.Notes.IndexOf((Note)_lastNoteBuffer) + _iterChord.IndexOf(_iterNote)) % 7;

                    // avoid 
                    if ((int)_scale.Notes[_thirdNoteBuffer] - _lastNoteBuffer == 1)
                    {
                        _avoid[_chordBuffer.IndexOf(_iterChord)].Add(_scale.Notes[_thirdNoteBuffer]);
                    }
                    // tension
                    else
                    {
                        _tension[_chordBuffer.IndexOf(_iterChord)].Add(_scale.Notes[_thirdNoteBuffer]);
                    }
                }
            }

            switch(_mellTone)
            {
                case (MellTone.Tone):
                    return _tone;
                case (MellTone.Tension):
                    return _tension;
                case (MellTone.Avoid):
                    return _avoid;
            }

            return _tone;
        }

        /// <summary>
        /// Makes diatonic chord mood list
        /// </summary>
        /// <param name="ch_count"></param>
        /// <returns></returns>
        List<ReplChord> MakeReplList(int _chordCount, int _chordLength)
        {
            List<ReplChord> _repls = new List<ReplChord>();

            int _count = _chordCount * _chordLength;
            int _ranBuffer;

            // 1. main chord form making
            while (_count > 0)
            {
                _ranBuffer = random.Next() % 3;

                if (_ranBuffer == 0)
                {
                    _repls.Add(ReplChord.Dominant);
                }
                else if(_ranBuffer == 1)
                {
                    _repls.Add(ReplChord.SubDominant);
                }
                else
                {
                    _repls.Add(ReplChord.Tonic);
                }

                _count--;
            }

            return _repls;  
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

                        _result.Add(CopyNoteList(_scaleChords[(int)GetRandomRepl(iter)]));

                        break;

                    case (ReplChord.SubDominant):

                        _result.Add(CopyNoteList(_scaleChords[(int)GetRandomRepl(iter)]));

                        break;

                    case (ReplChord.Dominant):

                        _result.Add(CopyNoteList(_scaleChords[(int)GetRandomRepl(iter)]));

                        break;
                }
            }

            // copy chord as pure
            List<List<Note>> _chordBuffer = new List<List<Note>>();
            List<Note> _subBuffer = new List<Note>();

            foreach (List<Note> _iterChord in _result)
            {
                foreach (Note _iterNote in _iterChord)
                {
                    _subBuffer.Add(GetPureNote(_iterNote));
                }
                _chordBuffer.Add(CopyNoteList(_subBuffer));
                _subBuffer.Clear();
            }

            // 1. specific chord making
            for (int _iter = 0; _iter < _chordBuffer.Count; _iter++)
            {
                for(int _itt = 0; _itt < _chordBuffer[_iter].Count; _itt++)
                {
                    _result[_iter][_itt] = GetOctaveNote(_chordBuffer[_iter][_itt], _baseOctave - 1);
                }
            }
            

            return _result;
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
        /// Copy note array
        /// </summary>
        /// <param name="_copy"></param>
        /// <returns></returns>
        List<Note> CopyNoteList(List<Note> _copy)
        {
            List<Note> _result = new List<Note>();

            foreach(Note _iter in _copy)
            {
                _result.Add(_iter);
            }

            return _result;
        }

        /// <summary>
        /// makes code dictionary data
        /// </summary>
        public void InitiateChordDic()
        {
            chordDic = new Dictionary<ChordNote, Note[]>();

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
