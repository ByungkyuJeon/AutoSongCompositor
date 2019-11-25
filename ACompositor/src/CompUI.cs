using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ACompositor.src
{
    /// <summary>
    /// Composition UI set
    /// </summary>
    class CompUI
    {
        /// <summary>
        /// Composition
        /// </summary>
        Composition composition;

        /// <summary>
        /// Composition UI mother grid
        /// </summary>
        Grid grid_Mother;

        // UIs
        Grid grid_Header;
        TextBox textBox_CompositionName;
        Label label_Jengre;
        Label label_Scale;
        Button button_Setting;
        Button button_Composite;
        Button button_Play;
        Button button_Pause;
        Button button_Stop;

        Grid grid_FormLine;
        Label label_FormName;

        Grid grid_NodeLine;
        Canvas canvas_Content;

        Grid grid_NoteLine;
        Label label_Line1;
        Label label_Line2;
        Label label_Line3;
        Label label_Line4;

        /// <summary>
        /// True if UIs are allocated
        /// </summary>
        bool isInitiated = false;

        /// <summary>
        /// Composition UI
        /// </summary>
        public Grid UI { get => grid_Mother; set => grid_Mother = value; }

        /// <summary>
        /// Composition
        /// </summary>
        internal Composition Composition { get => composition; set => composition = value; }

        /// <summary>
        /// Constructor with drawing
        /// </summary>
        /// <param name="_composition"></param>
        public CompUI(Composition _composition)
        {
            composition = _composition;

            Draw(composition);
        }

        void Draw(Composition _composition)
        {
            // check if initiated
            if (isInitiated)
            {

            }
            // instaciate UIs
            else
            {
                grid_Mother = new Grid()
                {
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Height = 141,
                    Background = new SolidColorBrush(Color.FromRgb(40, 40, 40))
                };

                grid_Header = new Grid()
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    Width = 144,
                    Background = new SolidColorBrush(Color.FromRgb(50, 50, 50))
                };
                textBox_CompositionName = new TextBox()
                {
                    Margin = new System.Windows.Thickness(52, 20, 0, 0),
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    Background = new SolidColorBrush(Color.FromRgb(50, 50, 50)),
                    Text = composition.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 10,
                    Height = 17,
                    Width = 82,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };
                label_Jengre = new Label()
                {
                    Content = composition.Setting.Jengre,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 10,
                    Height = 24,
                    Width = 51,
                    Margin = new System.Windows.Thickness(27, 42, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left

                };
                label_Scale = new Label()
                {
                    Content = composition.Setting.ScaleNote.ToString() + " " + composition.Setting.Scale.ToString(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 10,
                    Height = 24,
                    Width = 51,
                    Margin = new System.Windows.Thickness(27, 66, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left

                };

                if(composition.Setting.ScaleNote.ToString() == "NULL")
                {
                    label_Scale.Content = "Random Scale";
                }

                button_Setting = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 17,
                    Width = 17,
                    Margin = new System.Windows.Thickness(30, 20, 0, 0),
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };
                button_Composite = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 85, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };
                button_Play = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 60, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };
                button_Stop = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 10, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };
                button_Pause = new Button()
                {
                    // TODO :: 이미지 필요
                    Height = 25,
                    Width = 25,
                    Margin = new System.Windows.Thickness(0, 0, 35, 19.4),
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                };

                grid_FormLine = new Grid()
                {
                    Height = 20,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(174, 0, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(50, 50, 50))
                };
                label_FormName = new Label()
                {
                    Content = "Form",
                    Padding = new System.Windows.Thickness(0, 0, 0, 0),
                    Margin = new System.Windows.Thickness(281, 0, 280, 0),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontSize = 9,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center
                };

                grid_NodeLine = new Grid()
                {
                    Height = 20,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(174, 20, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(40, 40, 40))
                };

                canvas_Content = new Canvas()
                {
                    Margin = new System.Windows.Thickness(174, 40, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(30, 30, 30))
                };

                grid_NoteLine = new Grid()
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    Width = 30,
                    Background = new SolidColorBrush(Color.FromRgb(35, 35, 35)),
                    Margin = new System.Windows.Thickness(144, 0, 0, 0)
                };
                label_Line1 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 39, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };
                label_Line2 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 61, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };
                label_Line3 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 85, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };
                label_Line4 = new Label()
                {
                    Content = "-",
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(8, 109, 0, 0),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                };

                grid_Header.Children.Add(textBox_CompositionName);
                grid_Header.Children.Add(label_Jengre);
                grid_Header.Children.Add(label_Scale);
                grid_Header.Children.Add(button_Setting);
                grid_Header.Children.Add(button_Composite);
                grid_Header.Children.Add(button_Play);
                grid_Header.Children.Add(button_Stop);
                grid_Header.Children.Add(button_Pause);

                grid_FormLine.Children.Add(label_FormName);

                grid_NodeLine.Children.Add(canvas_Content);

                grid_NoteLine.Children.Add(label_Line1);
                grid_NoteLine.Children.Add(label_Line2);
                grid_NoteLine.Children.Add(label_Line3);
                grid_NoteLine.Children.Add(label_Line4);

                grid_Mother.Children.Add(grid_Header);
                grid_Mother.Children.Add(grid_FormLine);
                grid_Mother.Children.Add(grid_NodeLine);
                grid_Mother.Children.Add(grid_NoteLine);

                // make the boolean true
                isInitiated = true;
            }
        }

        public void SetCompName(string _str)
        {
            composition.Name = _str;
        }

        void DrawNotes()
        {

        }

        void DrawCompName(string _str)
        {
            label_FormName.Content = _str;
        }

        public void NextPage()
        {

        }

        void ClearNotes()
        {

        }



    }
}
