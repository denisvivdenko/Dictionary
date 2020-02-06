  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Collections.ObjectModel;
using Dictionary.ViewModel;
using System.ComponentModel;

namespace Dictionary.Model
{
    class DataBaseAccess
    {
        private static DataBaseAccess instance;
        private SQLiteConnection myConnection;
        private string DB_PATH = SettingsModel.PATH + "//WordList.sqlite3";
        private DataBaseAccess()
        {
            myConnection = new SQLiteConnection($"Data Source = {DB_PATH}");
            if (!File.Exists(DB_PATH))
            {
                SQLiteConnection.CreateFile(DB_PATH);
                CreateTable();
            }

            openConnection(); 
        }
        
        // realization of singleton because it is a database interface
        public static DataBaseAccess GetInstance()
        {
            if (instance == null)
            {
                return new DataBaseAccess();
            }
            return instance;
        }

        private void CreateTable()
        {
            myConnection.Open();
            string query = @"CREATE TABLE 'Words' (
                        'Id' INTEGER NOT NULL,
	                    'Word' TEXT NOT NULL,
                        'Part' TEXT NOT NULL,
	                    'Definition' TEXT NOT NULL
                        );";
            SQLiteCommand command = new SQLiteCommand(query, myConnection);
            command.ExecuteNonQuery();

        }

        public void SaveWord(WordModel word)
        {
            string query = @"INSERT INTO Words ('Id', 'Word', 'Part', 'Definition')
                            VALUES (@id, @word, @part, @definition);";

            SQLiteCommand command = new SQLiteCommand(query, myConnection);
            command.Parameters.AddWithValue("@id", word.Id);
            command.Parameters.AddWithValue("@word", word.Word);
            command.Parameters.AddWithValue("@part", word.Part.ToString());
            command.Parameters.AddWithValue("@definition", word.Definition);
            command.ExecuteNonQuery();
        }

        public void RemoveWord(WordModel word)
        {
            string query = @"DELETE FROM Words WHERE Id=@id";
            SQLiteCommand command = new SQLiteCommand(query, myConnection);
            command.Parameters.AddWithValue("@id", word.Id);
            command.ExecuteNonQuery();
        }

        // load all words that are in database 
        public BindingList<WordModel> LoadWords()
        {
            BindingList<WordModel> list = new BindingList<WordModel>();
            string query = "SELECT * FROM Words";
            SQLiteCommand command = new SQLiteCommand(query, myConnection);
            using (SQLiteDataReader data = command.ExecuteReader())
            {
                WordModel Word;
                while (data.Read())
                {
                    int id = data.GetInt32(0);
                    string word = data.GetString(1);
                    PartSpeech part;
                    Enum.TryParse<PartSpeech>(data.GetString(2), out part);
                    string definition = data.GetString(3);

                    Word = new WordModel(id, word, definition, part);

                    list.Add(Word);
                }
            }

            return list;
        }

        // search for words that contains substring 
        public BindingList<WordModel> SearchRequest(string filterWord, FilterPartSpeech filterPart)
        {
            BindingList<WordModel> list = new BindingList<WordModel>();
            string query;
            if (filterPart == FilterPartSpeech.All) // if filter part is not choosen
            {
                query = string.Format("SELECT * FROM Words WHERE Word LIKE '%{0}%' ORDER BY Word;", filterWord);
            }
            else
            {
                // query for both part and word
                query = string.Format("SELECT * FROM Words WHERE Word LIKE '%{0}%' AND Part = '{1}' ORDER BY Word;", filterWord, filterPart.ToString());

                if (String.IsNullOrEmpty(filterWord)) // extra query if word search is empty
                {
                    query = string.Format("SELECT * FROM Words WHERE Part = '{0}' ORDER BY Word;", filterPart.ToString());
                }
            }

            SQLiteCommand command = new SQLiteCommand(query, myConnection);
            using (SQLiteDataReader data = command.ExecuteReader())
            {
                WordModel Word;
                while (data.Read())
                {
                    int id = data.GetInt32(0);
                    string word = data.GetString(1);
                    PartSpeech part;
                    Enum.TryParse<PartSpeech>(data.GetString(2), out part);
                    string definition = data.GetString(3);

                    Word = new WordModel(id, word, definition, part);

                    list.Add(Word);
                }
            }
            return list;
        }

        // update value in the database when it is changed
        public void UpdateValue(WordModel updatedWord)
        {
            string query = @"UPDATE Words
                            SET Word = @word,
                                Definition = @definition,
                                Part = @part
                            WHERE
                                Id = @id;";
            SQLiteCommand command = new SQLiteCommand(query, myConnection);
            command.Parameters.AddWithValue("@id", updatedWord.Id);
            command.Parameters.AddWithValue("@word", updatedWord.Word);
            command.Parameters.AddWithValue("@part", updatedWord.Part);
            command.Parameters.AddWithValue("definition", updatedWord.Definition);

            command.ExecuteNonQuery();
        }
            
        private void openConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        private void closeConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
