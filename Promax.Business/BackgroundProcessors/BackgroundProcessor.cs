using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public class BackgroundProcessor
    {
        public static List<BackgroundProcessor> List = new List<BackgroundProcessor>();

        public static BackgroundProcessor GetProcessor(Action run, Action completed)
        {
            var a = new BackgroundProcessor();
            a.RunAction = run;
            a.CompletedAction = completed;
            List.Add(a);
            return a;
        }
        public static BackgroundProcessor GetProcessor()
        {
            var a = new BackgroundProcessor();
            List.Add(a);
            return a;
        }
        private BackgroundProcessor()
        {

        }
        public Action RunAction { get; set; }
        public Action CompletedAction { get; set; }

        public void Run()
        {
            RunAction?.Invoke();
        }
        public void WhenCompleted()
        {
            CompletedAction?.Invoke();
        }
    }
}
