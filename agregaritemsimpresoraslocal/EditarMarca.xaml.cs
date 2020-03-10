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

namespace agregaritemsimpresoraslocal
{
    /// <summary>
    /// Interaction logic for EditarMarca.xaml
    /// </summary>
    public partial class EditarMarca : Window
    {
        public EditarMarca(string desint)
        {
            InitializeComponent();
            tbEdit.Text = desint;
        }

        public string ModifiedValue
        {
            get { return tbEdit.Text; }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
