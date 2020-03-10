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
    /// Interaction logic for EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        public EditarUsuario(Personal p)
        {
            InitializeComponent();
            tbLegajo.Text = p.NumeroLegajo;
            tbAcceso.Text = p.NumeroAcceso;
            tbNombre.Text = p.Nombre;
            tbApellido.Text = p.Apellido;
        }

        public string ModifiedLegajo
        {
            get { return tbLegajo.Text; }
        }

        public string ModifiedAcceso
        {
            get { return tbAcceso.Text; }
        }

        public string ModifiedNombre
        {
            get { return tbNombre.Text; }
        }

        public string ModifiedApellido
        {
            get { return tbApellido.Text; }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
