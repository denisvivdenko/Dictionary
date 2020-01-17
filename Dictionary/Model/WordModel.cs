using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.ViewModel;

namespace Dictionary.Model
{
    class WordModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Definition { get; set; }
        public PartSpeech Part { get; set; }

        public WordModel(string word, string definition, PartSpeech part)
        {
            SettingsModel settings = SettingsModel.GetInstance();
            Word = word;
            Definition = definition;
            Part = part;
            Id = ++(settings.LastId); 
        }

        public WordModel(int id, string word, string definition, PartSpeech part)
        {
            Word = word;
            Definition = definition;
            Part = part;
            Id = id;
        }
    }
}
