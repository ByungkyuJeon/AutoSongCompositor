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
        int length;

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

        static Dictionary<FormType, int> lengthDic = new Dictionary<FormType, int>()
        {
            {FormType.Intro, 1},
            {FormType.Verse, 2},
            {FormType.Verse2, 2},
            {FormType.Hook, 2},
            {FormType.Bridge, 1},
            {FormType.Interlude, 1},
            {FormType.Outro, 1}
        };

        /// <summary>
        /// Form type
        /// </summary>
        public FormType Type { get => type; set => type = value; }

        /// <summary>
        /// Form time length
        /// </summary>
        public int Length { get => length; set => length = value; }

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

        public Form()
        {
            length = 1;
        }

        /// <summary>
        /// Constructor that directly setting form type and varition type
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_vari"></param>
        public Form(FormType _type, Variation _vari)
        {
            type = _type;
            variation = _vari;

            length = lengthDic[type];
        }

        /// <summary>
        /// Deep copy
        /// </summary>
        /// <param name="_origin"></param>
        public void Copy(Form _origin)
        {
            type = _origin.Type;
            variation = _origin.Variation;
            length = _origin.Length;

            rhythm.Copy(_origin.Rhythm);

            chord.Copy(_origin.Chord);

            mellody.Copy(_origin.Mellody);

        }
    }
}
