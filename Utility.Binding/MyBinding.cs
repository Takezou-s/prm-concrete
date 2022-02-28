using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Utility.Binding
{
    public class MyBinding
    {
        private DateTime _lastMappedTime;
        private class NullExceptionHandler : IExceptionLogger
        {
            public void Log(Action action)
            {
                action();
            }
        }
        public static IExceptionLogger ExceptionHandler { get; set; } = new NullExceptionHandler();
        private List<BindingHandler> _handlers = new List<BindingHandler>();
        public IEnumerable<BindingHandler> Handlers => _handlers;
        public MyBinding(MyBindingType bindingType)
        {
            BindingType = bindingType;
        }

        public MyBinding()
        {
        }

        public bool PeriodicMapIgnoreChanged { get; set; }
        public int MapPeriod { get; set; }
        public MyBindingType BindingType { get; private set; } = MyBindingType.Automatic;
        public void RemoveBindingOfSource(object source)
        {
            _handlers.Where(x => x.SourceObject.IsEqual(source)).ToList().ForEach(x => _handlers.Remove(x));
        }
        
        public BindingHandler CreateBinding()
        {
            var a = new BindingHandler();
            a.Type(BindingType);
            _handlers.Add(a);
            return a;
        }
        public int Map()
        {
            int count = 0;
            DateTime dateTime = _lastMappedTime.AddMilliseconds(MapPeriod);
            bool timeElapsed = DateTime.Now.IsLaterThan(dateTime) || DateTime.Now.IsSame(dateTime);
            _handlers.ForEach(x => x.DoIfElse(y =>
            {
                return !PeriodicMapIgnoreChanged;
            }, y =>
            {
                y.Map().DoIf(z => z, z => count++);
            }, y =>
            {
                y.DoIf(z =>
                {
                    return timeElapsed;
                }, t =>
                {
                    y.MapIgnoreChanged().DoIf(z => z, z => { count++; });
                });
            }));
            (timeElapsed && PeriodicMapIgnoreChanged).DoIf(x => x, x =>
            {
                _lastMappedTime = DateTime.Now;
            });
            return count;
        }
        public void InitialMapping()
        {
            _handlers.ForEach(x => x.Do(y => y.InitialMapping()));
        }
        public void ClearBindings()
        {
            _handlers.ForEach(x => x.Do(y => y.Deactivate()));
            _handlers.Clear();
        }
        public void RemoveBinding(BindingHandler handler)
        {
            handler.DoIf(x => Handlers.Contains(x), x =>x.DoReturn(y=>x.Deactivate()).Do(y=>_handlers.Remove(x)));
        }
        
        ~MyBinding()
        {
            ClearBindings();
        }
    }
}
