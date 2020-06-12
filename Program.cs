using System;
using System.Collections.Generic;
using System.Linq;

namespace SD_23_FIKUMIKU
{
    class Program
    {
        static void Main(string[] args)
        {
            var nbs = Enumerable.Range(1, 100);
            var msgs = Enumerable.Range(1, 100)
                .ToList()
                .ConvertAll(x => x.ToString()
                );
            IEnumerable<(int nbs, string msgs)> initialHundred = nbs.Zip(msgs);

            var by3 = initialHundred.ChangeLabelOn(3, "Fiku");
            var linq_by3 = from nb in initialHundred
                           join test_nb in by3
                           on nb.nbs equals test_nb.number
                           select (nb.nbs, test_nb.msg);
            var linqBy3_By5 = linq_by3.ChangeLabelOn(5, "Miku");
            var linqBy3_By5_By15 = linqBy3_By5.ChangeLabelOn(15, "Fiku Miku");
            linqBy3_By5_By15.Print();


            Console.WriteLine("Program ended.");
        }
    }


    public static class ExtensionMethods
    {
        public static IEnumerable<(int number, string msg)> ChangeLabelOn(
            this IEnumerable<(int number, string msg)> dataset, int whichIndexes, string newLabel)
        {
            IEnumerable<(int number, string msg)> newImprovedDataset = dataset
                .Where(x => x.number % whichIndexes == 0)
                .Select(x => (x.number, x.msg = newLabel))
                .Concat(dataset
                .Where(x => x.number % whichIndexes != 0)
                .Select(x => (x.number, x.msg))
                ).OrderBy(x => x);

            return newImprovedDataset;
        }


        public static void Print(this IEnumerable<(int number, string msg)> subject)
        {
            subject.ToList().ForEach(x => Console.WriteLine(string.Concat(x.msg, " ")));
            Console.WriteLine();
        }


    }
}
