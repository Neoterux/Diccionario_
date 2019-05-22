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

namespace Diccionario_
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            configColor();
        }


        private void configColor()
        {
            try
            {
                
                Colors settingsColors = new Colors();

                lbel1.Foreground = settingsColors.textColorBrush();
                lbel2.Foreground = settingsColors.textColorBrush();

                btnExit.Foreground = settingsColors.textColorBrush();
                gridBg.Background = settingsColors.primaryLightColorBrush();
                tblockNames.Foreground = settingsColors.textColorBrush();
                tblockNames.Background = settingsColors.PrimaryColorBrush();
                btnExit.Background = settingsColors.PrimaryColorBrush();
            }
            catch (Exception e) { Console.WriteLine("Impossible to apply colors");  }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
            
        }
    }
}
