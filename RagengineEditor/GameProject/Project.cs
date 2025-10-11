using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace RagengineEditor.GameProject
{
    [DataContract(Name = "Game")]
    public class Project : ViewModelBase
    {
        public static string Extension { get; } = ".ragengine";
        [DataMember]
        public string ProjectName { get; private set; }
        [DataMember]
        public string ProjectPath { get; private set; }
        public string FullPath => System.IO.Path.Combine(ProjectPath, ProjectName + Extension);
        [DataMember(Name = "Scenes")]
        private ObservableCollection<Scene> _scenes = new ObservableCollection<Scene>();
        public ReadOnlyObservableCollection<Scene> Scenes { get; }
        public Project(string projectName, string projectPath)
        {
            ProjectName = projectName;
            ProjectPath = projectPath;
            _scenes.Add(new Scene(this, "DefaultScene"));
        }
    }
}
