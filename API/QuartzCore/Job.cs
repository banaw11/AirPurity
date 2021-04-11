using System;

namespace API.QuartzCore
{
    public class Job
    {
        public Job(Type type, string expression)
        {
            Type = type;
            Expression = expression;
        }

        public Type Type { get; set; }
        public string Expression { get; set; }
    }
}