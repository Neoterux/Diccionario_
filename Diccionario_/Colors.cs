using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;


namespace Diccionario_
{
    class Colors
    {
        private Color primary = Properties.Settings.Default.Primary;
        private Color primaryLight = Properties.Settings.Default.Primary_Light;
        private Color primaryDark = Properties.Settings.Default.Primary_Dark;
        private Color textColor = Properties.Settings.Default.Text_color;

        public Color Primary { get; }
        public Color PrimaryLight { get; }
        public Color PrimaryDark { get; }
        public Color TextColor { get; }

        public Colors()
        {
            this.Primary = primary;
            this.PrimaryLight = primaryLight;
            this.PrimaryDark = primaryDark;
            this.TextColor = textColor;
        }

        public System.Windows.Media.Color getColor(Color Property)
        {

            return System.Windows.Media.Color.FromRgb(Property.R, Property.G, Property.B);
        }


        public System.Windows.Media.SolidColorBrush textColorBrush()
        {
            return new System.Windows.Media.SolidColorBrush(getColor(TextColor));
        }
        public System.Windows.Media.SolidColorBrush PrimaryColorBrush()
        {
            return new System.Windows.Media.SolidColorBrush(getColor(Primary));
        }
        public System.Windows.Media.SolidColorBrush primaryLightColorBrush()
        {
            return new System.Windows.Media.SolidColorBrush(getColor(PrimaryLight));
        }
        public System.Windows.Media.SolidColorBrush primaryDarkColorBrush()
        {
            return new System.Windows.Media.SolidColorBrush(getColor(PrimaryDark));
        }

        public bool isPrimary(Color c)
        {

            return (c == Primary) ? true : false;
        }
        public bool isPrimaryLight(Color c)
        {

            return (c == PrimaryLight) ? true : false;
        }
        public bool isPrimaryDark(Color c)
        {

            return (c == PrimaryDark) ? true : false;
        }
        public bool isTextColor(Color c)
        {

            return (c == TextColor) ? true : false;
        }


    }
}
