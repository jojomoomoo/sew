using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SewConsole
{
    /*
       Write a program that reads a file containing a sorted list of words (one word per line, no spaces, all lower case), then identifies  
        1. The longest word in the file that can be constructed by concatenating copies of shorter words also found in the file.  
        2. The program should then go on to report the 2nd longest word found  
        3. Total count of how many of the words in the list can be constructed of other words in the list. 
     */

    class Dictionary
    {
        public List<string> Scraps { get; set; }
        public List<string> Words { get; set; }
        public List<string> WordsFromScraps { get { return DetermineWordsFromScraps(); } }
        public int WordsWithinWords { get { return CountWordsWithinWords(); } }

        public Dictionary()
        {
            Scraps = new List<string>(System.IO.File.ReadAllLines(@"NET Test 00.txt"));
            Words = new List<string>(System.IO.File.ReadAllLines(@"words.txt"));
            Scraps.Sort();
            Words.Sort();
        }

        public void DisplayScraps()
        {
            foreach (string scrap in Scraps)
            {
                Console.WriteLine("\t" + scrap);
            }
        }

        public void DisplayWords()
        {
            foreach (string word in Words)
            {
                Console.WriteLine("\t" + word);
            }
        }

        public void DisplayWordsFromScraps()
        {
            foreach (string word in WordsFromScraps)
            {
                Console.WriteLine("\t" + word);
            }
        }

        public void DisplayAnswers()
        {

            Console.WriteLine("\t1. Longest word = " + string.Join("", WordsFromScraps));
            var secondLongest = WordsFromScraps.Remove(WordsFromScraps[0]);
            Console.WriteLine("\t1. Second longest word = " + string.Join("", secondLongest));
            Console.WriteLine("\t1. How many words are in other words = " + WordsWithinWords.ToString());
        }


        public List<string> DetermineWordsFromScraps()
        {
            List<string> wordsFromScraps = new List<string>();

            foreach (string scrap in Scraps)
            {
                if (scrap == "")
                {
                    continue;
                }
             
                foreach (string word in Words)
                {                                     
                    if (scrap.Trim() == word.Trim())
                    {
                        Console.WriteLine($"\tFound Word: {word}");
                        wordsFromScraps.Add(scrap);                    
                        break;
                    }                  
                }                                
            }

            return wordsFromScraps;
        }
        // was attempting to speed up search for words

        //public List<string> DetermineWordsFromScraps()
        //{
        //    List<string> wordsFromScraps = new List<string>();
        //    List<string> tempWords1 = Words;
        //    List<string> tempWords2 = Words;
        //    bool isFirst = true;

        //    foreach (string scrap in Scraps)
        //    {
        //        if (scrap == "")
        //        {
        //            continue;
        //        }

        //        if (isFirst == true)
        //        {
        //            tempWords2 = new List<string>(tempWords1);

        //            foreach (string word1 in tempWords1)
        //            {
        //                tempWords2.Remove(word1);

        //                if (char.IsUpper(word1[0]))
        //                {
        //                    continue;
        //                }

        //                if (scrap.Trim() == word1.Trim())
        //                {
        //                    Console.WriteLine($"\tFound Word 1: {word1}");
        //                    wordsFromScraps.Add(scrap);
        //                    isFirst = false;
        //                    break;
        //                }

        //                Console.WriteLine("\t" + "Word 1: " + word1);
        //            }
        //        }
        //        else
        //        {

        //            tempWords1 = new List<string>(tempWords2);

        //            foreach (string word2 in tempWords2)
        //            {
        //                tempWords1.Remove(word2);

        //                if (char.IsUpper(word2[0]))
        //                {
        //                    continue;
        //                }

        //                if (scrap.Trim() == word2.Trim())
        //                {
        //                    Console.WriteLine("\t" + "Found Word 2: " + word2);
        //                    wordsFromScraps.Add(scrap);
        //                    isFirst = true;
        //                    break;
        //                }

        //                Console.WriteLine("\t" + "Word 2: " + word2);
        //            }
        //        }

        //        Console.WriteLine("\t" + "Scrap: " + scrap);
        //    }

        //    return wordsFromScraps;
        //}

        public int CountWordsWithinWords()
        {
            int i = 0;
            var longWordList = string.Join("", WordsFromScraps);

            foreach (var word in WordsFromScraps)
            {
                i += i + Regex.Matches(Regex.Escape(longWordList), word).Count;
            }

            return i;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var dictionary = new Dictionary();            
            dictionary.DisplayAnswers();

            Console.ReadLine();
        }
    }
}