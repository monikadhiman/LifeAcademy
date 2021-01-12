using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserApi.Core.Dtos;
using UserApi.Core.Models;

namespace UserApi.Core.Service
{
   public interface IUserService
    {
        Task<GetAllUserDto> GetByIdAsync(Guid ID);
        Task<List<GetAllUserDto>> GetAllAsync();
        Task<(bool, string)> CreateAsync(CretaeUserDto userDto);

        Task<(bool, string)> UpdateAsync(UpdateUserDto userDto);

        Task<(bool, string)> DeleteAsync(Guid Id);
        Task<State> GetStateById(int StateId);
        Task<IList<State>> GetStateByCountryId(int CountryId);

        Task<List<State>> GetAllState();
        Task<Country> GetCountryById(int CountryId);
        Task<List<Country>> GetAllCountry();
        //Task<bool> SetPassword(SetPasswordDto setPasswordDto);
        Task SendEmailAsync(MailRequest mailRequest, string guid);

        Task<(bool success, string message)> VerifyEmail(string Id);
        Task<(bool, string, LoginResponse)> Login(loginDto loginDto);



    }
}
