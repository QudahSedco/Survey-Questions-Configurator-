using SurveyQuestionsConfiguratorModels.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorModels
{
    /// <summary>
    /// Represents the outcome of an operation, containing a status and an optional value.
    /// Use Success(T) or Failure(ResultStatus) to create instances.
    /// </summary>
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

        /// <summary>
        /// Creates a successful result with the given value.
        /// </summary>
        /// <param name="value">The value produced by the operation.</param>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value, ResultStatus.Success);
        }

        /// <summary>
        /// Creates a failed result with the given status.
        /// </summary>
        /// <param name="status">The failure status describing what went wrong.</param>
        public static Result<T> Failure(ResultStatus status)
        {
            return new Result<T>(status);
        }
    }
}