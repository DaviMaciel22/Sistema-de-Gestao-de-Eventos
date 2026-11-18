using System.Windows;
using System.Windows.Controls;
using WPFGestaoDeEventos.Controller;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.View
{
    // MVC: a View só lê os campos, chama o controller e mostra o resultado
    public partial class MeusIngressos : UserControl
    {
        private readonly InscricaoController _inscricaoController = new();
        private readonly ParticipanteController _participanteController = new();
        private int _participanteId;
        private bool _carregado;

        public MeusIngressos()
        {
            InitializeComponent();
            Loaded += (s, e) => Iniciar();
        }

        private void Iniciar()
        {
            if (_carregado) return;

            try
            {
                _participanteId = _participanteController.ObterAtual().ParticipanteId;
                _carregado = true;
                CarregarLista();
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_carregado) CarregarLista();
        }

        private void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_carregado) CarregarLista();
        }

        private void LbIngressos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarDetalhes(lbIngressos.SelectedItem as Inscricao);
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (lbIngressos.SelectedItem is not Inscricao inscricao)
                return;

            var resposta = MessageBox.Show(
                $"Cancelar o ingresso de \"{inscricao.Evento.Titulo}\"?", "Confirmar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resposta != MessageBoxResult.Yes)
                return;

            try
            {
                _inscricaoController.Cancelar(inscricao.InscricaoId);
                MessageBox.Show("Ingresso cancelado. A vaga voltou para o evento.", "Sucesso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                CarregarLista();
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

        private void BtnCertificado_Click(object sender, RoutedEventArgs e)
        {
            if (lbIngressos.SelectedItem is not Inscricao inscricao)
                return;

            try
            {
                var certificado = _inscricaoController.ObterCertificado(inscricao.InscricaoId);
                MessageBox.Show(
                    $"Certificado nº {certificado.CertificadoId}\n" +
                    $"Evento: {certificado.Inscricao.Evento.Titulo}\n" +
                    $"Duração: {certificado.Inscricao.Evento.Duracao}\n" +
                    $"Participante: {certificado.Inscricao.Participante.Nome}\n" +
                    $"Emitido em: {certificado.DataEmissao:dd/MM/yyyy}",
                    "Certificado", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void CarregarLista()
        {
            try
            {
                string? status = cbStatus.SelectedIndex > 0
                    ? (cbStatus.SelectedItem as ComboBoxItem)?.Content?.ToString()
                    : null;

                lbIngressos.ItemsSource = _inscricaoController.ListarPorParticipante(_participanteId, txtBuscar.Text, status);
                MostrarDetalhes(null);
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }
        }

        private void MostrarDetalhes(Inscricao? inscricao)
        {
            if (inscricao == null)
            {
                txtTituloEvento.Text = "Selecione um ingresso";
                runData.Text = runTipo.Text = runDuração.Text = runLocal.Text = runDescricao.Text = "";
                runCodigo.Text = runStatus.Text = "";
                icPalestras.ItemsSource = null;
                lblSemPalestras.Visibility = Visibility.Collapsed;
                btnCancelar.IsEnabled = false;
                btnCertificado.IsEnabled = false;
                lblAviso.Visibility = Visibility.Visible;
                return;
            }

            var evento = inscricao.Evento;
            txtTituloEvento.Text = evento.Titulo;
            runData.Text = evento.DataEHora.ToString("dd/MM/yyyy HH:mm");
            runTipo.Text = evento.TipoEvento;
            runDuração.Text = evento.Duracao;
            runLocal.Text = $"{evento.Local.Nome} - {evento.Local.Cidade}/{evento.Local.Estado}";
            runDescricao.Text = evento.Descricao;
            runCodigo.Text = $"ING-{inscricao.InscricaoId:D5}";
            runStatus.Text = inscricao.Status;

            try
            {
                var palestras = _inscricaoController.ListarPalestras(evento.EventoId);
                icPalestras.ItemsSource = palestras;
                lblSemPalestras.Visibility = palestras.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MostrarErro(ex);
            }

            btnCancelar.IsEnabled = inscricao.Status == StatusInscricao.Confirmado;
            bool utilizado = inscricao.Status == StatusInscricao.Utilizado;
            btnCertificado.IsEnabled = utilizado;
            lblAviso.Visibility = utilizado ? Visibility.Collapsed : Visibility.Visible;
        }

        private static void MostrarErro(Exception ex)
        {
            MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
