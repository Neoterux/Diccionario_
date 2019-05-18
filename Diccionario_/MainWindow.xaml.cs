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
    }
}
