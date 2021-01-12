using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserApi.Core.Dtos;
using UserApi.Core.Models;
using UserApi.Core.Repository;
using UserApi.Core.Service;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;
        public UserController(IUserService _userService, ILogger<UserController> _logger)
        {
            userService = _userService;
            logger = _logger;
        }
        [HttpPost("CreateUser")]
        public async Task<(bool, string)> CreateAsync([FromForm] CretaeUserDto userDto)
        {

            //var postedFile = Request.Form.Files[0];


            //foreach (var file in postedFile)
            //{
            //    if (file.Length > 0)
            //    {
            //        using (var ms = new MemoryStream())
            //        {
            //            file.CopyTo(ms);
            //            var fileBytes = ms.ToArray();
            //            userDto.Image1= Convert.ToBase64String(fileBytes);
            //            // act on the Base64 data
            //        }
            //    }
            //}

            ////Create custom filename
            //var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            //imageName = imageName + DateTime.Now.ToString("yymmdd") + Path.GetExtension(postedFile.FileName);
            //using (var ms = new MemoryStream())
            //{
            //    userDto.Image1.CopyTo(ms);
            //    var fileBytes = ms.ToArray();

            //    //data:image/jpeg;base64
            //    string s = Convert.ToBase64String(fileBytes);

            //    string image=$"data:image/jpeg;base64{s}";
            //    // act on the Base64 data
            //}

            logger.LogInformation("create", userDto);
            return await userService.CreateAsync(userDto);
        }
        [HttpDelete("{Id}")]
        public async Task<(bool, string)> DeleteAsync(Guid Id)
        {
            logger.LogInformation("Delete Successfulluy", Id);
            return await userService.DeleteAsync(Id);

        }
        [HttpGet("GetAllUser")]
        public async Task<List<GetAllUserDto>> GetAllAsync()
        {
            logger.LogInformation("Get Successfulluy");
            return await userService.GetAllAsync();
        }
        [HttpGet("[action]")]

        public async Task<List<Country>> GetAllCountry()
        {
            logger.LogInformation("GetAllCountry Successfulluy");
            return await userService.GetAllCountry();
        }
        [HttpGet("[action]")]

        public async Task<List<State>> GetAllState()
        {
            logger.LogInformation("GetAllState Successfulluy");
            return await userService.GetAllState();
        }

        [HttpGet("{Id}")]
        public async Task<GetAllUserDto> GetByIdAsync(Guid Id)
        {
            logger.LogInformation("GetById Successfulluy", Id);
            return await userService.GetByIdAsync(Id);
        }
        [HttpGet("[action]")]

        public async Task<Country> GetCountryById(int CountryId)
        {
            logger.LogInformation("GetCountryById Successfulluy", CountryId);
            return await userService.GetCountryById(CountryId);
        }
        [HttpGet("[action]")]
        public async Task<IList<State>> GetStateByCountryId(int CountryId)
        {
            logger.LogInformation("", CountryId);
            return await userService.GetStateByCountryId(CountryId);
        }

        [HttpGet("[action]")]
        public async Task<State> GetStateById(int StateId)
        {
            logger.LogInformation("GetCountryById Successfulluy", StateId);
            return await userService.GetStateById(StateId);
        }



        [HttpPost("[action]")]

        public async Task<(bool, string, LoginResponse)> Login(loginDto loginDto)
        {
            logger.LogInformation("Login Successfulluy", loginDto);

            return await userService.Login(loginDto);
        }


        [HttpPut("UpdateUser")]
        public async Task<(bool, string)> UpdateAsync([FromForm] UpdateUserDto updateUserDto)
        {
            logger.LogInformation("Updated Successfulluy", updateUserDto);


            return await userService.UpdateAsync(updateUserDto);
        }
        [HttpPost("[action]")]
        public async Task<(bool success, string message)> VerifyEmail(string Id)
        {
            logger.LogInformation("Updated Successfulluy", Id);


            return await userService.VerifyEmail(Id);
        }
    }
}
