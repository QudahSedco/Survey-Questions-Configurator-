using SurveyQuestionsConfiguratorModels.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorModels
{
    public class Result<T>
    {
        public ResultStatus Status { get; }
        public bool IsSuccess => Status == ResultStatus.Success;
        public string MessageKey { get; }
        public T Value { get; }

        private Result(T value, ResultStatus status)
        {
            Value = value;
            Status = status;
        }

        private Result(ResultStatus pStatus, string pMessageKey = null)
        {
            Value = default;
            Status = pStatus;
            MessageKey = pMessageKey ?? pStatus.ToString();
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, ResultStatus.Success);
        }

        public static Result<T> Failure(ResultStatus status)
        {
            return new Result<T>(status);
        }
    }
}