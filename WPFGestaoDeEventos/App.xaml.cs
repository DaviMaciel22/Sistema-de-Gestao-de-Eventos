using System.Configuration;
using System.Data;
using System.Windows;

namespace WPFGestaoDeEventos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                using var context = new WpfGestaoDeEventosDbContext();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao conectar ou atualizar o banco de dados:\n{ex.Message}",
                    "Erro de Banco de Dados",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }

}
