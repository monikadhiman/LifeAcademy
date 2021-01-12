
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApi.Core.Dtos;
using UserApi.Core.Models;
using UserApi.Core.Repository;
using UserApi.Core.Service;

namespace UserApi.Service.Service
{
    public class UserService : IUserService
    {
        private readonly MailSettings _mailSettings;

        public readonly JWT _jwt;
        private IConfiguration _config;
        private readonly IUserRepository userRepository;
        //private readonly IMailService mailService;
        public UserService(IUserRepository _userRepository,  IOptions<MailSettings> mailSettings, IOptions<JWT> jWT, IConfiguration configuration)
        {
            userRepository = _userRepository;
         
            _mailSettings = mailSettings.Value;
            _jwt = jWT.Value;
            _config = configuration;
        }
        public async Task<(bool, string)> CreateAsync(CretaeUserDto cretaeUserDto)
        {
           // logger.Information("create");

           // data1.isVerified = true;
            cretaeUserDto.isVerified = false;

          var (data,success,message)=await userRepository.CreateAsync(cretaeUserDto);
            if(data!=null)
            {
                MailRequest mailRequest = new MailRequest()
                {
                    ToEmail = cretaeUserDto.Email,
                    Subject = "welcome"

                };
                await SendEmailAsync(mailRequest, data.Guid.ToString());
                return (true, message);
            }

            return (false, message);





        }

        public async Task<(bool, string)> DeleteAsync(Guid Id)
        {
            return await userRepository.DeleteAsync(Id);
        }

        public async Task<List<GetAllUserDto>> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

    

     

        public async Task<GetAllUserDto> GetByIdAsync(Guid Id)
        {
            return await userRepository.GetByIdAsync( Id);
        }

        public async Task<Country> GetCountryById(int CountryId)
        {
            return await userRepository.GetCountryById(CountryId);
        }
        public async Task<List<Country>> GetAllCountry()
        {
            return await userRepository.GetAllCountry();
        }

        public async Task<State> GetStateById(int StateId)
        {
            return await userRepository.GetStateById(StateId);
        }
        public async Task<List<State>> GetAllState()
        {
            return await userRepository.GetAllState();
        }

        public async Task<(bool, string)> UpdateAsync(UpdateUserDto updateUserDto)
        {
            return await userRepository.UpdateAsync(updateUserDto);
        }

        public async Task<IList<State>> GetStateByCountryId(int CountryId)
        {
            return await userRepository.GetStateByCountryId(CountryId);
        }

        public async Task SendEmailAsync(MailRequest mailRequest, string Id)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                var token = GenerateToken(Id);
                var Message = $"<a href='http://localhost:4200/verfiy-email/{token}'> Click here</a> to verify you email address ";
                mailRequest.Body = Message;
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        private string GenerateToken(string Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Jti,Id),
                    new Claim("Date",DateTime.Now.AddMinutes(10).ToString())
                };

            var token = new JwtSecurityToken(_jwt.Issuer,
               _jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpirtyTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<(bool success, string message)> VerifyEmail(string Id)
        {
            return await userRepository.VerifyEmail(Id);
        }

        public async Task<(bool, string, LoginResponse)> Login(loginDto  loginDto)
        {

            var (success,message,data)=await userRepository.Login(loginDto);


            return (true, "", new LoginResponse { AccessToken = GenerateToken(data.Guid.ToString()),Email=data.Email,Id=data.Guid.ToString()});



        }

        //public async Task<(bool, string, string)> Login(User user)
        //{
        //    return await userRepository.Login(user);  
        //}
        //public async Task<IActionResult> ConfirmEmail(Guid guid, string user)
        //{
        //if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
        //{
        //    ModelState.AddModelError("", "User Id and Code are required");
        //    return BadRequest(ModelState);

        //}

        //var user = await userRepository.GetByIdAsync(userId);

        //if (user == null)
        //{
        //    return new JsonResult("ERROR");
        //}

        //if (user.EmailConfirmed)
        //{
        //    return Redirect("/login");
        //}

        //var result = await _userManager.ConfirmEmailAsync(user, code);

        //if (result.Succeeded)
        //{

        //    return RedirectToAction("EmailConfirmed", "Notifications", new { userId, code });

        //}
        //else
        //{
        //    List<string> errors = new List<string>();
        //    foreach (var error in result.Errors)
        //    {
        //        errors.Add(error.ToString());
        //    }
        //    return new JsonResult(errors);
        //}


    }
}



