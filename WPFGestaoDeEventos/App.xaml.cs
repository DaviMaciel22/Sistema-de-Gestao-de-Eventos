using System.Windows;
using WPFGestaoDeEventos.Data;

namespace WPFGestaoDeEventos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                using var contexto = new AppDbContext();
                contexto.Inicializar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível preparar o banco de dados. Verifique se a migration foi criada e aplicada (Update-Database).\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Shutdown();
                return;
            }

            base.OnStartup(e);
        }
    }
}
