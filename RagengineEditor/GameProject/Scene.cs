using System.Diagnostics;
using System.Runtime.Serialization;

namespace RagengineEditor.GameProject
{
    [DataContract]
    public class Scene : ViewModelBase
    {
        private string _sceneName;
        [DataMember]
        public string SceneName 
        {
            get => _sceneName;
            set
            {
                if(_sceneName  != value)
                {
                    _sceneName = value;
                    OnPropertyChanged(nameof(SceneName));
                }
            }
        }
        [DataMember]
        public Project Project { get; private set; }
        public Scene(Project project, string sceneName)
        {
            Debug.Assert(project != null);
            Project = project;
            SceneName = sceneName;
        }
    }
}
