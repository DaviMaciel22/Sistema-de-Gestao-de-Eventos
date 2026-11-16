using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFGestaoDeEventos.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
        }

        private void BtnEventos_Click(object sender, RoutedEventArgs e)
        {
            // Instancia a Tela Inicial e a joga para dentro da área principal
            AreaPrincipal.Content = new WPFGestaoDeEventos.View.TelaInicial();
        }

        private void BtnIngressos_Click(object sender, RoutedEventArgs e)
        {
            AreaPrincipal.Content = new WPFGestaoDeEventos.View.MeusIngressos();
        }

        private void BtnPerfil_Click(object sender, RoutedEventArgs e)
        {
            AreaPrincipal.Content = new WPFGestaoDeEventos.View.Perfil();
        }

    }
}

