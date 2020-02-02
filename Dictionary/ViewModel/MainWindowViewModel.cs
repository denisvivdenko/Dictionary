using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dictionary.Model;
using Dictionary.View;

namespace Dictionary.ViewModel
{
    // Enum that represents part of speech
    public enum PartSpeech
    {
        Unknown,
        Noun,
        Pronoun,
        Verb,
        Adjective,
        Adverb,
        Preposition,
        Conjunction,
        Articles,
        Interjection
    }
    public enum FilterPartSpeech
    {
        All,
        Unknown,
        Noun,
        Pronoun,
        Verb,
        Adjective,
        Adverb,
        Preposition,
        Conjunction,
        Articles,
        Interjection
    }

    class MainWindowViewModel : ObservableObject
    {
        private MainWindow view;
        private bool _isCheckedEditMode;
        private string _searchRequest;
        private FilterPartSpeech _filterPart;
        private int _from;
        private int _to;

        public SettingsModel settings;
        public DataBaseAccess dataAccess;

        // Fields that receive an input
        public string Word { get; set; }
        public string Definition { get; set; }
        public PartSpeech Part { get; set; }

        // Buttons interaction interface
        public Command AddButton { get; set; }
        public Command DelButton { get; set; }

        // Binging collection for DataGrid
        public BindingList<WordModel> WordCollection { get; set; }
       
        // filters 
        public FilterPartSpeech FilterPart
        {
            get
            {
                return _filterPart;
            }
            set
            {
                _filterPart = value;
                SearchRequest = _searchRequest;
            }
        }

        public string FromLength
        {
            get
            {
                return _from.ToString();
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _from = int.Parse(value);
                } 
                else
                {
                    _from = 0;
                }
                SearchRequest = _searchRequest;
            }
        }

        public string ToLength
        {
            get
            {
                return _to.ToString();
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _to = int.Parse(value);
                } 
                else
                {
                    _to = -1;
                }
                SearchRequest = _searchRequest;
            }
        }

        // Selected item in DataGrid
        public WordModel SelectedWord { get; set; }

        public bool IsCheckedEditMode
        {
            get
            {
                return _isCheckedEditMode;
            }
            set
            {
                _isCheckedEditMode = value;
                OnPropertyChanged("IsCheckedEditMode");
            }
        }

        public string SearchRequest
        {
            get
            {
                return _searchRequest;
            }
            set
            {
                _searchRequest = value;
                BindingList<WordModel> searchedList;
                if (String.IsNullOrEmpty(_searchRequest) && FilterPart == FilterPartSpeech.All
                    && (_from != 0) && (_to != -1))
                {
                    WordCollection = dataAccess.LoadWords();
                }                
                else
                {
                    WordCollection = SearchModel.SearchByLength(dataAccess.SearchRequest(value, FilterPart), _from, _to); 
                }
                OnPropertyChanged("WordCollection");
            }
        }

        public MainWindowViewModel(MainWindow view)
        {   
            AddButton = new Command(AddAction);
            DelButton = new Command(DelAction);

            settings = SettingsModel.GetInstance();
            dataAccess = DataBaseAccess.GetInstance();
            WordCollection = dataAccess.LoadWords();

            this.view = view;
        }

        // Action when user click Add button
        private void AddAction()
        {
            WordModel word = new WordModel(Word, Definition, Part);
            settings.WordsAmount += 1;
            WordCollection.Add(word);
            dataAccess.SaveWord(word);

            // this is for update the search request
            SearchRequest = _searchRequest;
        }

        // Action when user click Del button
        private void DelAction()
        {
            if (SelectedWord != null)
            {
                settings.WordsAmount -= 1;
                dataAccess.RemoveWord(SelectedWord);
                WordCollection.Remove(SelectedWord);
            }
        }
    }
}
 