using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ExamSharp;
using System.Reflection.PortableExecutable;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

/*
Разработать класс-коллекцию, который позволяет хранить список интервалов.
Любой интервал имеет начало и конец,которые заданы 2 числами типа double.
Интервал представляет собой отдельный класс.
*/

namespace ExamSharp
{
    public class Interval   //класс интервал
    {
        public double from;   // поля класса хранящие координаты
        public double to;

        public Interval(double from, double to)   //конструктор с параметрами, заполняющий поля класса значениями
        {
            if (from < to)
            {
                this.from = from;
                this.to = to;
            }
        }
            public double Length { get { return to - from; } } //свойство класса для получения длины

        //q.
        public override string ToString()
        {
            return $" [{from}, {to}]";
        }
    }
}


public class IntervalCollection
{
    protected List<Interval> intervals; //список интервалов

    public int Count { get { return intervals.Count; } } //свойство класса для получения количества интервалов в коллекции

    public bool HasHoles
    {
        get
        {
            for (int i = 1; i < intervals.Count; i++)
                if (intervals[i].from <= intervals[i - 1].to)
                    return true;
            return false;

        }
    }
    public double Start { get { return intervals[0].from; } } //cвойство получения первой координаты

    public double End { get { return intervals[intervals.Count - 1].to; } } //свойство получения последней координаты

    public IntervalCollection() //конструктор без параметров
    {
        intervals = new List<Interval>();
    }

    public IntervalCollection(Interval interval)  //конструктор с параметрами
    {
        intervals = new List<Interval>();
        intervals.Add(interval);
    }

    public IntervalCollection(List<Interval> intervals) //конструктор копирования (копирует один список интервалов в другой)
    {
        this.intervals = intervals;
    }

    public void Add(Interval interval) //метод добавления в список интервалов интервала
    {
        intervals.Add(interval);
    }

    public void RemoveByPos(int pos) //Метод удаления интервала из списка интервалов по индексу
    {
        intervals.Remove(intervals[pos]);
    }

    public void RemoveByValue(double value) //Метод удаления интервалов у которых начальная координата меньше значения
    {
        foreach (var interval in intervals)
            if (interval.from < value)
                intervals.Remove(interval);
    }

    public void RemoveByLength(double length) //Метод удаления из списка интервалов, у которых длинна меньше заданой
    {
        foreach (var interval in intervals)
            if (interval.Length < length)
                intervals.Remove(interval);
    }

    public Interval GetLongest() //получение самого длинного интервала
    {
        Interval longest = intervals[0]; ;
        foreach (var interval in intervals)
        {
            if (interval.Length > longest.Length)
                longest = interval;
        }
        return longest;
    }

    public Interval GetShortest() //получить самый короткий интервал 
    {
        Interval shortest = intervals[0]; ;
        foreach (var interval in intervals)
        {
            if (interval.Length < shortest.Length)
                shortest = interval;

        }
        return shortest;
    }
    public void Save(string path) //Метод сохранения коллекции интервалов в файл. Проходимся по каждому интервалу и записываем его в файл в формате от и до
    {
        StreamWriter file = new StreamWriter(path, false, Encoding.Default);
        
        foreach (var interval in intervals)
        {
            file.WriteLine("[" + interval.from + "," + interval.to + "]");
        }
       
        file.Close();
    }

  

public void Load(string path) //загрузка из файла, в массив строк помещаем текст прочитанный из файла
    {
        string[] fileIntervals = File.ReadAllText(path).Split(new string[] { ",", "\r\n", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < fileIntervals.Length; i += 2) 
        {
            intervals.Add(new Interval(double.Parse(fileIntervals[i]), double.Parse(fileIntervals[i+1])));
        }
    }

    public static IntervalCollection operator +(IntervalCollection intervals, Interval interval) //перегрузка оператора +, добавляет в коллекцию  интервалов интервал
                                                                                                 //вместе с оператором + автоматически перегружается оператор +=
    {
        intervals.intervals.Add(interval);
        return intervals;
    }

    public static bool operator ==(IntervalCollection intervals1, IntervalCollection intervals2) //перегрузка оператора равно и не равно,
                                                                                                 //сравниваем размеры если они не равны возвращаем true/false
    {
        if (intervals1.intervals.Count != intervals2.intervals.Count)
            return false;
        for (int i = 0; i < intervals1.intervals.Count; i++)
            if (intervals1[i].from != intervals2[i].from || intervals1[i].to != intervals2[i].to)
                return false;
        return true;
    
    }

    public static bool operator !=(IntervalCollection intervals1, IntervalCollection intervals2) //оператор не равно
    {
        if (intervals1.intervals.Count != intervals2.intervals.Count)
            return false;
        for(int i = 0; i < intervals1.Count; i++)
            if (intervals1[i].from == intervals2[i].from || intervals1[i].to != intervals2[i].to)
                return true;
        return false;
    }

    public Interval this[int index] 
    {
        get//Индексатор - возвращает элемент по индексу
        {
            return intervals.ElementAt(index);
        }
        set//Сет позволяет по индексу занести значение
        {
            if (index >= 0 && index < intervals.Count)
            {
                intervals[index] = value;
            }
        }
    }

    public IEnumerator GetEnumerator()//Энумератор позволяет выводить коллекцию
    {
        foreach (var item in intervals)
        {
            yield return item;  
        }
    }
  }

