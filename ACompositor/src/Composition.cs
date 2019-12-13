using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACompositor.src
{
    public class Composition
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
        /// Song Setting
        /// </summary>
        SongSetting setting;

        /// <summary>
        /// Song Form List
        /// </summary>
        List<Form> forms;

        /// <summary>
        /// Core form
        /// </summary>
        Form coreForm;

        /// <summary>
        /// Core scale notes
        /// </summary>
        ScaleNote coreScale;

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
        /// Song Setting
        /// </summary>
        internal SongSetting Setting { get => setting; set => setting = value; }

        /// <summary>
        /// Song form list
        /// </summary>
        internal List<Form> Forms { get => forms; set => forms = value; }

        /// <summary>
        /// Core form
        /// </summary>
        internal Form CoreForm { get => coreForm; set => coreForm = value; }

        /// <summary>
        /// Core scale notes
        /// </summary>
        internal ScaleNote CoreScale { get => coreScale; set => coreScale = value; }


        /// <summary>
        /// Empty Initiation
        /// </summary>
        public Composition()
        {
            setting = new SongSetting();

            forms = new List<Form>();

            coreScale = new ScaleNote();
        }

        /// <summary>
        /// Setting Initiation
        /// </summary>
        /// <param name="setting"></param>
        public Composition(SongSetting _setting)
        {
            setting = _setting;

            coreScale = new ScaleNote();

            CoreForm = new Form();

            forms = new List<Form>();
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
