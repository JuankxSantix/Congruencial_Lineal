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

namespace Congruencia_lineal_AplicacionDeEscritorio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void Borrar()
        {
            txtCMultiplicativa.Clear();
            txtMoulo.Clear();
            txtSemilla.Clear();
            lblResultados.Content = "";
            txtIncremento.Clear();
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Borrar();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {

            if(!string.IsNullOrEmpty(txtMoulo.Text) && !string.IsNullOrEmpty(txtSemilla.Text) && !string.IsNullOrEmpty(txtIncremento.Text) && !string.IsNullOrEmpty(txtCMultiplicativa.Text))
            {
                if(int.Parse(txtSemilla.Text)>=0 && int.Parse(txtCMultiplicativa.Text)%2==0 && int.Parse(txtIncremento.Text) % 2 != 0 && int.Parse(txtMoulo.Text) % 2 == 0)
                {
                    int i = 0;
                    int multiplicador;
                    int semilla;
                    int incremento;
                    double modulo;
                    multiplicador = 1 + 4 * int.Parse(txtCMultiplicativa.Text);
                    semilla = int.Parse(txtSemilla.Text);
                    incremento = int.Parse(txtIncremento.Text);
                    modulo = Math.Pow(2, int.Parse(txtMoulo.Text));
                    int numero;
                    int Xi=0;
                    numero = semilla;
                    do
                    {
                        Xi += 1;
                        Xi = (multiplicador * numero + incremento);
                        double residuo;
                        double PEntera;
                        PEntera = Xi / modulo;
                        residuo = Xi % modulo;
                        double NAleatorio;
                         numero = int.Parse(residuo.ToString());
                        NAleatorio = numero / (modulo-1);
                        lblResultados.Content += " EL Numero aleatorio es: " + NAleatorio.ToString()+"\n";
                        i++;

                    } while (i!=modulo);
                    
                }
                else
                {
                    MessageBox.Show("Error de datos \n Por favor pruebe con otros", "Error", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                    MessageBox.Show("La semilla debe ser mayor o igual a 0\nLa C. Multiplicativa asi como el modulo deben ser enteros\nEl incremento debe ser impar", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

                
        }
    }
}
