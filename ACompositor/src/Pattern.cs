using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Pattern
    {
        /// <summary>
        /// Node unit loop length
        /// </summary>
        int nodeLoop;

        /// <summary>
        /// random
        /// </summary>
        Random random;

        /// <summary>
        /// Node unit loop length
        /// </summary>
        public int NodeLoop { get => nodeLoop; set => nodeLoop = value; }

        public Pattern()
        {
            random = new Random();
        }

        /// <summary>
        /// Makes pattern
        /// </summary>
        /// <returns></returns>
        public void MakePattern(int _loopCount)
        {
            if(_loopCount == 0)
            {
                nodeLoop = ((random.Next() % 4) + 1) * 8;
            }

            nodeLoop = _loopCount;
        }
    }
}
