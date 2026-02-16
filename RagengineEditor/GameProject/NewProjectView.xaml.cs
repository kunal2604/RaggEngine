using System.Windows;
using System.Windows.Controls;

namespace RagengineEditor.GameProject
{
    /// <summary>
    /// Interaction logic for NewProjectView.xaml
    /// </summary>
    public partial class NewProjectView : UserControl
    {
        public NewProjectView()
        {
            InitializeComponent();
        }

        private void On_Create_Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as NewProject;
            var projectPath = vm.CreateProject(templateListBox.SelectedItem as ProjectTemplate);
            bool dialogResult = false;
            var window = Window.GetWindow(this);
            if(!string.IsNullOrEmpty(projectPath))
            {
                dialogResult = true;
            }
            window.DialogResult = dialogResult;
            window.Close();
        }
    }
}