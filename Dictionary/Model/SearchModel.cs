using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Model
{
    class SearchModel
    {
        public static BindingList<WordModel> SearchByLength(BindingList<WordModel> list, int from, int to)
        {
            BindingList<WordModel> newList = new BindingList<WordModel>();

            foreach (WordModel word in list)
            {
                int len = word.Word.Length;
                if (len >= from)
                {
                    if (to == -1)
                    {
                        newList.Add(word);
                    }
                    else if (len <= to)
                    {
                        newList.Add(word);
                    }
                }
            }

            return newList;
        }

    }
}
