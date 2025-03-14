﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            
            sw.Stop();

            Console.WriteLine($"原方法花費時間: {sw.ElapsedMilliseconds} ms");


           



            imageProcess.Clean(destinationPath);
            sw = new Stopwatch();
            sw.Start();

            var allFiles = imageProcess.FindImages(sourcePath);
            Task[] tasks = new Task[allFiles.Count];

            for (int i = 0; i < allFiles.Count; i++)
            {
                tasks[i] = imageProcess.ResizeImagesV2(allFiles[i], sourcePath, destinationPath, 2.0);
            }

            Task.WaitAll(tasks);
            sw.Stop();

            Console.WriteLine($"新方法花費時間: {sw.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }
    }
}
