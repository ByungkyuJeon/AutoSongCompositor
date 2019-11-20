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

            timeDic = new Dictionary<int, int>();

            MakeTimeDic();
        }

        /// <summary>
        /// Makes rhythm
        /// </summary>
        public void MakeRhythm(int _loopCount)
        {
            // loop buffer
            int _loopBuffer;

            // key buffer
            int _keyBuffer;

            // make pattern
            pattern.MakePattern(_loopCount);

            _loopBuffer = pattern.NodeLoop;

            // makes rhythm
            while(_loopBuffer > 0)
            {
                _keyBuffer = random.Next() % 3;

                if(!(_loopBuffer < _keyBuffer))
                {
                    noteTime.Add(timeDic[_keyBuffer]);

                    _loopBuffer -= _keyBuffer;
                }
            }


        }

        /// <summary>
        /// Makes note time dictionary
        /// </summary>
        void MakeTimeDic()
        {
            timeDic.Add(0, 1);
            timeDic.Add(1, 2);
            timeDic.Add(2, 4);
        }
    }
}
