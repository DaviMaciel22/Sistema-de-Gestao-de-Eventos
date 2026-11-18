using System.Windows;
using WPFGestaoDeEventos.Controller;

namespace WPFGestaoDeEventos.View
{
    // MVC: a janela principal só troca as telas e mostra o nome de quem está usando o app
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => CarregarSaudacao();
        }

        // Chamado pelo Perfil depois de salvar, para o nome lá em cima mudar na hora
        public void AtualizarSaudacao(string nome)
        {
            txtSaudacao.Text = $"Olá, {nome}";
        }

        private void CarregarSaudacao()
        {
            try
            {
                AtualizarSaudacao(new ParticipanteController().ObterAtual().Nome);
            }
            catch (Exception)
            {
                // Se o banco falhar, a Home já mostra o erro; aqui fica só "Olá"
                txtSaudacao.Text = "Olá";
            }
        }

        private void BtnEventos_Click(object sender, RoutedEventArgs e)
        {
            AreaPrincipal.Content = new TelaInicial();
        }

        private void BtnIngressos_Click(object sender, RoutedEventArgs e)
        {
            AreaPrincipal.Content = new MeusIngressos();
        }

        private void BtnPerfil_Click(object sender, RoutedEventArgs e)
        {
            AreaPrincipal.Content = new Perfil();
        }
    }
}
