using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleSum.Interfaces
{
  public interface ICell
  {
    int Value { get; set; }
    int Sum { get; set; }
    bool IsValidSummary { get; set; }
    int Row { get; }
    int Index { get; }
    int AddParent(ICell parent);
    List<ICell> Parents { get; }
  }
}
