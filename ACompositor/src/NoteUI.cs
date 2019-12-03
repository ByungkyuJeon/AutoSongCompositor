using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ACompositor.src
{
    class NoteUI
    {
        /// <summary>
        /// Specific name of note
        /// </summary>
        Label noteName;

        /// <summary>
        /// A rectangle that shows position of the note
        /// </summary>
        Label noteRect;

        /// <summary>
        /// Size of the note rect
        /// </summary>
        int noteSize_X;

        /// <summary>
        /// Size of the note rect
        /// </summary>
        int noteSize_Y;

        /// <summary>
        /// Distance between note name and note rect
        /// </summary>
        int nameMargin = 10;

        /// <summary>
        /// Specific name of note
        /// </summary>
        public Label NoteName { get => noteName; set => noteName = value; }

        /// <summary>
        /// A rectangle that shows position of the note
        /// </summary>
        public Label NoteRect { get => noteRect; set => noteRect = value; }

        /// <summary>
        /// Note UI constructor
        /// </summary>
        /// <param name="_note">specific note</param>
        /// <param name="_position">note pos in canvas</param>
        /// <param name="_width">canvas width</param>
        /// <param name="_height">canvas height</param>
        /// <param name="_baseOct">comp base octave</param>
        /// <param name="_option">1 for chord, 0 for mellody</param>
        /// <param name="_chordDex">chord note index</param>
        public NoteUI(Note _note, int _position, int _width, int _height, int _baseOct, int _option, int _chordDex)
        {
            // 0. set size
            noteSize_X = Math.Abs((_width - 5) / 32);
            noteSize_Y = Math.Abs((_height - 5) / 36);

            // 1. caluate note position first
            noteRect = new Label()
            {
                Width = noteSize_X / 5 * 3,
                Height = noteSize_Y / 5 * 3,
                Background = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                
            };

            Canvas.SetLeft(noteRect, 20 + ((_width - 40) / 36 * _position));
            Canvas.SetBottom(noteRect, 20 + (_height - 40) / 36 * (GetNoteTop(_note, _baseOct)) + (((_height - 40) / 36 * _chordDex) * Math.Abs(_option)));

            // 2. than, make the name above it
            noteName = new Label()
            {
                Content = NoteToString(_note),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 8
            };

            Canvas.SetLeft(noteName, (20 + (_width - 40) / 36 * _position) + ((noteSize_X * Math.Abs(_option))));
            Canvas.SetBottom(noteName, 20 + (_height - 40) / 36 * GetNoteTop(_note, _baseOct) + (nameMargin * Math.Abs(_option - 1)) + (((_height - 40) / 24 * _chordDex) * Math.Abs(_option)));
        }

        /// <summary>
        /// Sets new position for the note name and rect
        /// </summary>
        /// <param name="_position"></param>
        public void SetPosition(int _position)
        {
            noteRect.Margin = new System.Windows.Thickness(_position, noteRect.Margin.Top, 0, 0);

            noteName.Margin = new System.Windows.Thickness(_position, noteName.Margin.Top, 0, 0);
        }

        /// <summary>
        /// Sets the size of note rect and refesh
        /// </summary>
        /// <param name="_size"></param>
        public void SetSize(int _x, int _y)
        {
            noteSize_X = _x;
            noteSize_Y = _y;

            noteRect.Width = noteSize_X;
            noteRect.Height = noteSize_Y;
        }

        /// <summary>
        /// set note position depending on canvas size
        /// </summary>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        public void SetCanvasSize(int _width, int _height)
        {
            noteSize_X = _width / 32;
            noteSize_Y = _height / 36;

            noteRect.Width = noteSize_X;
            noteRect.Height = noteSize_Y;
        }

        /// <summary>
        /// calculate note top
        /// </summary>
        /// <param name="_note"></param>
        /// <param name="_baseOct"></param>
        /// <returns></returns>
        private int GetNoteTop(Note _note, int _baseOct)
        {
            if ((int)_note == -1) { return 0; }
            return ((int)_note / 12 - _baseOct + 1) * 12 + (int)_note % 12;
        }

        private string NoteToString(Note note)
        {
            if ((int)note % 12 == 0)
            {
                return "C" + ((int)note / 12);
            }
            else if ((int)note % 12 == 1)
            {
                return "C#" + ((int)note / 12);
            }
            else if ((int)note % 12 == 2)
            {
                return "D" + ((int)note / 12);
            }
            else if ((int)note % 12 == 3)
            {
                return "D#" + ((int)note / 12);
            }
            else if ((int)note % 12 == 4)
            {
                return "E" + ((int)note / 12);
            }
            else if ((int)note % 12 == 5)
            {
                return "F" + ((int)note / 12);
            }
            else if ((int)note % 12 == 6)
            {
                return "F#" + ((int)note / 12);
            }
            else if ((int)note % 12 == 7)
            {
                return "G" + ((int)note / 12);
            }
            else if ((int)note % 12 == 8)
            {
                return "G#" + ((int)note / 12);
            }
            else if ((int)note % 12 == 9)
            {
                return "A" + ((int)note / 12);
            }
            else if ((int)note % 12 == 10)
            {
                return "A#" + ((int)note / 12);
            }
            else if ((int)note % 12 == 11)
            {
                return "B" + ((int)note / 12);
            }
            else
            {
                return "NN";
            }
        }


    }
}
