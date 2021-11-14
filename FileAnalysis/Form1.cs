using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //open dialog for open file
        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "*.txt";
            openFile.Filter = "Text files (*.txt)|*.txt";
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //read the file and show results
                try
                {
                    richTextBox.Text = ReadFile(openFile.FileName);
                }
                catch
                {
                    richTextBox.Text = "לא ניתן למצוא או לפתוח קבצים בכתובת זאת";
                }
            }
        }

        //open file by text path
        private void openOnline_Click(object sender, EventArgs e)
        {
            string path = textPath.Text;
            try
            {
                richTextBox.Text = ReadFile(path);
            }
            catch
            {
                richTextBox.Text = "לא ניתן למצוא או לפתוח קבצים בכתובת זאת";
            }
        }
        //the button be active only if have path
        private void textPath_TextChanged(object sender, EventArgs e)
        {
            openOnline.Enabled = textPath.Text != "";
        }


        //function to read file and  data analysis
        private string ReadFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            //path to return data analysis
            int indexStart = fileName.LastIndexOf(@"\");
            int indexEnd = fileName.LastIndexOf(@".");
            string name = fileName.Substring(indexStart + 1, indexEnd - indexStart - 1);
            string path = fileName.Substring(0, indexStart);//@"C:\Users\user\Downloads"; //
          
            //Statistics Report for the select file
            StreamWriter sw = new StreamWriter(path + @"\" + name + @"_StatisticsReport.txt");
            try
            {
                //data analysis
                string fileText = sr.ReadToEnd();//text from the file
                long numOfLines = CounterLines(fileText);//num of lines
                long numOfWords = CounterWords(fileText);//num of Words
                long numOfUniqWords = CounterUniqWords(fileText);//num of uniq words
                //max and average length of sentences at file
                int[] max_avg = MaxAndAvgLenOfSentence(fileText);
                double avgLenOfSentence = max_avg[1];
                int maxLenOfSentence = max_avg[0];
                string popularWord = popularWordInText(fileText);//the popular word
                int longestWordSequenceWithoutLetter = getLongestWordSequenceWithoutLetter(fileText, 'k');//longest word sequence without lettet k
                Dictionary<string, int> colorsCount = getColorsNameInTextAndCount(fileText);//color names and count in the file
                                                                                            // long largestNum = largestNumber(fileText);//largest num in the file



                string dataFromText = "";
                //write Data report
                dataFromText += "\nnum of lines: " + numOfLines;
                dataFromText += "\nnum of words: " + numOfWords;
                dataFromText += "\nnum of uniq words: " + numOfUniqWords;
                dataFromText += "\navg length of sentence: " + avgLenOfSentence;
                dataFromText += "\nmax length of sentence: " + maxLenOfSentence;
                dataFromText += "\nThe popular word in the file: " + popularWord;
                dataFromText += "\nlongest word sequence without letter k: " + longestWordSequenceWithoutLetter;
                
                //dataFromText += "\nlargest number in the file: " + largestNum;
                dataFromText += "\ncolors name and count that be in the file: ";
                foreach (var color in colorsCount)
                {
                    dataFromText += "\n     " + color.Key+"-"+color.Value;
                }
                if(colorsCount.Count()==0)
                {
                    dataFromText += "\n     ---not have colors name---";
                }

                sw.WriteLine(dataFromText);
                return dataFromText;
            }
            catch
            {
                return "";
            }
            finally
            {
                sr.Close();
                sw.Close();
            }
        }


        //function to count lines in the text
        private int CounterLines(string text)
        {
            int counter = 0;
            string[] lines = text.Split(new string[] { Environment.NewLine,"\n" }, StringSplitOptions.None);
            counter = lines.Length;
            return counter;
        }
        //function to count words in the text
        private long CounterWords(string text)
        {
            string[] words = text.Split(new string[] { Environment.NewLine, " ", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        //function to count uniq words in the text
        private long CounterUniqWords(string text)
        {
            text = text.ToLower();//do all text in lower case
            string[] words = text.Split(new string[] { Environment.NewLine, " ", "\n", ",", ".", ":", ";","?","!","(",")" }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> wordswithoutDuplicates = new HashSet<string>();//All words without duplicates
            Dictionary<string, long> counterWords = new Dictionary<string, long>();
            //over on the words
            foreach (var word in words)
            {
                //if thus word was contain
                if(counterWords.ContainsKey(word))
                {
                    //add counter
                    long count = counterWords.GetValueOrDefault(word);
                    counterWords[word] = count+1;
                }
                else
                {
                    //add new word
                    counterWords.Add(word, 1);
                }
            }
            //check whitch words uniq
            long countUniq = 0;
            foreach (var item in counterWords)
            {
                if (item.Value == 1)
                {
                    countUniq++;
                }
            }
            return countUniq;
        }

        //function to return array of sentence from the text
        private string[] getArrayOfSentenceOfString(string text)
        {
            string[] sentences = text.Split(new string[] { Environment.NewLine, ". ", "..", "!", "?",";","\n" }, StringSplitOptions.RemoveEmptyEntries);
            return sentences;
        }

        //function to find max and average length of sentence
        private int[] MaxAndAvgLenOfSentence(string text)
        {
            string[] sentence = getArrayOfSentenceOfString(text);//array for sentence from the text
            string[] wordsForSentence = text.Split(new string[] { Environment.NewLine, " ","\n" }, StringSplitOptions.RemoveEmptyEntries);

            wordsForSentence= sentence[0].Split(new string[] { Environment.NewLine, " ","\n" }, StringSplitOptions.RemoveEmptyEntries);
            int max = wordsForSentence.Length;
            int sum = 0;
            foreach (var s in sentence)
            {
                wordsForSentence = s.Split(new string[] { Environment.NewLine, " ","\n" }, StringSplitOptions.RemoveEmptyEntries);
                sum += wordsForSentence.Length;
                if (wordsForSentence.Length > max)
                {
                    max = wordsForSentence.Length;
                }
            }
            //results
            int[] results = { max, sum / sentence.Length };
            return results;
        }
        
        //function to find longest word sequence without letter
        private int getLongestWordSequenceWithoutLetter(string text, char c)
        {
            text = text.ToLower();//do all text in lower case
            string ch =""+c;
            c = (ch.ToLower())[0];
            int count = 0;//counter for length of sequence
            int max = 0;//max length of sequence
            string[] words = text.Split(new string[] { Environment.NewLine, " " ,"\n"}, StringSplitOptions.RemoveEmptyEntries);//all the words
           //over on words
            foreach (var word in words)
            {
                //if current word contain this letter
                if (word.Contains(c))
                {
                    //check if this sequence more big from the last bigest sequence
                    if (count > max)
                    {
                        max = count;
                    }
                    count = 0;
                }
                //if word doen't conatin this letter, counter+1
                else count++;
            }
            //check for last sequence
            if (count > max)
            {
                max = count;
            }
            return max;
        }

        //array from colors
        string[] colorsName = { "black", "white", "brown", "orange", "blue", "pink", "yellow", "grey", "green", "purple", "red", "gold", "silver"};

        //function to find colors name at the file and hou much times the colors be
        private Dictionary<string, int> getColorsNameInTextAndCount(string text)
        {
            Dictionary<string, int> colors = new Dictionary<string, int>();//color that be at file
            text = text.ToLower();//do all text in lower case
            string[] words = text.Split(new string[] { Environment.NewLine, " ", "\n", ",", ".", ":", ";", "?", "!", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);//all the words from file
            int count = 0;//counter
            //over on the words
            foreach (var word in words)
            {
                //check if the word is color name
                foreach (var color in colorsName)
                {
                    if (word.Equals(color))
                    {
                        //check if this color be at the dictionary
                        if (colors.ContainsKey(color))
                        {
                            //add counter
                            count = colors.GetValueOrDefault(color);
                            colors[color]=count+1;
                        }
                        else//add this color to dictionary
                        {
                            colors.Add(color, 1);
                        }
                        break;
                    }
                }
            }
            return colors;
        }

     
        //function to find poppular word in the text
        private string popularWordInText(string text)
        {
            text = text.ToLower();//do all text in lower case
            string[] words = text.Split(new string[] { Environment.NewLine, " ", "\n", ",", ".", ":", ";", "?", "!", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);//all the words
            Dictionary<string, int> popularWord = new Dictionary<string, int>();//all the word from the text
            //Conjunctions
            string[] Conjunctions = { "am", "are", "is", "amn't", "aren't", "isn't", "do", "does", "don't", "doesn't", "that", "the", "is", "not", "will", "won't", "did", "didn't", "was", "where", "wasn't", "wheren't","'", "than","and","of","to","a","an","in" };
            Boolean b = false;//Indicates whether the word is Conjunctions
            //over on the words
            foreach (string word in words)
            {
                //check if current word isn't Conjunctions
                foreach (var c in Conjunctions)
                {
                    if (word.Equals(c))
                    {
                        b = true;
                    }
                }
                //add to dictionary
                if (b == false)
                {
                    //if already apper, add counter
                    if (popularWord.ContainsKey(word))
                    {
                        int value = popularWord.GetValueOrDefault(word);
                        popularWord[word] = value + 1;
                    }
                    //add new word to dictionary
                    else { popularWord.Add(word, 1); }
                }
                b = false;
            }

            string Smax = "";
            int max = 0;
            //check witch word was most popular
            foreach (var item in popularWord)
            {
                if (item.Value > max)
                {
                    Smax = item.Key;
                    max = item.Value;
                }
            }

                return Smax;
            }

        //function to find largest number 
        private long largestNumber(string text)
        {
            text = text.ToLower();//do all text in lower case
            string[] words = text.Split(new string[] { Environment.NewLine, " ", "\n", ",", ".", ":", ";", "?", "!", "(", ")","-" }, StringSplitOptions.RemoveEmptyEntries);//all the words from the text
            long maxNum = 0;
            //all the basic numbers
            Dictionary<long, string> numbers = new Dictionary<long, string>()
            {{0, "zero" },{1,"one" },{2,"two" },{3,"three" },{4,"four" },{5,"five" },{6,"six" },{7, "seven" },{8,"eight" },{9,"nine" },{10, "ten" },{11,"eleven" },{12,"twelev" },{13,"thirteen"},{14,"fourteen" }, {15,"fifteen" },{16,"sixteen" },{17,"seventeen" }, {18,"eighteen" },{19,"nineteen" },{20,"twenty" },{30,"thirty" }, {40,"forty" },{50,"fifty" },{60,"sixty" },{70,"seventy" },{80,"eighty" }, {90,"ninety" },{100,"hundred" },{1000,"thousand" },{1000000,"million" },{1000000000,"billion"} };
 
            string word = "";
            //over on the word
            for(long i=0;i<words.Length;i++)
            {
                word = words[i];
                //check if is number
                try
                {
                    long num = long.Parse(word);
                    if (num > maxNum)
                    {
                        maxNum = num;
                    }
                }
                //check if is number in words
                catch
                {
                    Boolean ifNum = true;
                    long numfromStr = 0;
                   
                    Stack<long> s = new Stack<long>();//stack to help cast the number from words
                    //As long as I have consecutive numbers
                    while (ifNum == true)
                    {
                        ifNum = false;

                       //check if is num at word and push to stack
                        foreach (var num in numbers)
                        {
                            if (word.Equals(num.Value))
                            {
                                ifNum = true;
                                s.Push(num.Key);
                                break;
                            }
                        }
                        //Promoting iteration
                        if (word.Equals("and"))
                        {
                            ifNum = true;
                        }
                        //if current word was last word in the text
                        if ((i + 1) >= words.Length)
                        {
                            ifNum = false;
                        }
                        if (ifNum == true)
                        {
                            word = words[i + 1];
                            i++;
                        }
                    }

                    //Convert words to a number
                    while (s.Count>0)
                    {
                        long n = s.Pop();
                        //if the befor number was more small from curren, גouble them
                        while (s.Count > 0&&s.Peek() < n)
                        {
                            n *= s.Pop();
                        }
                        //add to the num
                        numfromStr += n;
                    }
                    //check if current num  more big from last bigest number
                    if(numfromStr>maxNum)
                    {
                        maxNum = numfromStr;
                    }
                }
            }
            return maxNum;
        }
    }
}
