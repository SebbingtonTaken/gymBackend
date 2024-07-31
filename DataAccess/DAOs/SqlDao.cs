using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace DataAccess.DAOs
{
    public class SqlDao
    {
        // Unico con el canal abierto a sql
        private string connectionString;

        private static SqlDao instance;

        private SqlDao()
        {
            connectionString = "Data Source=(localdb)\\proyect;Initial Catalog=gymProjectDataBase;Integrated Security=True;Encrypt=False";
        }

        public static SqlDao GetInstance()
        {
            if (instance == null)
            {
                return instance = new SqlDao();
            }
            else
            {
                return instance;
            }
        }

        // Ejecutar SP en la base de datos
        public void ExecuteProcedure(SqlOperation sqlOperation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var listResults = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    conn.Open();
                    //Aca cambia con respecto a lo otro
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var rowDict = new Dictionary<string, object>();

                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                rowDict[key] = value;
                            }

                            listResults.Add(rowDict);
                        }
                    }
                }
            }

            return listResults;
        }

        public int ExecuteProcedureWithReturnValue(SqlOperation sqlOperation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    var returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(returnValue);

                    conn.Open();
                    command.ExecuteNonQuery();

                    return (int)returnValue.Value;
                }
            }

        }
    }
}
