using QuizGame;
using System.IO.Pipes;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\tQuiz Game");

        var questions = new List<Question>();
        var answers = new List<Answer>();
        var scores = new Dictionary<string, int>();


        SeedQuestionAndOptions();

        StartGame();

        void StartGame()
        {
            Console.WriteLine("Are you ready? We are starting now!!");
            Console.Write("What is your name?: ");
            var player = Console.ReadLine();

            Console.WriteLine("-------------------------");
            Console.WriteLine($"Ok {player} let´s do this");

            foreach (var item in questions)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine(item.QuestionText);
                Console.WriteLine("Please, enter 1, 2, 3 or 4");

                foreach (var option in item.Options)
                {
                    Console.WriteLine($"{option.Id}. {option.Text}");
                }

                var answer = GetSelectedAnswer();
                AddAnswerToList(answer, item);
            }

            int score = GetScore();
            Console.WriteLine($"Nice try {player}! You answered well {score} questions...");
            UpdateScore(player, score);
            ShowScore();

            answers = new List<Answer>();
            Console.WriteLine("Do you want to play again?");
            Console.WriteLine("Enter Y to play again or any other key to stop..");

            var playAgain = Console.ReadLine();
            if (playAgain?.ToLower().Trim() == "y")
            {
                StartGame();
            }
            else
            {
                Console.WriteLine("Okey!! Good Bye");
            }


        }

        void SeedQuestionAndOptions()
        {
            questions.Add(new Question
            {
                Id = 1,
                QuestionText = "What is the biggest country on earth?",
                Options = new List<Options>()
                {
                    new Options { Id = 1, Text = "Australia" },
                    new Options { Id = 2, Text = "China" },
                    new Options { Id = 3, Text = "Canada" },
                    new Options { Id = 4, Text = "Rusia", IsValid = true }
                }
            });

            questions.Add(new Question
            {
                Id = 2,
                QuestionText = "What is the country with the greatest population?",
                Options = new List<Options>()
                {
                    new Options {Id = 1, Text = "India"},
                    new Options {Id = 2, Text = "China", IsValid = true},
                    new Options {Id = 3, Text = "Argentina"},
                    new Options {Id = 4, Text = "Colombia"}
                }
            });

            questions.Add(new Question 
            { 
                Id = 3,
                QuestionText = "What was the less corrupt country in the world in 2021?",
                Options = new List<Options>()
                {
                    new Options { Id = 1, Text = "Finland"},
                    new Options { Id = 2, Text = "New Zealand"},
                    new Options { Id = 3, Text = "Denmark", IsValid = true},
                    new Options { Id = 4, Text = "Norway"}
                }
            });

            questions.Add(new Question
            {
                Id = 4,
                QuestionText = "What was the best country for quality of life in 2021?",
                Options = new List<Options>()
                {
                    new Options { Id = 1, Text = "Norway", IsValid = true},
                    new Options { Id = 2, Text = "Begium"},
                    new Options { Id = 3, Text = "Sweden"},
                    new Options { Id = 4, Text = "Switzerland"}
                }
            });
        }
        
        string GetSelectedAnswer()
        {
            Console.Write("Option: ");
            var answer = Console.ReadLine();
            
            if (answer != null && (answer == "1") || (answer == "2") || (answer == "3") || (answer == "4"))
                return answer;
            else
            {
                Console.WriteLine("That is not a valid option, please try again..");
                answer = GetSelectedAnswer();
            }
            return answer; 
        }

        void AddAnswerToList(string answer, Question question)
        {
            answers.Add(new Answer
            {
                QuestionId = question.Id,
                SelectedOption = GetSelectedOption(answer, question)
            });
        }
    
        Options GetSelectedOption(string answer, Question question)
        {
            var selectedOption = new Options();

            foreach (var item in question.Options)
            {
                if (item.Id == int.Parse(answer))
                    selectedOption = item;
            }

            return selectedOption;
        }

        int GetScore()
        {
            int score = 0;
            foreach (var item in answers)
            {
                if (item.SelectedOption.IsValid)
                    score++;
            }
            return score;
        }
        
        void UpdateScore(string player, int score)
        {
            bool updated = false;
            foreach (var item in scores)
            {
                if (item.Key == player)
                {
                    scores[item.Key] = score;
                }
                
            }
            if (!updated)
                scores.Add(player, score);
        }

        void ShowScore()
        {
            Console.WriteLine("Scores:");

            foreach (var item in scores)
            {
                Console.WriteLine($"Name: {item.Key} - Score: {item.Value}");
            }
        }
    }

}