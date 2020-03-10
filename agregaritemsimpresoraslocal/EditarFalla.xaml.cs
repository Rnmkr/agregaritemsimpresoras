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
    /// Interaction logic for EditarFalla.xaml
    /// </summary>
    public partial class EditarFalla : Window
    {
        IQueryable<ImpresorasINT> intl;

        public EditarFalla(IQueryable<ImpresorasINT> intlist, ImpresorasFalla f)
        {
            InitializeComponent();
            tbDescripcion.Text = f.DescripcionFalla;
            cbINT.SelectedValue = intlist.Where(w => w.IdINT == f.FK_IdINT).Select(s => s.DescripcionINT).SingleOrDefault();
            intl = intlist;
            var papa = intl.Select(s=>s.DescripcionINT).Distinct().ToList();
            cbINT.ItemsSource = papa;
        }

        public string ModifiedValue
        {
            get { return tbDescripcion.Text; }
        }

        public int ModifiedINT
        {
            get
            {
                int mi = intl.Where(w => w.DescripcionINT == cbINT.SelectedValue.ToString()).Select(s => s.IdINT).SingleOrDefault();
                return mi;
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
