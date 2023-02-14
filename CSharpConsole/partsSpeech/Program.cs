using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp;

class Program
{
    static string path = "/home/omon/dir";

    public static void Main(string[] args)
    {
        string[] mest = {"меня", "тебя", "тебе", "нами",
        "вами", "этом", "если", "потому", };

        void readFiles(string path, List<string> verbs, List<string> adj, List<string> nouns)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);

            if (dinfo.Exists)
            {
                try
                {
                    FileInfo[] files = dinfo.GetFiles();
                    foreach (FileInfo current in files)
                    {
                        if (current.Extension != ".txt")
                            continue;

                        StreamReader reader = new StreamReader(current.FullName, Encoding.Default);

                        for (string? line = reader.ReadLine(); line != null; line = reader.ReadLine())
                        {
                            foreach (string word in line.Split(new string[] { ",", ".", " ", "!", "?", "—", ";" }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (Int32.TryParse(word, out var N))
                                    continue;
                                if (word.Count() < 4)
                                    continue;
                                if (word.EndsWith("ть") || word.EndsWith("ат") || word.EndsWith("ят") || word.EndsWith("ут") || word.EndsWith("ют") || word.EndsWith("ит")
                                    || word.EndsWith("ся"))
                                {
                                    verbs.Add(word);
                                    continue;
                                }
                                if (word.EndsWith("ый") || word.EndsWith("ая") || word.EndsWith("ое") || word.EndsWith("ой") || word.EndsWith("ые")
                                || word.EndsWith("ое") || word.EndsWith("ые") || word.EndsWith("их") || word.EndsWith("ым") || word.EndsWith("ых")
                                || word.EndsWith("ие") || word.Contains("ее"))
                                {
                                    adj.Add(word);
                                    continue;
                                }
                                if (mest.Contains(word))
                                    continue;

                                nouns.Add(word);
                            }
                        }
                        reader.Close();
                    }

                    DirectoryInfo[] dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                        readFiles(current.FullName, verbs, adj, nouns);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
                Console.WriteLine("Path is not exists");
        }

        List<string> verbs = new List<string>();
        List<string> adj = new List<string>();
        List<string> nouns = new List<string>();
        readFiles(path, verbs, adj, nouns);

        StreamWriter verbsWriter = new StreamWriter("verbs.txt", false, Encoding.Default);
        StreamWriter adjWriter = new StreamWriter("adj.txt", false, Encoding.Default);
        StreamWriter nounsWriter = new StreamWriter("nouns.txt", false, Encoding.Default);


        foreach (var item in verbs)
            verbsWriter.WriteLine(item);
        foreach (var item in adj)
            adjWriter.WriteLine(item);
        foreach (var item in nouns)
            nounsWriter.WriteLine(item);

        verbsWriter.Close();
        adjWriter.Close();
        nounsWriter.Close();
    }
}