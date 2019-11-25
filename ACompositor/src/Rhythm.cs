using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Rhythm
    {
        /// <summary>
        /// random
        /// </summary>
        Random random;

        Dictionary<int, int> timeDic;
        Dictionary<int, int> loopDic;
        

        /// <summary>
        /// Time list for each note
        /// </summary>
        List<int> noteTime;

        /// <summary>
        /// Form looping pattern in song
        /// </summary>
        Pattern pattern;

        /// <summary>
        /// Time list for each note
        /// </summary>
        public List<int> NoteTime { get => noteTime; set => noteTime = value; }

        /// <summary>
        /// Form looping pattern in song
        /// </summary>
        internal Pattern Pattern { get => pattern; set => pattern = value; }

        /// <summary>
        /// Fundamental constructor
        /// </summary>
        public Rhythm()
        {
            noteTime = new List<int>();

            pattern = new Pattern();

            random = new Random();

            MakeTimeDic();
        }

        /// <summary>
        /// Makes rhythm
        /// </summary>
        public void MakeRhythm(int _loopCount, int _chordCount, int _formLength)
        {
            // loop buffer
            int _loopBuffer;

            // repeat buffer
            int _repeatBuffer;

            // key buffer
            int _keyBuffer;

            // note time buffer
            List<int> _noteTimeBuffer = new List<int>();

            // make loop dictionary
            MakeLoopDic(_chordCount);

            // make pattern
            pattern.MakePattern(_loopCount, loopDic);

            _loopBuffer = pattern.NodeLoop;

            _repeatBuffer = _formLength * _chordCount * 8 / _loopBuffer;

            // makes one part rhythm
            while (_loopBuffer > 0)
            {
                _keyBuffer = random.Next() % 3;

                if(!(_loopBuffer < _keyBuffer))
                {
                    _noteTimeBuffer.Add(timeDic[_keyBuffer]);

                    _loopBuffer -= _keyBuffer;
                }
            }

            // makes full part rhythm
            while(_repeatBuffer > 0)
            {
                foreach(int _iter in _noteTimeBuffer)
                {
                    noteTime.Add(_iter);
                }

                _repeatBuffer--;
            }


        }

        /// <summary>
        /// Makes note time dictionary
        /// </summary>
        void MakeTimeDic()
        {
            timeDic = new Dictionary<int, int>();

            timeDic.Add(0, 1);
            timeDic.Add(1, 2);
            timeDic.Add(2, 4);
        }

        /// <summary>
        /// Makes note loop dictionary
        /// </summary>
        void MakeLoopDic(int _chordCount)
        {
            int _keyBuffer = 1;

            loopDic = new Dictionary<int, int>();

            loopDic.Add(0, 1);

            for (int _num = 2; _num < _chordCount; _num++)
            {
                if (_chordCount % _num == 0)
                {
                    loopDic.Add(_keyBuffer, _num);

                    _keyBuffer++;
                }
            }

            loopDic.Add(_keyBuffer, _chordCount);
        }

        /// <summary>
        /// Deep copy
        /// </summary>
        /// <param name="_origin"></param>
        public void Copy(Rhythm _origin)
        {
            // note time list copy
            foreach(int _iter in _origin.NoteTime)
            {
                noteTime.Add(_iter);
            }

            pattern.Copy(_origin.Pattern);
        }
    }
}
