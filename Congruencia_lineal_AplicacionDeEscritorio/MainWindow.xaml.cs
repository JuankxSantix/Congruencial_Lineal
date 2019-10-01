using OxyPlot;
using OxyPlot.Series;
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
        PlotModel modelfx;
        bool NumDeIteraciones;
        int NDeIteraciones;

        public MainWindow()
        {
            InitializeComponent();
            Editable(false);
        }

        void Borrar()
        {
            txtCMultiplicativa.Clear();
            txtMoulo.Clear();
            txtSemilla.Clear();
            lblResultados.Content = "";
            lblResultados2.Content = "";
            txtIncremento.Clear();
            txtNIteraciones.Clear();
            CClompletoRadiobutton.IsChecked = false;
            DIteracionesRadiobutton.IsChecked = false;
        }
         void Editable(bool X)
        {
            txtNIteraciones.IsEnabled = X;
            txtSemilla.IsEnabled = X;
            txtMoulo.IsEnabled = X;
            txtIncremento.IsEnabled = X;
            txtCMultiplicativa.IsEnabled = X;
            btnCalcular.IsEnabled = X;
            btnGraficar.IsEnabled = X;
            StkRadios.IsEnabled = X;
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            Borrar();
            Editable(true);

        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            lblResultados.Content = "";
            lblResultados2.Content = "";

            if (CClompletoRadiobutton.IsChecked == true)
                txtNIteraciones.Text = "0";

            if (!string.IsNullOrEmpty(txtMoulo.Text) && !string.IsNullOrEmpty(txtSemilla.Text) && !string.IsNullOrEmpty(txtIncremento.Text) && !string.IsNullOrEmpty(txtCMultiplicativa.Text) && !string.IsNullOrEmpty(txtNIteraciones.Text) && CClompletoRadiobutton.IsChecked==true || DIteracionesRadiobutton.IsChecked==true)
            {
                int i = 0;
                int multiplicador;
                int semilla;
                int incremento;
                double modulo;
                modulo = double.Parse(txtMoulo.Text);
                multiplicador = int.Parse(txtCMultiplicativa.Text);
                semilla = int.Parse(txtSemilla.Text);
                incremento = int.Parse(txtIncremento.Text);
                int numero;
                int Xi;
                numero = semilla;
                int y = 0;

                if (NumDeIteraciones)
                    NDeIteraciones = int.Parse(txtNIteraciones.Text);
                else
                    NDeIteraciones = int.Parse(txtMoulo.Text);

                double[] datos = new double[NDeIteraciones];

                for (int k = 0; k < NDeIteraciones; k++)
                    datos[k] = -1;

                do
                {
                    Xi = (multiplicador * numero + incremento);
                    double residuo;
                    double PEntera;
                    PEntera = Xi / modulo;
                    residuo = Xi % modulo;
                    double NAleatorio;
                    numero = int.Parse(residuo.ToString());
                    NAleatorio = numero / (modulo - 1);

                    bool SeRepite=false;

                    foreach (var item in datos)
                    {
                        if (item == NAleatorio)
                        {
                            SeRepite = true;
                            break;
                        }
                        else
                        {
                            SeRepite = false;
                        }

                    }

                    if (!NumDeIteraciones)
                    {
                        if (SeRepite)
                        {
                            MessageBox.Show("Se repite");
                            NDeIteraciones = i;
                            break;
                        }
                        else
                        {
                            datos[i] = NAleatorio;
                            if (y % 2 == 0)

                                lblResultados.Content += string.Format(" U{0}={1}\n", (i + 1), NAleatorio.ToString());

                            else
                                lblResultados2.Content += string.Format(" U{0}={1}\n", (i + 1), NAleatorio.ToString());

                            i++;
                            y++;
                        }
                    }
                    else
                    {
                        datos[i] = NAleatorio;
                        if (y % 2 == 0)

                            lblResultados.Content += string.Format(" U{0}={1}\n", (i + 1), NAleatorio.ToString());

                        else
                            lblResultados2.Content += string.Format(" U{0}={1}\n", (i + 1), NAleatorio.ToString());

                        i++;
                        y++;
                    }
                    

                } while (i != NDeIteraciones);

            }
            else
            {
                MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnGraficar_Click(object sender, RoutedEventArgs e)
        {
            graficarL();
        }

        private void graficarL()
        {
            try
            {
                if (CClompletoRadiobutton.IsChecked == true)
                    txtNIteraciones.Text = "0";

                if (!string.IsNullOrEmpty(txtMoulo.Text) && !string.IsNullOrEmpty(txtSemilla.Text) && !string.IsNullOrEmpty(txtIncremento.Text) && !string.IsNullOrEmpty(txtCMultiplicativa.Text) && !string.IsNullOrEmpty(txtNIteraciones.Text) && CClompletoRadiobutton.IsChecked == true || DIteracionesRadiobutton.IsChecked == true)
                {
                    modelfx = new PlotModel();
                    List<Punto> resultado = new List<Punto>();
                    LineSeries Algorit = new LineSeries();


                    int multiplicador;
                    int incremento;
                    double modulo;
                    modulo = double.Parse(txtMoulo.Text);
                    multiplicador = int.Parse(txtCMultiplicativa.Text);
                    incremento = int.Parse(txtIncremento.Text);
                    int numero;
                    numero = int.Parse(txtSemilla.Text);
                    
                    for (int i = 1; i <= NDeIteraciones; i++)
                    {

                        int Xi = (multiplicador * numero + incremento);
                        double residuo;
                        residuo = Xi % modulo;
                        numero = int.Parse(residuo.ToString());
                        resultado.Add(new Punto { X = i, Y = (numero / (modulo - 1)) });
                    }

                    foreach (var punto in resultado)
                    {
                        Algorit.Points.Add(new DataPoint(punto.X, punto.Y));
                        Algorit.MarkerType = MarkerType.Star;
                        Algorit.Color = OxyColors.Red;
                        Algorit.MarkerStroke = OxyColors.Red;
                    }

                    Algorit.Color = OxyColors.DarkRed;
                    Algorit.Title = "Puntos";
                    modelfx.Series.Add(Algorit);

                    chartfx2.Model = modelfx;
                }
                else
                {
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cronguencia Lineal", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        class Punto
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        private void CClompletoRadiobutton_Click(object sender, RoutedEventArgs e)
        {
            txtNIteraciones.IsEnabled = false;
            txtNIteraciones.Clear();
            NumDeIteraciones = false;
        }

        private void DIteracionesRadiobutton_Click(object sender, RoutedEventArgs e)
        {
            txtNIteraciones.IsEnabled = true;
            NumDeIteraciones = true;
        }
    }
}
