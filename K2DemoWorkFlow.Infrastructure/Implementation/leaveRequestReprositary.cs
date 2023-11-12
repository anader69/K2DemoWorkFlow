using K2DemoWorkFlow.Application.IReprositary;
using K2DemoWorkFlow.Domain.Entities.Workflow;
using K2DemoWorkFlow.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace K2DemoWorkFlow.Infrastructure.Implementation
{
    public class leaveRequestReprositary : IleaveRequestReprositary
    {
        private readonly WorkFlowContext dbcontext;

        public leaveRequestReprositary(WorkFlowContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }
        public List<Domain.Entities.Workflow.Task> GetUserTask(string Originator)
        {
            return dbcontext.Tasks.Where(item => item.Originator == Originator).Include(x => x.TaskStatus).ToList();
        }

        public async Task< bool> SaveTask(Domain.Entities.Workflow.Task model)
        {
            dbcontext.Tasks.Add(model);

          var result=  await dbcontext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task< bool> UpdateTask(TaskAction Action, int ProcessId)
        {
            var task = dbcontext.Tasks.Where(item => item.ProcessId == ProcessId).FirstOrDefault();
            if (task != null)
            {
                task.TaskStatusId = (int)Action;
                await dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
