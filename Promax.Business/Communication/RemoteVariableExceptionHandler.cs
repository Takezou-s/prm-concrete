using Extensions;
using Newtonsoft.Json;
using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Promax.Business
{
    public class RemoteVariableExceptionHandler
    {
        private Dictionary<IRemoteVariable, Action> _pendingActions = new Dictionary<IRemoteVariable, Action>();
        private List<VariableOperationPair> _operations = new List<VariableOperationPair>();
        private volatile object _locker = new object();
        private volatile object _opLocker = new object();
        private string Path = "saves\\pending.txt";
        public RemoteVariableExceptionHandler(IVariableCommunicator communicator)
        {
            if (File.Exists(Path))
            {
                Operations = JsonConvert.DeserializeObject<List<VariableOperationPair>>(File.ReadAllText(Path), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

                //byte[] b = Convert.FromBase64String(File.ReadAllText(Path));
                //using (var stream = new MemoryStream(b))
                //{
                //    var formatter = new BinaryFormatter();
                //    stream.Seek(0, SeekOrigin.Begin);
                //    _operations = (List<VariableOperationPair>)formatter.Deserialize(stream);
                //}
                //    //using (var serializer = new UniversalSerializer(Path))
                //    //{
                //    //    PendingActions = serializer.Deserialize<Dictionary<IRemoteVariable, Action>>();
                //    //}
                foreach (var item in Operations)
                {
                    PendingActions.Add(item.Variable as IRemoteVariable, () =>
                    {
                        if (item.Operation == "Read")
                        {
                            communicator.Read(item.Variable as IRemoteVariable);
                        }
                        else
                            communicator.Write(item.Variable as IRemoteVariable);
                    });
                }
            }
        }

        public List<IRemoteVariable> PendingVariables { get; set; } = new List<IRemoteVariable>();
        public Dictionary<IRemoteVariable, Action> PendingActions
        {
            get
            {
                lock (_locker)
                {
                    return _pendingActions;
                }
            }
            private set => _pendingActions = value;
        }

        internal List<VariableOperationPair> Operations
        {
            get
            {
                lock (_opLocker)
                {
                    return _operations;
                }
            }

            set
            {
                _operations = value;
            }
        }

        public bool Contains(IRemoteVariable x)
        {
            bool result = false;
            var handler = this;
            result = ContainsByName(x, handler) ||
                ContainsByReference(x, handler);

            return result;
        }

        private static bool ContainsByReference(IRemoteVariable x, RemoteVariableExceptionHandler handler)
        {
            return handler.PendingActions.ContainsKey(x);
        }

        private static bool ContainsByName(IRemoteVariable x, RemoteVariableExceptionHandler handler)
        {
            return handler.PendingActions.Keys.FirstOrDefault(z => z.VariableName.IsEqual(x.VariableName)) != null;
        }

        public void Add(IRemoteVariable x, Action action, string operation)
        {
            PendingActions.Add(x, action);
            Operations.Add(new VariableOperationPair(x, operation));
            Save();
        }
        public void Set(IRemoteVariable x, Action action, string operation)
        {
            var handler = this;
            if (ContainsByReference(x, this))
            {
                handler.PendingActions[x] = action;
                Operations.FirstOrDefault(y => y.Variable.IsEqual(x)).Do(y => y.Operation = operation);
                Save();
            }
            else if (ContainsByName(x, this))
            {
                handler.PendingActions[GetReferenceFromName(x, handler)] = action;
                Operations.FirstOrDefault(y => (y.Variable as dynamic).VariableName == x.VariableName).Do(y => y.Operation = operation);
                Save();
            }

        }

        private static IRemoteVariable GetReferenceFromName(IRemoteVariable x, RemoteVariableExceptionHandler handler)
        {
            return handler.PendingActions.Keys.FirstOrDefault(z => z.VariableName.IsEqual(x.VariableName));
        }

        public void Remove(IRemoteVariable x)
        {
            if (ContainsByReference(x, this))
            {
                PendingActions.Remove(x);
                Save();
            }
            else if (ContainsByName(x, this))
            {
                PendingActions.Remove(GetReferenceFromName(x, this));
                Save();
            }
            if (PendingActions.Count <= 0)
                DeleteSave();
        }
        private void Save()
        {
            //using (var stream = new MemoryStream())
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(stream, _operations);
            //    stream.Flush();
            //    stream.Position = 0;
            //    File.WriteAllText(Path, Convert.ToBase64String(stream.ToArray()));
            //}
            //using (var serializer = new UniversalSerializer(Path))
            //{
            //    serializer.Serialize(PendingActions);
            //}
            string contents = JsonConvert.SerializeObject(Operations, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(Path, contents);
        }
        private void DeleteSave()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
    }
}
