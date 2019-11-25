using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACompositor.src
{
    /// <summary>
    /// CompSettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CompSettingWindow : Window
    {
        /// <summary>
        /// Form listing window in composition
        /// </summary>
        FormListingWindow formListingWindow;

        /// <summary>
        /// Maden composition
        /// </summary>
        Composition new_composition;

        /// <summary>
        /// Set song setting
        /// </summary>
        internal Composition New_composition { get => new_composition; }

        /// <summary>
        /// Initiates default window set
        /// </summary>
        public CompSettingWindow()
        {
            InitializeComponent();

            new_composition = new Composition();
        }

        /// <summary>
        /// Click event callback : cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Click event callback : confirm button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Confrim_Click(object sender, RoutedEventArgs e)
        {
            // name text check
            if (Comp_Name.Text != "")
            {
                new_composition.Name = Comp_Name.Text;
            }
            else
            {
                MessageBox.Show("Please fill in the name text");
            }

            // jengre
            new_composition.Setting.Jengre = (Jengre)Comp_Jengre.SelectedIndex;

            // scale
            if (Comp_Scale.SelectedIndex == 0)
            {
                new_composition.Setting.Scale = Scale.NULL;
            }
            else
            {
                new_composition.Setting.Scale = (Scale)Comp_Scale.SelectedIndex - 1;
            }

            // scale note
            new_composition.Setting.ScaleNote = (Note)(Comp_ScaleNote.SelectedIndex - 1);

            // chord height
            new_composition.Setting.ChordHeight = Comp_ChordHeight.SelectedIndex + 2;

            // chord count
            new_composition.Setting.ChordCount = Comp_ChordCount.SelectedIndex + 2;

            // base octave
            new_composition.Setting.BaseOctave = Comp_BaseOctave.SelectedIndex + 1;

            // save state
            New_composition.IsChanged = true;

            // close
            Close();
        }

        /// <summary>
        /// String to Variation
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public Variation StringToVariation(string par)
        {
            if (par == "Random")
            {
                return Variation.NULL;
            }
            else if (par == "Origin")
            {
                return Variation.Origin;
            }
            else if (par == "Extend")
            {
                return Variation.Extend;
            }
            else if (par == "Shrink")
            {
                return Variation.Shrink;
            }
            else if (par == "Newition")
            {
                return Variation.Newition;
            }
            else if (par == "Octivation")
            {
                return Variation.Octaviation;
            }
            else
            {
                return Variation.Tailing;
            }
        }

        /// <summary>
        /// String to FormType
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private FormType StringToFormType(string str)
        {
            if (str == "Intro")
            {
                return FormType.Intro;
            }
            else if (str == "Outro")
            {
                return FormType.Outro;
            }
            else if (str == "Verse")
            {
                return FormType.Verse;
            }
            else if (str == "Verse2")
            {
                return FormType.Verse2;
            }
            else if (str == "Bridge")
            {
                return FormType.Bridge;
            }
            else if (str == "Hook")
            {
                return FormType.Hook;
            }
            else
            {
                return FormType.Interlude;
            }

        }

        /// <summary>
        /// Click event callback : set form button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comp_SetForm_Click(object sender, RoutedEventArgs e)
        {
            // show form setting window 
            formListingWindow = new FormListingWindow();
            formListingWindow.ShowDialog();

            // updating data
            foreach (string iter in formListingWindow.Forms)
            {
                new_composition.Setting.Forms.Add(StringToFormType(iter));
            }

            foreach(string iter in formListingWindow.Variations)
            {
                new_composition.Setting.Variations.Add(StringToVariation(iter));
            }
        }

    }
}
