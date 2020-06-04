using ExcelWorker.Excel_model_v3;
using System;

namespace ExcelWorker
{
    class Program
    {
        static void Main(string[] args)
        {

            var filePath = @"test_files\test1.xlsx";

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var document = new ExcelDocument(filePath);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            var menu = document.menu.GetMenuTree();

            Console.ReadKey();
        }






    }
}
