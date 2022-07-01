using System;
using System.Collections.Generic;
using System.IO;
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
using Conexion;

namespace FichasMedicas
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string pss = txtUser.Text;
            string usr = txtPss.Password;
            int validado = UsuarioController.ValidarUsusario(pss, usr);
            if (validado == 1)
            {
                MainWindow main = new();
                main.Show();
                this.Close();
            }
            else if (validado == 0)
            {
                MessageBox.Show("El usuario y la contraseña no coincide");
            }
            else
            {
                MessageBox.Show("Error interno");

            }
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            grdMain.Visibility = Visibility.Collapsed;
            grdEditar.Visibility = Visibility.Visible;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            grdMain.Visibility = Visibility.Visible;
            grdEditar.Visibility = Visibility.Collapsed;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUserA.Text;
            string pss = txtPassA.Password;
            string userN = txtUserN.Text;
            string pssN = txtPassN.Password;
            if (userN.Trim().Length > 0 && pssN.Trim().Length > 0)
            {
                if (pssN.Equals(txtPassNB.Password.Trim()))
                {
                    int modificado = UsuarioController.ModificarUsusario(user, pss, userN, pssN);
                    if (modificado > 0)
                    {
                        MessageBox.Show("Usuario correctamente actualizado");
                        grdMain.Visibility = Visibility.Visible;
                        grdEditar.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Algo salió mal, por favor revise la combinación de datos");
                    }
                }
                else
                {
                        MessageBox.Show("Las contraseñas no coinciden");
                }
            }
        }
    }
}
