using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Threading;

namespace ACompositor.src
{
    /// <summary>
    /// Music Player
    /// </summary>
    public class Player
    {
        Main dispatcher;

        List<WaveOut> audioStreams;

        Dictionary<Note, byte[]> audioDic;

        bool playState = false;

        List<Composition> compositions;

        /// <summary>
        /// Play timer
        /// </summary>
        System.Timers.Timer _playTimer;

        /// <summary>
        /// New Position
        /// </summary>
        private int position;

        public int Position { get => position; }

        public Player(Main _main)
        {
            dispatcher = _main;

            Initiate();
        }

        /// <summary>
        /// Player initiation
        /// </summary>
        private void Initiate()
        {
            audioStreams = new List<WaveOut>();

            audioDic = new Dictionary<Note, byte[]>();

            InitiateDictionary();

            _playTimer = new System.Timers.Timer(200);
            _playTimer.AutoReset = true;
            _playTimer.Elapsed += OnTimed;
        }

        public void Play(List<Composition> _compositions, int _position)
        {
            compositions = _compositions;

            position = _position;

            playState = true;

            WaveOut _waveBuffer;

            int _totalTime = GetTotalTime(_compositions);

            if (position > _totalTime)
            {
                Stop();

                return;
            }

            foreach (Note _note in GetNotes(_compositions, position))
            {
                _waveBuffer = new WaveOut();
                _waveBuffer.Init(GetReader(_note));

                audioStreams.Add(_waveBuffer);
            }

            foreach (WaveOut _wave in audioStreams)
            {
                if (_wave.PlaybackState != PlaybackState.Playing)
                {
                    _wave.Play();
                }
            }

            if (!_playTimer.Enabled)
            {
                _playTimer.Start();
            }

            position++;

        }

        void Play()
        {
            WaveOut _waveBuffer;

            int _totalTime = GetTotalTime(compositions);

            if (position > _totalTime)
            {
                Stop();

                return;
            }

            foreach (Note _note in GetNotes(compositions, position))
            {
                _waveBuffer = new WaveOut();
                _waveBuffer.Init(GetReader(_note));

                audioStreams.Add(_waveBuffer);
            }

            foreach (WaveOut _wave in audioStreams)
            {
                if (_wave.PlaybackState != PlaybackState.Playing)
                {
                    _wave.Play();
                }
            }

            position++;
        }

        public void Pause()
        {
            playState = false;

            audioStreams.Clear();

            _playTimer.Stop();
        }

        public void Stop()
        {
            playState = false;

            audioStreams.Clear();

            _playTimer.Stop();

            position = 0;

            dispatcher.Dispatcher.Invoke(new Action(delegate
            {
                dispatcher.DrawBar(Position);
            }));
        }

        public int GetTotalTime(List<Composition> _compositions)
        {
            List<int> _totalBuffer = new List<int>();

            int _timeBuffer = 0;

            foreach (Composition _comp in _compositions)
            {
                // mellody search
                foreach (Form _form in _comp.Forms)
                {
                    for (int _index = 0; _index < _form.Rhythm.NoteTime.Count; _index++)
                    {
                        _timeBuffer += _form.Rhythm.NoteTime[_index];
                    }
                }

                _totalBuffer.Add(_timeBuffer);
                _timeBuffer = 0;
            }

            foreach(int _time in _totalBuffer)
            {
                if(_timeBuffer < _time)
                {
                    _timeBuffer = _time;
                }
            }

            if(_timeBuffer == 0) { return 1; }
            return _timeBuffer;
        }

