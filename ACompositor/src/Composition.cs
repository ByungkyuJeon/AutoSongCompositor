using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    class Composition
    {
        /// <summary>
        /// Name of composition
        /// </summary>
        string name;

        /// <summary>
        /// File path of composition
        /// </summary>
        string directory;

        /// <summary>
        /// Composition save state
        /// </summary>
        bool isChanged;

        /// <summary>
        /// Spotting Switch
        /// </summary>
        bool isSpotting;

        /// <summary>
        /// TwoHanding Switch
        /// </summary>
        bool isTwoHanding;

        /// <summary>
        /// Song Setting
        /// </summary>
        SongSetting setting;

        List<Form> forms;

        /// <summary>
        /// Name of composition
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// File path of composition
        /// </summary>
        public string Directory { get => directory; set => directory = value; }

        /// <summary>
        /// Composition save state
        /// </summary>
        public bool IsChanged { get => isChanged; set => isChanged = value; }

        /// <summary>
        /// Spotting Switch
        /// </summary>
        public bool IsSpotting { get => isSpotting; set => isSpotting = value; }

        /// <summary>
        /// TwoHanding Switch
        /// </summary>
        public bool IsTwoHanding { get => isTwoHanding; set => isTwoHanding = value; }

        /// <summary>
        /// Song Setting
        /// </summary>
        internal SongSetting Setting { get => setting; set => setting = value; }

        /// <summary>
        /// Empty Initiation
        /// </summary>
        public Composition()
        {

        }

        /// <summary>
        /// Setting Initiation
        /// </summary>
        /// <param name="setting"></param>
        public Composition(SongSetting setting)
        {

        }

        /// <summary>
        /// Composition Copy
        /// </summary>
        /// <param name="composition"></param>
        public Composition(Composition composition)
        {

        }
    }
}
