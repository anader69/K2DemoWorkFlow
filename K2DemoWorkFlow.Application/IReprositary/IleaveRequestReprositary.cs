using K2DemoWorkFlow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2DemoWorkFlow.Application.IReprositary
{
    public interface IleaveRequestReprositary
    {
         Task<bool> SaveTask(Domain.Entities.Workflow.Task model);
        List<Domain.Entities.Workflow.Task> GetUserTask(string Originator);
        Task<bool> UpdateTask(TaskAction Action, int ProcessId);
    }
}
