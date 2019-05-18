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

using System.Windows.Forms;
using System.IO;
using System.Data;
//using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
//using MySql.Data;
using System.Data.OleDb;

namespace Diccionario_
{
    /// <summary>
    /// Lógica de interacción para AddWordWindow.xaml
    /// </summary>
    public partial class AddWordWindow : Window
    {
        
       
        public AddWordWindow()
        {
            InitializeComponent();
            ___Cbox_Other_.SelectedIndex = 2;
        }
        
        void onExitClicked(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //------------------------------------------<Add Button Config>
        void onAddClicked(Object sender, RoutedEventArgs e)
        {
            //string cn_String = Properties.Settings.Default.Connection_String;
            string sql = "INSERT INTO Words(WORD, DEFINITION, SINONYM, ANTONYM, OTHER, SENTENCE) VALUES(@word, @def, @syn, @ant, @other, @sent)";
            //using (SqlConnection connection = new SqlConnection(cn_String))
            //using (MySqlConnection msqlConnection = new MySqlConnection(Properties.Settings.Default.Connection_String))
            try
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.Connection_String))
                {




                    if (!(tbox_Word.Text == "" || tbox_def.Text == "" || tbox_sent.Text == "" ))
                    {
                        connection.Open();
                        //using (SqlCommand sqlCmd = new SqlCommand(sql, connection))
                        using (OleDbCommand msqlCmd = new OleDbCommand(sql, connection))
                        {
                            //sqlCmd.CommandType = CommandType.StoredProcedure;
                            msqlCmd.Parameters.AddWithValue("@word", tbox_Word.Text);
                            msqlCmd.Parameters.AddWithValue("@def", tbox_def.Text);
                            msqlCmd.Parameters.AddWithValue("@syn", tbox_syn.Text);
                            msqlCmd.Parameters.AddWithValue("@ant", tbox_ant.Text);

                            if (___Cbox_Other_.Text == "Nada")
                            {
                                msqlCmd.Parameters.AddWithValue("@other", " ");
                            }
                            else
                            {
                                msqlCmd.Parameters.AddWithValue("@other", ___Cbox_Other_.Text);
                            }

                            msqlCmd.Parameters.AddWithValue("@sent", tbox_sent.Text);


                            int i = msqlCmd.ExecuteNonQuery();

                            if (i != 0)
                            {
                                System.Windows.MessageBox.Show("Datos Agregados Correctamente");

                            }
                        }
                        connection.Close();
                        //this.Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Complete todas las entradas");
                    }

                }

            }
            catch (OleDbException odbe)
            {
                System.Windows.MessageBox.Show("Necesita tener permisios de administrador para realizar esta accion", "ERR_DB_PRIVILEGIES", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            
                
            
        }

        private void Btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            Stream mStream = null;
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Title = "Abrir  archivo...";
            ofd.Filter = "HTML Files (*.html)|*html";
            Nullable<bool> result = ofd.ShowDialog();
            
            if(result == true)
            {
                try
                {
                    if ( (mStream = ofd.OpenFile()) != null)
                    {
                        using (mStream)
                        {
                            StreamReader strReader = new StreamReader(mStream);
                            doc.LoadHtml(strReader.ReadToEnd());
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al abrir archivo:" + ex.Message);
                }
                
            }//end if to read file

            /*var root = doc.DocumentNode;
            var sb = new StringBuilder();
            foreach(var node in root.DescendantNodesAndSelf())
            {
                if (!node.HasChildNodes)
                {
                    String text = node.InnerText;
                    Console.WriteLine(text);
                }
            }*/
          

            tbox_Word.Text = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//h3/text()").DescendantsAndSelf().First().InnerText);
            tbox_def.Text = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//span[@id='sig']").DescendantsAndSelf().First().InnerText);
            tbox_syn.Text = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//span[@id='syn']").DescendantsAndSelf().First().InnerText);
            tbox_ant.Text = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//span[@id='ant']").DescendantsAndSelf().First().InnerText);
            String other = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//span[@id='prop']").DescendantsAndSelf().First().InnerText);
           tbox_sent.Text = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode("//span[@id='sent']").DescendantsAndSelf().First().InnerText);


            if ( other == "Adjetivo")
            {
                ___Cbox_Other_.SelectedIndex = 0;
            }
            else if (other == "Sustantivo")
            {
                ___Cbox_Other_.SelectedIndex = 1;
            }
            else if (other == "Verbo")
            {
                ___Cbox_Other_.SelectedIndex = 3;
            }
            else
            {
                ___Cbox_Other_.SelectedIndex = 2;

            }



        }
    }
}
