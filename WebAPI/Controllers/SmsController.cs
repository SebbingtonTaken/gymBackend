using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System;

namespace WebAPI.Controllers
{
    public class SmsService
    {
        private const string accountSid = "AC0babf55bea240b2f504ab51cdc814389";
        private const string authToken = "f8e225833f2cfbe92c9ebd62b5462e70";
        private const string twilioPhoneNumber = "+19387661257";

        public bool SendSmsWithOtp(int recipientPhoneNumber, string otp)
        {
            try
            {
                TwilioClient.Init(accountSid, authToken);

                string fullPhoneNumber = $"+506{recipientPhoneNumber}";

                string body = $"Tu código de verificación para el registro en nuestro gimnasio es: {otp}. Es válido por 15 minutos.";


                var message = MessageResource.Create(
                    body: body,
                    from: new PhoneNumber(twilioPhoneNumber),
                    to: new PhoneNumber(fullPhoneNumber)
                );

           

                return true;
            }
            catch (Exception ex)
            {
           
                return false; 
            }
        }
    }
}
