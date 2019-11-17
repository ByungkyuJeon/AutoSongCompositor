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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ACompositor.src;

namespace ACompositor
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Window
    {
        /// <summary>
        /// Localy loaded compositions in the view
        /// </summary>
        List<Composition> compositions = new List<Composition>();

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initial loading procesure to run software
        /// </summary>
        private void Initiate()
        {

        }

        /// <summary>
        /// Draw loaded compositions on view
        /// </summary>
        private void DrawNotes()
        {

        }

        /// <summary>
        /// Clear drawings on view
        /// </summary>
        private void ClearNotes()
        {

        }




    }
}

