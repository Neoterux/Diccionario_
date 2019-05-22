using System;
using System.Collections.Generic;
using System.Drawing;
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


namespace Diccionario_
{
    /// <summary>
    /// Lógica de interacción para Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {

        public Settings()
        {
            InitializeComponent();
            tboxTitle.Text = Properties.Settings.Default.TitleSize.ToString();
            tboxDef.Text = Properties.Settings.Default.DefSize.ToString();
            tboxSinAnt.Text = Properties.Settings.Default.SinAntSize.ToString();
            tboxSent.Text = Properties.Settings.Default.SentSize.ToString();
            tboxList.Text = Properties.Settings.Default.ListSize.ToString();

            //set settings colors
            tbox_primary.Text = HexConverter( Properties.Settings.Default.Primary);
            tbox_plight.Text = HexConverter(Properties.Settings.Default.Primary_Light) ;
            tbox_pdark.Text = HexConverter(Properties.Settings.Default.Primary_Dark);
            tbox_text.Text = HexConverter(Properties.Settings.Default.Text_color);
            cfgColor();
            updateRectamgleColor();
            //select index of combobox
            if (tbox_primary.Text == "#DBDBDB" && tbox_plight.Text == "#EFEFEF" && tbox_pdark.Text == "#8D8D8D" && tbox_text.Text == "#000000")
            {
                cboxTheme.SelectedIndex = 0;
                setEnabled(false);
            }
            else if (tbox_primary.Text == "#2E3233" && tbox_plight.Text == "#3B3F42" && tbox_pdark.Text == "#313538" && tbox_text.Text == "#FFFFFF")
            {
                //dark theme
                cboxTheme.SelectedIndex = 1;
                setEnabled(false);
            }
            else
            {
                //custom theme
                cboxTheme.SelectedIndex = 2;
                setEnabled(true);
            }


        }

        private void cfgColor()
        {
            
            Colors settingsColors = new Colors();

            bgGrid.Background = settingsColors.primaryLightColorBrush();
            gboxlett.Foreground = settingsColors.textColorBrush();
            gboxtheme.Foreground = settingsColors.textColorBrush();
            tbox_primary.Foreground = settingsColors.textColorBrush();
            tbox_primary.Background = settingsColors.primaryLightColorBrush();
            tbox_plight.Foreground = settingsColors.textColorBrush();
            tbox_plight.Background = settingsColors.primaryLightColorBrush();
            tbox_pdark.Foreground = settingsColors.textColorBrush();
            tbox_pdark.Background = settingsColors.primaryLightColorBrush();
            tbox_text.Foreground = settingsColors.textColorBrush();
            tbox_text.Background = settingsColors.primaryLightColorBrush();
            lbel1.Foreground = settingsColors.textColorBrush();
            lbel2.Foreground = settingsColors.textColorBrush();
            lbel3.Foreground = settingsColors.textColorBrush();
            lbel4.Foreground = settingsColors.textColorBrush();
            lbel5.Foreground = settingsColors.textColorBrush();
            lbel6.Foreground = settingsColors.textColorBrush();
            lbel7.Foreground = settingsColors.textColorBrush();
            lbel8.Foreground = settingsColors.textColorBrush();
            lbel9.Foreground = settingsColors.textColorBrush();
            btnApply.Foreground = settingsColors.textColorBrush();
            btnCancel.Foreground = settingsColors.textColorBrush();
            btnApply.Background = settingsColors.PrimaryColorBrush();
            btnCancel.Background = settingsColors.PrimaryColorBrush();
            
        }

