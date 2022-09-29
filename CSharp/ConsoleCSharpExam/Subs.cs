using System.Collections.Generic;
using System.Net;
using System.Text;
interface IServerable
{
    public void Messages(string msg);
}


// первый класс-подписчик
class Client1 : IServerable
{
    public void Messages(string filename)
    {
        StreamWriter Writer = new StreamWriter("data.log", true, Encoding.Default);
        Writer.WriteLine($"{filename} added at {DateTime.Now.ToString()}");
        Writer.Close();
    }
}

class Client2 : IServerable                  
{
    public string Path;
    public Client2(string path) { Path = path; }
    
    public void Messages(string filename)
    {
        StreamReader reader = new StreamReader(filename, Encoding.Default);
        Dictionary<string, int> dict = new Dictionary<string, int>();
        if (File.Exists("dict.txt"))
        {
            StreamReader dictReader = new StreamReader("dict.txt");
            for (string? line = dictReader.ReadLine(); line != null; line = dictReader.ReadLine())
            {
                string[] strArr = line.Split(':');
                Int32.TryParse(strArr[1], out int N);
                dict.Add(strArr[0], N);
            }
            dictReader.Close();
        }

        for (string? line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            List<string> words = line.Split(new string[] { " ", ",", "-", ":", ";", ".", "(", ")"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var word in words)
            {
                if (dict.ContainsKey(word))
                    dict[word] += 1;
                else
                    dict.Add(word, 1);
            }
        }
        reader.Close();

        StreamWriter Writer = new StreamWriter("dict.txt", false, Encoding.Default);
        foreach (var pair in dict)
            Writer.WriteLine($"{pair.Key}: {pair.Value}");
        Writer.Close();
    }
}