        private List<Note> GetNotes(List<Composition> _compositions, int _position)
        {
            List<Note> _result = new List<Note>();

            int _timeBuffer = 0;

            foreach (Composition _comp in _compositions)
            {
                // mellody search
                foreach(Form _form in _comp.Forms)
                {
                    for(int _index = 0; _index < _form.Rhythm.NoteTime.Count; _index++)
                    {
                        if(_timeBuffer == _position)
                        {
                            _result.Add(_form.Mellody.FullMellody[_index]);
                        }
                        else if(_timeBuffer > _position)
                        {
                            break;
                        }

                        _timeBuffer += _form.Rhythm.NoteTime[_index];
                    }
                }

                /*
                if(_position % 8 == 0)
                {
                    foreach(Note _iter in _comp.Forms[GetFormIndex(_comp.Forms, _position)].Chord.FullChord
                            [((_position % (_comp.Forms[GetFormIndex(_comp.Forms, _position)].Length * 32))) / 8])
                    {
                        _result.Add(_iter);
                    }
                }
                */
                
                // chord search
                if(_position % 2 == 0)
                {
                    int _formIndex = GetFormIndex(_comp.Forms, _position);

                    if (_comp.Setting.ChordHeight == 3)
                    {
                        if ((_position % 8 / 2) == 3)
                        {
                            _result.Add(_comp.Forms[_formIndex].Chord.FullChord[((_position % (_comp.Forms[_formIndex].Length * 32))) / 8][1]);
                        }
                        else
                        {
                            _result.Add(_comp.Forms[_formIndex].Chord.FullChord
                                [((_position % (_comp.Forms[_formIndex].Length * 32))) / 8][(_position % 8) / 2]);
                        }
                    }
                    else
                    {
                        _result.Add(_comp.Forms[_formIndex].Chord.FullChord
                            [((_position % (_comp.Forms[_formIndex].Length * 32))) / 8][(_position % 8) / 2]);
                    }
                }
                
                
                

                _timeBuffer = 0;
            }

            return _result;
        }

        /// <summary>
        /// Returns form index form param position
        /// </summary>
        /// <param name="_forms"></param>
        /// <param name="_position"></param>
        /// <returns></returns>
        private int GetFormIndex(List<Form> _forms, int _position)
        {
            int _timeBuffer = 0;
            int _countBuffer = 0;

            foreach(Form _form in _forms)
            {
                if(_timeBuffer <= _position && _position < _timeBuffer + _form.Length * 32)
                {
                    return _countBuffer;
                }

                _countBuffer++;
                _timeBuffer += _form.Length * 32;
            }

            return 0;
        }

        private Mp3FileReader GetReader(Note _note)
        {
            if(_note == Note.A6)
            {
                return new Mp3FileReader(new MemoryStream(audioDic[Note.A5]));
            }
            return new Mp3FileReader(new MemoryStream(audioDic[_note]));        
        }


