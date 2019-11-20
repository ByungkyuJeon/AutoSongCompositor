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
    /// FormListingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FormListingWindow : Window
    {
        /// <summary>
        /// Forms list
        /// </summary>
        List<string> forms;

        /// <summary>
        /// Variation list matched with form list
        /// </summary>
        List<string> variations;

        /// <summary>
        /// Forms list
        /// </summary>
        public List<string> Forms { get => forms; }

        /// <summary>
        /// Variation list matched with form list
        /// </summary>
        public List<string> Variations { get => variations; }

        /// <summary>
        /// Initiates default window set
        /// </summary>
        public FormListingWindow()
        {
            InitializeComponent();

            forms = new List<string>();
            variations = new List<string>();
        }

        /// <summary>
        /// Click event callback : delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            // delete selected in list
            if (FormList.SelectedItem != null)
            {
                VariList.Items.RemoveAt(FormList.Items.IndexOf(FormList.SelectedItem));

                forms.RemoveAt(FormList.Items.IndexOf(FormList.SelectedItem));
                variations.RemoveAt(FormList.Items.IndexOf(FormList.SelectedItem));

                FormList.Items.Remove(FormList.SelectedItem);
            }
        }

        /// <summary>
        /// Click event callback : add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_Click(object sender, RoutedEventArgs e)
        {
            // adding selection to list
            if (SongForm_Forms.SelectedItem != null)
            {
                FormList.Items.Add(((Button)SongForm_Forms.SelectedItem).Content);
                VariList.Items.Add(((Button)SongForm_Varia.SelectedItem).Content);

                forms.Add((string)((Button)(SongForm_Forms.SelectedItem)).Content);
                variations.Add((string)((Button)(SongForm_Varia.SelectedItem)).Content);
            }
            // no selection exist
            else
            {
                MessageBox.Show("Nothing selected. Please select one");
            }
        }

        /// <summary>
        /// Click event callback : down button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void down_Click(object sender, RoutedEventArgs e)
        {
            // rearrange list : down
            if (FormList.SelectedItem != null)
            {
                object buffer_item_form = FormList.SelectedItem;
                object buffer_item_variation = VariList.SelectedItem;
                int index = FormList.Items.IndexOf(buffer_item_form);

                FormList.Items.RemoveAt(index);
                FormList.Items.Insert(index + 1, buffer_item_form);

                VariList.Items.RemoveAt(index);
                VariList.Items.Insert(index + 1, buffer_item_variation);
            }
        }

        /// <summary>
        /// Click event callback : up button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void up_Click(object sender, RoutedEventArgs e)
        {
            // rearrange list : up
            if (FormList.SelectedItem != null)
            {
                object buffer_item_form = FormList.SelectedItem;
                object buffer_item_variation = VariList.SelectedItem;
                int index = FormList.Items.IndexOf(buffer_item_form);

                FormList.Items.RemoveAt(index);
                FormList.Items.Insert(index - 1 , buffer_item_form);

                VariList.Items.RemoveAt(index);
                VariList.Items.Insert(index - 1, buffer_item_variation);
            }
        }

        /// <summary>
        /// Click event callback : confirm button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            // close window
            Close();
        }

        /// <summary>
        /// Selection changed event callback : form list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // giving selection to matching object
            VariList.SelectedIndex = VariList.Items.IndexOf(VariList.SelectedItem);
        }

        /// <summary>
        /// Selection changed event callback : variation list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VariList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // giving selection to matching object
            FormList.SelectedIndex = FormList.Items.IndexOf(FormList.SelectedItem);
        }

    }
}
