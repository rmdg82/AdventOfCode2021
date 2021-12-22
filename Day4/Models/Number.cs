using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4.Models
{
    public class Number
    {
        public int Value { get; set; }
        public bool IsDrawn { get; set; }

        public Number(int value, bool isDrawn = false)
        {
            Value = value;
            IsDrawn = isDrawn;
        }

        public void Extract()
        {
            if (!IsDrawn)
            {
                IsDrawn = true;
            }
        }
    }
}