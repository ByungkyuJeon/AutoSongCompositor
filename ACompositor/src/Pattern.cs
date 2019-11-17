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
        /// Form unit loop length
        /// </summary>
        int upperLoop;

        /// <summary>
        /// Node unit loop length
        /// </summary>
        int lowerLoop;

        /// <summary>
        /// Form unit loop length
        /// </summary>
        public int UpperLoop { get => upperLoop; set => upperLoop = value; }

        /// <summary>
        /// Node unit loop length
        /// </summary>
        public int LowerLoop { get => lowerLoop; set => lowerLoop = value; }
    }
}
