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
                Colors settingsColors = new Colors();
              
                this.rootGrid.Background = settingsColors.primaryLightColorBrush();
                rootMenu.Background = settingsColors.PrimaryColorBrush();
                rootMenu.Foreground = settingsColors.textColorBrush();
                
                mabt.Background = settingsColors.PrimaryColorBrush();
                mcfg.Background = settingsColors.PrimaryColorBrush();
                mnp.Background = settingsColors.PrimaryColorBrush();
                mdp.Background = settingsColors.PrimaryColorBrush();

                tboxsearch.Background = settingsColors.primaryLightColorBrush();
                tboxsearch.Foreground = settingsColors.textColorBrush();
                lstbox_Words.Background = settingsColors.primaryLightColorBrush();
                lstbox_Words.Foreground = settingsColors.textColorBrush();
                lbel_other.Foreground = settingsColors.textColorBrush();
                word_name.Foreground = settingsColors.textColorBrush();
                tboxDef.Foreground = settingsColors.textColorBrush();
                tboxDef.Background = settingsColors.primaryLightColorBrush();
                tboxSin.Foreground = settingsColors.textColorBrush();
                tboxSin.Background = settingsColors.primaryLightColorBrush();
                tboxAnt.Background = settingsColors.primaryLightColorBrush();
                tboxAnt.Foreground = settingsColors.textColorBrush();
                tboxSent.Foreground = settingsColors.textColorBrush();
                tboxSent.Background = settingsColors.primaryLightColorBrush();

                lbelSent.Foreground = settingsColors.textColorBrush();
                lbelSin.Foreground = settingsColors.textColorBrush();
                lbelAnt.Foreground = settingsColors.textColorBrush();
            }
            catch(Exception e) { Console.WriteLine("The Theme Could not be applied"); }
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
            //Deprecated
            if (lstWords.Any())
            {
                //Download
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
            loadTheme();
        }

        
    }

    
}
