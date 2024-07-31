using Azure;
using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        public UserCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "INS_USER_EMP_PR";

            if (baseDTO is User user)
            {
                sqlOperation.AddStringParameter("name", user.Name);
                sqlOperation.AddStringParameter("last_name", user.LastName);
                sqlOperation.AddStringParameter("email", user.Email);
                sqlOperation.AddDateTimeParameter("birthdate", user.BirthDate);
                sqlOperation.AddStringParameter("address", user.Address);
                sqlOperation.AddCharParameter("gender", user.Gender);
                sqlOperation.AddStringParameter("password", user.Password);
                sqlOperation.AddIntParameter("dni", user.Dni);
                sqlOperation.AddIntParameter("phone_number", user.PhoneNumber);

                if (user is Customer customer)
                {
                    sqlOperation.AddIntParameter("user_membership", customer.UserMembership.Id);
                    sqlOperation.AddCharParameter("is_verified", customer.IsVerified);
                }

                if (user  is Employee employee)
                {
                    sqlOperation.AddStringParameter("employee_role", employee.EmployeeRole);
                    sqlOperation.AddDoubleParameter("salary", employee.Salary);
                    sqlOperation.AddDoubleParameter("hourly_rate", employee.HourlyRate);
                }

                sqlDao.ExecuteProcedure(sqlOperation);
            }
        }

        public void SaveOtp(int userId, string otp)
        {
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "INS_OTP_PR";

            sqlOperation.AddIntParameter("Dni", userId);
            sqlOperation.AddStringParameter("OTP", otp);

            sqlDao.ExecuteProcedure(sqlOperation);
        }


        public bool ValidateOtp(int dni, string otp)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "VALIDATE_OTP_PR";

            sqlOperation.AddIntParameter("DNI", dni);
            sqlOperation.AddStringParameter("OTP", otp);

            int result = sqlDao.ExecuteProcedureWithReturnValue(sqlOperation);

            return result == 1;
        }




        public override void DeleteByID(int iD)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var customerList = new List<T>();

            var sqlOperation = new SqlOperation()
            {

                ProcedureName = "RET_ALL_USER_PR"
            };

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                foreach (var row in listResults)
                {
                    var customer = BuildUser(row);
                    customerList.Add((T)Convert.ChangeType(customer, typeof(T)));
                }
            }
            return customerList;
        }


        public List<T> RetrieveAllEmployee<T>()
        {
            var employeeList = new List<T>();

            var sqlOperation = new SqlOperation()
            {

                ProcedureName = "RET_ALL_EMPLOYEE_PR"
            };

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                foreach (var row in listResults)
                {
                    var customer = BuildUser(row);
                    employeeList.Add((T)Convert.ChangeType(customer, typeof(T)));
                }
            }
            return employeeList;
        }


        public  T RetrieveAllMeasuresById<T>(int dni)
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_USER_BY_ID_PR"
            };

            sqlOperation.AddIntParameter("@dni", dni);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var retuser = (T)Convert.ChangeType(BuildUser(row), typeof(T));
                return retuser;
            }
            return default(T);

        }



        public T RetrieveEmployeeById<T>(int id)
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_EMPLOYEE_BY_ID_PR"
            };

            sqlOperation.AddIntParameter("@UserID", id);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var retEmployee = (T)Convert.ChangeType(BuildUser(row), typeof(T));
                return retEmployee;
            }
            return default(T);

        }


        public override void Update(BaseDTO baseDTO, int? id = null)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UP_USER_EMP_PR";

            if (baseDTO is User user)
            {

                sqlOperation.AddIntParameter("id", user.Id);
                sqlOperation.AddStringParameter("name", user.Name);
                sqlOperation.AddStringParameter("last_name", user.LastName);
                sqlOperation.AddStringParameter("email", user.Email);
                sqlOperation.AddDateTimeParameter("birthdate", user.BirthDate);
                sqlOperation.AddStringParameter("address", user.Address);
                sqlOperation.AddCharParameter("gender", user.Gender);
                sqlOperation.AddIntParameter("dni", user.Dni);
                sqlOperation.AddIntParameter("phone_number", user.PhoneNumber);
                Console.WriteLine(user);

                if (baseDTO is Customer customer)
                {
                    sqlOperation.AddIntParameter("user_membership", customer.UserMembership.Id);
                    sqlOperation.AddCharParameter("is_verified", customer.IsVerified);
                }
                else
                {
                    sqlOperation.AddIntParameter("user_membership", 0);
                    sqlOperation.AddCharParameter("is_verified", 'Y');
                }

                if (baseDTO is Employee employee)
                {
                    sqlOperation.AddStringParameter("employee_role", employee.EmployeeRole);
                    sqlOperation.AddDoubleParameter("salary", employee.Salary);
                    sqlOperation.AddDoubleParameter("hourly_rate", employee.HourlyRate);
                }
                else
                {
                    sqlOperation.AddStringParameter("employee_role", null);
                    sqlOperation.AddDoubleParameter("salary", 0);
                    sqlOperation.AddDoubleParameter("hourly_rate", 0);
                }

                sqlDao.ExecuteProcedure(sqlOperation);
            }
        }

        public T LoginUser<T>(string email, string password) where T : User
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "LOGIN_USER_PR"
            };

            sqlOperation.AddStringParameter("@email", email);
            sqlOperation.AddStringParameter("@password", password);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var user = BuildUser(row);

                // Verificar si el usuario puede ser convertido al tipo deseado
                if (user is T)
                {
                    return user as T;
                }
                else
                {
                    throw new InvalidCastException($"No se puede convertir el objeto de tipo {user.GetType().Name} al tipo {typeof(T).Name}");
                }
            }
            return default(T);
        }


        public T RetrieveUserByEmail<T>(string email) where T : User
        {
            var sqlOperation = new SqlOperation()
            {
                ProcedureName = "RET_USER_BY_EMAIL_PR"
            };

            sqlOperation.AddStringParameter("@Email", email);

            var listResults = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listResults.Count > 0)
            {
                var row = listResults[0];
                var user = BuildUser(row);

                // Verificar si el usuario puede ser convertido al tipo deseado
                if (user is T)
                {
                    return user as T;
                }
                else
                {
                    throw new InvalidCastException($"No se puede convertir el objeto de tipo {user.GetType().Name} al tipo {typeof(T).Name}");
                }
            }
            return default(T);
        }



        private User BuildUser(Dictionary<string, object> row)
        {
            User userToReturn;

            var isCustomer = row.ContainsKey("user_membership") && row["user_membership"] != DBNull.Value
                          ? new Membership() { Id = Convert.ToInt32(row["user_membership"]) }
                          : null;

            // Luego, verifica si el usuario es un empleado
            bool isEmployee = row.ContainsKey("employee_role") ||
                               isCustomer == null;

            if (isEmployee)
            {   
                var employee = new Employee()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    Email = row["email"].ToString(),
                    PhoneNumber = Convert.ToInt32(row["phone_number"]),
                    CreationDate = Convert.ToDateTime(row["creation_date"]),
                    BirthDate = Convert.ToDateTime(row["birthdate"]),
                    PasswordBytes = row["password"] as byte[],
                    Address = row["address"].ToString(),
                    Gender = Convert.ToChar(row["gender"]),
                    Dni = Convert.ToInt32(row["dni"]),
                    EmployeeRole = row.ContainsKey("employee_role") ? row["employee_role"].ToString() : null,
                    Salary = row.ContainsKey("salary") && row["salary"] != DBNull.Value ? Convert.ToDouble(row["salary"]) : 0.0,
                    HourlyRate = row.ContainsKey("hourly_rate") && row["hourly_rate"] != DBNull.Value ? Convert.ToDouble(row["hourly_rate"]) : 0.0
                };



                employee.UserPermissions = GetPermissions(employee.Id);
           

                userToReturn = employee;
            }
            else
            {
                userToReturn = new Customer()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    Email = row["email"].ToString(),
                    PhoneNumber = Convert.ToInt32(row["phone_number"]),
                    CreationDate = Convert.ToDateTime(row["creation_date"]),
                    BirthDate = Convert.ToDateTime(row["birthdate"]),
                    PasswordBytes = row["password"] as byte[],
                    Address = row["address"].ToString(),
                    Gender = Convert.ToChar(row["gender"]),
                    Dni = Convert.ToInt32(row["dni"]),
                    UserMembership = new Membership() { Id = Convert.ToInt32(row["user_membership"]) },
                    IsVerified = Convert.ToChar(row["is_verified"])
                };

            }

            return userToReturn;
        }

        private List<int> GetPermissions(int userId)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "RET_PERMISSIONS_ID_PR";
            sqlOperation.AddIntParameter("UserID", userId);

            var permissions = new List<int>();


            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);


            foreach (var row in result)
            {

                permissions.Add((int)row["permission_id"]);
            }

            return permissions;
        }


        public void UpdatePassword(User user)
        {
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "UP_PASSWORD_PR"
            };

            sqlOperation.AddIntParameter("@id", user.Id);
            sqlOperation.AddStringParameter("@new_password", user.Password);

            try
            {
                sqlDao.ExecuteProcedure(sqlOperation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la contraseña: {ex.Message}");
                throw;
            }
        }

        public void CreateEmployeeSchedule(EmployeeSchedule employeeSchedule)
        {
            var sqlOperation = new SqlOperation
            {
                ProcedureName = "INS_TRAINER_SCHEDULE_PR"
            };

            sqlOperation.AddIntParameter("@Year", employeeSchedule.Year);
            sqlOperation.AddIntParameter("@Month", employeeSchedule.Month);
            sqlOperation.AddStringParameter("@FreeDays", employeeSchedule.FreeDays);
            sqlOperation.AddTimeParameter("@StartTime", employeeSchedule.StartTime);
            sqlOperation.AddTimeParameter("@EndTime", employeeSchedule.EndTime);
            sqlOperation.AddIntParameter("@employee_id", employeeSchedule.EmployeeId);
            sqlOperation.AddCharParameter("@is_available", employeeSchedule.IsAvailable);

            try
            {
                sqlDao.ExecuteProcedure(sqlOperation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear el horario del entrenador: {ex.Message}");
                throw;
            }
        }



        public void UpdatePermissions(List<int> permissionIds, List<int> permissionsForDelete, int employeeId)
        {
            if (permissionIds.Count > 0)
            {
                foreach (var permissionId in permissionIds)
                {
                    var sqlOperation = new SqlOperation
                    {
                        ProcedureName = "INS_USER_ROLE_PERMISSION_PR"
                    };

    
                    sqlOperation.AddIntParameter("@permission_id", permissionId);
                    sqlOperation.AddIntParameter("@employee_id", employeeId);

                    sqlDao.ExecuteQueryProcedure(sqlOperation);
                }
            }


            if (permissionsForDelete.Count > 0)
            {
                foreach (var permissionId in permissionsForDelete)
                {
                    var sqlOperation = new SqlOperation
                    {
                        ProcedureName = "DEL_USER_ROLE_PERMISSION_PR" 
                    };

                  
                    sqlOperation.AddIntParameter("@permission_id", permissionId);
                    sqlOperation.AddIntParameter("@employee_id", employeeId);

         
                    sqlDao.ExecuteQueryProcedure(sqlOperation);
                }
            }
        }





    }
}