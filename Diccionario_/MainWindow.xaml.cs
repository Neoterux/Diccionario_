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

using System.Configuration;
//using System.Data.SqlClient;
//using System.Data;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OleDb;

using System.IO;

namespace Diccionario_
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection ole_Connetion;
        string cnString;
        List<Word> lstWords;
        public MainWindow()
        {
            InitializeComponent();
            connectDb();
            lstbox_Words.FontSize = Properties.Settings.Default.ListSize;
            word_name.FontSize = Properties.Settings.Default.TitleSize;
            tboxDef.FontSize = Properties.Settings.Default.DefSize;
            tboxAnt.FontSize = Properties.Settings.Default.SinAntSize;
            tboxSin.FontSize = Properties.Settings.Default.SinAntSize;
            tboxSent.FontSize = Properties.Settings.Default.SentSize;
            loadTheme();

        }


        private void loadTheme()
        {
            try
            {
                System.Drawing.Color primary = Properties.Settings.Default.Primary;
                System.Drawing.Color primaryLight = Properties.Settings.Default.Primary_Light;
                System.Drawing.Color primaryDark = Properties.Settings.Default.Primary_Dark;
                System.Drawing.Color textColor = Properties.Settings.Default.Text_color;
                this.rootGrid.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                rootMenu.Background = new SolidColorBrush(Color.FromRgb(primaryDark.R, primaryDark.G, primaryDark.B));
                rootMenu.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                mabt.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                mcfg.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                mnp.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                mdp.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                
                tboxsearch.Background = new  SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                tboxsearch.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                lstbox_Words.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                lstbox_Words.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                lbel_other.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                word_name.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                tboxDef.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                tboxDef.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                tboxSin.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                tboxSin.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                tboxAnt.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                tboxAnt.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                tboxSent.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                tboxSent.Background = new SolidColorBrush(Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));

                lbelSent.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                lbelSin.Foreground = new  SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
                lbelAnt.Foreground = new SolidColorBrush(Color.FromRgb(textColor.R, textColor.G, textColor.B));
            }
            catch(Exception e) { }
        }
            //-------------------<Start Database Connection and data>--------------
        void connectDb()
            {
            //<Initialize database connection>
            string query = "SELECT * FROM Words ORDER BY Words.WORD;";
            cnString = Properties.Settings.Default.Connection_String;

            using (ole_Connetion = new OleDbConnection(Properties.Settings.Default.Connection_String))
            using (OleDbCommand oleCmd = new OleDbCommand(query, ole_Connetion))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(oleCmd))
            {
                ole_Connetion.Open();
                DataTable dtTable = new DataTable();

                adapter.Fill(dtTable);
                try
                {
                    lstWords.Clear();
                }catch (Exception e) { }
                    
                
                

                lstWords = dtTable.AsEnumerable().Select(m => new Word()
                {
                    wordName = m.Field<string>("Word"),
                    Definition = m.Field<string>("Definition"),
                    Sinonyms = m.Field<string>("Sinonym"),
                    Antonyms = m.Field<string>("Antonym"),
                    Other = m.Field<string>("Other"),
                    Sentence = m.Field<string>("Sentence")

                }).ToList();
                ole_Connetion.Close();
                lstbox_Words.ItemsSource = lstWords;
                lstbox_Words.DisplayMemberPath = "wordName";
                lstbox_Words.SelectedValuePath = "wordName";

                /*foreach (Word word in lstWords)
                {
                    using (StreamWriter file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "Words"+ "\\" + word.wordName + ".html" ))
                    {
                        string final_html = "<div class=\"row\">\n" +
                            "\t<div class=\"col s6\">\n" +
                                    "\t\t<h3 id =\"name\" >" + word.wordName + "</h3>\n" +
                                    "\t\t<span id = \"prop\">" + word.Other + "</span><br/>\n" +
                                    "\t\t<p><span id=\"sig\">" + word.Definition + "</span></p>\n" +
                                    "\t\t<p><span id=\"syn\">" + word.Sinonyms + "</span></p>\n" +
                                    "\t\t<p><span id=\"ant\">" + word.Antonyms + "</span></p>\n" +
                                    "\t\t<p><span id=\"sent\">" + word.Sentence + "</span></p>\n" +
                            "\t</div>" +
                          "</div>";
                        file.Write(final_html);
                    }
                }*/

                if (lstWords.Any())
                {
                    word_name.Content = lstWords[0].wordName;
                    lbel_other.Content = lstWords[0].Other;
                    tboxDef.Text = lstWords[0].Definition;
                    tboxSin.Text = lstWords[0].Sinonyms;
                    tboxAnt.Text = lstWords[0].Antonyms;
                    tboxSent.Text = lstWords[0].Sentence;
                }
                //< Show / hide word grid>
                if (!lstbox_Words.HasItems)
                {
                    word_container.Visibility = Visibility.Hidden;
                }
                else
                {
                    word_container.Visibility = Visibility.Visible;
                }
                //lstbox_Words.ItemsSource = lstWords.wordName;



                //-------------------</End Database connection>--------------------

            }
        }

        void onClickNewWord(Object sender, RoutedEventArgs e)
        {
            AddWordWindow a = new AddWordWindow();
            try
            {
                a.ShowDialog();
                connectDb();
            }catch (NullReferenceException nre) { }
            
        }

        //<INIT DB CONNECTION>

        void initListAndResources()
        {
                lstbox_Words.ItemsSource = lstWords;
                lstbox_Words.DisplayMemberPath = "wordName";
                lstbox_Words.SelectedValuePath = "wordName";

                if (lstWords.Any())
                {
                    word_name.Content = lstWords[0].wordName;
                    lbel_other.Content = lstWords[0].Other;
                    tboxDef.Text = lstWords[0].Definition;
                    tboxSin.Text = lstWords[0].Sinonyms;
                    tboxAnt.Text = lstWords[0].Antonyms;
                    tboxSent.Text = lstWords[0].Sentence;
                }
                //< Show / hide word grid>
            if (!lstbox_Words.HasItems)
                {
                    word_container.Visibility = Visibility.Hidden;
                }
                else
                {
                    word_container.Visibility = Visibility.Visible;
                }
            
        }
         
        //</INIT DB CONNECTION>

        void onClickAbout(Object sender, RoutedEventArgs e)
        {
            Window1 a = new Window1();
            a.ShowDialog();
            connectDb();
       
        }

        private void Lstbox_Words_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*int index = lstbox_Words.SelectedIndex;

            word_name.Content = lstWords[index].wordName;
            lbel_other.Content = lstWords[index].Other;
            tboxDef.Text = lstWords[index].Definition;
            tboxSin.Text = lstWords[index].Sinonyms;
            tboxAnt.Text = lstWords[index].Antonyms;*/
            bool continue_ = true;
            int index = 0;
            try {
                index= lstWords.FindIndex(x => x.wordName.Contains(lstbox_Words.SelectedValue.ToString()));
             

            }
            catch ( NullReferenceException nre)
            {
                continue_ = false;
                Console.WriteLine(nre.Message);
            }
            if (continue_)
            {
                
                word_name.Content = lstWords[index].wordName;
                lbel_other.Content = lstWords[index].Other;
                tboxDef.Text = lstWords[index].Definition;
                tboxSin.Text = lstWords[index].Sinonyms;
                tboxAnt.Text = lstWords[index].Antonyms;
                tboxSent.Text = lstWords[index].Sentence;
                

            }
            

            //Console.WriteLine(lstWords.FindIndex(x => x.wordName.Contains(lstbox_Words.SelectedValue.ToString())));
        }

        private void Tboxsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtOrig = tboxsearch.Text;
            string upper = txtOrig.ToUpper();
            string lower = txtOrig.ToLower();
            var wordFiltered = from word in lstWords
                               let wname = word.wordName
                               where wname.StartsWith(lower)
                               || wname.StartsWith(upper)
                               || wname.StartsWith(txtOrig)
                               select word;
            lstbox_Words.ItemsSource = wordFiltered;
            

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lstWords.Any())
            {

            }
            else
            {
                System.Windows.MessageBox.Show("No se encuentran disponibles palabras para descargar");
            }
        }

        private void SettingItem_Click(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings();
            set.ShowDialog();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (rootGrid.ActualHeight > 700)
            {
                tboxDef.MinHeight = 350;
            }
            else
            {
                tboxDef.MinHeight = 268;
            }
        }
    }

    
}
