using System.Windows;
using System.Windows.Controls;
using WPFGestaoDeEventos.Controller;

namespace WPFGestaoDeEventos.View
{
    // MVC: a View só lê os campos, chama o controller e mostra o resultado
    public partial class Perfil : UserControl
    {
        private readonly ParticipanteController _controller = new();

        public Perfil()
        {
            InitializeComponent();
            Loaded += (s, e) => CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                var participante = _controller.ObterAtual();
                txtNome.Text = participante.Nome;
                txtEmail.Text = participante.Email;
                txtIniciais.Text = Iniciais(participante.Nome);
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.AtualizarDados(txtNome.Text, txtEmail.Text);
                string nome = txtNome.Text.Trim();
                txtIniciais.Text = Iniciais(nome);
                (Window.GetWindow(this) as MainWindow)?.AtualizarSaudacao(nome);
                MessageBox.Show("Dados atualizados.", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private void BtnAlterarSenha_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _controller.AlterarSenha(pbSenhaAtual.Password, pbNovaSenha.Password, pbConfirmacao.Password);
                MessageBox.Show("Senha alterada.", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
            finally
            {
                // Limpa as senhas sempre, dando certo ou não
                pbSenhaAtual.Clear();
                pbNovaSenha.Clear();
                pbConfirmacao.Clear();
            }
        }

        private void BtnExcluirConta_Click(object sender, RoutedEventArgs e)
        {
            var resposta = MessageBox.Show(
                "Tem certeza que deseja excluir sua conta? Essa ação não pode ser desfeita.",
                "Excluir conta", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (resposta != MessageBoxResult.Yes)
                return;

            try
            {
                _controller.ExcluirConta();
                MessageBox.Show("Conta excluída. O sistema será fechado.", "Conta excluída",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private static string Iniciais(string nome)
        {
            var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 0) return "";
            if (partes.Length == 1) return partes[0][..1].ToUpper();
            return (partes[0][..1] + partes[^1][..1]).ToUpper();
        }

        private static void MostrarErro(Exception ex)
        {
            MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
