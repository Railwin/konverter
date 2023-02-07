using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Serialization;

namespace converter6practa
{
    public class a_vot_eto_vot_eto_baza
    {
        public static string Edit(string text)
        {
            string[] lines = text.Replace(" ", "").Split("\n");
            int pos = 1;
            int ex = 0;
            while (ex != 1)
            {
                Console.Clear();
                Console.WriteLine("Press f1 to exit");
                int max = 0;
                foreach (string line in lines)
                {
                    Console.WriteLine("   " + line);
                    max += 1;
                }
                Console.SetCursorPosition(0, pos);
                Console.Write("=>");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        for (int i = 0; i < lines[pos - 1].Length + 3; i++)
                            Console.Write(" ");
                        Console.SetCursorPosition(3, pos);
                        lines[pos - 1] = Console.ReadLine();
                        break;
                    case ConsoleKey.DownArrow:
                        if (pos == max)
                            pos = 1;
                        else
                            pos++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos == 1)
                            pos = max;
                        else
                            pos--;
                        break;
                    case ConsoleKey.F1:
                        ex++;
                        break;
                }
            }
            return String.Join("\n", lines);
        }
    }
    public class Figura
    {
        public string name;
        public int width;
        public int height;
        public Figura()
        {

        }
        public Figura(string Name, int Height, int Width)
        {
            name = Name;
            height = Height;
            width = Width;
        }
    }
    public class MyConverter
    {
        private static List<Figura> ConvertToObject(string file)
        {
            List<Figura> figury = new List<Figura>();
            if (file.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figura>));
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    figury = (List<Figura>)xml.Deserialize(fs);
                }
            }
            if (file.Contains(".json"))
            {
                figury = JsonConvert.DeserializeObject<List<Figura>>(File.ReadAllText(file));
            }

            if (file.Contains(".txt"))
            {
                string[] linii = File.ReadAllLines(file);
                for (int i = 0; i < linii.GetLength(0); i = i + 3)
                {
                    Figura figura = new Figura();
                    if (i != linii.GetLength(0))
                    {
                        figura.name = linii[i];
                    }
                    else break;
                    if (i + 1 != linii.GetLength(0))
                    {
                        figura.width = Convert.ToInt32(linii[i + 1]);

                    }
                    else break;
                    if (i + 2 != linii.GetLength(0))
                    {
                        figura.height = Convert.ToInt32(linii[i + 2]);
                    }
                    else break;
                    figury.Add(figura);
                }
            }
            return figury;
        }
        public static string ConvertToText(string file)
        {
            string text = "";
            List<Figura> figury = ConvertToObject(file);
            for (int i = 0; i < figury.Count(); i++)
            {
                text = text + figury[i].name + "\n";
                text = text + figury[i].height + "\n";
                text = text + figury[i].width + "\n";
            }
            return text;
        }
        public static void SaveFile(string oldFile, string newFile)
        {
            List<Figura> figury = ConvertToObject(oldFile);
            if (newFile.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figura>));
                using (FileStream fs = new FileStream(newFile, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, figury);
                }
            }
            if (newFile.Contains(".json"))
            {
                File.WriteAllText(newFile, JsonConvert.SerializeObject(figury));
            }
            if (newFile.Contains(".txt"))
            {
                File.WriteAllText(newFile, ConvertToText(newFile));
            }
        }
    }
}