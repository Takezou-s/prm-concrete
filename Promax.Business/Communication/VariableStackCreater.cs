using Extensions;
using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;

namespace Promax.Business
{
    public class VariableStackCreater
    {
        private readonly Dictionary<ushort, ushort> _holdingRegistersStack = new Dictionary<ushort, ushort>();
        private readonly Dictionary<ushort, ushort> _holdingCoilsStack = new Dictionary<ushort, ushort>();
        private List<VariableStack> _holdingRegisters = new List<VariableStack>();
        private List<VariableStack> _holdingCoils = new List<VariableStack>();

        public IEnumerable<VariableStack> HoldingRegisters => _holdingRegisters;
        public IEnumerable<VariableStack> HoldingCoils => _holdingCoils;
        public void CreateStack(VariablesBase variableScope)
        {
            CreateStack(variableScope, _holdingRegistersStack, 125);
            foreach (var item in _holdingRegistersStack)
            {
                _holdingRegisters.Add(new VariableStack(item.Key, item.Value));
            }
            foreach (var item in _holdingCoilsStack)
            {
                _holdingCoils.Add(new VariableStack(item.Key, item.Value));
            }
            string res = "Registers: " + Environment.NewLine;
            _holdingRegisters.ForEach(x => res += x.StartingIndex.ToString() + ", " + x.Count.ToString() + Environment.NewLine);
            res += "Coils: " + Environment.NewLine;
            _holdingCoils.ForEach(x => res += x.StartingIndex.ToString() + ", " + x.Count.ToString() + Environment.NewLine);
        }
        private void CreateStack(VariablesBase variableScope, Dictionary<ushort, ushort> dictionary, int maxCount)
        {
            dictionary.Clear();

            var addressList = new List<ushort>();
            variableScope.RemoteVariables.ForEach(variable =>
            {
                variable.DoIf(x => x is IRemoteVariable<short> && x.Definition is IRegisterNumberedVariableDefinition, x =>
                {
                    ushort register = (x.Definition as IRegisterNumberedVariableDefinition).Register;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                });
                variable.DoIf(x => x is IRemoteVariable<bool> && x.Definition is BitOfShortVariableDefinition, x =>
                {
                    ushort register = (x.Definition as BitOfShortVariableDefinition).Register;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                });
                variable.DoIf(x => x is IRemoteVariable<float> && x.Definition is IRegisterNumberedVariableDefinition, x =>
                {
                    ushort register = (x.Definition as IRegisterNumberedVariableDefinition).Register;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                    register++;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                });
                variable.DoIf(x => x is IRemoteVariable<int> && x.Definition is IRegisterNumberedVariableDefinition, x =>
                {
                    ushort register = (x.Definition as IRegisterNumberedVariableDefinition).Register;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                    register++;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                });
            });
            addressList.Sort();
            if (addressList.Count == 1)
            {
                if (!dictionary.ContainsKey(addressList[0]))
                {
                    dictionary.Add(addressList[0], (ushort)1);
                }
            }
            else if (addressList.Count > 1)
            {
                for (int i = 0; i < addressList.Count; i++)
                {
                    ushort a1 = addressList[i];
                    int newIndex = i;
                    int count = 1;
                    for (int j = i + 1; j < addressList.Count; j++)
                    {
                        if (a1 + (j - i) != addressList[j] || j - i >= maxCount)
                        {
                            newIndex = j;
                            count = j - i;
                            break;
                        }
                        else if (j >= addressList.Count - 1)
                        {
                            j++;
                            newIndex = j;
                            count = j - i;
                            break;
                        }
                    }
                    if (!dictionary.ContainsKey(addressList[i]))
                    {
                        dictionary.Add(addressList[i], (ushort)count);
                    }
                    if (i < addressList.Count - 1)
                        i = newIndex - 1;
                }
            }
        }
        private void CreateStack<T>(VariablesBase variableScope, Dictionary<ushort, ushort> dictionary, int maxCount)
        {
            dictionary.Clear();

            var addressList = new List<ushort>();
            variableScope.RemoteVariables.ForEach(variable =>
            {
                variable.DoIf(x => x is IRemoteVariable<T> && x.Definition is IRegisterNumberedVariableDefinition, x =>
                {
                    ushort register = (x.Definition as IRegisterNumberedVariableDefinition).Register;
                    addressList.DoIf(list => !list.Contains(register), list => list.Add(register));
                });
            });
            addressList.Sort();
            if (addressList.Count == 1)
            {
                if (!dictionary.ContainsKey(addressList[0]))
                {
                    dictionary.Add(addressList[0], (ushort)1);
                }
            }
            else if (addressList.Count > 1)
            {
                for (int i = 0; i < addressList.Count; i++)
                {
                    ushort a1 = addressList[i];
                    int newIndex = i;
                    int count = 1;
                    for (int j = i + 1; j < addressList.Count; j++)
                    {
                        if (a1 + (j - i) != addressList[j] || j - i >= maxCount)
                        {
                            newIndex = j;
                            count = j - i;
                            break;
                        }
                        else if (j >= addressList.Count - 1)
                        {
                            j++;
                            newIndex = j;
                            count = j - i;
                            break;
                        }
                    }
                    if (!dictionary.ContainsKey(addressList[i]))
                    {
                        dictionary.Add(addressList[i], (ushort)count);
                    }
                    if (i < addressList.Count - 1)
                        i = newIndex - 1;
                }
            }
        }
    }
}
