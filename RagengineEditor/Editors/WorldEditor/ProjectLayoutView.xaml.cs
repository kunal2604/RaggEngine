using RagengineEditor.GameProject;
using System.Windows;
using System.Windows.Controls;

namespace RagengineEditor.Editors
{
    /// <summary>
    /// Interaction logic for ProjectLayoutView.xaml
    /// </summary>
    public partial class ProjectLayoutView : UserControl
    {
        public ProjectLayoutView()
        {
            InitializeComponent();
        }

        private void OnAddScene_ButtonClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as Project;
            vm.AddScene("New Scene " + vm.Scenes.Count);
        }
    }
}
