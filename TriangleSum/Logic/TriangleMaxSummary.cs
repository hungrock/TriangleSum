using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangleSum.Contracts;
using TriangleSum.Interfaces;
using TriangleSum.Logic;

namespace TriangleSum.Logic
{
  public class TriangleMaxSummary
  {
    /// <summary>
    /// Validate and parse any valid data for summary with:
    /// 1. Odd/Even
    /// 2. Parent/child nodes
    /// </summary>
    /// <param name="triangle"></param>
    /// <returns></returns>
    public static List<List<ICell>> ValidateAndParseInputData(List<List<int>> triangle)
    {
      var validData = new List<List<ICell>>();
      var row = 0;
      var index = 0;
      var expectOddEven = Common.IsOdd(triangle[row][0]);
      foreach (var values in triangle)
      {
        validData.Add(new List<ICell>());
        if (row > 0)
        {
          index = 0;
          foreach (var val in values)
          {
            ValidateAndParseCellValue(validData, row, index, expectOddEven, val);
            index++;
          }
        }
        else
          validData[row].Add(new Cell(values[0], 0, 0, values[0]));

        expectOddEven = !expectOddEven;
        row++;
      }

      return validData;
    }

    /// <summary>
    /// Validate and parse cell's value for summary with:
    /// 1. Odd/Even
    /// 2. Parent/child nodes
    /// </summary>
    /// <param name="validData"></param>
    /// <param name="row"></param>
    /// <param name="index"></param>
    /// <param name="expectOddEven"></param>
    /// <param name="val"></param>
    private static void ValidateAndParseCellValue(List<List<ICell>> validData, int row, int index, bool expectOddEven, int val)
    {
      //should has at least one parent node if not top most (row>0)
      var hasAnyValidParent = AnyValidParent(validData[row - 1], index);
      var currentOddEven = Common.IsOdd(val);
      validData[row].Add(
        new Cell(val, row, index,
          expectOddEven == currentOddEven && hasAnyValidParent ? val : default(int?)));
    }

    /// <summary>
    /// Check parent nodes at current index
    /// </summary>
    /// <param name="parentRow"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private static bool AnyValidParent(List<ICell> parentRow, int index)
    {
      return index == 0
        ? parentRow[index].IsValidSummary
        : index == parentRow.Count
          ? parentRow[index - 1].IsValidSummary
          : parentRow[index].IsValidSummary || parentRow[index - 1].IsValidSummary;
    }

    private static void ShowTriangle(List<List<int>> triangle, string prefix, string format = null)
    {
      Console.WriteLine(prefix);
      foreach (var values in triangle)
        Console.WriteLine(
          string.Join(
            " ",
            string.IsNullOrWhiteSpace(format)
              ? values.Select(x => x.ToString())
              : values.Select(x => x.ToString(format))));
    }

    private static void ShowTriangleSummaries(
      List<List<ICell>> triangle, string prefix, string format = null)
    {
      Console.WriteLine(prefix);
      foreach (var values in triangle)
        Console.WriteLine(
          string.Join(
            " ",
            string.IsNullOrWhiteSpace(format)
              ? values.Select(x => x.Sum.ToString())
              : values.Select(x => x.Sum.ToString(format))));
    }

    private static List<ICell> GetParentSummaries(List<ICell> parents, int index, int lastIndex)
    {
      List<ICell> parentSummaries = new List<ICell>();
      if (index > 0 && parents[index - 1].IsValidSummary)
        parentSummaries.Add(parents[index - 1]);
      if (index < lastIndex && parents[index].IsValidSummary)
        parentSummaries.Add(parents[index]);

      return parentSummaries;
    }

    public static void GenerateTriangleMaxSummaries(List<List<ICell>> sum)
    {
      var count = sum.Count;
      var row = 1; //start at 2.row
      int index = 0;
      while (row < count)
      {
        index = 0;
        var lastIndex = sum[row].Count - 1;
        while (index <= lastIndex)
        {
          MaxSummaryCalculating(sum, row, index, lastIndex);

          index++;
        }

        row++;
      }
    }

    private static void MaxSummaryCalculating(List<List<ICell>> sum, int row, int index, int lastIndex)
    {
      var parentSum = GetParentSummaries(sum[row - 1], index, lastIndex);
      var currentSum = sum[row][index].Sum;
      if (sum[row][index].IsValidSummary)
      {
        if (!parentSum.Any())
        {
          sum[row][index].IsValidSummary = false;
          currentSum = 0;
        }
        else
        {
          var parentIndex = index;
          if (index == 0 && parentSum.Any(x => x.Index == index))
          {
            currentSum += parentSum.First(x => x.Index == index).Sum;
          }
          else if (index == lastIndex && parentSum.Any(x => x.Index == index - 1))
          {
            currentSum += parentSum.First(x => x.Index == index - 1).Sum;
            parentIndex--;
          }
          else
          {
            var max = parentSum.Max(x => x.Sum);
            currentSum += max;
            if (parentSum[0].Sum == max)
              parentIndex = parentSum[0].Index;
          }

          sum[row][index].AddParent(sum[row - 1][parentIndex]);
        }

        sum[row][index].Sum = currentSum;
      }
    }

    public static int MaxSummaryOf(List<List<int>> triangle)
    {
      ShowTriangle(triangle, "\nInput Data:", "D3");
      var sum = ValidateAndParseInputData(triangle);
#if DEBUG
      ShowTriangleSummaries(sum, "\nInput with Odd/Even and valid parents rules:");
#endif
      GenerateTriangleMaxSummaries(sum);
#if DEBUG
      ShowTriangleSummaries(sum, "\nMax summary triangle:");
#endif
      var maxSum = sum.Last().Max(x => x.Sum);
      Console.WriteLine("\nMax = {0}", maxSum);
      ShowMaxSummaryPath(sum, maxSum);
      return maxSum;
    }

    private static void ShowMaxSummaryPath(List<List<ICell>> sum, int max)
    {
      var cell = sum.Last().First(x => x.Sum == max);
      var path = $"{cell.Value}";
      while (cell != null && cell.Parents != null && cell.Parents.Count > 0)
      {
        cell = cell.Parents.First();
        path = $"{cell.Value}->" + path;
      }

      Console.WriteLine("Path = {0}", path);
    }
  }
}
