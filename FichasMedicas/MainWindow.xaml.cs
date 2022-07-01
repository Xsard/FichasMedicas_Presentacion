using Conexion;
using Datos;
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
using System.Windows.Threading;
    
namespace FichasMedicas
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            Llenar();
            txtNumeroFicha.Focus();
            txtFechaBit.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        List<Prevision> previsiones;
        List<MAC> Metodos;
        List<Extras> TRH;
        List<Extras> RS;
        private int cambios = 0;
        private string rutEnUso = "";
        private int nfichaEnUso = 0;
        private void Llenar()
        {
            previsiones = PrevisionController.SelectPrevision();
            Metodos = MACController.SelectMetodos();
            TRH = ExtrasController.SelectTRH();
            RS = ExtrasController.SelectRS();
            
            cboPrevision.ItemsSource = previsiones;
            cboMAC.ItemsSource = Metodos;
            cboTRH.ItemsSource = TRH;
            cboRs.ItemsSource = RS;
        }

        private void btnGrdCam_Click(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text.Trim().Length == 0 || txtNumeroFicha.Text.Trim().Length == 0)
            {
                MessageBox.Show("El Rut y el número de ficha son campos obligatorios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string nRut = txtRut.Text.Split('-').First();
                string dvRut = txtRut.Text.Split('-').Last();
                if (Regla.ValidaRut(nRut, dvRut))
                {
                    if (int.TryParse(txtNumeroFicha.Text, out int nFicha))
                    {
                        int fono = 0;
                        if (int.TryParse(txtFono.Text, out int f)) fono = f;
                        Prevision prevision = (Prevision)cboPrevision.SelectedItem;
                        Persona persona = new()
                        {
                            Rut = txtRut.Text,
                            PNombre = txtNombre1.Text,
                            SNombre = txtNombre2.Text,
                            APaterno = txtAPaterno.Text,
                            AMaterno = txtAMaterno.Text,
                            Direccion = txtDireccion.Text,
                            Telofono = fono,
                            Prevision = prevision.Id_Prev,
                            FechaNac = txtFecha.Text
                        };
                        if (!int.TryParse(txtGesta.Text, out int Gs))
                        {
                            Gs = -1;
                        }
                        if (!int.TryParse(txtPV.Text, out int Pv))
                        {
                            Pv = -1;
                        }
                        if (!int.TryParse(txtParTotal.Text, out int PT))
                        {
                            PT = -1;
                        }
                        if (!int.TryParse(txtParPrematuros.Text, out int PP))
                        {
                            PP = -1;
                        }
                        if (!int.TryParse(txtParAbortos.Text, out int PM))
                        {
                            PM = -1;
                        }
                        if (!int.TryParse(txtParVivos.Text, out int PV))
                        {
                            PV = -1;
                        }
                        if (!int.TryParse(txtCantCesarea.Text, out int CC))
                        {
                            CC = -1;
                        }
                        if (!int.TryParse(txtCantMeno.Text, out int CM))
                        {
                            CM = -1;
                        }
                        Extras trh = (Extras)cboTRH.SelectedItem;
                        Extras rs = (Extras)cboRs.SelectedItem;
                        FichaMedica fichaMedica = new()
                        {
                            NFicha = nFicha,
                            Gesta = Gs,
                            Pv = Pv,
                            Rs = rs.Id,
                            Metodo = (MAC)cboMAC.SelectedItem,
                            Thr = trh.Id,
                            Observacion = txtObs.Text,
                            FecUpdate = DateTime.Now,
                            PartosT = PT,
                            PartosP = PP,
                            PartosM = PM,
                            PartosV = PV
                        };
                        PatologiasMed patologiasMed = new()
                        {
                            Dm = Convert.ToInt32(cbDm.IsChecked.Value),
                            Dismenorrea = Convert.ToInt32(cbDis.IsChecked.Value),
                            Epilepsia = Convert.ToInt32(cbEpilepsia.IsChecked.Value),
                            Hiper = Convert.ToInt32(cbHiper.IsChecked.Value),
                            Hta = Convert.ToInt32(cbHta.IsChecked.Value),
                            Itu = Convert.ToInt32(cbItu.IsChecked.Value),
                            Nie = Convert.ToInt32(cbNie.IsChecked.Value),
                            Jaqueca = Convert.ToInt32(cbJaqueca.IsChecked.Value),
                            Mfqb = Convert.ToInt32(cbMFQB.IsChecked.Value),
                            Sop = Convert.ToInt32(cbSop.IsChecked.Value),
                            Vaginitis = Convert.ToInt32(cbVaginitis.IsChecked.Value),
                            Alergia = Convert.ToInt32(cbAlergia.IsChecked.Value),
                            Rh = Convert.ToInt32(cbRh.IsChecked.Value),
                            No = Convert.ToInt32(cbNoPM.IsChecked.Value),
                            NFicha = Convert.ToInt32(txtNumeroFicha.Text),
                            Menopausia = Convert.ToInt32(cbMeno.IsChecked.Value),
                            CantMeno = CM
                        };
                        PatoloagiaQui patoloagiaQui = new()
                        {
                            Apendictomia = Convert.ToInt32(cbApendicec.IsChecked.Value),
                            Ca = Convert.ToInt32(cbCA.IsChecked.Value),
                            Cesarea = Convert.ToInt32(cbCesarea.IsChecked.Value),
                            Cant_cesarea = CC,
                            Colcistectomia = Convert.ToInt32(cbColcistectomia.IsChecked.Value),
                            Conizacion = Convert.ToInt32(cbConizacion.IsChecked.Value),
                            Esterilizacion = Convert.ToInt32(cbEsterilizacion.IsChecked.Value),
                            Salpingectomia = Convert.ToInt32(cbSalpin.IsChecked.Value),
                            Crioterapia = Convert.ToInt32(cbCrio.IsChecked.Value),
                            Hat = Convert.ToInt32(cbHat.IsChecked.Value),
                            HerInguinal = Convert.ToInt32(cbHerIn.IsChecked.Value),
                            HerUmb = Convert.ToInt32(cbHerUm.IsChecked.Value),
                            Ooforectomia = Convert.ToInt32(cbOoferectomia.IsChecked.Value),
                            Quistectomia = Convert.ToInt32(cbQuis.IsChecked.Value),
                            No = Convert.ToInt32(cbNoPQ.IsChecked.Value),
                            NFicha = Convert.ToInt32(txtNumeroFicha.Text)
                        };
                        txtNumeroFicha.Focus();
                        txtNumeroFicha.SelectionStart = txtNumeroFicha.Text.Length;
                        txtNumeroFicha.SelectionLength = 0;
                        bool aux = true;
                        if (nfichaEnUso != nFicha && nfichaEnUso > 0)
                        {
                            aux = false;
                        }
                        if (FichaController.Existe(nFicha) == 0 && aux)
                        {
                            if (PersonaController.InsertPersona(persona) > 0)
                            {
                                if (FichaController.InsertFicha(fichaMedica, txtRut.Text) > 0)
                                {
                                    if (FichaController.InsertFichaMed(patologiasMed) > 0)
                                    {
                                        if (FichaController.InsertFichaQui(patoloagiaQui) > 0)
                                        {
                                            InsertPmExtra();
                                            InsertPqExtra();
                                            dhInserted.IsOpen = true;
                                            txtGuardado.Text = "GUARDADO";
                                            if (!txtFechaBit.Text.Trim().Equals("", StringComparison.Ordinal) && !txtDescBit.Text.Trim().Equals("", StringComparison.Ordinal))
                                            {
                                                if (DateTime.TryParse(txtFechaBit.Text, out DateTime fechaBit))
                                                {
                                                    Bitacora bitacora = new()
                                                    {
                                                        FechaBitacora = fechaBit,
                                                        NFicha = int.Parse(txtNumeroFicha.Text),
                                                        Nom = txtNomBit.Text,
                                                        Descripcion = txtDescBit.Text
                                                    };
                                                    BitacoraController.InsertBitacora(bitacora);
                                                    try
                                                    {
                                                        List<Bitacora> bitacoras = BitacoraController.Bitacoras(int.Parse(txtNumeroFicha.Text));
                                                        ListaBitacora(bitacoras);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        return;
                                                    }

                                                }
                                                else
                                                {
                                                    MessageBox.Show("Formato de fecha no válido para la bitácora", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                                                }
                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("La ficha no ha sido generada, ha ocurrido un error al añadir las patologías quirúrgicas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            txtGuardado.Text = "NO GUARDADO";
                                            PersonaController.DeletePersona(persona.Rut);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("La ficha no ha sido generada, ha ocurrido un error al añadir las patologías médicas y quirúrgicas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                                        PersonaController.DeletePersona(persona.Rut);
                                        txtGuardado.Text = "NO GUARDADO";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("La ficha no ha sido creada, revise que el número de la ficha no se encuentre ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    PersonaController.DeletePersona(persona.Rut);
                                }
                            }
                            else
                            {
                                MessageBox.Show("La ficha no ha sido creada, revise que el Rut o Pasaporte no se encuentren ingresados", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                PersonaController.DeletePersona(persona.Rut);
                            }
                        }
                        else if (FichaController.Existe(nfichaEnUso) > 0)
                        {

                            if (txtRut.Text != rutEnUso || nfichaEnUso != nFicha)
                            {
                                if (PersonaController.Existe(txtRut.Text) && txtRut.Text != rutEnUso)
                                {
                                    MessageBox.Show("El nuevo rut ingresado ya está en uso");
                                    return;
                                }
                                if (FichaController.Existe(nFicha) > 0 && nfichaEnUso != nFicha)
                                {
                                    MessageBox.Show("El nuevo número ingresado ya está en uso en otra ficha");
                                    return;
                                }
                            }

                            if (cambios == 1)
                            {
                                
                                if (PersonaController.ActualizarPersona(persona, rutEnUso) > 0)
                                {
                                    if (FichaController.ActualizarFicha(fichaMedica, nfichaEnUso) > 0)
                                    {
                                        FichaController.ActualizarFichaMed(patologiasMed);
                                        FichaController.ActualizarFichaQui(patoloagiaQui);
                                        InsertPmExtra();
                                        UpdatePmExtras();
                                        DeletePmExtras();
                                        InsertPqExtra();
                                        UpdatePQExtras();
                                        DeletePqExtras();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se ha podido llevar a cabo la actualización, revise los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No se ha podido llevar a cabo la actualización, revise los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            txtGuardado.Text = "GUARDADO";
                            if (!txtFechaBit.Text.Trim().Equals("", StringComparison.Ordinal) && !txtDescBit.Text.Trim().Equals("", StringComparison.Ordinal))
                            {
                                if (DateTime.TryParse(txtFechaBit.Text, out DateTime fechaBit))
                                {
                                    Bitacora bitacora = new()
                                    {
                                        FechaBitacora = fechaBit,
                                        NFicha = int.Parse(txtNumeroFicha.Text),
                                        Nom = txtNomBit.Text,
                                        Descripcion = txtDescBit.Text
                                    };
                                    int bit = BitacoraController.InsertBitacora(bitacora);
                                    if (bit > 0)
                                    {
                                        txtNomBit.Text = string.Empty;
                                        txtFechaBit.Text = DateTime.Today.ToString("dd/MM/yyyy");
                                        txtDescBit.Text = string.Empty;
                                    }
                                    dhUpdated.IsOpen = true;
                                    try
                                    {
                                        List<Bitacora> bitacoras = BitacoraController.Bitacoras(int.Parse(txtNumeroFicha.Text));
                                        ListaBitacora(bitacoras);
                                    }
                                    catch (Exception)
                                    {
                                        return;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Formato de fecha no válido para la bitácora", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }

                            }
                            else
                            {
                                dhUpdated.IsOpen = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error mientras se ingresaba la ficha, revise los datos, de persistir el percance debe consultar al desarrollador", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            PersonaController.DeletePersona(persona.Rut);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El número de ficha ingresado no es válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El Rut ingresado no es válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                cambios = 0;

            }
        }

        private void txtNumeroFicha_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtGuardado.Text.Equals("NO GUARDADO"))
                {
                    var resultado = MessageBox.Show("¿Está seguro que desea búscar? Tenga en cuenta que los campos llenados actualmente serán modificados", "Confirmación", MessageBoxButton.YesNo);
                    if (resultado == MessageBoxResult.No)
                    {
                        return;
                    }

                }

                if (int.TryParse(txtNumeroFicha.Text, out int numero))
                {
                    FichaMedica fichaMedica = FichaController.SelectFicha(numero);
                    if (fichaMedica != null)
                    {
                        Limpiar();
                        txtGuardado.Text = "GUARDADO";
                        txtRut.Text = fichaMedica.Persona.Rut;
                        BuscarPaciente();
                        txtNombre1.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El número de ficha ingresado no ha sido encontrado");
                    }
                }
            }

        }
        private void txtRut_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                if (txtGuardado.Text.Equals("NO GUARDADO"))
                {
                    var resultado = MessageBox.Show("¿Está seguro que desea búscar? Tenga en cuenta que los campos llenados actualmente serán modificados", "Confirmación", MessageBoxButton.YesNo);
                    if (resultado == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        Limpiar();
                        BuscarPaciente();
                        txtNombre1.Focus();
                    }
                }
                else
                {
                    BuscarPaciente();
                }
            }

        }
        private void txtNombre1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtGuardado.Text.Equals("NO GUARDADO"))
                {
                    var resultado = MessageBox.Show("¿Está seguro que desea búscar? Tenga en cuenta que los campos llenados actualmente serán modificados", "Confirmación", MessageBoxButton.YesNo);
                    if (resultado == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (txtNombre1.Text.Length > 0 || txtNombre2.Text.Length > 0 || txtAPaterno.Text.Length > 0 || txtAMaterno.Text.Length > 0)
                {
                    try
                    {
                        List<FichaMedica> fichaMedicas = PersonaController.SelectFicha(txtNombre1.Text, txtNombre2.Text, txtAPaterno.Text, txtAMaterno.Text);
                        FichaMedica fichaMedica = fichaMedicas[0];
                        if (fichaMedica != null)
                        {
                            Limpiar();
                            txtRut.Text = fichaMedica.Persona.Rut;
                            BuscarPaciente();
                            txtNombre1.Focus();
                        }
                        if (fichaMedicas.Count > 1) 
                        {
                            dhPaciente.IsOpen = true;

                            dtgPacientes.Items.Clear();
                            foreach (var item in fichaMedicas)
                            {
                                dtgPacientes.Items.Add(item);
                            }
                            e.Handled = true;
                        }

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("El paciente no ha sido encontrado");
                    }
                    
                }
            }
        }
        
        private void BuscarPaciente()
        {

            FichaMedica fichaMedica = FichaController.SelectFicha(txtRut.Text);
            if (fichaMedica != null)
            {
                rutEnUso = fichaMedica.Persona.Rut;
                nfichaEnUso = fichaMedica.NFicha;
                txtNombre1.Text = fichaMedica.Persona.PNombre;
                txtNombre2.Text = fichaMedica.Persona.SNombre;
                txtAPaterno.Text = fichaMedica.Persona.APaterno;
                txtAMaterno.Text = fichaMedica.Persona.AMaterno;
                txtRut.Text = fichaMedica.Persona.Rut;
                txtDireccion.Text = fichaMedica.Persona.Direccion;
                txtFecha.Text = fichaMedica.Persona.FechaNac;
                try
                {
                    if (DateTime.TryParse(fichaMedica.Persona.FechaNac, out DateTime Fecha))
                    {
                        DateTime zeroTime = new DateTime(1, 1, 1);

                        DateTime a = Fecha;
                        DateTime b = DateTime.Today;

                        TimeSpan span = b - a;
                        // Because we start at year 1 for the Gregorian
                        // calendar, we must subtract a year here.
                        int years = (zeroTime + span).Year - 1;

                        txtEdad.Text = years.ToString();
                    }
                }
                catch (Exception)
                {

                    txtEdad.Text = "N/A";
                }
                cboPrevision.SelectedIndex = fichaMedica.Persona.Prevision;
                txtFono.Text = fichaMedica.Persona.Telofono.ToString();
                txtNumeroFicha.Text = fichaMedica.NFicha.ToString();
                if (fichaMedica.Gesta > -1) txtGesta.Text = fichaMedica.Gesta.ToString();
                if (fichaMedica.Pv > -1) txtPV.Text = fichaMedica.Pv.ToString();
                cboRs.SelectedIndex = fichaMedica.Rs;
                cboMAC.SelectedIndex = fichaMedica.Metodo.Id_Mac;
                cboTRH.SelectedIndex = fichaMedica.Thr;
                txtObs.Text = fichaMedica.Observacion;
                if (fichaMedica.PartosT > -1) txtParTotal.Text = fichaMedica.PartosT.ToString();
                if (fichaMedica.PartosP > -1) txtParPrematuros.Text = fichaMedica.PartosP.ToString();
                if (fichaMedica.PartosM > -1) txtParAbortos.Text = fichaMedica.PartosM.ToString();
                if (fichaMedica.PartosV > -1) txtParVivos.Text = fichaMedica.PartosV.ToString();
                cbDm.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Dm);
                cbDis.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Dismenorrea);
                cbEpilepsia.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Epilepsia);
                cbHiper.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Hiper);
                cbItu.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Itu);
                cbNie.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Nie);
                cbJaqueca.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Jaqueca);
                cbMFQB.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Mfqb);
                cbHta.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Hta);
                cbSop.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Sop);
                cbVaginitis.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Vaginitis);
                cbAlergia.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Alergia);
                cbRh.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Rh);
                cbApendicec.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Apendictomia);
                cbCA.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Ca);
                cbCesarea.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Cesarea);
                if (fichaMedica.PatoloagiaQui.Cant_cesarea > -1) txtCantCesarea.Text = fichaMedica.PatoloagiaQui.Cant_cesarea.ToString();
                cbColcistectomia.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Colcistectomia);
                cbConizacion.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Conizacion);
                cbEsterilizacion.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Esterilizacion);
                cbSalpin.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Salpingectomia);
                cbCrio.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Crioterapia);
                cbHat.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Hat);
                cbHerIn.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.HerInguinal);
                cbHerUm.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.HerUmb);
                cbOoferectomia.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Ooforectomia);
                cbQuis.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.Quistectomia);
                txtFechaUpdate.Text = "Actualización: " + fichaMedica.FecUpdate.ToString();
                cbNoPM.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.No);
                cbNoPQ.IsChecked = Convert.ToBoolean(fichaMedica.PatoloagiaQui.No);
                cbMeno.IsChecked = Convert.ToBoolean(fichaMedica.PatologiasMed.Menopausia);
                if (fichaMedica.PatologiasMed.CantMeno > -1) txtCantMeno.Text = fichaMedica.PatologiasMed.CantMeno.ToString();
                List<Bitacora> bitacoras = BitacoraController.Bitacoras(fichaMedica.NFicha);
                List<PatExtras> PatME = FichaController.SelectPatMExtras(fichaMedica.NFicha);
                List<PatExtras> PatQE = FichaController.SelectPatQExtras(fichaMedica.NFicha);
                txtGuardado.Text = "GUARDADO";
                if (bitacoras != null)
                {
                    ListaBitacora(bitacoras);
                }
                if (PatME != null)
                {
                    try
                    {
                        cbPM1.IsChecked = Convert.ToBoolean(PatME[0].Estado);
                        txtPM1.Text = PatME[0].Nombre;
                        cbPM2.IsChecked = Convert.ToBoolean(PatME[1].Estado);
                        txtPM2.Text = PatME[1].Nombre;
                        cbPM3.IsChecked = Convert.ToBoolean(PatME[2].Estado);
                        txtPM3.Text = PatME[2].Nombre;
                        cbPM4.IsChecked = Convert.ToBoolean(PatME[3].Estado);
                        txtPM4.Text = PatME[3].Nombre;
                        cbPM5.IsChecked = Convert.ToBoolean(PatME[4].Estado);
                        txtPM5.Text = PatME[4].Nombre;
                    }
                    catch (Exception)
                    {

                    }
                }
                if (PatQE != null)
                {
                    try
                    {
                        cbPQ1.IsChecked = Convert.ToBoolean(PatQE[0].Estado);
                        txtPQ1.Text = PatQE[0].Nombre;
                        cbPQ2.IsChecked = Convert.ToBoolean(PatQE[1].Estado);
                        txtPQ2.Text = PatQE[1].Nombre;
                        cbPQ3.IsChecked = Convert.ToBoolean(PatQE[2].Estado);
                        txtPQ3.Text = PatQE[2].Nombre;
                        cbPQ4.IsChecked = Convert.ToBoolean(PatQE[3].Estado);
                        txtPQ4.Text = PatQE[3].Nombre;
                        cbPQ5.IsChecked = Convert.ToBoolean(PatQE[4].Estado);
                        txtPQ5.Text = PatQE[4].Nombre;
                    }
                    catch (Exception)
                    {

                    }
                }
                cambios = 0;
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro que desea limpiar los campos?","Confirmación",MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
                txtNumeroFicha.Focus();
                txtNumeroFicha.SelectionStart = txtNumeroFicha.Text.Length;
                txtNumeroFicha.SelectionLength = 0;
                Limpiar();
                nfichaEnUso = 0;
            }
        }

        private void Limpiar()
        {
            txtNombre1.Text = string.Empty;
            txtNombre2.Text = string.Empty;
            txtAPaterno.Text = string.Empty;
            txtAMaterno.Text = string.Empty;
            txtRut.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtFecha.Text = string.Empty;
            cboPrevision.SelectedIndex = 0;
            txtFono.Text = string.Empty;
            txtNumeroFicha.Text = string.Empty;
            txtGesta.Text = string.Empty;
            txtPV.Text = string.Empty;
            cboRs.SelectedIndex = 0;
            cboMAC.SelectedIndex = 0;
            cboTRH.SelectedItem = 0;
            txtEdad.Text = string.Empty;
            txtObs.Text = string.Empty;
            cbDm.IsChecked = false;
            cbDis.IsChecked = false;
            cbEpilepsia.IsChecked = false;
            cbHiper.IsChecked = false;
            cbItu.IsChecked = false;
            cbNie.IsChecked = false;
            cbJaqueca.IsChecked = false;
            cbMFQB.IsChecked = false;
            cbSop.IsChecked = false;
            cbVaginitis.IsChecked = false;
            cbAlergia.IsChecked = false;
            cbRh.IsChecked = false;
            cbApendicec.IsChecked = false;
            cbCA.IsChecked = false;
            cbCesarea.IsChecked = false;
            txtCantCesarea.Text = string.Empty;
            cbColcistectomia.IsChecked = false;
            cbConizacion.IsChecked = false;
            cbEsterilizacion.IsChecked = false;
            cbSalpin.IsChecked = false;
            cbCrio.IsChecked = false;
            cbHat.IsChecked = false;
            cbHerIn.IsChecked = false;
            cbHerUm.IsChecked = false;
            cbOoferectomia.IsChecked = false;
            cbQuis.IsChecked = false;
            txtFechaUpdate.Text = string.Empty;
            txtParTotal.Text = string.Empty;
            txtParAbortos.Text = string.Empty;
            txtParPrematuros.Text = string.Empty;
            txtParVivos.Text = string.Empty;
            stkBitacora.Children.Clear();
            cbHta.IsChecked = false;
            cboTRH.SelectedIndex = 0;
            txtPM1.Text = "";
            txtPQ1.Text = "";
            txtEdad.Text = string.Empty;
            txtNomBit.Text = string.Empty;
            txtFechaBit.Text = string.Empty;
            txtDescBit.Text = string.Empty;
            cbPM1.IsChecked = false;
            cbPM2.IsChecked = false;
            cbPM3.IsChecked = false;
            cbPM4.IsChecked = false;
            cbPM5.IsChecked = false;
            cbPQ1.IsChecked = false;
            cbPQ2.IsChecked = false;
            cbPQ3.IsChecked = false;
            cbPQ4.IsChecked = false;
            txtGuardado.Text = string.Empty;
            cbPQ5.IsChecked = false;
            cambios = 0;
            cbNoPM.IsChecked = false;
            cbNoPQ.IsChecked = false;
            cbMeno.IsChecked = false;
            txtCantMeno.Text = string.Empty;
            txtFechaBit.Text = DateTime.Today.ToString("dd/MM/yyyy");

        }

        private void ListaBitacora(List<Bitacora> bitacoras)
        {            
            stkBitacora.Children.Clear();
            try
            {
                for (int i = 0; i < bitacoras.Count; i++)
                {
                    try
                    {
                        stkBitacora.UnregisterName("txtBitID" + bitacoras[i].Id.ToString());
                        stkBitacora.UnregisterName("txtBitFecha" + bitacoras[i].Id.ToString());
                        stkBitacora.UnregisterName("txtBitTitulo" + bitacoras[i].Id.ToString());
                        stkBitacora.UnregisterName("txtBitDesc" + bitacoras[i].Id.ToString());
                        stkBitacora.UnregisterName("txtBitEstado" + bitacoras[i].Id.ToString());
                    }
                    catch (Exception)
                    {
                    }
                    TextBox textBox = new()
                    {
                        Text = bitacoras[i].FechaBitacora.ToString(),
                        Height = 20,
                        Width = 100,
                        Margin = new Thickness(20, 10, 0, 0),
                        BorderThickness = new Thickness(0, 0, 0, 1),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Background = Brushes.White,
                        TextWrapping = TextWrapping.Wrap,
                        Style = null,
                        AcceptsReturn = true,
                        Name = "txtBitFecha" + bitacoras[i].Id.ToString()
                    };
                    TextBox textBox2 = new()
                    {
                        Text = bitacoras[i].Nom,
                        Height = 20,
                        Width = 200,
                        Margin = new Thickness(140, 10, 0, 0),
                        BorderThickness = new Thickness(0, 0, 0, 1),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Background = Brushes.White,
                        TextWrapping = TextWrapping.Wrap,
                        Style = null,
                        AcceptsReturn = true,
                        CharacterCasing = CharacterCasing.Upper,
                        Name = "txtBitTitulo" + bitacoras[i].Id.ToString()

                    };
                    TextBox Estado = new()
                    {
                        Text = "GUARDADO",
                        IsReadOnly = true,
                        Height = 20,
                        Width = 200,
                        Margin = new Thickness(360, 10, 0, 0),
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Background = Brushes.White,
                        TextWrapping = TextWrapping.Wrap,
                        Style = null,
                        AcceptsReturn = true,
                        CharacterCasing = CharacterCasing.Upper,
                        Name = "txtBitEstado" + bitacoras[i].Id.ToString()

                    };
                    MaterialDesignThemes.Wpf.PackIcon packIcon = new()
                    {
                        Kind = MaterialDesignThemes.Wpf.PackIconKind.Delete
                    };
                    Button btn = new()
                    {
                        Background = Brushes.Red,
                        Width = 25,
                        Height = 25,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Padding = new Thickness(0, 0, 0, 0),
                        Margin = new Thickness(0, 1.5, 1, 0),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    btn.Content = packIcon;
                    MaterialDesignThemes.Wpf.PackIcon packIcon2 = new()
                    {
                        Kind = MaterialDesignThemes.Wpf.PackIconKind.Pencil,
                        Foreground = Brushes.Black
                    };
                    Button btn2 = new()
                    {
                        Background = Brushes.White,
                        Width = 25,
                        Height = 25,
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        Padding = new Thickness(0, 0, 0, 0),
                        Margin = new Thickness(0, 1.5, 26, 0),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    btn2.Content = packIcon2;

                    Grid grid2 = new()
                    {

                    };
                    grid2.Children.Add(textBox);
                    grid2.Children.Add(textBox2);
                    grid2.Children.Add(btn);
                    grid2.Children.Add(btn2);
                    grid2.Children.Add(Estado);
                    TextBox textBoxDesc = new()
                    {
                        Text = bitacoras[i].Descripcion,
                        MinHeight = 40,
                        MinWidth = 100,
                        Margin = new Thickness(10, 5, 10, 5),
                        BorderThickness = new Thickness(0, 0, 0, 0),
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.Wrap,
                        Background = null,
                        Style = null,
                        AcceptsReturn = true,
                        CharacterCasing = CharacterCasing.Upper,
                        Name = "txtBitDesc" + bitacoras[i].Id.ToString()

                    };

                    StackPanel stackPanel = new()
                    {

                    };
                    stackPanel.Children.Add(grid2);
                    stackPanel.Children.Add(textBoxDesc);
                    Rectangle rectangle = new()
                    {
                        Stroke = Brushes.Black
                    };
                    Grid grid = new()
                    {
                        Margin = new Thickness(30, 10, 20, 0)

                    };
                    TextBox id = new()
                    {
                        Text = bitacoras[i].Id.ToString(),
                        Name = "txtBitID" + bitacoras[i].Id.ToString(),
                        Visibility = Visibility.Hidden
                    };
                    grid.Children.Add(rectangle);
                    grid.Children.Add(stackPanel);
                    stkBitacora.Children.Add(id);
                    stkBitacora.RegisterName(id.Name, id);
                    stkBitacora.RegisterName(textBox.Name, textBox);
                    stkBitacora.RegisterName(textBox2.Name, textBox2);
                    stkBitacora.RegisterName(textBoxDesc.Name, textBoxDesc);
                    stkBitacora.RegisterName(Estado.Name, Estado);
                    btn.Click += delegate (object sender, RoutedEventArgs e) { btn_DeleteBit(sender, e, id.Name); };
                    btn2.Click += delegate (object sender, RoutedEventArgs e) { btn_EditBit(sender, e, id.Name, textBox.Name, textBox2.Name, textBoxDesc.Name, Estado.Name); };
                    textBox.TextChanged += delegate (object sender, TextChangedEventArgs e) { BitTextChanged(sender, e, Estado.Name); };
                    textBox2.TextChanged += delegate (object sender, TextChangedEventArgs e) { BitTextChanged(sender, e, Estado.Name); };
                    textBoxDesc.TextChanged += delegate (object sender, TextChangedEventArgs e) { BitTextChanged(sender, e, Estado.Name); };
                    textBox.KeyDown += txtEnter_KeyDown;
                    textBox2.KeyDown += txtEnter_KeyDown;
                    textBoxDesc.KeyDown += txtEnter_KeyDown;

                    stkBitacora.Children.Add(grid);
                }
            }
            catch (Exception)
            {

            }
        }

        private void BitTextChanged(object sender, TextChangedEventArgs e, string name)
        {
            TextBox textBox = (TextBox)this.stkBitacora.FindName(name);
            textBox.Text = "NO GUARDADO";
        }

        private void btn_DeleteBit(object sender, RoutedEventArgs e, string name)
        {
            TextBox textBox = (TextBox)this.stkBitacora.FindName(name);
            int message = int.Parse(textBox.Text);
            int bit = BitacoraController.EliminarBitacoras(message);
            if (bit > 0) 
            {
                try
                {
                    List<Bitacora> bitacoras = BitacoraController.Bitacoras(int.Parse(txtNumeroFicha.Text));
                    ListaBitacora(bitacoras);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("No ha sido posible eliminar el registro", "Error");
            }


        }
        private void btn_EditBit(object sender, RoutedEventArgs e, string name, string fechaName, string tituloName, string descName, string estado)
        {
            TextBox textBox = (TextBox)this.stkBitacora.FindName(name);
            TextBox textBox2 = (TextBox)this.stkBitacora.FindName(fechaName);
            TextBox textBox3 = (TextBox)this.stkBitacora.FindName(tituloName);
            TextBox textBox4 = (TextBox)this.stkBitacora.FindName(descName);
            TextBox textBox5 = (TextBox)this.stkBitacora.FindName(estado);

            if (DateTime.TryParse(textBox2.Text, out DateTime fechaBit))
            {
                Bitacora bitacora = new()
                {
                    Id = int.Parse(textBox.Text),
                    FechaBitacora = fechaBit,
                    Nom = textBox3.Text,
                    Descripcion = textBox4.Text
                };
                int bit = BitacoraController.UpdateBitacora(bitacora);
                if (bit > 0)
                {
                    try
                    {
                        textBox5.Text = "GUARDADO";
                        List<Bitacora> bitacoras = BitacoraController.Bitacoras(int.Parse(txtNumeroFicha.Text));
                        ListaBitacora(bitacoras);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No ha sido posible editar el registro", "Error");
                }


            }

        }
        private void InsertPmExtra()
        {
            if (txtPM1.Text.Length > 0) FichaController.PatMExtras(int.Parse(txtNumeroFicha.Text), txtPM1.Text, Convert.ToInt32(cbPM1.IsChecked.Value),1);
            if (txtPM2.Text.Length > 0) FichaController.PatMExtras(int.Parse(txtNumeroFicha.Text), txtPM2.Text, Convert.ToInt32(cbPM2.IsChecked.Value),2);
            if (txtPM3.Text.Length > 0) FichaController.PatMExtras(int.Parse(txtNumeroFicha.Text), txtPM3.Text, Convert.ToInt32(cbPM3.IsChecked.Value),3);
            if (txtPM4.Text.Length > 0) FichaController.PatMExtras(int.Parse(txtNumeroFicha.Text), txtPM4.Text, Convert.ToInt32(cbPM4.IsChecked.Value),4);
            if (txtPM5.Text.Length > 0) FichaController.PatMExtras(int.Parse(txtNumeroFicha.Text), txtPM5.Text, Convert.ToInt32(cbPM5.IsChecked.Value),5);
        }
        private void UpdatePmExtras()
        {
            if (txtPM1.Text.Length > 0) FichaController.UpdateMExtras(int.Parse(txtNumeroFicha.Text), txtPM1.Text, Convert.ToInt32(cbPM1.IsChecked.Value), 1);
            if (txtPM2.Text.Length > 0) FichaController.UpdateMExtras(int.Parse(txtNumeroFicha.Text), txtPM2.Text, Convert.ToInt32(cbPM2.IsChecked.Value), 2);
            if (txtPM3.Text.Length > 0) FichaController.UpdateMExtras(int.Parse(txtNumeroFicha.Text), txtPM3.Text, Convert.ToInt32(cbPM3.IsChecked.Value), 3);
            if (txtPM4.Text.Length > 0) FichaController.UpdateMExtras(int.Parse(txtNumeroFicha.Text), txtPM4.Text, Convert.ToInt32(cbPM4.IsChecked.Value), 4);
            if (txtPM5.Text.Length > 0) FichaController.UpdateMExtras(int.Parse(txtNumeroFicha.Text), txtPM5.Text, Convert.ToInt32(cbPM5.IsChecked.Value), 5);
        }
        private void DeletePmExtras()
        {
            if (cbPM1.IsChecked.Value == false) FichaController.DeletePatMExtra(int.Parse(txtNumeroFicha.Text), 1);
            if (cbPM2.IsChecked.Value == false) FichaController.DeletePatMExtra(int.Parse(txtNumeroFicha.Text), 2);
            if (cbPM3.IsChecked.Value == false) FichaController.DeletePatMExtra(int.Parse(txtNumeroFicha.Text), 3);
            if (cbPM4.IsChecked.Value == false) FichaController.DeletePatMExtra(int.Parse(txtNumeroFicha.Text), 4);
            if (cbPM5.IsChecked.Value == false) FichaController.DeletePatMExtra(int.Parse(txtNumeroFicha.Text), 5);

        }
        private void InsertPqExtra()
        {
            if (txtPQ1.Text.Length > 0) FichaController.PatQExtras(int.Parse(txtNumeroFicha.Text), txtPQ1.Text, Convert.ToInt32(cbPQ1.IsChecked.Value),1);
            if (txtPQ2.Text.Length > 0) FichaController.PatQExtras(int.Parse(txtNumeroFicha.Text), txtPQ2.Text, Convert.ToInt32(cbPQ2.IsChecked.Value),2);
            if (txtPQ3.Text.Length > 0) FichaController.PatQExtras(int.Parse(txtNumeroFicha.Text), txtPQ3.Text, Convert.ToInt32(cbPQ3.IsChecked.Value),3);
            if (txtPQ4.Text.Length > 0) FichaController.PatQExtras(int.Parse(txtNumeroFicha.Text), txtPQ4.Text, Convert.ToInt32(cbPQ4.IsChecked.Value),4);
            if (txtPQ5.Text.Length > 0) FichaController.PatQExtras(int.Parse(txtNumeroFicha.Text), txtPQ5.Text, Convert.ToInt32(cbPQ5.IsChecked.Value),5);
        }
        private void UpdatePQExtras()
        {
            if (txtPQ1.Text.Length > 0) FichaController.UpdateQExtras(int.Parse(txtNumeroFicha.Text), txtPQ1.Text, Convert.ToInt32(cbPQ1.IsChecked.Value), 1);
            if (txtPQ2.Text.Length > 0) FichaController.UpdateQExtras(int.Parse(txtNumeroFicha.Text), txtPQ2.Text, Convert.ToInt32(cbPQ2.IsChecked.Value), 2);
            if (txtPQ3.Text.Length > 0) FichaController.UpdateQExtras(int.Parse(txtNumeroFicha.Text), txtPQ3.Text, Convert.ToInt32(cbPQ3.IsChecked.Value), 3);
            if (txtPQ4.Text.Length > 0) FichaController.UpdateQExtras(int.Parse(txtNumeroFicha.Text), txtPQ4.Text, Convert.ToInt32(cbPQ4.IsChecked.Value), 4);
            if (txtPQ5.Text.Length > 0) FichaController.UpdateQExtras(int.Parse(txtNumeroFicha.Text), txtPQ5.Text, Convert.ToInt32(cbPQ5.IsChecked.Value), 5);
        }
        private void DeletePqExtras()
        {
            if (cbPQ1.IsChecked.Value == false) FichaController.DeletePatQExtra(int.Parse(txtNumeroFicha.Text), 1);
            if (cbPQ2.IsChecked.Value == false) FichaController.DeletePatQExtra(int.Parse(txtNumeroFicha.Text), 2);
            if (cbPQ3.IsChecked.Value == false) FichaController.DeletePatQExtra(int.Parse(txtNumeroFicha.Text), 3);
            if (cbPQ4.IsChecked.Value == false) FichaController.DeletePatQExtra(int.Parse(txtNumeroFicha.Text), 4);
            if (cbPQ5.IsChecked.Value == false) FichaController.DeletePatQExtra(int.Parse(txtNumeroFicha.Text), 5);

        }
        private void txtPM1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPM1.Text.Length > 0) cbPM2.Visibility = Visibility.Visible;
            else
            {
                cbPM2.Visibility = Visibility.Collapsed;
                txtPM2.Clear();
                cbPM3.Visibility = Visibility.Collapsed;
                txtPM3.Clear();
                cbPM4.Visibility = Visibility.Collapsed;
                txtPM4.Clear();
                cbPM5.Visibility = Visibility.Collapsed;
                txtPM5.Clear();
            }
            cambios = 1;
        }

        private void txtPM2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPM2.Text.Length > 0) cbPM3.Visibility = Visibility.Visible;
            else
            {
                cbPM3.Visibility = Visibility.Collapsed;
                txtPM3.Clear();
                cbPM4.Visibility = Visibility.Collapsed;
                txtPM4.Clear();
                cbPM5.Visibility = Visibility.Collapsed;
                txtPM5.Clear();
            }
            cambios = 1;
        }

        private void txtPM3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPM3.Text.Length > 0) cbPM4.Visibility = Visibility.Visible;
            else
            {
                cbPM4.Visibility = Visibility.Collapsed;
                txtPM4.Clear();
                cbPM5.Visibility = Visibility.Collapsed;
                txtPM5.Clear();
            }
            cambios = 1;
        }

        private void txtPM4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPM4.Text.Length > 0) cbPM5.Visibility = Visibility.Visible;
            else
            {
                cbPM5.Visibility = Visibility.Collapsed;
                txtPM5.Clear();
            }
            cambios = 1;
        }

        private void txtPQ1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPQ1.Text.Length > 0) cbPQ2.Visibility = Visibility.Visible;
            else
            {
                cbPQ2.Visibility = Visibility.Collapsed;
                txtPQ2.Clear();
                cbPQ3.Visibility = Visibility.Collapsed;
                txtPQ3.Clear();
                cbPQ4.Visibility = Visibility.Collapsed;
                txtPQ4.Clear();
                cbPQ5.Visibility = Visibility.Collapsed;
                txtPQ5.Clear();
            }
            cambios = 1;
        }

        private void txtPQ2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPQ2.Text.Length > 0) cbPQ3.Visibility = Visibility.Visible;
            else
            {
                cbPQ3.Visibility = Visibility.Collapsed;
                txtPQ3.Clear();
                cbPQ4.Visibility = Visibility.Collapsed;
                txtPQ4.Clear();
                cbPQ5.Visibility = Visibility.Collapsed;
                txtPQ5.Clear();
            }
            cambios = 1;
        }

        private void txtPQ3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPQ3.Text.Length > 0) cbPQ4.Visibility = Visibility.Visible;
            else
            {
                cbPQ4.Visibility = Visibility.Collapsed;
                txtPQ4.Clear();
                cbPQ5.Visibility = Visibility.Collapsed;
                txtPQ5.Clear();
            }
            cambios = 1;
        }

        private void txtPQ4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPQ4.Text.Length > 0) cbPQ5.Visibility = Visibility.Visible;
            else
            {
                cbPQ5.Visibility = Visibility.Collapsed;
                txtPQ5.Clear();
            }
            cambios = 1;
        }

        private void txtRut_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtRut.Text.Length >= 8 && int.TryParse(txtRut.Text.Substring(0, txtRut.Text.Length - 1), out int rut)) 
            {
                txtRut.Text = txtRut.Text.ToString().Insert(txtRut.Text.Length - 1, "-").ToUpper();
            }
            cambios = 1;
        }
        private string GetSelectedValue(DataGrid grid)
        {
            DataGridCellInfo cellInfo = grid.SelectedCells[0];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }
        private void dtgPacientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgPacientes.SelectedCells.Count > 0)
            {
                Limpiar();
                string rut = GetSelectedValue(dtgPacientes);
                txtRut.Text = rut;
                BuscarPaciente();
                txtNombre1.Focus();
                dhPaciente.IsOpen = false;
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro que desea limpiar los campos?", "Confirmación", MessageBoxButton.YesNo);
            if (resultado == MessageBoxResult.Yes)
            {
                LogIn log = new();
                log.Show();
                this.Close();
            }

        }

        private void txtEnter_KeyDown(object sender, KeyEventArgs e)
        {
            var GUI = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                GUI.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void Up_TextChanged(object sender, TextChangedEventArgs e)
        {
            cambios = 1;
            txtGuardado.Text = "NO GUARDADO";
        }

        private void Up_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cambios = 1;
            txtGuardado.Text = "NO GUARDADO";
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            cambios = 1;
            txtGuardado.Text = "NO GUARDADO";
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            txtHoy.Text = "Fecha: " + DateTime.Now.ToString();
        }

        private void txtNumeroFicha_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtGuardado.Text = "NO GUARDADO";
            cambios = 1;
        }
    }
}
