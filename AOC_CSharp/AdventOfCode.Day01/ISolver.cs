using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day01
{
    public interface ISolver
    {
        int Compute(IEnumerable<int> frequencyChanges);
    }
}
