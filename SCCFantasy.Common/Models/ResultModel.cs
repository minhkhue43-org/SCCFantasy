using SCCFantasy.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Models
{
    public class ResultModel : ResultModel<object>
    {

    }

    public class ResultModel<T> : ResultModelBase
    {

        public T Result { get; set; }

        public ResultModel<T> AddError(string errorMessage, int? statusCode = null)
        {
            AddError(new ErrorMessage(errorMessage), statusCode);
            return this;
        }

        public ResultModel<T> AddError(ErrorMessage errorMessage, int? statusCode = null)
        {
            StatusCode = statusCode;
            _errors.Add(errorMessage);

            return this;
        }

        public ResultModel<T> AddErrors(IEnumerable<string> errorMessage, int? statusCode = null)
        {
            AddErrors(errorMessage.Select(a => new ErrorMessage(a)), statusCode);

            return this;
        }

        public ResultModel<T> AddErrors(IEnumerable<ErrorMessage> errorMessages, int? statusCode = null)
        {
            StatusCode = statusCode;
            _errors.AddRange(errorMessages);

            return this;
        }

        public string GetErrorString()
        {
            return _errors.Select(a => a.Error + ":" + a.Description).GetString();
        }
    }


    public abstract class ResultModelBase
    {
        protected readonly List<ErrorMessage> _errors = new List<ErrorMessage>();

        public IReadOnlyCollection<ErrorMessage> Errors
        {
            get
            {
                return _errors;
            }
        }

        public bool HasErrors => _errors.Any();

        public int? StatusCode { get; set; }
    }
}
