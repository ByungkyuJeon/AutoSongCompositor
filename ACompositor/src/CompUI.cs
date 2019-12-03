using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ACompositor.src
{
    /// <summary>
    /// Composition UI set
    /// </summary>
    class CompUI
    {
        /// <summary>
        /// Composition
        /// </summary>
        Composition composition;

        /// <summary>
        /// Composition UI mother grid
        /// </summary>
        Grid grid_Mother;

        // UIs
        Grid grid_Header;
        TextBox textBox_CompositionName;
        Label label_Jengre;
        Label label_Scale;
        Button button_Setting;
        Button button_Composite;
        Button button_Play;
        Button button_Pause;
        Button button_Wide;

        Grid grid_FormLine;
        Label label_FormName;

        Grid grid_NodeLine;

        Grid grid_NoteView;
        Canvas canvas_Mellody;
        Canvas canvas_Chord;

        Grid grid_NoteLine;
        Label label_Line1;
        Label label_Line2;
        Label label_Line3;
        Label label_Line4;

        /// <summary>
        /// True if UIs are allocated
        /// </summary>
        bool isInitiated = false;

        /// <summary>
        /// True if UI is in wide mode
        /// </summary>
        bool isWide = false;

        /// <summary>
        /// Compostion UI index number for position
        /// </summary>
        int indexUI = 0;

        /// <summary>
        /// Current note pos
        /// </summary>
        int currentPos = 0;

        /// <summary>
        /// UI for mellodies
        /// </summary>
        List<NoteUI> mellody_Notes;

        /// <summary>
        /// UI for chords
        /// </summary>
        List<NoteUI> chord_Notes;

        /// <summary>
        /// Composition UI
        /// </summary>
        public Grid UI { get => grid_Mother; set => grid_Mother = value; }

        /// <summary>
        /// Composition
        /// </summary>
        internal Composition Composition { get => composition; set => composition = value; }

        /// <summary>
        /// Compostion UI index number for position
        /// </summary>
        public int IndexUI { get => indexUI; set => indexUI = value; }

        /// <summary>
        /// Composition setting button
        /// </summary>
        public Button Button_Setting { get => button_Setting; set => button_Setting = value; }

        /// <summary>
        /// Composition composite button
        /// </summary>
        public Button Button_Composite { get => button_Composite; set => button_Composite = value; }

        /// <summary>
        /// Composition play button
        /// </summary>
        public Button Button_Play { get => button_Play; set => button_Play = value; }

        /// <summary>
        /// Composition pause button
        /// </summary>
        public Button Button_Pause { get => button_Pause; set => button_Pause = value; }

        /// <summary>
        /// Composition stop button
        /// </summary>
        public Button Button_Wide { get => button_Wide; set => button_Wide = value; }

        /// <summary>
        /// Composition name box
        /// </summary>
        public TextBox TextBox_CompositionName { get => textBox_CompositionName; set => textBox_CompositionName = value; }

        /// <summary>
        /// Constructor with drawing
        /// </summary>
        /// <param name="_composition"></param>
        public CompUI(Composition _composition, int _index)
        {
            composition = _composition;

            indexUI = _index;

            Draw(composition);
        }

        void Draw(Composition _composition)
        {
            // check if initiated
            if (isInitiated)
            {

            }
            // instaciate UIs
            else
            {
                grid_Mother = new Grid()
                {
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 150 + (150 * IndexUI),
                    Background = new SolidColorBrush(Color.FromRgb(40, 40, 40))
                };

                grid_Header = new Grid()
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    Width = 144,
                    Background = new SolidColorBrush(Color.FromRgb(50, 50, 50))
                };
                TextBox_CompositionName = new TextBox()
                {
                    Margin = new System.Windows.Thickness(52, 20, 0, 0),
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    Background = new SolidColorBrush(Color.FromRgb(50, 50, 50)),
                    Text = composition.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 10,
                    Height = 17,
                    Width = 82,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };
                label_Jengre = new Label()
                {
                    Content = composition.Setting.Jengre,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 10,
                    Height = 24,
                    Width = 51,
                    Margin = new System.Windows.Thickness(27, 42, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left

                };
                label_Scale = new Label()
                {
                    Content = composition.Setting.ScaleNote.ToString() + " " + composition.Setting.Scale.ToString(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 10,
                    Height = 24,
                    Width = 51,
                    Margin = new System.Windows.Thickness(27, 66, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left

                };

                if(composition.Setting.ScaleNote.ToString() == "NULL")
                {
                    label_Scale.Content = "Random Scale";
                }

                Button_Setting = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 17,
                    Width = 17,
                    Margin = new System.Windows.Thickness(30, 20, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };
                Button_Composite = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 85, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };
                Button_Play = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 60, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };
                Button_Wide = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 10, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };
                Button_Pause = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 35, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };

                grid_FormLine = new Grid()
                {
                    Height = 20,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(174, 0, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(50, 50, 50))
                };
                label_FormName = new Label()
                {
                    Content = "Form",
                    Padding = new System.Windows.Thickness(0, 0, 0, 0),
                    Margin = new System.Windows.Thickness(281, 0, 280, 0),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 9,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                grid_NodeLine = new Grid()
                {
                    Height = 20,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(174, 20, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(40, 40, 40))
                };

                grid_NoteView = new Grid()
                {
                    Margin = new System.Windows.Thickness(174, 40, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(30, 30, 30))
                };

                // canvas for mellody
                canvas_Mellody = new Canvas()
                {
                    Margin = new System.Windows.Thickness(0, 0, 0, grid_NoteView.ActualHeight / 2 - 30)
                };

                // canvas for chord
                canvas_Chord = new Canvas()
                {
                    Margin = new System.Windows.Thickness(0, grid_NoteView.ActualHeight / 2 - 30, 0, 0)
                };

                grid_NoteLine = new Grid()
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    Width = 30,
                    Background = new SolidColorBrush(Color.FromRgb(35, 35, 35)),
                    Margin = new System.Windows.Thickness(144, 0, 0, 0)
                };
                label_Line1 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 39, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };
                label_Line2 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 61, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };
                label_Line3 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 85, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };
                label_Line4 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 109, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };

                grid_Header.Children.Add(TextBox_CompositionName);
                grid_Header.Children.Add(label_Jengre);
                grid_Header.Children.Add(label_Scale);
                grid_Header.Children.Add(Button_Setting);
                grid_Header.Children.Add(Button_Composite);
                grid_Header.Children.Add(Button_Play);
                grid_Header.Children.Add(Button_Wide);
                grid_Header.Children.Add(Button_Pause);

                grid_FormLine.Children.Add(label_FormName);

                grid_NoteLine.Children.Add(label_Line1);
                grid_NoteLine.Children.Add(label_Line2);
                grid_NoteLine.Children.Add(label_Line3);
                grid_NoteLine.Children.Add(label_Line4);

                grid_NoteView.Children.Add(canvas_Mellody);
                grid_NoteView.Children.Add(canvas_Chord);

                grid_Mother.Children.Add(grid_NoteView);
                grid_Mother.Children.Add(grid_Header);
                grid_Mother.Children.Add(grid_FormLine);
                grid_Mother.Children.Add(grid_NodeLine);
                grid_Mother.Children.Add(grid_NoteLine);

                // make the boolean true
                isInitiated = true;
            }
        }

        /// <summary>
        /// Sets name of composition
        /// </summary>
        /// <param name="_str"></param>
        public void SetCompName(string _str)
        {
            composition.Name = _str;
        }

        /// <summary>
        /// Set new position for view
        /// </summary>
        /// <param name="_position"></param>
        public void SetPosition(int _position)
        {
            currentPos = _position;

            RefreshNotes();
        }

        public void SetWide(int _height)
        {
            if(isWide)
            {
                grid_Mother.Height = 150;

                isWide = false;
            }
            else
            {
                grid_Mother.Height = _height;

                isWide = true;
            }

            RefreshNotes();
        }

        /// <summary>
        /// Re-draw notes
        /// </summary>
        void RefreshNotes()
        {

            canvas_Mellody.Margin = new System.Windows.Thickness(0, 0, 0, grid_NoteView.ActualHeight / 2);
            canvas_Chord.Margin = new System.Windows.Thickness(0, grid_NoteView.ActualHeight / 2, 0, 0);


            ClearMellody();

            ClearChord();

            DrawNotes();
        }

        /// <summary>
        /// Draw notes on view depending on set position range
        /// </summary>
        void DrawNotes()
        {
            mellody_Notes = new List<NoteUI>();
            chord_Notes = new List<NoteUI>();

            int _timeBuffer = 0;
            int _chordBuffer = currentPos;
            bool _passBuffer = false;

            // check content
            if(composition.Forms.Count > 0)
            {
                foreach(Form _form in composition.Forms)
                {
                    foreach(Note _note in _form.Mellody.FullMellody)
                    {
                        // check for mellodies in range
                        if(_timeBuffer >= currentPos && _timeBuffer <= currentPos + 32)
                        {
                            // add mellody notes in range
                            mellody_Notes.Add(new NoteUI(_note, _timeBuffer - currentPos, (int)canvas_Mellody.ActualWidth, (int)canvas_Mellody.ActualHeight, 
                                composition.Setting.BaseOctave, 0, 0));
                        }
                        else if(_timeBuffer > currentPos + 32)
                        {
                            _passBuffer = true;

                            break;
                        }

                        // count time
                        _timeBuffer += _form.Rhythm.NoteTime[_form.Mellody.FullMellody.IndexOf(_note)];
                    }

                    if(_passBuffer)
                    {
                        while (_chordBuffer <= currentPos + 32)
                        {
                            if (_chordBuffer % 8 == 0)
                            {
                                foreach (Note _chordNote in _form.Chord.FullChord[(_chordBuffer % 32) / 8])
                                {
                                    // add chord notes in range
                                    chord_Notes.Add(new NoteUI(_chordNote, _chordBuffer - currentPos, (int)canvas_Chord.ActualWidth, (int)canvas_Chord.ActualHeight,
                                        composition.Setting.BaseOctave, 1, _form.Chord.FullChord[(_timeBuffer % 32) / 8].IndexOf(_chordNote)));
                                }
                            }

                            _chordBuffer++;
                        }

                        break;
                    }
                }
            }

            // adding notes to GUI canvas
            // mellody
            foreach(NoteUI _iter in mellody_Notes)
            {
                canvas_Mellody.Children.Add(_iter.NoteRect);
                canvas_Mellody.Children.Add(_iter.NoteName);
            }

            // chord
            foreach(NoteUI _iter in chord_Notes)
            {
                canvas_Chord.Children.Add(_iter.NoteRect);
                canvas_Chord.Children.Add(_iter.NoteName);
            }
        }

        /// <summary>
        /// Clears every notes in mellody canvas
        /// </summary>
        void ClearMellody()
        {
            int _itemBuffer = -1;

            bool _pass = true;

            while (_pass)
            {
                _pass = true;

                for (int _iter = 0; _iter < canvas_Mellody.Children.Count; _iter++)
                {
                    if (canvas_Mellody.Children[_iter].GetType() == typeof(Label))
                    {
                        _itemBuffer = _iter;

                        break;
                    }
                }

                if (_itemBuffer == -1)
                {
                    _pass = false;
                }
                else
                {
                    canvas_Mellody.Children.RemoveAt(_itemBuffer);
                }

                _itemBuffer = -1;

            }
        }

        /// <summary>
        /// Clears every notes in chord canvas
        /// </summary>
        void ClearChord()
        {
            int _itemBuffer = -1;

            bool _pass = true;

            while (_pass)
            {
                _pass = true;

                for (int _iter = 0; _iter < canvas_Chord.Children.Count; _iter++)
                {
                    if (canvas_Chord.Children[_iter].GetType() == typeof(Label))
                    {
                        _itemBuffer = _iter;

                        break;
                    }
                }

                if (_itemBuffer == -1)
                {
                    _pass = false;
                }
                else
                {
                    canvas_Chord.Children.RemoveAt(_itemBuffer);
                }

                _itemBuffer = -1;

            }
        }
    }
}
