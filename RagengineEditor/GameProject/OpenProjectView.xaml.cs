using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RagengineEditor.GameProject
{
    /// <summary>
    /// Interaction logic for OpenProjectView.xaml
    /// </summary>
    public partial class OpenProjectView : UserControl
    {
        public OpenProjectView()
        {
            InitializeComponent();
        }

        private void On_Open_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedProject();
        }

        private void On_ListBoxItem_Mouse_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSelectedProject();
        }

        private void OpenSelectedProject()
        {
            var project = OpenProject.Open(projectsListBox.SelectedItem as ProjectData);
            bool dialogResult = false;
            var window = Window.GetWindow(this);
            if(project != null)
            {
                dialogResult = true;
                window.DataContext = project;
            }
            window.DialogResult = dialogResult;
            window.Close();
        }
    }
}