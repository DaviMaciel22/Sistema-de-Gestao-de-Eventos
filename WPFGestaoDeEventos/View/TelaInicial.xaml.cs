using System.Windows;
using System.Windows.Controls;
using WPFGestaoDeEventos.Controller;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.View
{
    // MVC: a View só lê os campos, chama o controller e mostra o resultado
    public partial class TelaInicial : UserControl
    {
        private readonly EventoController _eventoController = new();
        private readonly InscricaoController _inscricaoController = new();
        private readonly ParticipanteController _participanteController = new();

        public TelaInicial()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Evita duplicar as cidades se a tela for carregada de novo
                while (cbCidade.Items.Count > 1)
                    cbCidade.Items.RemoveAt(cbCidade.Items.Count - 1);

                foreach (var cidade in _eventoController.ListarCidades())
                    cbCidade.Items.Add(new ComboBoxItem { Content = cidade });

                ExibirEventos(_eventoController.ListarDisponiveis());
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private void BtnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            AplicarFiltros();
        }

        private void BtnGarantirIngresso_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button botao || botao.Tag is not int eventoId)
                return;

            try
            {
                var participante = _participanteController.ObterAtual();
                _inscricaoController.Inscrever(participante.ParticipanteId, eventoId);

                MessageBox.Show("Ingresso garantido! Veja em \"Meus Ingressos\".", "Sucesso",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                AplicarFiltros();
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

        private void AplicarFiltros()
        {
            try
            {
                string? cidade = cbCidade.SelectedIndex > 0 ? TextoSelecionado(cbCidade) : null;
                string? tipo = ConverterTipo(cbTipo.SelectedIndex > 0 ? TextoSelecionado(cbTipo) : null);

                ExibirEventos(_eventoController.Buscar(txtBuscar.Text, dpData.SelectedDate, cidade, tipo));
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        // O banco guarda "Conferencia" (sem acento) na coluna Discriminator
        private static string? ConverterTipo(string? texto)
        {
            return texto switch
            {
                "Conferência" => "Conferencia",
                "Workshop" => "Workshop",
                _ => null
            };
        }

        private static string? TextoSelecionado(ComboBox combo)
        {
            return (combo.SelectedItem as ComboBoxItem)?.Content?.ToString();
        }

        private void ExibirEventos(List<Evento> eventos)
        {
            icEventos.ItemsSource = eventos;
            lblSemEventos.Visibility = eventos.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private static void MostrarErro(Exception ex)
        {
            MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
