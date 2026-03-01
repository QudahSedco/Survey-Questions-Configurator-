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

        public T Value { get; }

        private Result(T value, ResultStatus status)
        {
            Value = value;
            Status = status;
        }

        private Result(ResultStatus pStatus)
        {
            Value = default;
            Status = pStatus;
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