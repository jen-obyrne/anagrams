using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace anagrams
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var thing = DetermineNumbersToDelete("fcrxzwscanmligyxyvym", "jxwtrhvujlmrpdoqbisbwhmgpmeoke");
            Assert.Equal(30, thing);
        }

        public static int DetermineNumbersToDelete(string a, string b)
        {
            var letters = a.Select(x => x.ToString()).ToArray();
            var sortedLetters = b.Select(x => x.ToString()).ToArray();
            Array.Sort(sortedLetters);
            
            for(var i = 0; i < letters.Length && sortedLetters.Length > 0; i++)
            {
                var first = 0;
                var last = sortedLetters.Length - 1;
                var mid = 0;
                var matchFound = false;
                
                while (first <= last && !matchFound)
                {
                    mid = (first + last) / 2; 
                    //if our string is smaller, then go left
                    if (string.Compare(letters[i].ToString(), sortedLetters[mid].ToString()) < 0)
                    {
                        last = mid - 1;
                    } 
                    else if (string.Compare(letters[i].ToString(), sortedLetters[mid].ToString()) > 0)
                    {
                        first = mid + 1;
                    }
                    else 
                    {
                        //if letter is found in the letters then remove it
                        letters = RemoveMatchingCharacters(letters, i);
                        sortedLetters = RemoveMatchingCharacters(sortedLetters, mid);
                        matchFound = true;

                        //bump i back down so that it will correctly start the index we just removed
                        i--;
                    }
                }
            }
            
            return letters.Length + sortedLetters.Length;
        }

        public static string[] RemoveMatchingCharacters(string[] list, int indexToRemove)
        {
            var letters = new List<string>(list);
            letters.RemoveAt(indexToRemove);
            return letters.ToArray();
        }
    }
}
