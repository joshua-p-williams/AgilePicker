using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AgileMethodologySelector
{
    enum AgileType
    {
        Scrum,
        Kanban
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Agile Methodology Selector!");
            Console.WriteLine("Answer the following questions to determine the best methodology for your work center.");
            Console.WriteLine("Please answer with 'yes' or 'no'.");

            List<Question> questions = InitializeQuestions();
            Random rand = new Random();

            List<Answer> userAnswers = new List<Answer>();

            Shuffle(questions, rand);

            foreach (Question question in questions)
            {
                ClearConsole();
                Console.WriteLine(question.Text);

                userAnswers.Add(new Answer(question, GetAffirmativeAnswer()));
            }

            ClearConsole();
            PrintSummary(userAnswers);

            Console.ReadLine();
        }

        static List<Question> InitializeQuestions()
        {
            List<Question> questions = new List<Question>
            {
                new Question("Does your work flow in a steady continuous pattern?", AgileType.Kanban,
                            "In a Kanban work cycle, as soon as one task is completed, the team seamlessly transitions to the next task. Kanban is particularly well-suited for tasks characterized by a continuous flow, such as support and services.",
                            "Scenarios involving intricate, iterative undertakings, such as developing new products or features, could find greater alignment with the Scrum methodology."),
                new Question("Does your team take focused methodical work that it receives in a planned fashion, instead of just working one thing after another?", AgileType.Scrum,
                            "If teams need a sense of accomplishment/completion/closure, use scrum.",
                            "If teams keep working on one thing after another, use kanban."),
                new Question("Does your team work on task that can be completed without multiple increments of iteration?", AgileType.Kanban,
                            "If your work consistently evolves and requires frequent improvisation, Scrum is a suitable choice. In Scrum, each sprint serves as an opportunity for inspection and adaptation. Work cycles through multiple sprints, enabling improvisational adjustments whenever necessary.",
                            "If the task at hand is a singular endeavor and doesn't demand continuous inspection and adaptation, Kanban is the preferable approach. With Kanban, there's no need for a dedicated mechanism to inspect and adapt. Work progresses linearly without the need for predefined iterations."),
                new Question("Does your work require many articles of transparency and tracking such as elaborate requirements, and implementation isn't the only concern?", AgileType.Scrum,
                            "Scrum encompasses key artifacts like the product backlog, sprint backlog, and the resulting increment. These components respectively offer transparency into requirements, implementation, and deliverables. Opt for Scrum when the need arises to manage requirements distinctively from work in progress tracking.",
                            "While there aren't dedicated artifacts, the Kanban board does offer a degree of transparency. Numerous teams blend the product backlog from Scrum with Kanban boards. If the emphasis is solely on tracking implementation, the Kanban approach becomes fitting."),
                new Question("Does your work require elaborate and detailed planning?", AgileType.Scrum,
                            "Scrum involves distinct events such as sprint planning and daily scrum for orchestrating the sprint and day's activities. Opt for Scrum when a disciplined approach to regular planning intervals is essential.",
                            "Kanban lacks prescribed provisions for planning work. Teams establish their own rhythm and strategy for planning. Kanban planning can occur sporadically or whenever the need arises."),
                new Question("Can your work be done with a group of individuals with a high level of expertise with less level of accountability and lots interaction from others responsible for this level of focus?", AgileType.Kanban,
                            "Kanban operates without distinct roles like product owner or developers. It assumes a collective of individuals collaborating on tasks. If your team primarily consists of individuals with varied expertise, the Kanban methodology aligns well.",
                            "Scrum fosters a sense of responsibility through roles like the product owner for business aspects, developers for domain expertise, and the scrum master for overcoming impediments. If your teams require individuals dedicated to these specific responsibilities, Scrum becomes the appropriate choice."),
                new Question("Can your work be done without a lot of creative involvement from others like customers or stakeholders?", AgileType.Kanban,
                            "Kanban lacks a formal mechanism for engaging stakeholders or customers. Several teams opt for a regular sprint review practice. If your tasks predominantly involve daily routines and infrequent stakeholder engagement, the Kanban approach is well-suited.",
                            "Scrum emphasizes consistent involvement of stakeholders and customers, typically through a sprint review event. Opt for Scrum when your tasks involve innovation, creativity, new endeavors, and necessitate stakeholder and customer feedback and engagement."),
                new Question("Is your work centered around fixed-length iterations?", AgileType.Scrum,
                            "Fixed-length iterations provide clear milestones and foster collaboration.",
                            "Work not centered around fixed-length iterations may not benefit as much from Scrum's structured approach."),
                new Question("Is your teams workload highly variable?", AgileType.Kanban,
                            "Kanban's flexibility suits variable workloads and focuses on continuous flow.",
                            "If your workload isn't highly variable, Kanban's flow-focused approach might not provide significant benefits."),
                new Question("Does your team value incremental improvements and continuous feedback?", AgileType.Scrum,
                            "Scrum encourages iterative improvement and regular feedback, which can lead to better outcomes.",
                            "If your team doesn't prioritize continuous improvement and feedback, Scrum's benefits might be limited."),
                new Question("Do you need to respond to changing requirements frequently?", AgileType.Scrum,
                            "Scrum's adaptive nature helps teams handle changing requirements effectively.",
                            "If requirements are relatively stable, Scrum's frequent adaptations might not be necessary."),
                new Question("Is your team highly experienced and self-organized?", AgileType.Scrum,
                            "Scrum empowers self-organized teams, which can excel with experienced members.",
                            "If your team isn't highly experienced or self-organized, Scrum's benefits might not be fully realized."),
                new Question("Does your team require high visibility of work in progress?", AgileType.Kanban,
                            "Kanban's visual boards provide clear visibility of work items and their status.",
                            "If visibility isn't a top priority, Kanban's visual management might not be as valuable."),
                new Question("Do you often deal with unpredictable work items?", AgileType.Kanban,
                            "Kanban handles unpredictability well, as it focuses on flow and responsiveness.",
                            "If your work items are mostly predictable, Kanban's responsiveness might not be as critical."),
                new Question("Is your team open to frequent process changes?", AgileType.Kanban,
                            "Kanban encourages continuous process improvement and gradual changes.",
                            "If your team is resistant to frequent changes, Kanban's iterative approach might not align well."),
                new Question("Is cross-functional collaboration essential for your team?", AgileType.Scrum,
                            "Scrum's emphasis on cross-functional teams can lead to better collaboration and holistic solutions.",
                            "If cross-functional collaboration isn't crucial, Scrum's focus might not be fully utilized."),
                // Add more questions here...
            };

            return questions;
        }


        static bool GetAffirmativeAnswer()
        {
            while (true)
            {
                Console.Write("Your answer: ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input.StartsWith("y"))
                {
                    return true;
                }
                else if (input.StartsWith("n"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
        }

        static void ClearConsole()
        {
            Console.Clear();
        }

        static void PrintSummary(List<Answer> userAnswers)
        {
            int scrumScore = 0;
            int kanbanScore = 0;
            int totalScore = 0;

            Console.WriteLine("Explanations for each answer:\n");

            foreach (var answer in userAnswers)
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine($"Question: {answer.Question.Text}");
                var yourAnswer = answer.AffirmativeAnswer ? "Yes" : "No";
                Console.WriteLine($"Your answer: {yourAnswer}");
                
                var favors = AgileType.Scrum.ToString();
                if ((answer.AffirmativeAnswer && answer.Question.Affirms == AgileType.Kanban) || (!answer.AffirmativeAnswer && answer.Question.Affirms == AgileType.Scrum))
                {
                    favors = AgileType.Kanban.ToString();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    kanbanScore++;
                }
                else
                {
                    favors = AgileType.Scrum.ToString();
                    Console.ForegroundColor = ConsoleColor.Green;
                    scrumScore++;
                }

                Console.WriteLine($"Favors: {favors}");
                Console.ResetColor();
                Console.WriteLine("Explanation:");
                Console.WriteLine(answer.AffirmativeAnswer ? answer.Question.ExplanationForAffirmative : answer.Question.ExplanationForNonAffirmative);
                Console.WriteLine();
            }

            totalScore = scrumScore + kanbanScore;

            var favoredResult = "Either Scrum or Kanban could work";
            if (scrumScore > kanbanScore)
            {
                favoredResult = "Scrum";
            }
            else if (kanbanScore > scrumScore)
            {
                favoredResult = "Kanban";
            }


            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine($"Scrum percentage: {((double)scrumScore / totalScore) * 100:F2}%");
            Console.WriteLine($"Kanban percentage: {((double)kanbanScore / totalScore) * 100:F2}%");
            Console.WriteLine("----------------------------------------------------------------------");

            if (favoredResult == "Scrum")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (favoredResult == "Kanban")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            Console.WriteLine($"Based on your answers, the recommended methodology for your work center is: {favoredResult}");

            Console.ResetColor();
        }


        static void Shuffle<T>(IList<T> list, Random rand)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    class Question
    {
        public string Text { get; }
        public AgileType Affirms { get; }
        public string ExplanationForAffirmative { get; }
        public string ExplanationForNonAffirmative { get; }

        public Question(string text, AgileType affirms, string explanationForAffirmative, string explanationForNonAffirmative)
        {
            Text = text;
            Affirms = affirms;
            ExplanationForAffirmative = explanationForAffirmative;
            ExplanationForNonAffirmative = explanationForNonAffirmative;
        }
    }

    class Answer
    {
        public Question Question { get; }
        public bool AffirmativeAnswer { get; }

        public Answer(Question question, bool affirmative)
        {
            Question = question;
            AffirmativeAnswer = affirmative;
        }
    }
}
