using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }

        public List<SqlParameter> Parameters { get; set; }
        public SqlOperation()
        {
            Parameters = new List<SqlParameter>();
        }


        public void AddStringParameter(string parameterName, string parameterValue)
        {
            Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }

        public void AddTimeParameter(string parameterName, TimeSpan parameterValue)
        {
            var timeParameter = new SqlParameter(parameterName, System.Data.SqlDbType.Time)
            {
                Value = parameterValue
            };
            Parameters.Add(timeParameter);
        }

        public void AddIntParameter(string parameterName, int parameterValue)
        {
            Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }
        public void AddDateTimeParameter(string parameterName, DateTime parameterValue)
        {
            Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }
        public void AddDoubleParameter(string parameterName, double parameterValue)
        {
            Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }
        public void AddCharParameter(string parameterName, char parameterValue)
        {
            Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }
    }
}
