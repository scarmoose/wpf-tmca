using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using wpf_tmca.Model;
using System.IO;
using System.Xml.Serialization;

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

        public Diagram LoadFromFile(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                XmlSerializer s = new XmlSerializer(typeof(Diagram));
                Diagram d = s.Deserialize(fs) as Diagram;
                return d;
            }
        }

        public async void AsyncSaveToFile(Diagram d, string path)
        {
            await Task.Run(() => SaveToFile(d, path));
        }

        public async Task<Diagram> AsyncLoadFromFile(string path)
        {
            return await Task.Run(() => LoadFromFile(path));
        }
    }
}
