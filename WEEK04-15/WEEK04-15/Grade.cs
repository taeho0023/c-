using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week04Homework
{
    class Grade
    {
        //static field
        public static int MAX_GRADE_COUNT = 9;
        //instance field
        public string StudentNumber;
#if false

        public List<double> Scores = new List<double>();
#else
        private List<double> _scores = new List<double>();
#endif
        public double Average()
        {
#if false
            if(this.Scores.Count == 0)
            {
                return -1.0;
            }
            double sum = 0.0;
            foreach (var score in this.Scores)
            {
                sum += score;
            }
            return sum / Scores.Count;
#else
            if (this._scores.Count == 0)
            {
                return -1.0;
            }
            return _scores.Average();
            return _scores.Sum() / _scores.Count;
#endif
            
        }
    }
}
