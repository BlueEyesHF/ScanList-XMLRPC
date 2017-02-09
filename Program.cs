using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Net;


namespace XMLRPC_ScanList
{
    class Program
    {
        static string[] lists;
        static List<string> Valide = new List<string>();
        static void Main(string[] args)
        {
            lists = File.ReadAllLines(args[0]);
            Parallel.For(0, lists.Length - 1, delegate (int i)
            {
                    try
                    {
                        string[] url = lists[i].Split(' ');
                   
                        string webc = new WebClient().DownloadString(url[0]);
                        if (webc.Contains("XML-RPC server accepts POST requests only."))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(url[0] + " -> Valide");
                            Valide.Add(lists[i]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(url[0] + " -> Non Valide");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                    }
               
            });
       
            File.WriteAllLines(args[0].Replace(".txt","_Valide.txt"), Valide.ToArray());
        }
    }
}
