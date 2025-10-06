using RagengineEditor.Utilities;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace RagengineEditor.GameProject
{
    [DataContract]
    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }
    }

    class NewProject : ViewModelBase
    {
        // TO DO: get the path from the installation location
        private readonly string _templatePath = @"..\..\RagengineEditor\ProjectTemplates";
        private string _name = "NewProject";
        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string _path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\RagengineProjects\";
        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }

        public NewProject()
        {
            try
            {
                var templateFiles = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templateFiles.Any());
                foreach(var file in templateFiles)
                {
                    var template = new ProjectTemplate()
                    {
                        ProjectType = "Empty Project",
                        ProjectFile = "project.ragnengine",
                        Folders = new List<string>() { ".Ragengine", "Content", "GameCode" }
                    };

                    Serializer.ToFile(template, file);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // TO DO: log error
            }
        }
    }
}
