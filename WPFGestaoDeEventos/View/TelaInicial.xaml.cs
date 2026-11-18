using System.Windows;
using System.Windows.Controls;
using WPFGestaoDeEventos.Controller;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.View
{
    /// <summary>
    /// Interação lógica para TelaInicial.xam
    /// A View só repassa os eventos da tela para os controllers; as regras ficam nos controllers.
    /// </summary>
    public partial class TelaInicial : UserControl
    {
        private readonly EventoController _eventoController = new();
        private readonly InscricaoController _inscricaoController = new();
        private readonly ParticipanteController _participanteController = new();

        public TelaInicial()
        {
            InitializeComponent();
        }

        // Ao abrir a tela: lista os eventos disponíveis e preenche o filtro de cidades
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PreencherCidades();
                ExibirEventos(_eventoController.ListarDisponiveis());
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        // Botão Filtrar: usa a busca feita em SQL (EventoController.Buscar)
        private void BtnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            AplicarFiltros();
        }

        // Botão Garantir ingresso (um por card; o Tag guarda o Id do evento)
        private void BtnGarantirIngresso_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button botao || botao.Tag is not int eventoId)
            {
                return;
            }

            try
            {
                Participante participante = _participanteController.ObterAtual();
                _inscricaoController.Inscrever(participante.IdParticipante, eventoId);

                MessageBox.Show("Ingresso garantido com sucesso!", "Inscrição confirmada",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Recarrega a lista mantendo os filtros, para as vagas atualizarem
                AplicarFiltros();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Não foi possível garantir o ingresso",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
                string? nome = txtBuscar.Text;
                DateTime? data = dpData.SelectedDate;
                string? cidade = cbCidade.SelectedIndex > 0 ? TextoSelecionado(cbCidade) : null;
                string? tipo = ConverterTipo(TextoSelecionado(cbTipo));

                ExibirEventos(_eventoController.Buscar(nome, data, cidade, tipo));
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private void PreencherCidades()
        {
            // Mantém o primeiro item ("Todas") e acrescenta as cidades que existem no banco
            while (cbCidade.Items.Count > 1)
            {
                cbCidade.Items.RemoveAt(1);
            }

            foreach (string cidade in _eventoController.ListarCidades())
            {
                cbCidade.Items.Add(new ComboBoxItem { Content = cidade });
            }
        }

        private void ExibirEventos(List<Evento> eventos)
        {
            icEventos.ItemsSource = eventos;
            lblSemEventos.Visibility = eventos.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private static string? TextoSelecionado(ComboBox combo)
        {
            return (combo.SelectedItem as ComboBoxItem)?.Content?.ToString();
        }

        // A tela mostra "Conferência" (com acento); no banco o valor é "Conferencia"
        private static string? ConverterTipo(string? texto)
        {
            return texto switch
            {
                "Conferência" => "Conferencia",
                "Workshop" => "Workshop",
                _ => null
            };
        }

        private static void MostrarErro(Exception ex)
        {
            MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
