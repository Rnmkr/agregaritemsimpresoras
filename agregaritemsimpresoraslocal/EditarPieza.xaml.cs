using System.Linq;
using System.Windows;

namespace agregaritemsimpresoraslocal
{
    /// <summary>
    /// Interaction logic for EditarPieza.xaml
    /// </summary>
    public partial class EditarPieza : Window
    {
        IQueryable<ImpresorasINT> ints;

        public EditarPieza(IQueryable<ImpresorasINT> i, ImpresorasPieza p)
        {
            InitializeComponent();
            tbCodigo.Text = p.CodigoPieza;
            tbDescripcion.Text = p.DescripcionPieza;
            this.ints = i;
            cbINTPieza.ItemsSource = ints.Select(s => s.DescripcionINT).Distinct().ToList();
            string idpint = ints.Where(w => w.IdINT == p.FK_IdINT).Select(s => s.DescripcionINT).SingleOrDefault();
            cbINTPieza.SelectedItem = idpint;
        }

        public string ModifiedValue
        {
            get { return tbDescripcion.Text; }
        }

        public string ModifiedCode
        {
            get { return tbCodigo.Text; }
        }

        public int ModifiedINT
        {
            get
            {
                int rint = ints.Where(w => w.DescripcionINT == cbINTPieza.SelectedValue.ToString()).Select(s => s.IdINT).SingleOrDefault();
                return rint;
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
