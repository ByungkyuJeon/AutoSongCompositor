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
        /// Composition setting window
        /// </summary>
        CompSettingWindow compSettingWindow;

        /// <summary>
        /// Music player;
        /// </summary>
        Player player;

        /// <summary>
        /// Compositor engine
        /// </summary>
        Compositor compositor;

        /// <summary>
        /// Compostiion GUIs in the view
        /// </summary>
        List<CompUI> compUIList;

        public Main()
        {
            InitializeComponent();

            Initiate();
        }

        /// <summary>
        /// Initial loading procesure to run software
        /// </summary>
        private void Initiate()
        {
            player = new Player();

            compositor = new Compositor();

            compUIList = new List<CompUI>();
        }

        /// <summary>
        /// Draw loaded compositions on view
        /// </summary>
        private void DrawView()
        {
            foreach(CompUI _iter in compUIList)
            {
                grid_View.Children.Add(_iter.UI);
            }
        }

        /// <summary>
        /// Clear drawings on view
        /// </summary>
        private void ClearView()
        {
            int _itemBuffer = -1;

            bool _pass = true;

            while (_pass)
            {
                _pass = true;

                for (int _iter = 0; _iter < grid_View.Children.Count; _iter++)
                {
                    if (grid_View.Children[_iter].GetType() == typeof(Grid))
                    {
                        if (((Grid)grid_View.Children[_iter]).Name == "")
                        {
                            _itemBuffer = _iter;

                            break;
                        }
                    }
                }

                if(_itemBuffer == -1)
                {
                    _pass = false;
                }
                else
                {
                    grid_View.Children.RemoveAt(_itemBuffer);
                }

                _itemBuffer = -1;

            }
        }

        /// <summary>
        /// Click event callback : file menu > save file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_SaveFile_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Click event callback : file menu > open file    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OpenFile_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Click event callback : file menu > environment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Environment_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Click event callback : project menu > new composition 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_NewComposition_Click(object sender, RoutedEventArgs e)
        {
            compSettingWindow = new CompSettingWindow();
            compSettingWindow.ShowDialog();

            compUIList.Add(new CompUI(compSettingWindow.New_composition));

            compSettingWindow = null;

            ClearView();

            DrawView();
        }
    }
}

