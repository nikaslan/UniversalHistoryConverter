using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UniversalHistoryConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converter MT5 to cTrader\nPlease make sure that source file is named MT5Source.csv " +
                "and is saved in the same folder as converter");

            Console.WriteLine("\nPress any key to convert");
            Console.ReadKey();

            string sourcePath = "MT5Source.csv"; // path to source file
            

            if (File.Exists(sourcePath))
            {
                ConvertMtToCtrader(sourcePath);
            }
            else
            {
                Console.WriteLine("No source file found.");
            }
            
            Console.WriteLine("\nPress any key to close the converter");
            Console.ReadKey();
        }

        static void ConvertMtToCtrader(string filePath)
        {
            
            //actual convert

            string resultPath = "results.csv";  // path for resulting file

            using (StreamReader sr = new StreamReader(filePath))
            {
                using (StreamWriter sw = new StreamWriter(resultPath))
                {
                    string readLine;
                    string writeLine;

                    Console.WriteLine("Converting");

                    sw.WriteLine("TimeFrame,Timestamp,Open,High,Low,Close,Volume"); // correct header
                    while ((readLine = sr.ReadLine()) != null)
                    {
                        if (sbyte.TryParse(readLine.Substring(0,1),out sbyte result))
                        {
                            writeLine = "m1,";

                            // 2009.06.12 00:00;8947.29;8982.59;8894.39;8978.39  <- MT5
                            // m1,24/07/2023 00:00:00.000,35186,35186.5,35179.8,35181.2,105  <- cTrader

                            DateTime dateTime = Convert.ToDateTime(readLine.Substring(0,16));
                            writeLine = writeLine + dateTime.ToString("dd/MM/yyyy HH:mm:ss") + ",";
                            writeLine += readLine.Substring(17).Replace(";", ",");
                            writeLine += ",1";

                            sw.WriteLine(writeLine);
                        }
                        else
                        {

                        }                        
                    }
                    Console.WriteLine("Completed");
                }
                                
            }
                        
        }


    }
}
