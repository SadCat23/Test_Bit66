using System;
using ClassLibrary1;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

namespace ConsoleApp5
{
    class Program
    {
        static public Dictionary<string, List<string>> dllParser(string path)
        {
            //словарь где ключом является имя класса а значениями именна методов
            Dictionary<string, List<string>> all_methods = new Dictionary<string, List<string>>
            {
                
            };
            //получаем массив имен всех dll файлов в директории
            string[] allDll = Directory.GetFiles(path, "*.dll");
            //перебираем все dll файлы в директории
            foreach (string dll in allDll)
            {
                //загружаем dll библиотеку 
                Assembly asm = Assembly.LoadFrom(dll);
               
                // получаем массив описанных в библиотеке типов
                Type[] types = asm.GetTypes();

                //перебираем все типы (классы)
                foreach (Type t in types)
                {
                    List<string> methods_name = new List<string>();
                    Console.WriteLine(t.Name);
                   //получам массив все объявленных методов класса 
                    MethodInfo[] methods = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    foreach (MethodInfo met in methods)
                    {
                        //Console.Write("    -");
                       // Console.WriteLine(met.Name);
                       //формируем список методов
                        methods_name.Add(met.Name);
                    }
                    //из списка методов и имени класса формируем словарь 
                    all_methods.Add(t.Name, methods_name);

                }
               
               
            }
            return all_methods;
        }
        static void Main(string[] args)
        {
           
            dllParser(@"C:\Users\user\Desktop\dll");
            Console.ReadLine();
        }

        

}
    
}
