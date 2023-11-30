using Microsoft.Win32;
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

namespace Kurs
{
    /// <summary>
    /// Interaction logic for AddingMeme.xaml
    /// </summary>
    public partial class AddingMeme : Window
    {
        public event EventHandler<Meme> FileCreated;
        string selectedFileName;
        string category;
        string memeTitle;
        string url;

        public AddingMeme()
        {
            InitializeComponent();
        }

        private void ChooseFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFileName = openFileDialog.FileName;
            }
        }

        private void CreateMeme_Click(object sender, RoutedEventArgs e)
        {
            if (memeTitle == null)
            {
                MessageBox.Show("Add a title for your meme");
            } else if(category == null)
            {
                MessageBox.Show("Provide meme category!");
            } else if(selectedFileName == null && url == null)
            {
                MessageBox.Show("Provide a file path to your meme");
            } else
            {
                if(category == "GIF")
                {
                    var filePath = selectedFileName == null ? url : selectedFileName;
                    GifMeme gif = new GifMeme(memeTitle, category, filePath);
                    FileCreated.Invoke(this, gif);
                } else if(category == "Memes") 
                {
                    var filePath = selectedFileName == null ? url : selectedFileName;
                    StaticMeme sm = new StaticMeme(memeTitle, category, filePath);
                    FileCreated.Invoke(this, sm);
                } else
                {
                    var filePath = selectedFileName == null ? url : selectedFileName;
                    Sticker stick = new Sticker(memeTitle, category, filePath);
                    FileCreated.Invoke(this, stick);
                }
            }

            this.Close();
        }


        private void CategorySelected(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem) categories_filter.SelectedItem;
            var comboBoxItemContent = comboBoxItem.Content as Label;
            category = (string) comboBoxItemContent.Content;
        }

        private void memeTitle_changed(object sender, TextChangedEventArgs e)
        {
            memeTitle = title.Text;
        }

        private void memeUrl_Changed(object sender, TextChangedEventArgs e)
        {
            url = meme_url.Text;
        }
    }
}
