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
using System.Timers;

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

        /// <summary>
        /// Selected compostions
        /// </summary>
        List<int> selectedComp;

        /// <summary>
        /// Current time position
        /// </summary>
        int currentPosition = 0;

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

            selectedComp = new List<int>();
        }

        /// <summary>
        /// Draw specific mellodies, rhythm, chord
        /// </summary>
        private void DrawComp()
        {
            foreach(CompUI _iter in compUIList)
            {
                _iter.SetPosition(currentPosition);
            }
        }

        /// <summary>
        /// Draw loaded compositions on view
        /// </summary>
        private void DrawView()
        {
            ClearView();

            DrawComp();

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
        /// Removes composition on view
        /// </summary>
        /// <param name="_index"></param>
        private void RemoveComp()
        {
            foreach(int _iter in selectedComp)
            {
                compUIList.RemoveAt(_iter);
            }

            selectedComp.Clear();

            DrawView();
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

            compUIList.Add(new CompUI(compSettingWindow.New_composition, compUIList.Count));

            compUIList[compUIList.Count - 1].Button_Composite.Click += OnCompositeClick;
            compUIList[compUIList.Count - 1].Button_Pause.Click += OnPauseClick;
            compUIList[compUIList.Count - 1].Button_Wide.Click += OnWideClick;
            compUIList[compUIList.Count - 1].Button_Play.Click += OnPlayClick;
            compUIList[compUIList.Count - 1].Button_Setting.Click += OnSettingClick;

            compSettingWindow = null;

            ClearView();

            DrawView();

        }

        /// <summary>
        /// Click event callback : composition UI composite button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnCompositeClick(object _sender, RoutedEventArgs _e)
        {
            // composite
            compUIList[GetEventSource(_sender)].Composition = compositor.GenerateFull(compUIList[GetEventSource(_sender)].Composition);

            // draw
            DrawView();
        }

        /// <summary>
        /// Click event callback : composition UI play button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnPlayClick(object _sender, RoutedEventArgs _e)
        {
            List<Composition> _playBuffer = new List<Composition>();
            _playBuffer.Add(compUIList[GetEventSource(_sender)].Composition);

            player.Play(_playBuffer, currentPosition);
        }

        /// <summary>
        /// Click event callback : composition UI stop button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnStopClick(object _sender, RoutedEventArgs _e)
        {
            player.Stop();
        }

        /// <summary>
        /// Click event callback : composition UI pause button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnPauseClick(object _sender, RoutedEventArgs _e)
        {

        }

        /// <summary>
        /// Click event callback : composition UI wide button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnWideClick(object _sender, RoutedEventArgs _e)
        {
            compUIList[GetEventSource(_sender)].SetWide((int)grid_View.ActualHeight - 20);
        }

        /// <summary>
        /// Click event callback : composition UI setting button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnSettingClick(object _sender, RoutedEventArgs _e)
        {
            compSettingWindow = new CompSettingWindow();
            compSettingWindow.SetComposition(compUIList[GetEventSource(_sender)].Composition);

            compSettingWindow.ShowDialog();

            compUIList[GetEventSource(_sender)].Composition = compSettingWindow.New_composition;

            DrawView();
        }

        /// <summary>
        /// Click event callback : composition UI composition click
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnCompClick(object _sender, RoutedEventArgs _e)
        {

            // checks if already in the list
            foreach(int _iter in selectedComp)
            {
                if(_iter == GetEventSource(_sender))
                {
                    return;
                }
            }

            // add to list
            selectedComp.Add(GetEventSource(_sender));
        }

        /// <summary>
        /// Returns index of event source
        /// </summary>
        /// <param name="_sender"></param>
        /// <returns></returns>
        private int GetEventSource(object _sender)
        {
            foreach(CompUI _iter in compUIList)
            {
                if(_iter.Button_Setting.Equals(_sender) || _iter.Button_Composite.Equals(_sender) || _iter.Button_Pause.Equals(_sender) 
                    || _iter.Button_Play.Equals(_sender) || _iter.Button_Wide.Equals(_sender))
                {
                    return compUIList.IndexOf(_iter);
                }
            }

            return -1;
        }

        /// <summary>
        /// Key down event call back : whole window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object _sender, KeyEventArgs _e)
        {
            switch(_e.Key)
            {
                case (Key.Delete):

                    // removement
                    RemoveComp();

                    break;
            }
        }
    }
}

