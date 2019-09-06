using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriangleSum.Interfaces;

namespace TriangleSum.Contracts
{
  public class Cell : ICell
  {
    public Cell(int value, int row, int index, int? sum = default(int?))
    {
      Value = value;
      Row = row;
      Index = index;
      Sum = sum ?? 0;
      IsValidSummary = sum != default(int?);
    }

    public int AddParent(ICell parent)
    {
      int add = 0;
      if (Parents == null)
        Parents = new List<ICell>();

      if (!Parents.Exists(x => x == parent))
      {
        Parents.Add(parent);
        add++;
      }

      return add;
    }

    public int Value { get; set; }
    public int Sum { get; set; }
    public bool IsValidSummary { get; set; }
    public int Row { get; set; }
    public int Index { get; set; }
    public List<ICell> Parents { get; private set; }
  }
}
