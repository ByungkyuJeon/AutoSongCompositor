using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Form
    {
        /// <summary>
        /// Form type
        /// </summary>
        FormType type;

        /// <summary>
        /// Adapted variation to form
        /// </summary>
        Variation variation;

        /// <summary>
        /// Form time length
        /// </summary>
        int timeLength;

        /// <summary>
        /// Chord
        /// </summary>
        Chord chord;

        /// <summary>
        /// mellody
        /// </summary>
        Mellody mellody;

        /// <summary>
        /// rhythm
        /// </summary>
        Rhythm rhythm;

        /// <summary>
        /// Form type
        /// </summary>
        public FormType Type { get => type; set => type = value; }

        /// <summary>
        /// Form time length
        /// </summary>
        public int TimeLength { get => timeLength; set => timeLength = value; }

        /// <summary>
        /// Adapted variation to form
        /// </summary>
        public Variation Variation { get => variation; set => variation = value; }

        /// <summary>
        /// Rhythm
        /// </summary>
        internal Rhythm Rhythm { get => rhythm; set => rhythm = value; }

        /// <summary>
        /// Chord
        /// </summary>
        internal Chord Chord { get => chord; set => chord = value; }

        /// <summary>
        /// Mellody
        /// </summary>
        internal Mellody Mellody { get => mellody; set => mellody = value; }


    }
}
