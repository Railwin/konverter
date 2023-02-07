using System;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
namespace converter6practa
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Открыть файл");
            string openPath = Console.ReadLine();
            Console.Clear();
            File.WriteAllText(openPath, a_vot_eto_vot_eto_baza.Edit(MyConverter.ConvertToText(openPath)));
            Console.Clear();
            Console.WriteLine("Путь до файла чтобы сохранить");
            string putDlyaSohranenia = Console.ReadLine();
            MyConverter.SaveFile(openPath, putDlyaSohranenia);
            Console.Clear();
            Console.WriteLine("Сохранено!");

        }
    }
}