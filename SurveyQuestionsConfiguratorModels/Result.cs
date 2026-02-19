using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfiguratorModels
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }

        private Result(T pValue)
        {
            IsSuccess = true;
            Value = pValue;
        }

        private Result(string pError)
        {
            IsSuccess = false;
            Error = pError;
        }

        public static Result<T> Success(T pValue) => new Result<T>(pValue);

        public static Result<T> Failure(String pError) => new Result<T>(pError);
    }
}