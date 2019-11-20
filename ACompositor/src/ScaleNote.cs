using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class ScaleNote
    {
        /// <summary>
        /// Base note for scale
        /// </summary>
        Note scaleNote;

        /// <summary>
        /// Scale
        /// </summary>
        Scale scale;

        /// <summary>
        /// Notes
        /// </summary>
        List<Note> notes;

        /// <summary>
        /// Scale chords
        /// </summary>
        List<List<Note>> scaleChord;

        /// <summary>
        /// Chord length
        /// </summary>
        int chord_length;

        /// <summary>
        /// Checker for scaled
        /// </summary>
        bool isScaled;

        /// <summary>
        /// First scale num
        /// </summary>
        int valFirst;

        /// <summary>
        /// Second scale num
        /// </summary>
        int valSecond;

        /// <summary>
        /// Scale
        /// </summary>
        public Scale Scale { get => scale; set => scale = value; }

        /// <summary>
        /// Base note for scale
        /// </summary>
        public Note BaseNote { get => scaleNote; set => scaleNote = value; }

        /// <summary>
        /// Notes
        /// </summary>
        public List<Note> Notes { get => notes; set => notes = value; }

        /// <summary>
        /// Scale chords
        /// </summary>
        public List<List<Note>> ScaleChord { get => scaleChord; set => scaleChord = value; }

        /// <summary>
        /// Chord length
        /// </summary>
        public int Chord_length { get => chord_length; set => chord_length = value; }

        /// <summary>
        /// Checker for scaled
        /// </summary>
        public bool IsScaled { get => isScaled; set => isScaled = value; }

        /// <summary>
        /// First scale num
        /// </summary>
        public int ValFirst { get => valFirst; set => valFirst = value; }

        /// <summary>
        /// Second scale num
        /// </summary>
        public int ValSecond { get => valSecond; set => valSecond = value; }

        /// <summary>
        /// Fundamental constructor
        /// </summary>
        public ScaleNote()
        {
            notes = new List<Note>();

            scaleChord = new List<List<Note>>();

            isScaled = false;
        }

        /// <summary>
        /// Make random scale
        /// </summary>
        public void MakeScale(Scale _scale, Note _baseNote, int _chordLength)
        {
            scaleNote = _baseNote;
            scale = _scale;
            chord_length = _chordLength;

            Note buffer_note = scaleNote;

            int count = 1;

            // 1. scale decision
            // 1.1. random decision
            if (scale == Scale.NULL || scaleNote == Note.NULL)
            {
                if (new Random().Next() % 2 == 0)
                {
                    scale = Scale.Major;

                    valFirst = 3;
                    valSecond = 7;
                }
                else
                {
                    scale = Scale.Minor;

                    valFirst = 2;
                    valSecond = 5;
                }

                scaleNote = (Note)(new Random().Next() % 12);
            }
            // 1.2. preset decision
            else
            {
                if (scale == Scale.Major)
                {
                    valFirst = 3;
                    valSecond = 7;
                }
                else
                {
                    valFirst = 2;
                    valSecond = 5;
                }
            }

            // 2. make scale note list 
            notes.Add(scaleNote);

            buffer_note = scaleNote;

            count = 1;

            while (notes.Count < 7)
            {
                if (count == valFirst || count == valSecond)
                {
                    buffer_note = (Note)(((int)buffer_note + 1) % 12);
                    notes.Add(buffer_note);
                }
                else
                {
                    buffer_note = (Note)(((int)buffer_note + 2) % 12);
                    notes.Add(buffer_note);
                }
                count++;
            }

            count = 0;

            // 3. make scale chords
            while (scaleChord.Count < 7)
            {
                if (chord_length == 3)
                {
                    scaleChord.Add(NoteArrayToList(new Note[] { notes[count], notes[(count + 2) % 7], notes[(count + 4) % 7] }));
                }
                else if (chord_length == 4)
                {
                    scaleChord.Add(NoteArrayToList(new Note[] { notes[count], notes[(count + 2) % 7], notes[(count + 4) % 7], notes[(count + 6) % 7] }));
                }

                count++;

            }

            isScaled = true;

            return;
        }

        /// <summary>
        /// Clears all data
        /// </summary>
        public void Clear()
        {
            notes.Clear();

            scaleChord.Clear();

            scale = Scale.NULL;

            BaseNote = Note.NULL;

            chord_length = 0;

            isScaled = false;
        }

        /// <summary>
        /// trasform note array to note list
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        List<Note> NoteArrayToList(Note[] notes)
        {
            List<Note> result = new List<Note>();

            foreach (Note iter in notes)
            {
                result.Add(iter);
            }

            return result;
        }


    }
}
