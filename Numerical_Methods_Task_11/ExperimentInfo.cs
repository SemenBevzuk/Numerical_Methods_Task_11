using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical_Methods_Task_11
{
    class ExperimentInfo
    {
        public TaskInfo TaskInformation { get; private set; }
        public List<MetodInfo> MetodInformation { get; private set; }

        public ExperimentInfo(TaskInfo _taskInfo, List<MetodInfo> _listMetiInfo)
        {
            TaskInformation = _taskInfo;
            MetodInformation = _listMetiInfo;
        }
    }
}
