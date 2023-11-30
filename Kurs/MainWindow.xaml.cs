using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using Newtonsoft.Json;


namespace Kurs
{
    public partial class MainWindow : Window
    {
        List<Meme> memes = new List<Meme>{};
        public MainWindow()
        {
            InitializeComponent();
        }


        private void updateImageGallery(GifMeme meme)
        {
            ImageBehavior.SetAnimatedSource(memeDisplay, new BitmapImage(new Uri(meme.filePath)));
        }

        private void updateImageGallery(Meme meme)
        {
            ImageBehavior.SetAnimatedSource(memeDisplay, null);
            memeDisplay.Source = new BitmapImage(new Uri(meme.filePath));
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                if(catalog.SelectedItem.GetType() == typeof(GifMeme))
                {
                    GifMeme selectedItemGif = (GifMeme) catalog.SelectedItem;
                    updateImageGallery(selectedItemGif);
                } else
                {
                    Meme selectedItem = (Meme)catalog.SelectedItem;
                    updateImageGallery(selectedItem);
                }
            } catch { }
        }

        private void Category_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem) categories_filter.SelectedItem;

            if (comboBoxItem.Name == "seleceted_gifs") {
                gifs_catal.Visibility = Visibility.Visible;
                stickers_catal.Visibility = Visibility.Hidden;
                memes_catal.Visibility = Visibility.Hidden;
            } else if(comboBoxItem.Name == "seleceted_memes")
            {
                memes_catal.Visibility = Visibility.Visible;
                stickers_catal.Visibility = Visibility.Hidden;
                gifs_catal.Visibility = Visibility.Hidden;
            } else if(comboBoxItem.Name == "seleceted_stic")
            {
                stickers_catal.Visibility = Visibility.Visible;
                gifs_catal.Visibility = Visibility.Hidden;
                memes_catal.Visibility = Visibility.Hidden;
            }
        }

        private void AddNewMemeClick(object sender, RoutedEventArgs e)
        {
            AddingMeme newfileWindow = new AddingMeme();
            newfileWindow.FileCreated += NewFileWindow_FileCreated;
            newfileWindow.ShowDialog();
        }

        private void NewFileWindow_FileCreated(object sender, Meme meme)
        {
            switch(meme.category)
            {
                case "GIF":               
                    gifs_catal.Items.Add(meme);
                    memes.Add(meme);
                    break;
                case "Memes":
                    memes_catal.Items.Add(meme);
                    memes.Add(meme);
                    break;
                case "Stickers":
                    stickers_catal.Items.Add(meme);
                    memes.Add(meme);
                    break;
            }
        }

        private void SaveAsJSON_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(memes);
            File.WriteAllText("C:\\Users\\miyaz\\Labs\\C#\\Kurs\\Kurs\\data.json", json);
        }

        private void LoadFromJSON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = File.ReadAllText("C:\\Users\\miyaz\\Labs\\C#\\Kurs\\Kurs\\data.json");
                List<Meme>  deserialiazedMemes = JsonConvert.DeserializeObject<List<Meme>>(json);

                foreach (Meme meme in deserialiazedMemes)
                {
                    if(meme.category == "GIF")
                    {
                        GifMeme gif = new GifMeme(meme.title, meme.category, meme.filePath);
                        memes.Add(gif);
                        gifs_catal.Items.Add(gif);
                    } else if(meme.category == "Memes")
                    {
                        StaticMeme sm = new StaticMeme(meme.title, meme.category, meme.filePath);
                        memes.Add(sm);
                        memes_catal.Items.Add(sm);
                    } else if(meme.category == "Stickers")
                    {
                        Sticker stic = new Sticker(meme.title, meme.category, meme.filePath);
                        memes.Add(stic);
                        stickers_catal.Items.Add(stic);
                    }

                }

            } catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (catalog.SelectedItem != null)
            {
                Meme selectedItem = catalog.SelectedItem as Meme;
                memes.Remove(selectedItem);

                switch (selectedItem.category)
                {
                    case "GIF":
                        gifs_catal.Items.Remove(selectedItem);
                        break;
                    case "Memes":
                        memes_catal.Items.Remove(selectedItem);
                        break;
                    case "Stickers":
                        stickers_catal.Items.Remove(selectedItem);
                        break;
                }
            }
        }
    }
}
