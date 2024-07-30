using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using Twilio.Rest.Trusthub.V1.CustomerProfiles;
using static System.Net.WebRequestMethods;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        [HttpPost]
        [Route("CreateEmployee")]
        public ActionResult CreateEmployee(Employee employee)
        {
            try
            {
                var userManager = new UserManager();
                userManager.Create(employee);

               

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public ActionResult CreateCustomer(Customer customer)
        {
            try
            {
                var userManager = new UserManager();
                userManager.Create(customer);

                var otp = userManager.GenerateOTP(customer.Dni);

                bool emailSent;

                if (customer.ConfirmationMethod.Equals("Email"))
                {
                    var emailController = new EmailService();
                    emailSent = emailController.SendEmailWithOtp(customer.Email, otp);
                }
                else
                {
                    var smsController = new SmsService();
                    emailSent = smsController.SendSmsWithOtp(customer.PhoneNumber, otp);
                }

                var membershipManager = new MembershipManager();
                var membership = membershipManager.RetrieveMembershipById(customer.UserMembership.Id);
                customer.UserMembership = membership;

                if (emailSent)
                {
                    return Ok(customer);
                }
                else
                {
                    return StatusCode(500, "Failed to send OTP.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("ConfirmAccount/{otp}/{userId}")]
        public ActionResult ConfirmAccount(string otp, int userId)
        {
            try
            {
                var userManager = new UserManager();

              
                var isOtpValid = userManager.ValidateOtp(otp, userId);

                if (isOtpValid)
                {
                    var customer = userManager.RetrieveUserById(userId);
                    customer.IsVerified = 'Y';
                    UpdateCustomer(customer);

                    var responseMessage = new { Message = "El usuario fue verificado exitosamente." };
                    return Ok(responseMessage);
                }
                else
                {
                    
                    return BadRequest("OTP inválido o expirado.");
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("SendOTP/{userId}")]
        public ActionResult SendOTP(int userId)
        {
            try
            {
                var userManager = new UserManager();
                var otp = userManager.GenerateOTP(userId);

                var customer = userManager.RetrieveUserById(userId);
                var employee = userManager.RetrieveEmployeeById(userId);

                if (customer != null)
                {
                    var emailController = new EmailService();
                    emailController.SendEmailWithOtp(customer.Email, otp);

                    var smsController = new SmsService();
                    smsController.SendSmsWithOtp(customer.PhoneNumber, otp);
                }
                else if (employee != null)
                {
                    var emailController = new EmailService();
                    emailController.SendEmailWithOtp(employee.Email, otp);

                    var smsController = new SmsService();
                    smsController.SendSmsWithOtp(employee.PhoneNumber, otp);
                }
                else
                {
                    return NotFound("Usuario no encontrado.");
                }

                var responseMessage = new { Message = "OTP fue generado y enviado a su correo y celular" };
                return Ok(responseMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("ConfirmOTPForPassword/{otp}/{userId}")]
        public ActionResult ConfirmOTPForPassword(string otp, int userId)
        {
            try
            {
                var userManager = new UserManager();
                var isOtpValid = userManager.ValidateOtp(otp, userId);

                if (isOtpValid)
                {

                    var responseMessage = new { Message = "El OTP fue verificado exitosamente. Proceda a cambiar su clave." };
                    return Ok(responseMessage);

                }
                else
                {
                    return BadRequest("OTP inválido o expirado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("RetrieveAllUsers")]
        public ActionResult RetrieveAll()
        {
            try
            {   
                var userManager = new UserManager();
                return Ok(userManager.RetrieveAllUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveAllEmployees")]
        public ActionResult RetrieveAllEmployees()
        {
            try
            {
                var userManager = new UserManager();
                return Ok(userManager.RetrieveAllEmployees());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveUserByEmail/{email}")]
        public ActionResult RetrieveUserByEmail(string email)
        {
            try
            {
                var userManager = new UserManager();
                var user = userManager.RetrieveUserByEmail(email);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("RetrieveUserById/{id}")]
        public ActionResult RetrieveUserById(int id)
        {
            try
            {
                var userManager = new UserManager();
                var user = userManager.RetrieveUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(DTOs.LoginRequest request)
        {
            try
            {
                
                var userManager = new UserManager();
                var user = userManager.RetrieveByEmailPassword(request.Email,request.Password);

                if(user is Customer)
                {
                    user = userManager.RetrieveUserById(user.Dni);

                } else
                {
                    user = userManager.RetrieveEmployeeById(user.Dni);
                }
                
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveEmployeeById/{id}")]
        public ActionResult RetrieveEmployeeById(int id)
        {
            try
            {
                var userManager = new UserManager();
                var user = userManager.RetrieveEmployeeById(id);
                if (user == null)
                {
                    return NotFound(); 
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
        
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [Route("UpdatePassword")]
        public ActionResult UpdatePassword(DTOs.LoginRequest request)
        {
            try
            {
                var userManager = new UserManager();
                var customer = userManager.RetrieveUserById(request.Id);
                var employee = userManager.RetrieveEmployeeById(request.Id);

                if (customer != null)
                {
                    customer.Password = request.Password;

                    userManager.UpdatePassword(customer);

                    var responseMessage = new { Message = "Usuario actualizado" };
                    return Ok(responseMessage);
                  
                }
                else if (employee != null)
                {
                    employee.Password = request.Password;

                    userManager.UpdatePassword(employee);

                    var responseMessage = new { Message = "Usuario actualizado" };
                    return Ok(responseMessage);
                }
                else
                {
                    return NotFound("Usuario o empleado no encontrado.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpPut]
        [Route("UpdateEmployee")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                var userManager = new UserManager();
                userManager.Update(employee);



                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public ActionResult UpdateCustomer(Customer employee)
        {
            try
            {
                var userManager = new UserManager();
                userManager.Update(employee);



                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("UpdateEmployeePermissions")]
        public ActionResult UpdateEmployeePermissions([FromBody] UpdatePermissionsRequest request)
        {
            try
            {
                var userManager = new UserManager();
                userManager.UpdatePermissions(request.PermissionIds, request.PermissionsForDelete, request.Id);

                var employee = RetrieveEmployeeById(request.Id);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateScheduleForEmployees")]
        public ActionResult CreateScheduleForEmployees(EmployeeSchedule employeeSchedule)
        {
            try
            {
                var userManager = new UserManager();
                userManager.CreateEmployeeSchedule(employeeSchedule);

                var responseMessage = new { Message = "Se ha creado el horario para el empleado" };
                return Ok(responseMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