        void updateRectamgleColor()
        {
            try
            {
                System.Drawing.Color primary = ColorTranslator.FromHtml(tbox_primary.Text);
                System.Drawing.Color primaryLight = ColorTranslator.FromHtml(tbox_plight.Text);
                System.Drawing.Color primaryDark = ColorTranslator.FromHtml(tbox_pdark.Text);
                System.Drawing.Color textColor = ColorTranslator.FromHtml(tbox_text.Text);
                //set rectangle colors
                cPrimary.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(primary.R, primary.G, primary.B));
                cPrimaryL.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(primaryLight.R, primaryLight.G, primaryLight.B));
                cPrimaryD.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(primaryDark.R, primaryDark.G, primaryDark.B));
                cText.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(textColor.R, textColor.G, textColor.B));
            }
            catch(Exception e) { }
        }

        private void TboxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            verifyInput(this.tboxTitle);
        }
        private static string HexConverter(System.Drawing.Color c)
        {
            string rtn = string.Empty;
            try
            {
                rtn = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            {
                //doing nothing
            }

            return rtn;
        }

        private static string RGBConverter(System.Drawing.Color c)
        {
            string rtn = string.Empty;
            try
            {
                rtn = "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
            }
            catch (Exception ex)
            {
                //doing nothing
            }

            return rtn;
        } 
        private void verifyInput(TextBox tbox)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbox.Text, "[^0-9]") )
            {
                
                tbox.Text = tbox.Text.Remove(tbox.Text.Length - 1);
            }
            else
            {
                if (Int32.TryParse(tbox.Text, out int u))
                {
                   if (u < 1)
                    {
                        tbox.Text = "1";
                    }
                   if (u > 255)
                    {
                        tbox.Text = "255";
                    }
                }
            }
            
        }

        private void TboxDef_TextChanged(object sender, TextChangedEventArgs e)
        {
            verifyInput(this.tboxDef);
        }

        private void TboxSinAnt_TextChanged(object sender, TextChangedEventArgs e)
        {
            verifyInput(this.tboxSinAnt);
        }

        private void TboxSent_TextChanged(object sender, TextChangedEventArgs e)
        {
            verifyInput(this.tboxSent);
        }

        private void TboxList_TextChanged(object sender, TextChangedEventArgs e)
        {
            verifyInput(this.tboxList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Properties.Settings.Default.TitleSize = (byte)Int32.Parse(tboxTitle.Text);
            Properties.Settings.Default.DefSize = (byte)Int32.Parse(tboxDef.Text);
            Properties.Settings.Default.SinAntSize = (byte)Int32.Parse(tboxSinAnt.Text);
            Properties.Settings.Default.SentSize = (byte)Int32.Parse(tboxSent.Text);
            Properties.Settings.Default.ListSize = (byte)Int32.Parse(tboxList.Text);
            Properties.Settings.Default.Primary = System.Drawing.ColorTranslator.FromHtml(tbox_primary.Text);
            Properties.Settings.Default.Primary_Light = System.Drawing.ColorTranslator.FromHtml(tbox_plight.Text);
            Properties.Settings.Default.Primary_Dark = System.Drawing.ColorTranslator.FromHtml(tbox_pdark.Text);
            Properties.Settings.Default.Text_color = System.Drawing.ColorTranslator.FromHtml(tbox_text.Text);
            Properties.Settings.Default.Save();
            MessageBox.Show("Reinicie la aplicacion para visualizar cambios");
            this.Close();
        }


        private void setEnabled(bool a)
        {
            tbox_primary.IsEnabled = a;
            tbox_plight.IsEnabled = a;
            tbox_pdark.IsEnabled = a;
            tbox_text.IsEnabled = a;
        }
        private void CboxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cboxTheme.SelectedIndex)
            {
                case 0: //light
                    tbox_primary.Text = "#DBDBDB";
                    setEnabled(false);
                    tbox_plight.Text = "#EFEFEF";
                    tbox_pdark.Text = "#8D8D8D";
                    tbox_text.Text = "#000000";
                    updateRectamgleColor();
                    break;
                case 1://dark
                    tbox_primary.Text = "#2E3233";
                    tbox_plight.Text = "#3B3F42";
                    tbox_pdark.Text = "#313538";
                    tbox_text.Text = "#FFFFFF";
                    setEnabled(false);
                    updateRectamgleColor();
                    break;
                case 2://custom
                    setEnabled(true);
                    break;
            }
        }

        private void Tbox_primary_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateRectamgleColor();
            tbox_primary.Text = tbox_primary.Text.ToUpper();


        }

        private void Tbox_plight_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateRectamgleColor();
        }

        private void Tbox_pdark_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateRectamgleColor();
        }

        private void Tbox_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateRectamgleColor();
        }
    }
}
