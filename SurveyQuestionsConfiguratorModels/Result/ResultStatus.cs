using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorModels.Result
{
    /// <summary>
    /// Represents the possible outcomes of an Result object,
    /// including success and different error conditions.
    /// </summary>
    public enum ResultStatus
    {
        Success = 0,
        GenericError = -1,
        UnexpectedError = -2,
        DatabaseError = -4,
        UnknownTypeError = -5,
        ValidationError = -6,
        SqlTableDependencyError = -7,
        OutOfRangeError = -8,
        NullError = -9,
        NullOrWhiteSpaceError = -10,
        LengthTooLongError = -11,
        DatabaseConnectionError = -12
    }
}