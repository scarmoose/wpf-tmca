using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using wpf_tmca.Model;
using System.IO;
using System.Xml.Serialization;
using wpf_tmca.ViewModel;
using wpf_tmca.ViewModel.Items;

namespace wpf_tmca.SaveAndLoad
{
    class SaveLoadController
    {
        public static SaveLoadController Instance { get; } = new SaveLoadController();

        private SaveLoadController() { }

        public void SaveToFile(Diagram d, string path)
        {
            using (FileStream fs = File.Create(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(Diagram));
                s.Serialize(fs, d);
            }
        }

        public void SaveToFile(ItemsCollection d, string path)
        {
            using (FileStream fs = File.Create(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(ItemsCollection), new Type[] { typeof(Item), typeof(ClassViewModel) });
                s.Serialize(fs, d);
            }
        }

        public ItemsCollection LoadFromFile(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(ItemsCollection), new Type[] { typeof(Item), typeof(ClassViewModel) });
                ItemsCollection d = s.Deserialize(fs) as ItemsCollection;
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
        
        public async void AsyncSaveToFile(Diagram d, string path)
        {
            await Task.Run(() => SaveToFile(d, path));
        }

        
    }
}
