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
using System.Threading;
using System.Drawing;

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

        /// <summary>
        /// Current view position
        /// </summary>
        double currentView = 0;

        /// <summary>
        /// Mouse click click state
        /// </summary>
        bool clickOn = false;

        /// <summary>
        /// True if multi selection is on
        /// </summary>
        bool isMulti = false;

        /// <summary>
        /// True if select callback called
        /// </summary>
        bool isSelled = false;

        /// <summary>
        /// Track Bar drag state
        /// </summary>
        int trackBarOn = 0;

        /// <summary>
        /// Drag point buffer
        /// </summary>
        System.Windows.Point trackPoint;
        

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
            player = new Player(this);

            compositor = new Compositor();

            compUIList = new List<CompUI>();

            selectedComp = new List<int>();

            

        }

        /// <summary>
        /// Draw specific mellodies, rhythm, chord
        /// </summary>
        private void DrawComp()
        {
            foreach (CompUI _iter in compUIList)
            {
                _iter.UI.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));

                _iter.UI.UpdateLayout();

                _iter.SetPosition((int)currentView);
            }
        }

        private List<Composition> GetCompList()
        {
            List<Composition> _result = new List<Composition>();

            foreach(CompUI _iter in compUIList)
            {
                _result.Add(_iter.Composition);
            }

            return _result;
        }

        /// <summary>
        /// Draw loaded compositions on view
        /// </summary>
        private void DrawView()
        { 
            ClearView();

            DrawComp();

            DrawScroll();

            foreach (CompUI _iter in compUIList)
            {
                grid_View.Children.Add(_iter.UI);
            }

            RefreshCompPos();
        }

        private void DrawScroll()
        {
            if (compUIList.Count > 0)
            {
                Rect_timeScroll.Width = grid_backpanel2.ActualWidth / 11;
                Rect_timeScroll.Margin = new Thickness(currentView * (grid_backpanel2.ActualWidth / (player.GetTotalTime(GetCompList()))),
                    Rect_timeScroll.Margin.Top, Rect_timeScroll.Margin.Right, Rect_timeScroll.Margin.Bottom);
            }
        }

        public void DrawBar(int _position)
        {
            currentPosition = _position;

            grid_PlayBar.Margin = new Thickness(194 + currentPosition * ((grid_View.ActualWidth - 214) / player.GetTotalTime(GetCompList())),
                grid_PlayBar.Margin.Top, grid_PlayBar.Margin.Right, grid_PlayBar.Margin.Bottom);

            if(currentPosition - currentView > 16)
            {
                currentView = currentPosition - 16;

                DrawView();
            }
            else if (currentView - currentPosition > 16)
            {
                currentView = currentPosition + 16;

                DrawView();
            }
            else if(currentPosition == 0)
            {
                currentView = 0;

                DrawView();
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
        /// Return position from index param
        /// </summary>
        /// <param name="_index"></param>
        /// <returns></returns>
        private int GetIndexValue(int _index)
        {
            int _result = 0;

            int _countBuffer = _index;

            foreach(CompUI _iter in compUIList)
            {
                if (_countBuffer == 0)
                {
                    break;
                }

                _result += (int)_iter.UI.ActualHeight;

                _countBuffer--;
            }

            return _result;
        }

        private void RefreshCompPos()
        {
            int _indexBuffer = 0;

            foreach(CompUI _iter in compUIList)
            {
                _iter.RefreshMargin(GetIndexValue(_indexBuffer));

                _indexBuffer++;
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

        private void RefreshWidth()
        {
            foreach(CompUI _iter in compUIList)
            {
                _iter.RefreshWidth((int)grid_View.ActualHeight - 20);
            }
        }

        /// <summary>
        /// Click event callback : file menu > save file
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Button_SaveFile_Click(object _sender, RoutedEventArgs _e)
        {

        }

        /// <summary>
        /// Click event callback : file menu > open file    
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Button_OpenFile_Click(object _sender, RoutedEventArgs _e)
        {

        }

        /// <summary>
        /// Click event callback : file menu > environment
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Button_Environment_Click(object _sender, RoutedEventArgs _e)
        {

        }

        /// <summary>
        /// Click event callback : project menu > new composition 
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Button_NewComposition_Click(object _sender, RoutedEventArgs _e)
        {
            compSettingWindow = new CompSettingWindow();
            compSettingWindow.ShowDialog();

            compUIList.Add(new CompUI(compSettingWindow.New_composition, GetIndexValue(compUIList.Count - 1)));

            compUIList[compUIList.Count - 1].Button_Composite.Click += OnCompositeClick;
            compUIList[compUIList.Count - 1].Button_Pause.Click += OnPauseClick;
            compUIList[compUIList.Count - 1].Button_Wide.Click += OnWideClick;
            compUIList[compUIList.Count - 1].Button_Play.Click += OnPlayClick;
            compUIList[compUIList.Count - 1].Button_Setting.Click += OnSettingClick;
            compUIList[compUIList.Count - 1].Grid_Header.MouseDown += OnCompClick;

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
            if (compUIList[GetEventSource(_sender)].Composition.Forms.Count > 0)
            {
                Composition _newComp = new Composition(compUIList[GetEventSource(_sender)].Composition.Setting);
                compUIList[GetEventSource(_sender)].Composition = compositor.GenerateFull(_newComp);
            }
            else
            {
                compUIList[GetEventSource(_sender)].Composition = compositor.GenerateFull(compUIList[GetEventSource(_sender)].Composition);
            }

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
            player.Pause();
        }

        /// <summary>
        /// Click event callback : composition UI wide button
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void OnWideClick(object _sender, RoutedEventArgs _e)
        {
            int _totalBuffer = 0;

            foreach(CompUI _iter in compUIList)
            {
                _totalBuffer += (int)_iter.UI.ActualHeight;
            }

            _totalBuffer -= (int)compUIList[GetEventSource(_sender)].UI.ActualHeight;

            compUIList[GetEventSource(_sender)].SetWide((int)grid_View.ActualHeight - 20 - _totalBuffer);

            grid_View.UpdateLayout();

            RefreshCompPos();
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
            bool _checker = true;

            // checks if already in the list
            foreach(int _iter in selectedComp)
            {
                if(_iter == GetEventSource(_sender))
                {
                    _checker = false;
                }
            }

            // if mult is off, clear other selection
            if(!isMulti)
            {
                foreach(int _iter in selectedComp)
                {
                    compUIList[_iter].SetSelected(false);
                }

                selectedComp.Clear();
            }

            if (_checker)
            {
                compUIList[GetEventSource(_sender)].SetSelected(true);
                // add to list
                selectedComp.Add(GetEventSource(_sender));
            }
            else
            {
                compUIList[GetEventSource(_sender)].SetSelected(false);

                selectedComp.Remove(GetEventSource(_sender));
            }

            isSelled = true;
        }

        /// <summary>
        /// Returns index of event source
        /// </summary>
        /// <param name="_sender"></param>
        /// <returns></returns>
        private int GetEventSource(object _sender)
        {
            for(int _iter = 0; _iter < compUIList.Count; _iter++)
            {
                if(compUIList[_iter].Button_Setting.Equals(_sender) || compUIList[_iter].Button_Composite.Equals(_sender) || compUIList[_iter].Button_Pause.Equals(_sender) 
                    || compUIList[_iter].Button_Play.Equals(_sender) || compUIList[_iter].Button_Wide.Equals(_sender) || compUIList[_iter].Grid_Header.Equals(_sender))
                {
                    return _iter;
                }
            }

            return -1;
        }

        /// <summary>
        /// Key down event call back : whole window
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Window_KeyDown(object _sender, KeyEventArgs _e)
        {
            switch(_e.Key)
            {
                case (Key.LeftCtrl):

                    isMulti = true;

                    break;
            }
        }

        /// <summary>
        /// Key up event call back : whole window
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void main_window_KeyUp(object _sender, KeyEventArgs _e)
        {
            switch (_e.Key)
            {
                case (Key.Delete):

                    // removement
                    RemoveComp();

                    break;

                case (Key.LeftCtrl):

                    isMulti = false;

                    break;
            }
        }

        /// <summary>
        /// Track Bar click event call back
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Rect_timeScroll_MouseDown(object _sender, MouseButtonEventArgs _e)
        {
            clickOn = true;
        }

        /// <summary>
        /// Track Bar click event call back
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Rect_timeScroll_MouseUp(object _sender, MouseButtonEventArgs _e)
        {
            clickOn = false;

            trackBarOn = 0;
        }

        /// <summary>
        /// Track Bar click event call back
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Rect_timeScroll_MouseMove(object _sender, MouseEventArgs _e)
        {
            if(trackBarOn > 3 && clickOn)
            {
                currentView = _e.GetPosition(grid_backpanel2).X * (player.GetTotalTime(GetCompList()) / grid_backpanel2.ActualWidth)
                        - (Rect_timeScroll.Width / 2) * (player.GetTotalTime(GetCompList()) / grid_backpanel2.ActualWidth);

                // check over value
                if (currentView <= 0)
                {
                    currentView = 0;
                }
                else if(currentView >= player.GetTotalTime(GetCompList()))
                {
                    currentView = player.GetTotalTime(GetCompList());
                }

                trackPoint = _e.GetPosition(this);

                DrawView();
            }

            trackBarOn++;
        }

        /// <summary>
        /// Main window mouse down event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void main_window_MouseDown(object _sender, MouseButtonEventArgs _e)
        {
            if (!isSelled)
            {
                foreach (int _iter in selectedComp)
                {
                    compUIList[_iter].SetSelected(false);
                }

                selectedComp.Clear();

                isSelled = false;
            }
        }

        /// <summary>
        /// Main window mouse move event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Window_MouseMove(object _sender, MouseEventArgs _e)
        {
            Rect_timeScroll_MouseMove(this, _e);
        }

        /// <summary>
        /// Main window mouse up event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Window_MouseUp(object _sender, MouseButtonEventArgs _e)
        {
            Rect_timeScroll_MouseUp(this, _e);
        }

        /// <summary>
        /// Main window size changed event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void Window_SizeChanged(object _sender, SizeChangedEventArgs _e)
        {
            RefreshWidth();

            DrawView();
        }

        /// <summary>
        /// Play button click event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void button_play_Click(object _sender, RoutedEventArgs _e)
        {
            List<Composition> _playBuffer = new List<Composition>();
            
            foreach(CompUI _iter in compUIList)
            {
                _playBuffer.Add(_iter.Composition);
            }

            player.Play(_playBuffer, currentPosition);
        }

        /// <summary>
        /// Pause button click event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void button_pause_Click(object _sender, RoutedEventArgs _e)
        {
            player.Pause();
        }

        /// <summary>
        /// Stop button click event callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void button_stop_Click(object _sender, RoutedEventArgs _e)
        {
            player.Stop();
        }

        /// <summary>
        /// Time Bar Mouse Down callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void grid_PlayBar_MouseDown(object _sender, MouseButtonEventArgs _e)
        {

        }

        /// <summary>
        /// Time Bar Mouse move callback
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void grid_PlayBar_MouseMove(object _sender, MouseEventArgs _e)
        {

        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // not implemented
        }
    }
}

