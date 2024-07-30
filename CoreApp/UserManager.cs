using DataAccess.CRUD;
using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;

namespace CoreApp
{
    public class UserManager
    {
        private  UserCrudFactory userCrudFactory;

        public UserManager()
        {
            userCrudFactory = new UserCrudFactory();

        }

        //DUFZ4LR55J7T3ZR53C1NASMA RC Twillio
        public void Create(User user)
        {
        
            if (!IsOver12(user))
            {
                throw new Exception("User is not over 12 years old");
            }

            userCrudFactory.Create(user);
        }

        public string GenerateOTP(int userID)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int otpLength = 6;

            Random random = new Random();
            string otp = new string(Enumerable.Repeat(chars, otpLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var customer = RetrieveUserById(userID);
            var employee = RetrieveEmployeeById(userID);

            if (customer != null)
            {
                var dni = customer.Dni;
                userCrudFactory.SaveOtp(dni, otp);
            }
            else if (employee != null)
            {
                var dni = employee.Dni;
                userCrudFactory.SaveOtp(dni, otp);
            }
            else
            {
                throw new ArgumentException("Usuario o empleado no encontrado.");
            }

            return otp;
        }

        public bool ValidateOtp(string otp, int dni)
        {
            return userCrudFactory.ValidateOtp(dni, otp);
        }


        public List<User> RetrieveAllUsers()
        {
            List<User> users = new List<User>();
            users = userCrudFactory.RetrieveAll<User>();

  
            var membershipCrudFactory = new MembershipCrudFactory();


            foreach (var user in users)
            {
                if (user is Customer customer)
                {
                    int membershipId = customer.UserMembership.Id;
                    var membership = membershipCrudFactory.RetrieveAllMeasuresById<Membership>(membershipId);

                    if (membership != null)
                    {
                        customer.UserMembership = membership;
                    }
                }
            }

            return users;
        }


        public List<Employee> RetrieveAllEmployees()
        {
            return userCrudFactory.RetrieveAllEmployee<Employee>();
        }

        public User RetrieveByEmailPassword(string email, string password)
        {
            return userCrudFactory.LoginUser<User>(email, password);
        }


        public User RetrieveUserByEmail(string email)
        {

            var user = userCrudFactory.RetrieveUserByEmail<User>(email);

            return user;
        }

        public void UpdatePassword ( User user)
        {
            userCrudFactory.UpdatePassword(user);
        }


        public Customer RetrieveUserById(int dni)
        {
             
            var customer = userCrudFactory.RetrieveAllMeasuresById<Customer>(dni);
            var membershipManager = new MembershipManager();
            var membership = membershipManager.RetrieveMembershipById(customer.UserMembership.Id);
            customer.UserMembership = membership;

            return customer;
        }

        public Employee RetrieveEmployeeById(int employeeId)
        {
            return userCrudFactory.RetrieveEmployeeById<Employee>(employeeId);
        }

        public void Update(User user)
        {
            userCrudFactory.Update(user);
        }

        private bool IsOver12(User user)
        {
          
            DateTime today = DateTime.Today;
            int age = today.Year - user.BirthDate.Year;
            if (user.BirthDate > today.AddYears(-age))
            {
                age--;
            }
            return age >= 12;
        }

        public void UpdatePermissions(List<int> permissionIds, List<int> permissionsForDelete, int employeeId)
        {
            userCrudFactory.UpdatePermissions(permissionIds, permissionsForDelete, employeeId);
        }

        public void CreateEmployeeSchedule(EmployeeSchedule employeeSchedule)
        {
            ValidateEmployeeSchedule(employeeSchedule);
            userCrudFactory.CreateEmployeeSchedule(employeeSchedule);
        }


        //Validar los datos enviados para crear el schedule

        private void ValidateEmployeeSchedule(EmployeeSchedule employeeSchedule)
        {
        
            if (employeeSchedule.Year < 2024 || employeeSchedule.Year > 2100)
            {
                throw new ArgumentException("Year must be between 2024 and 2100.");
            }

          
            if (employeeSchedule.Month < 1 || employeeSchedule.Month > 12)
            {
                throw new ArgumentException("Month must be between 1 and 12.");
            }

            var freeDays = employeeSchedule.FreeDays.Split(',').Select(int.Parse);
            foreach (var day in freeDays)
            {
                if (day < 1 || day > 7)
                {
                    throw new ArgumentException("FreeDays must be between 1 and 7.");
                }
            }

      
            if (employeeSchedule.StartTime >= employeeSchedule.EndTime)
            {
                throw new ArgumentException("StartTime must be earlier than EndTime.");
            }

            if (employeeSchedule.IsAvailable != 'Y' && employeeSchedule.IsAvailable != 'N')
            {
                throw new ArgumentException("is_available must be either 'Y' or 'N'.");
            }
        }
    }

}

