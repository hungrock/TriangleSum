using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleSum
{
  class Program
  {
    static List<List<int>> testValues = new List<List<int>> {
      new List<int> { 1},
      new List<int> { 8, 9},
      new List<int> { 1, 5, 9},
      new List<int> { 4, 5, 2, 3}
    };

    static List<List<int>> triangle = new List<List<int>> {
      new List<int> { 215},
      new List<int> { 192, 124},
      new List<int> { 117, 269, 442},
      new List<int> { 218, 836, 347, 235},
      new List<int> { 320, 805, 522, 417, 345},
      new List<int> { 229, 601, 728, 835, 133, 124},
      new List<int> { 248, 202, 277, 433, 207, 263, 257},
      new List<int> { 359, 464, 504, 528, 516, 716, 871, 182 },
      new List<int> { 461, 441, 426, 656, 863, 560, 380, 171, 923},
      new List<int> { 381, 348, 573, 533, 448, 632, 387, 176, 975, 449},
      new List<int> { 223, 711, 445, 645, 245, 543, 931, 532, 937, 541, 444},
      new List<int> { 330, 131, 333, 928, 376, 733, 017, 778, 839, 168, 197, 197},
      new List<int> { 131, 171, 522, 137, 217, 224, 291, 413, 528, 520, 227, 229, 928},
      new List<int> { 223, 626, 034, 683, 839, 052, 627, 310, 713, 999, 629, 817, 410, 121},
      new List<int> { 924, 622, 911, 233, 325, 139, 721, 218, 253, 223, 107, 233, 230, 124, 233}
    };

    static void Main(string[] args)
    {
      // The code provided will print ‘Hello World’ to the console.
      // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
      Logic.TriangleMaxSummary.MaxSummaryOf(testValues);
      Logic.TriangleMaxSummary.MaxSummaryOf(triangle);
      Console.WriteLine("Touch any key...");
      Console.ReadKey();

      // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
    }
  }
}
