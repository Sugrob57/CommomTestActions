using CommonTestActions.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommonTestActions.Test
{
    public class MultiTest : Test
    {
        //public List<string> Values;
        public List<ResponseCollection> StepResponseCollections;
        public TreadingType RunType;
        public TimeSpan Delay;

        public MultiTest(string name) : base(name)
        {
            StepResponseCollections = new List<ResponseCollection>();
        }

        public override ItemStatus Run()
        {
            Stopwatch duration = new Stopwatch();
            duration.Start();
            try
            {
                Steps = Steps.OrderBy(step => step.Order).ToList();

                if (Steps.Count == 0)
                    Status = ItemStatus.Created;
                else
                {
                    Step previosStep = Steps[0];

                    foreach (Step step in Steps)
                    {
                        if ((previosStep.Action == ActionType.ExecuteList) && (step.Order > 1))
                        {
                            
                            // TODO - MultiRun tests
                            // - переписать StepResponseCollections как класс 
                            // - добавить поле "заменяемое значение"
                            // - добавить признак - где менять (в квери, в боди, везде)
                            // - подменить значения (можно реф-переменную в статик метод класса коллекций) 
                                

                        
                        //if (StepResponseCollections.)
                            // - вызвать ран в цикле, или тредами согласно параметров класса
                        }
                        else
                        {
                            Status = step.Run();
                        }                            

                        if (step.Action == ActionType.ExecuteList)
                        /// Если результатом запуска шага был список, создаем экземпляр этого списка
                        {
                            object _replValueObj = new object();
                            object _replParam = new object();

                            ResponseCollection respCol = new ResponseCollection(); 
                            if (step.Parameters.TryGetValue(ParameterType.ReplacementValue, out _replValueObj))
                            {
                                if (step.Parameters.TryGetValue(ParameterType.ReplacementParam, out _replParam))
                                {
                                    respCol = new ResponseCollection()
                                    {
                                        StepName = step.Name,
                                        ReplacementValue = _replValueObj.ToString(),
                                        ReplacementParam = (ParameterType)_replParam,
                                        Values = previosStep.ResponseCollection
                                    };
                                }
                            }

                            StepResponseCollections.Add(respCol);
                        }                            
                        else
                        {
                            StepResponses.Add(step.Name, step.Response);
                        }                          

                        previosStep = step;

                        if (Status.Equals(ItemStatus.Fail))
                            break;
                        else
                            Status = ItemStatus.Success;
                    }
                }                
            }
            catch
            {
                Status = ItemStatus.Fail;
            }

            duration.Stop();
            Duration = duration.ElapsedMilliseconds;
            return Status;
            //return base.Run();
        }
    }
}
