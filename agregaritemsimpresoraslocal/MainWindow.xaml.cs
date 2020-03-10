using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace agregaritemsimpresoraslocal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBContext db;
        IQueryable<ImpresorasINT> intl;
        string RemoteItemImageFile;
        string LocalItemImageFile;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "EDICION BASE DATOS IMPRESORAS 3D" + " / " + Assembly.GetExecutingAssembly().GetName().Version;

            db = new DBContext();
        }

        private void CargarINTs()
        {
            intl = db.ImpresorasINT.Select(s => s);
            dgINTs.ItemsSource = intl.OrderByDescending(o => o.IdINT).ToList();
        }

        private void CargarPiezas()
        {

            var listaPiezas = db.ImpresorasPieza.Select(s => s).ToList();
            var listaINTs = db.ImpresorasINT.Select(s => s.DescripcionINT).ToList();
            cbINTs3.ItemsSource = listaINTs;
            dgPiezas.ItemsSource = listaPiezas.OrderByDescending(o => o.IdPieza);
        }

        private void CargarFallas()
        {
            var listaFallas = db.ImpresorasFalla.Select(s => s).ToList();
            var listaINTs = db.ImpresorasINT.Select(s => s.DescripcionINT).ToList();
            cbINTs2.ItemsSource = listaINTs;
            dgFallas.ItemsSource = listaFallas.OrderByDescending(o => o.IdFalla);
        }

        private void ButtonINTs_Click(object sender, RoutedEventArgs e)
        {
            CargarINTs();
        }

        private void BtnCargarPiezas_Click(object sender, RoutedEventArgs e)
        {
            CargarPiezas();
        }

        private void BtnCargarPiezasImg_Click(object sender, RoutedEventArgs e)
        {
            CargarPiezasImg();
        }

        private void BtnAsignarImg_Click(object sender, RoutedEventArgs e)
        {
            SaveNewImage();
        }

        private void BtnBorrarImg_Click(object sender, RoutedEventArgs e)
        {
            DeleteFileAndDirectory();
        }

        private void BtnCargarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            CargarUsuarios();
        }

        private void BtnQuitarAccesoUsuario_Click(object sender, RoutedEventArgs e)
        {
            QuitarAccesoUsuario();
        }

        private void BtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            EditarUsuario();
        }

        private void CargarUsuarios()
        {
            var listaUsuarios = db.Personal.Select(s => s).ToList();
            dgUsuarios.ItemsSource = listaUsuarios.OrderByDescending(o => o.NumeroLegajo);
        }

        private void QuitarAccesoUsuario()
        {
            if (dgUsuarios.SelectedItem != null)
            {
                var res = MessageBox.Show("Desea impedir acceso al sistema a este usuario?", "Quitar Acceso", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (res == MessageBoxResult.OK)
                {
                    Personal p = (Personal)dgUsuarios.SelectedItem;
                    CargarUsuarios();
                    var usuario = db.Personal.SingleOrDefault(s => s.ID == p.ID);
                    if (usuario != null)
                    {
                        usuario.NumeroAcceso = "-----";
                        db.SaveChanges();
                        CargarUsuarios();
                        MessageBox.Show("Acceso Modificado");
                    }
                }
            }
        }

        private void EditarUsuario()
        {
            if (dgUsuarios.SelectedItem != null)
            {
                Personal p = (Personal)dgUsuarios.SelectedItem;
                CargarUsuarios();
                EditarUsuario eu = new EditarUsuario(p);

                if (eu.ShowDialog() == true)
                {
                    var usuario = db.Personal.SingleOrDefault(s => s.ID == p.ID);
                    if (usuario != null)
                    {
                        usuario.NumeroLegajo = eu.ModifiedLegajo;
                        usuario.NumeroAcceso = eu.ModifiedAcceso;
                        usuario.Nombre = eu.ModifiedNombre;
                        usuario.Apellido = eu.ModifiedApellido;
                        db.SaveChanges();
                        CargarUsuarios();
                        MessageBox.Show("Usuario Modificado");
                    }
                }
            }
        }

        private void DeleteFileAndDirectory()
        {
            if (dgPiezasImg.SelectedItem == null)
            {
                return;
            }

            if (ImageDisplay.Source == null)
            {
                return;
            }

            MessageBoxResult m = MessageBox.Show("Estas seguro lince?", "Borrar Imagen", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (m == MessageBoxResult.OK)
            {
                string local = Directory.GetCurrentDirectory() + @"\itemsimpresoras\";
                string remote = @"\\BUBBA\shared$\itemsimpresoras\";

                try
                {
                    if (File.Exists(RemoteItemImageFile))
                    {
                        File.Delete(Path.Combine(RemoteItemImageFile));

                        if (File.Exists(LocalItemImageFile))
                        {
                            File.Delete(Path.Combine(LocalItemImageFile));
                        }
                    }

                    foreach (var directory in Directory.GetDirectories(local))
                    {
                        if (Directory.GetFiles(directory).Length == 0 &&
                            Directory.GetDirectories(directory).Length == 0)
                        {
                            Directory.Delete(directory, false);
                        }
                    }

                    foreach (var directory in Directory.GetDirectories(remote))
                    {
                        if (Directory.GetFiles(directory).Length == 0 &&
                            Directory.GetDirectories(directory).Length == 0)
                        {
                            Directory.Delete(directory, false);
                        }
                    }

                    MessageBox.Show("Se Borró la imagen");
                    ImageDisplay.Source = null;

                }
                catch (Exception e)
                {

                    MessageBox.Show("Error Eliminando la Imagen" + e.ToString());
                }
            }
        }

        private void ButtonFallas_Click(object sender, RoutedEventArgs e)
        {
            CargarFallas();
        }

        private void BtnAgregarPieza_Click(object sender, RoutedEventArgs e)
        {
            CargarINTs();

            if (!dgPiezas.HasItems)
            {
                CargarPiezas();
            }

            var idi = intl.Where(w => w.DescripcionINT == cbINTs3.SelectedValue.ToString()).Select(s => s).SingleOrDefault();
            db.ImpresorasPieza.Add(new ImpresorasPieza { CodigoPieza = tbCodPieza.Text, DescripcionPieza = tbDescPieza.Text, FK_IdINT = idi.IdINT, FK_IdMarca = idi.FK_IdMarca });
            db.SaveChanges();
            CargarPiezas();
            MessageBox.Show("Registro Agregado");
        }

        private void BtnBorrarPieza_Click(object sender, RoutedEventArgs e)
        {
            if (dgPiezas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Borrar este registro puede causar errores en los programas debido a las referencias en otras tablas, desea continuar?", "BORRAR PIEZA", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (File.Exists(RemoteItemImageFile))
                    {
                        MessageBox.Show("Primero borrá la imagen asociada y despues la pieza");
                        return;
                    }

                    db.ImpresorasPieza.Remove(((ImpresorasPieza)dgPiezas.SelectedItem));
                    db.SaveChanges();
                    CargarPiezas();
                    MessageBox.Show("Registro Borrado");

                }
            }
        }

        private void BtnEditarPieza_Click(object sender, RoutedEventArgs e)
        {
            if (dgPiezas.SelectedItem != null)
            {
                ImpresorasPieza pi = (ImpresorasPieza)dgPiezas.SelectedItem;
                CargarINTs();
                EditarPieza ep = new EditarPieza(intl, pi);
                if (ep.ShowDialog() == true)
                {
                    var pieza = db.ImpresorasPieza.SingleOrDefault(s => s.IdPieza == pi.IdPieza);
                    if (pieza != null)
                    {
                        pieza.FK_IdINT = ep.ModifiedINT;
                        pieza.DescripcionPieza = ep.ModifiedValue;
                        pieza.CodigoPieza = ep.ModifiedCode;
                        db.SaveChanges();
                        CargarPiezas();
                        MessageBox.Show("Registro Modificado");
                    }
                }
            }
        }

        private void BtnAgregarFalla_Click(object sender, RoutedEventArgs e)
        {
            int idi = intl.Where(w => w.DescripcionINT == cbINTs2.SelectedValue.ToString()).Select(s => s.IdINT).SingleOrDefault();
            db.ImpresorasFalla.Add(new ImpresorasFalla { FK_IdINT = idi, DescripcionFalla = tbDescFalla.Text });
            db.SaveChanges();
            CargarFallas();
            MessageBox.Show("Registro Agregado");
        }

        private void BtnBorrarFalla_Click(object sender, RoutedEventArgs e)
        {
            if (dgFallas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Borrar este registro puede causar errores en los programas debido a las referencias en otras tablas, desea continuar?", "BORRAR FALLA", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    db.ImpresorasFalla.Remove(((ImpresorasFalla)dgFallas.SelectedItem));
                    db.SaveChanges();
                    CargarFallas();
                    MessageBox.Show("Registro Borrado");
                }

            }
        }

        private void BtnEditarFalla_Click(object sender, RoutedEventArgs e)
        {
            if (dgFallas.SelectedItem != null)
            {
                ImpresorasFalla f = (ImpresorasFalla)dgFallas.SelectedItem;
                CargarINTs();
                EditarFalla ef = new EditarFalla(intl, f);

                if (ef.ShowDialog() == true)
                {
                    var fa = db.ImpresorasFalla.SingleOrDefault(s => s.IdFalla == f.IdFalla);
                    if (fa != null)
                    {
                        fa.DescripcionFalla = ef.ModifiedValue;
                        fa.FK_IdINT = ef.ModifiedINT;
                        db.SaveChanges();
                        CargarFallas();
                        MessageBox.Show("Registro Modificado");
                    }
                }
            }
        }

        private void BtnAgregarINT_Click(object sender, RoutedEventArgs e)
        {
            if (intl == null)
            {
                MessageBox.Show("Carga los INTs primero");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbINT.Text))
            {
                return;
            }
            if (cbMarcas.SelectedItem == null)
            {
                MessageBox.Show("Selecciona la Marca/Modelo del INT primero");
                return;
            }
            var pepeid = cbMarcas.SelectedItem.ToString();
            var idmarc = db.ImpresorasMarca.Where(w => w.Marca == pepeid).Select(s => s.IdMarca).FirstOrDefault();
            db.ImpresorasINT.Add(new ImpresorasINT { FK_IdMarca = idmarc, DescripcionINT = tbINT.Text.ToUpper() });
            db.SaveChanges();
            CargarINTs();
            MessageBox.Show("Registro Agregado");
        }

        private void BtnBorrarINT_Click(object sender, RoutedEventArgs e)
        {
            if (dgINTs.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Borrar este registro puede causar errores en los programas debido a las referencias en otras tablas, desea continuar?", "BORRAR INT", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    db.ImpresorasINT.Remove(((ImpresorasINT)dgINTs.SelectedItem));
                    db.SaveChanges();
                    CargarINTs();
                    MessageBox.Show("Registro Borrado");
                }
            }
        }

        private void BtnEditarINT_Click(object sender, RoutedEventArgs e)
        {
            if (dgINTs.SelectedItem != null)
            {
                ImpresorasINT i = (ImpresorasINT)dgINTs.SelectedItem;
                EditarINT ei = new EditarINT(i.DescripcionINT);
                if (ei.ShowDialog() == true)
                {
                    var intc = db.ImpresorasINT.SingleOrDefault(s => s.IdINT == i.IdINT);
                    if (intc != null)
                    {
                        intc.DescripcionINT = ei.ModifiedValue;
                        db.SaveChanges();
                        CargarINTs();
                        MessageBox.Show("Registro Modificado");
                    }
                }
            }
        }

        private void DgPiezas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgPiezas.SelectedItem != null)
            {
                ImpresorasPieza iff = (ImpresorasPieza)dgPiezas.SelectedItem;
                cbINTs3.SelectedValue = db.ImpresorasINT.Where(w => w.IdINT == iff.FK_IdINT).Select(s => s.DescripcionINT).SingleOrDefault();
            }
        }

        private void DgFallas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgFallas.SelectedItem != null)
            {
                ImpresorasFalla iff = (ImpresorasFalla)dgFallas.SelectedItem;
                cbINTs2.SelectedValue = db.ImpresorasINT.Where(w => w.IdINT == iff.FK_IdINT).Select(s => s.DescripcionINT).SingleOrDefault();
            }
        }

        private void DgCantidad_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                dgInforme.SelectedItems.Clear();
                Unic su = (Unic)dgCantidad.SelectedItem;
                string cellValue = su.Codigo;
                var listaseleccionada = db.ImpresorasCambioView.Where(w => w.CodigoPieza == cellValue).Select(s => s);

                foreach (var v in listaseleccionada)
                {
                    dgInforme.SelectedItems.Add(v);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            dgInforme.Focus();
        }

        private void CargarPiezasImg()
        {
            var listaPiezas = db.ImpresorasPieza.Select(s => s).ToList();
            dgPiezasImg.ItemsSource = listaPiezas.OrderByDescending(o => o.IdPieza);
            //this.dgPiezasImg.Columns[2].Visibility = Visibility.Collapsed;

        }

        private void SaveNewImage()
        {
            if (dgPiezasImg.SelectedItem == null)
            {
                return;
            }

            string UserImageFile;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Sólo Archivos JPG (*.jpg, *.jpg)|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                UserImageFile = dlg.FileName;
                var bitmapFrame = BitmapFrame.Create(new Uri(UserImageFile), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                int width = bitmapFrame.PixelWidth;
                int height = bitmapFrame.PixelHeight;
                if (width > 960)
                {
                    System.Windows.MessageBox.Show("utilice un tamaño de imagen moderado, e.j. 720x480", "imagen demasiado grande, considere reducir las medidas", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                if (height > 480)
                {
                    System.Windows.MessageBox.Show("utilice un tamaño de imagen moderado, e.j. 720x480", "imagen demasiado grande, considere reducir las medidas", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

                try
                {
                    if (dgPiezasImg.SelectedItem != null)
                    {
                        //MessageBox.Show(NewItemsImageFile);
                        //return;
                        Directory.CreateDirectory(Path.GetDirectoryName(RemoteItemImageFile));
                        Directory.CreateDirectory(Path.GetDirectoryName(LocalItemImageFile));
                        File.Copy(UserImageFile, RemoteItemImageFile, true);
                        File.Copy(UserImageFile, LocalItemImageFile, true);
                        System.Windows.MessageBox.Show("La imagen se asignó correctamente!", "Guardar imagen", MessageBoxButton.OK, MessageBoxImage.Warning);
                        tblNADACHE.Content = "";
                        DisplayTehImage();
                    }
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Ocurrió un error al guardar la imagen, el nombre de archivo, o que esta en uso!....", "Guardar", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
        }

        private void DgPiezasImg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                ImpresorasPieza p = (ImpresorasPieza)dgPiezasImg.SelectedItem;
                ImpresorasMarca m = db.ImpresorasMarca.Where(w => w.IdMarca == p.FK_IdMarca).Select(s => s).SingleOrDefault();
                ImpresorasINT i = db.ImpresorasINT.Where(w => w.IdINT == p.FK_IdINT).Select(s => s).SingleOrDefault();
                LocalItemImageFile = Path.Combine(Directory.GetCurrentDirectory(), @"itemsimpresoras\", m.Marca, i.DescripcionINT, p.CodigoPieza, p.IdPieza + ".JPG").ToUpper();
                RemoteItemImageFile = Path.Combine(@"\\BUBBA\shared$\itemsimpresoras\", m.Marca, i.DescripcionINT, p.CodigoPieza, p.IdPieza + ".JPG").ToUpper();
                lblbSelectedItemINT.Content = m.Marca + " > " + i.DescripcionINT;
                if (File.Exists(RemoteItemImageFile))
                {

                    if (File.Exists(LocalItemImageFile))
                    {
                        tblNADACHE.Content = "";
                        DisplayTehImage();

                    }
                    else
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(LocalItemImageFile));
                        File.Copy(RemoteItemImageFile, LocalItemImageFile, true);
                        tblNADACHE.Content = "";
                        DisplayTehImage();
                    }
                }
                else
                {
                    ImageDisplay.Source = null;
                    tblNADACHE.Content = "ESTA PIEZA NO TIENE IMAGEN ASIGNADA!";
                }
            }
            catch (Exception)
            {

                return;
            }

        }

        private void DisplayTehImage()
        {
            string uniqueFileName = $@"{Guid.NewGuid()}.JPG";
            string tmpFile = Path.Combine(Path.GetTempPath(), uniqueFileName);
            File.Copy(LocalItemImageFile, tmpFile, true);

            Bitmap bmp = new Bitmap(tmpFile);
            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
            bitmapImage.EndInit();
            ImageDisplay.Source = bitmapImage;
        }

        private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("USESE BAJO PROPIA RESPONSABILIDAD. LAS ACCIONES INDISCRIMINADAS PUEDEN DAÑAR LA BASE DE DATOS, Y NO ME HAGO RESPONSABLE :P, AGUA Y AJO");
        }

        private void BtnCargarCambios_Click(object sender, RoutedEventArgs e)
        {
            CargarCambiosFull();
        }

        private void CargarCambiosFull()
        {
            dgInforme.ItemsSource = db.ImpresorasCambioView.Select(s => s).ToList();
            this.dgInforme.Columns[0].MaxWidth = 72; //id
            this.dgInforme.Columns[1].MaxWidth = 90; //legajo
            this.dgInforme.Columns[2].MaxWidth = 120; //apellido
            this.dgInforme.Columns[3].MaxWidth = 96; //nombre
            this.dgInforme.Columns[4].MaxWidth = 90; //marca
            this.dgInforme.Columns[5].MaxWidth = 200; //desc. int
            this.dgInforme.Columns[6].MaxWidth = 80; //cod. pieza
            this.dgInforme.Columns[7].MaxWidth = 340; //desc. pieza
            this.dgInforme.Columns[8].MinWidth = 140; //desc. falla
            this.dgInforme.Columns[8].MaxWidth = 200; //fecha cambio
            this.dgInforme.Columns[9].MaxWidth = 140; //obs
            MostrarCantidad();
        }

        private void BtnCargarCambiosReducidos_Click(object sender, RoutedEventArgs e)
        {
            dgInforme.ItemsSource = db.ImpresorasCambioReducidoView.Select(s => s).ToList();
            this.dgInforme.Columns[0].MaxWidth = 200; //fecha
            this.dgInforme.Columns[1].MaxWidth = 200; //desc. int
            this.dgInforme.Columns[2].MaxWidth = 80; //cod. pieza
            this.dgInforme.Columns[3].MaxWidth = 340; //desc. pieza
            this.dgInforme.Columns[4].MinWidth = 140; //desc. falla
            this.dgInforme.Columns[4].MaxWidth = 340; //desc. falla
            MostrarCantidad();
        }

        private void MostrarCantidad()
        {

            var uniq = db.ImpresorasCambioReducidoView.Select(s => s).ToList();
            var counts = uniq.GroupBy(g => g.CodigoPieza).Select(s => new Unic { Codigo = s.Key, Total = s.Select(se => se.CodigoPieza).Count() });
            dgCantidad.ItemsSource = counts;
            this.dgCantidad.Columns[0].MinWidth = 60; //pieza
        }

        private class Unic
        {
            public string Codigo { get; set; }
            public int Total { get; set; }
        }

        private void BtnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            dgInforme.SelectAll();
            dgInforme.Focus();

        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (dgInforme.SelectedItems == null)
            {
                return;
            }

            ApplicationCommands.Copy.Execute(null, dgInforme);
        }

        private void BtnBorrarRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (dgInforme.SelectedItems.Count == 1)
            {
                MessageBoxResult mr = MessageBox.Show("Seguro desea borrar el registro?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (mr == MessageBoxResult.Yes)
                {
                    if (dgInforme.SelectedItem.GetType() == typeof(ImpresorasCambioView))
                    {
                        ImpresorasCambioView icv = (ImpresorasCambioView)dgInforme.SelectedItem;
                        ImpresorasCambio ic = db.ImpresorasCambio.Where(w => w.IdCambio == icv.IdCambio).SingleOrDefault();
                        db.ImpresorasCambio.Remove(ic);
                        db.SaveChanges();
                        CargarCambiosFull();
                        MessageBox.Show("Registro Borrado", "Borrado mister", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Borre registros solo desde la la lista de Cambios FULL");
                    }
                }
            }
        }

        private void BtnGuardarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLegajo.Text))
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbAcceso.Text))
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbNombre.Text))
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbApellido.Text))
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (tbLegajo.Text == "Legajo...")
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (tbAcceso.Text == "Nro Acceso...")
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (tbNombre.Text == "Nombre...")
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            if (tbApellido.Text == "Apellido...")
            {
                MessageBox.Show("Ingrese todos los campos");
                return;
            }

            try
            {
                Personal p = new Personal { NumeroLegajo = tbLegajo.Text, NumeroAcceso = tbAcceso.Text, Nombre = tbNombre.Text, Apellido = tbApellido.Text, Celular = null, DNI = null, Domicilio = null, email = null, FechaIngreso = null, Sector = null, SegundoNombre = null, Telefono = null, Observaciones = null };
                DBContext c = new DBContext();
                c.Personal.Add(p);
                c.SaveChanges();
                CargarUsuarios();
                MessageBox.Show("Usuario Creado");
            }
            catch (Exception)
            {
                MessageBox.Show("Alguno de los datos no es válido para la creación del usuario."); //
            }
        }

        //

        private void dgMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Marcas_SelectionChanged();
        }

        private void btnAgregarMarca_Click(object sender, RoutedEventArgs e)
        {
            AgregarMarca();
        }

        private void btnBorrarMarca_Click(object sender, RoutedEventArgs e)
        {
            BorrarMarca();
        }

        private void btnEditarMarca_Click(object sender, RoutedEventArgs e)
        {
            EditarMarcas();

        }

        private void btnCargarMarcas_Click(object sender, RoutedEventArgs e)
        {
            CargarMarcas();
        }

        private void AgregarMarca()
        {
            db.ImpresorasMarca.Add(new ImpresorasMarca { Marca = tbDescMarca.Text });
            db.SaveChanges();
            CargarMarcas();
            MessageBox.Show("Registro Agregado");
        }

        private void BorrarMarca()
        {
            if (dgMarcas.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Borrar este registro puede causar errores en los programas debido a las referencias en otras tablas, desea continuar?", "BORRAR MARCA", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    db.ImpresorasMarca.Remove(((ImpresorasMarca)dgMarcas.SelectedItem));
                    db.SaveChanges();
                    CargarMarcas();
                    MessageBox.Show("Registro Borrado");
                }

            }
        }

        private void Marcas_SelectionChanged()
        {
            //if (dgMarcas.SelectedItem != null)
            //{
            //    ImpresorasMarca iff = (ImpresorasMarca)dgMarcas.SelectedItem;
                //cbMarcas.SelectedValue = db.ImpresorasINT.Where(w => w.IdINT == iff.FK_IdINT).Select(s => s.DescripcionINT).SingleOrDefault();
            //}
        }

        private void CargarMarcas()
        {
            var listaMarcas = db.ImpresorasMarca.Select(s => s).ToList();
            dgMarcas.ItemsSource = listaMarcas;
        }

        private void EditarMarcas()
        {
            if (dgMarcas.SelectedItem != null)
            {
                ImpresorasMarca i = (ImpresorasMarca)dgMarcas.SelectedItem;
                EditarMarca ei = new EditarMarca(i.Marca);
                if (ei.ShowDialog() == true)
                {
                    var intc = db.ImpresorasMarca.SingleOrDefault(s => s.IdMarca == i.IdMarca);
                    if (intc != null)
                    {
                        intc.Marca = ei.ModifiedValue;
                        db.SaveChanges();
                        CargarMarcas();
                        MessageBox.Show("Registro Modificado");
                    }
                }
            }
        }

        #region modelos
        //
        private void dgModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModeloSelectionChanged();
        }

        private void btnAgregarModelo_Click(object sender, RoutedEventArgs e)
        {
            AgregarModelo();
        }

        private void btnBorrarModelo_Click(object sender, RoutedEventArgs e)
        {
            BorrarModelo();
        }

        private void btnEditarModelo_Click(object sender, RoutedEventArgs e)
        {
            EditarModelo();

        }

        private void btnCargarModelos_Click(object sender, RoutedEventArgs e)
        {
            CargarModelos();
        }

        //

        private void ModeloSelectionChanged()
        {

        }
        private void CargarModelos()
        {
            /////
        }

        private void AgregarModelo()
        {
            /////
        }

        private void EditarModelo()
        {
            /////
        }

        private void BorrarModelo()
        {
            /////
        }

        #endregion


        private void cbMarcas_DropDownOpened(object sender, EventArgs e)
        {
            var pepe = db.ImpresorasMarca.Select(s => s.Marca).ToList();
            cbMarcas.ItemsSource = pepe;
        }
    }
}