        private void InitiateDictionary()
        {
            // C Note
            audioDic.Add(Note.C1, Properties.Resources.c1);
            audioDic.Add(Note.C2, Properties.Resources.c2);
            audioDic.Add(Note.C3, Properties.Resources.c3);
            audioDic.Add(Note.C4, Properties.Resources.c4);
            audioDic.Add(Note.C5, Properties.Resources.c5);
            audioDic.Add(Note.C6, Properties.Resources.c6);
            audioDic.Add(Note.C7, Properties.Resources.c7);

            // D Note
            audioDic.Add(Note.D1, Properties.Resources.d1);
            audioDic.Add(Note.D2, Properties.Resources.d2);
            audioDic.Add(Note.D3, Properties.Resources.d3);
            audioDic.Add(Note.D4, Properties.Resources.d4);
            audioDic.Add(Note.D5, Properties.Resources.d5);
            audioDic.Add(Note.D6, Properties.Resources.d6);
            audioDic.Add(Note.D7, Properties.Resources.d7);

            // E Note
            audioDic.Add(Note.E1, Properties.Resources.e1);
            audioDic.Add(Note.E2, Properties.Resources.e2);
            audioDic.Add(Note.E3, Properties.Resources.e3);
            audioDic.Add(Note.E4, Properties.Resources.e4);
            audioDic.Add(Note.E5, Properties.Resources.e5);
            audioDic.Add(Note.E6, Properties.Resources.e6);
            audioDic.Add(Note.E7, Properties.Resources.e7);

            // F Note
            audioDic.Add(Note.F1, Properties.Resources.f1);
            audioDic.Add(Note.F2, Properties.Resources.f2);
            audioDic.Add(Note.F3, Properties.Resources.f3);
            audioDic.Add(Note.F4, Properties.Resources.f4);
            audioDic.Add(Note.F5, Properties.Resources.f5);
            audioDic.Add(Note.F6, Properties.Resources.f6);
            audioDic.Add(Note.F7, Properties.Resources.f7);

            // G Note
            audioDic.Add(Note.G1, Properties.Resources.g1);
            audioDic.Add(Note.G2, Properties.Resources.g2);
            audioDic.Add(Note.G3, Properties.Resources.g3);
            audioDic.Add(Note.G4, Properties.Resources.g4);
            audioDic.Add(Note.G5, Properties.Resources.g5);
            audioDic.Add(Note.G6, Properties.Resources.g6);
            audioDic.Add(Note.G7, Properties.Resources.g7);

            // A Note
            audioDic.Add(Note.A1, Properties.Resources.a1);
            audioDic.Add(Note.A2, Properties.Resources.a2);
            audioDic.Add(Note.A3, Properties.Resources.a3);
            audioDic.Add(Note.A4, Properties.Resources.a4);
            audioDic.Add(Note.A5, Properties.Resources.a5);
            //audioDic.Add(Note.A6, Properties.Resources.a6);
            audioDic.Add(Note.A7, Properties.Resources.a7);

            // B Note
            audioDic.Add(Note.B1, Properties.Resources.b1);
            audioDic.Add(Note.B2, Properties.Resources.b2);
            audioDic.Add(Note.B3, Properties.Resources.b3);
            audioDic.Add(Note.B4, Properties.Resources.b4);
            audioDic.Add(Note.B5, Properties.Resources.b5);
            audioDic.Add(Note.B6, Properties.Resources.b6);
            audioDic.Add(Note.B7, Properties.Resources.b7);

            // Cu Note
            audioDic.Add(Note.Cu1, Properties.Resources.cu1);
            audioDic.Add(Note.Cu2, Properties.Resources.cu2);
            audioDic.Add(Note.Cu3, Properties.Resources.cu3);
            audioDic.Add(Note.Cu4, Properties.Resources.cu4);
            audioDic.Add(Note.Cu5, Properties.Resources.cu5);
            audioDic.Add(Note.Cu6, Properties.Resources.cu6);
            audioDic.Add(Note.Cu7, Properties.Resources.cu7);

            // Du Note
            audioDic.Add(Note.Du1, Properties.Resources.du1);
            audioDic.Add(Note.Du2, Properties.Resources.du2);
            audioDic.Add(Note.Du3, Properties.Resources.du3);
            audioDic.Add(Note.Du4, Properties.Resources.du4);
            audioDic.Add(Note.Du5, Properties.Resources.du5);
            audioDic.Add(Note.Du6, Properties.Resources.du6);
            audioDic.Add(Note.Du7, Properties.Resources.du7);

            // Fu Note
            audioDic.Add(Note.Fu1, Properties.Resources.fu1);
            audioDic.Add(Note.Fu2, Properties.Resources.fu2);
            audioDic.Add(Note.Fu3, Properties.Resources.fu3);
            audioDic.Add(Note.Fu4, Properties.Resources.fu4);
            audioDic.Add(Note.Fu5, Properties.Resources.fu5);
            audioDic.Add(Note.Fu6, Properties.Resources.fu6);
            audioDic.Add(Note.Fu7, Properties.Resources.fu7);

            // Gu Note
            audioDic.Add(Note.Gu1, Properties.Resources.gu1);
            audioDic.Add(Note.Gu2, Properties.Resources.gu2);
            audioDic.Add(Note.Gu3, Properties.Resources.gu3);
            audioDic.Add(Note.Gu4, Properties.Resources.gu4);
            audioDic.Add(Note.Gu5, Properties.Resources.gu5);
            audioDic.Add(Note.Gu6, Properties.Resources.gu6);
            audioDic.Add(Note.Gu7, Properties.Resources.gu7);

            // Au Note
            audioDic.Add(Note.Au1, Properties.Resources.au1);
            audioDic.Add(Note.Au2, Properties.Resources.au2);
            audioDic.Add(Note.Au3, Properties.Resources.au3);
            audioDic.Add(Note.Au4, Properties.Resources.au4);
            audioDic.Add(Note.Au5, Properties.Resources.au5);
            audioDic.Add(Note.Au6, Properties.Resources.au6);
            audioDic.Add(Note.Au7, Properties.Resources.au7);
        }

        void TrimWaves()
        {
            bool _passBuffer = true;

            int _indexBuffer = -1;

            while (_passBuffer)
            {
                _passBuffer = false;

                for (int _wave = 0; _wave < audioStreams.Count; _wave++)
                {
                    if (audioStreams[_wave].PlaybackState != PlaybackState.Playing)
                    {
                        _indexBuffer = _wave;

                        _passBuffer = true;

                        break;
                    }
                }

                if(_passBuffer)
                {
                    dispatcher.Dispatcher.Invoke(new Action(delegate
                    {
                        audioStreams[_indexBuffer].Dispose();
                    }));
                    audioStreams.RemoveAt(_indexBuffer);
                }
            }

            audioStreams.TrimExcess();
        }

        void OnTimed(object source, System.Timers.ElapsedEventArgs e)
        {
            TrimWaves();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (playState)
            {
                Play();

                dispatcher.Dispatcher.Invoke(new Action(delegate
                {
                    dispatcher.DrawBar(Position);
                }));
            }
        }

    }
}
