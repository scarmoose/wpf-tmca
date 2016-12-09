using System;
using wpf_tmca.Model;
using System.IO;
using System.Xml.Serialization;
using wpf_tmca.ViewModel;
using wpf_tmca.ViewModel.Items;
using System.Collections.ObjectModel;
using wpf_tmca.ViewModel.Associations;

namespace wpf_tmca.SaveAndLoad
{
    class SaveLoadController
    {
        public static SaveLoadController Instance { get; } = new SaveLoadController();

        private SaveLoadController() { }

        public void SaveToFile(DiagramRepresentation d, string path)
        {
            using (FileStream fs = File.Create(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(DiagramRepresentation), new Type[] { typeof(Item), typeof(Association),
                    typeof(ClassViewModel), typeof(AssociationViewModel), typeof(DependencyViewModel) });
                s.Serialize(fs, d);
            }
        }

        public DiagramRepresentation LoadFromFile(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(DiagramRepresentation), new Type[] { typeof(Item), typeof(Association),
                    typeof(ClassViewModel), typeof(AssociationViewModel), typeof(DependencyViewModel) });
                DiagramRepresentation d = s.Deserialize(fs) as DiagramRepresentation;
                return d;
            }
        }

        //public Diagram LoadFromFile(string path)
        //{
        //    using (FileStream fs = File.OpenRead(path))
        //    {
        //        XmlSerializer s = new XmlSerializer(typeof(Diagram));
        //        Diagram d = s.Deserialize(fs) as Diagram;
        //        return d;
        //    }
        //}
        
        //public async void AsyncSaveToFile(Diagram d, string path)
        //{
        //    await Task.Run(() => SaveToFile(d, path));
        //}

        
    }
}
