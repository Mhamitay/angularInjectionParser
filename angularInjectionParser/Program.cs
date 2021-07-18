using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace angularInjectionParser
{
    class Program
    {
        private static string saveToFile;
        private static string WorkDirectory;
        private static string result;
        static void Main(string[] args)
        {

           
           AppInit();
            //Get all the js files from app and it's sub folders to a files variable - jsFiles
            var jsFiles = DirSearch(WorkDirectory);
            //loop in all files and for every file 
            ProcessFiles(jsFiles);
            Console.Read();
        }


      

        private static void ProcessFiles(List<string> _files)
        {
            var file_type = "";
            int i = 0;
            foreach (var _file in _files)
            {
                if (i > 4)
                {
                    string text = File.ReadAllText(_file);
                    file_type = GetFileType(text, _file);

                }


                i++;

                GetInjectedValues(file_type);

                //displayResult();
                //   - add the output to variable to finally save it to file
                if (saveToFile == "true")
                {
                    //saveResaultToFile();
                }

            }
        }

        private static void GetInjectedValues(string file_type)
        {
            switch (file_type)
            {
                case "Directive":

                    break;
                case "Service":
                    // code block
                    break;
                case "Controller":
                    // code block
                    break;
                default:
                    // code block
                    break;
            }
        }

        private static string GetFileType(string text, string file)
        {
            string file_type = "";
            string pfx = "";
            int startIndex = -1;
            int endIndex = 0;
            string injectedlist = "";
            if (file.Contains(".spec.") || file.Contains(".html") || file.Contains("angucomplete-alt")  )
            {
                startIndex = 0;
            }else
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (text.IndexOf(").filter(") > -1)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    file_type = "Filter";
                    pfx = file_type;
                }

                if (text.IndexOf("app.diretive(") > -1 || text.IndexOf("app.directive") > -1 || text.IndexOf(".directive") > -1)
                {
                    if (text.IndexOf("template:") > -1)
                    {
                        return "";
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    file_type = "Directive";
                    injectedlist = ExtractInjectables.GetInjectableFromDirective(text);
                }
              
               
                if (text.IndexOf("app.factory(") > -1)
                {
                    //done
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    file_type = "Service";
                   // pfx = file_type;
                   // startIndex = text.IndexOf("app.factory(");
                   // startIndex = text.IndexOf("[", startIndex + 12);
                   //// injectedlist = GetInjectableFromService(text, "app.factory(");
                   // injectedlist = text.Substring(startIndex +1, text.IndexOf("]);", startIndex -1)-startIndex -1);\
                     injectedlist = ExtractInjectables.GetInjectableFromService(text, "app.factory(");
                }
                else if(text.IndexOf(".factory(") > -1)
                {
                   
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    file_type = "Service";
                    //pfx = file_type;

                    //startIndex = text.IndexOf("app.service(");
                    //startIndex = text.IndexOf("[", startIndex + 12);
                    //injectedlist = text.Substring(startIndex + 1, text.IndexOf("]);", startIndex - 1) - startIndex - 1);
                    injectedlist = ExtractInjectables.GetInjectableFromService(text, ".factory(");
                }
                else if (text.IndexOf("app.service(") > -1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    file_type = "Service";
                    //    pfx = file_type;
                    //    startIndex = text.IndexOf("app.service(");
                    //    startIndex = text.IndexOf("[", startIndex + 12);

                    //    injectedlist = text.Substring(startIndex + 1, text.IndexOf("]);", startIndex - 1) - startIndex - 1);
                    injectedlist = ExtractInjectables.GetInjectableFromService(text, ".service(");
                }
                
                if (text.IndexOf(".controller(controllerId") > -1)
                {
                    file_type = "Controller";
                    pfx = file_type;
                    startIndex = text.IndexOf("app.service(");
                    startIndex = text.IndexOf("[", startIndex + 12);

                    injectedlist = text.Substring(startIndex + 1, text.IndexOf("]);", startIndex - 1) - startIndex - 1);
                }
                else
                {
                    pfx = "Error ";
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine(pfx + "," + file + "," + injectedlist);
                result = result + (file_type + "," + file + "," + injectedlist + Environment.NewLine);
                injectedlist = string.Empty;
                text = string.Empty;
            }


          

            return file_type;
        }

       

        private static List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
               // MessageBox.Show(excpt.Message);
            }

            return files;
        }
        private static void saveResaultToFile()
        {
            throw new NotImplementedException();
        }

        private static void displayResult()
        {
            throw new NotImplementedException();
        }

        private static void AppInit()
        {
            printWelcomeMessage();
            getConfigs();
        }

        private static void getConfigs()
        {
             saveToFile = ConfigurationManager.AppSettings["saveToFile"];
             WorkDirectory = ConfigurationManager.AppSettings["WorkDirectory"];
        }

        private static void printWelcomeMessage()
        {
           

            Console.ForegroundColor = ConsoleColor.White;
            Display(" ==================================================================");
            Display(" ==================================================================");
            Display(" == SAMA Active Directory Data Transfere Module                  ==");
            Display(" == Squareone technology                                         ==");
            Display(" == Designed By Mohammed Hammad  12 Nov 2013                     ==");
            Display(" == Ver 1.0                                                      ==");
            Display(" ==================================================================");
            Display(" ==================================================================");
            Display("                                                  ");
            Display("                                                  ");
            Display(" == Application started on" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());

        }
        private static void Display(string _txt)
        {
            Console.WriteLine(_txt.ToString());
        }
    }
}
