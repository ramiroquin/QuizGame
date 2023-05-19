using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<Options> Options { get; set; }
    }
}
