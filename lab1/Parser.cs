using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Expressions;
namespace lab1
{
    internal class Parser
    {
        /*61) 2,4,9,10,11;*/
        public static int eval(string exp, Dictionary<string, int> values)
        {
            try
            {
                return Eval.Execute<int>(exp, values);
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
