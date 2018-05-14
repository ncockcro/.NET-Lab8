/*
 * Written by: Nicholas Cockcroft
 * Date: March 20, 2018
 * Course: .NET Environment
 * Assignment: Lab 8
 * 
 * Description: Write a program to read in a file and display a frequency distribution 
 * of the words that appear in the text.  Assume punctuation can be in the text.  Use hash tables. 
 * It would probably be a good idea to wait until I discuss regular expressions before doing this 
 * lab or read about the Split method in strings.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // File IO
using System.Text.RegularExpressions; // Regular Expressions
using System.Collections; // Hash Tables

namespace Lab8
{
    class lab8
    {
        static void Main(string[] args)
        {
            // Making sure there is only one command line argument (data.txt)
            if(args.Length != 1)
            {
                Console.WriteLine("Error: Could not find or open file.");
                return;
            }

            // Using regular expression to split up the text file
            // If there is a ',' ':' ' ''.'';' or a '?' we want to split on them and make sure
            // they are not being added to the words as the result
            string pattern = @"[,|:| |.|;|?]+";
            Regex regexPattern = new Regex(pattern);

            // Reading in the command line argument
            StreamReader sr = new StreamReader(args[0]);

            string fileLine;
            Hashtable ht = new Hashtable();

            // This while loop cycles through the file until it reaches the end
            while ((fileLine = sr.ReadLine()) != null)
            {
                // elts is a string array that splits up the line and indexs everyone word
                // that is on the line
                string[] elts = regexPattern.Split(fileLine);

                // Then we go through the indexes of elts and add to the hash table
                for (int i = 0; i < elts.Length; i++)
                {
                    // If a word is already in the hash table, then we just need to add 1 to the count
                    if (ht.Contains(elts[i]))
                    {
                        int currentCount = int.Parse(ht[elts[i]].ToString());

                        ht[elts[i]] = currentCount + 1;

                    }
                    // Otherwise, we add the word to the hash table and initialize the count to 1
                    else
                    {
                        ht.Add(elts[i], 1);
                    }
                }
            }

            // After everything has been added to the hash table, we output it to the console
            Console.WriteLine("Word            # of numbers appeared");
            foreach(DictionaryEntry de in ht)
            {
                if(de.Key.ToString() == "" || de.Key.ToString() == "?")
                {
                    continue;
                }
                Console.WriteLine(String.Format("{0,-15} | {1,-15} ","\"" + de.Key + "\"", de.Value));
            }
        }
    }
}
