using RagengineEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace RagengineEditor.GameProject
{
    [DataContract]
    public class ProjectData
    {
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string ProjectPath { get; set; }
        [DataMember]
        public DateTime LastOpened { get; set; }
        public string FullPath { get => $"{ProjectPath}{ProjectName}{Project.Extension}"; }
        public byte[] Icon { get; set; }
        public byte[] Screenshot { get; set; }
    }

    [DataContract]
    public class ProjectDataList
    {
        [DataMember]
        public List<ProjectData> Projects { get; set; }
    }

    class OpenProject
    {
        private static readonly string _applicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RagengineEditor");
        private static readonly string _projectDataPath;
        private static readonly ObservableCollection<ProjectData> _projects = new ObservableCollection<ProjectData>();
        public static ReadOnlyObservableCollection<ProjectData> Projects { get; }

        static OpenProject()
        {
            try
            {
                if(!Directory.Exists(_applicationDataPath))
                    Directory.CreateDirectory(_applicationDataPath);
                _projectDataPath = Path.Combine(_applicationDataPath, "ProjectsList.xml");
                Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
                ReadProjectData();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // TO DO: log error
            }
        }

        private static void ReadProjectData()
        {
            if(File.Exists(_projectDataPath))
            {
                var projects = Serializer.FromFile<ProjectDataList>(_projectDataPath).Projects.OrderByDescending(x => x.LastOpened);
                _projects.Clear();
                foreach(var project in projects)
                {
                    if(File.Exists(project.FullPath))
                    {
                        project.Icon = File.ReadAllBytes(Path.GetFullPath(Path.Combine(project.ProjectPath, ".Ragengine", "Icon.png")));
                        project.Screenshot = File.ReadAllBytes(Path.GetFullPath(Path.Combine(project.ProjectPath, ".Ragengine", "Screenshot.png")));
                        _projects.Add(project);
                    }
                }
            }
        }

        private static void WriteProjectData()
        {
            var projects = _projects.OrderBy(x => x.LastOpened).ToList();
            Serializer.ToFile(new ProjectDataList() { Projects = projects }, _projectDataPath);
        }

        public static Project Open(ProjectData data)
        {
            ReadProjectData();
            var project = _projects.FirstOrDefault(x => x.FullPath == data.FullPath);
            if(project != null)
            {
                project.LastOpened = DateTime.Now;
            }
            else
            {
                project = data;
                project.LastOpened = DateTime.Now;
                _projects.Add(project);
            }
            WriteProjectData();
            return Project.Load(project.FullPath);
        }        
    }
}