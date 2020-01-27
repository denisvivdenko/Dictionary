using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.ViewModel;

namespace Dictionary.Model
{
    class WordModel : ObservableObject
    {
        private string _word;
        private DataBaseAccess db;
        private string _definition;
        private PartSpeech _part;
        public int Id { get; set; }
        public string Word
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
                OnPropertyChanged("Word");
                if (db != null)
                {
                    db.UpdateValue(this);
                }
            }
        }
        public string Definition
        {
            get
            {
                return _definition;
            }
            set
            {
                _definition = value;
                OnPropertyChanged("Definition");
                if (db != null)
                {
                    db.UpdateValue(this);
                }
            }
        }
        public PartSpeech Part
        {
            get
            {
                return _part;
            }
            set
            {
                _part = value;
                OnPropertyChanged("Part");
                if (db != null)
                {
                    db.UpdateValue(this);
                }
            }
        }

        public WordModel(string word, string definition, PartSpeech part)
        {
            SettingsModel settings = SettingsModel.GetInstance();
            Word = word;
            Definition = definition;
            Part = part;
            Id = ++(settings.LastId); 
            db = DataBaseAccess.GetInstance();
        }

        public WordModel(int id, string word, string definition, PartSpeech part)
        {
            Word = word;
            
            Definition = definition;
            Part = part;
            Id = id;
            db = DataBaseAccess.GetInstance();
        }
    }
}
