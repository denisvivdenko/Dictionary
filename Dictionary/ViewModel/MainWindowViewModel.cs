using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Model;

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
    class MainWindowViewModel
    {
        // Fields that receive an input
        public string Word { get; set; }
        public string Definition { get; set; }
        public PartSpeech Part { get; set; }

        // Buttons interaction interface
        public Command AddButton { get; set; }
        public Command DelButton { get; set; }
        public Command EditButton { get; set; }

        // Binging collection for DataGrid
        public ObservableCollection<WordModel> WordCollection { get; set; }

        // Selected item in DataGrid
        public WordModel SelectedRow { get; set; }

        public MainWindowViewModel()
        {
            AddButton = new Command(AddAction);
            DelButton = new Command(DelAction);
            EditButton = new Command(EditAction);

            WordCollection = new ObservableCollection<WordModel>();
        }

        // Action when user click Add button
        private void AddAction()
        {
            WordModel word = new WordModel(Word, Definition, Part);
            WordCollection.Add(word);
        }

        // Action when user click Del button
        private void DelAction()
        {
            WordCollection.Remove(SelectedRow);
        }

        // Action when user click Edit button
        private void EditAction()
        {

        }
    }
}
 