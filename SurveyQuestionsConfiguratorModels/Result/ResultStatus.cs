using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorModels.Result
{
    public enum ResultStatus
    {
        Success = 0,
        Generic = -1,
        UnexpectedError = -2,
        DatabaseConnection = -3,
        DatabaseError = -4,
        UnknownType = -5,
        ValidationError = -6,
        SqlTableDependencyError = -7
    }
}