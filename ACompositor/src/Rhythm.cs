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
        /// Time list for each note
        /// </summary>
        List<int> noteTime;

        /// <summary>
        /// Time list for each part
        /// </summary>
        List<int> partTime;

        /// <summary>
        /// Form looping pattern in song
        /// </summary>
        Pattern pattern;

        /// <summary>
        /// Time list for each note
        /// </summary>
        public List<int> NoteTime { get => noteTime; set => noteTime = value; }

        /// <summary>
        /// Time list for each part
        /// </summary>
        public List<int> PartTime { get => partTime; set => partTime = value; }

        /// <summary>
        /// Form looping pattern in song
        /// </summary>
        internal Pattern Pattern { get => pattern; set => pattern = value; }
    }
}
