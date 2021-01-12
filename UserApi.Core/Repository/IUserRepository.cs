using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserApi.Core.Dtos;
using UserApi.Core.Models;

namespace UserApi.Core.Repository
{
   public interface IUserRepository
    {
        Task<GetAllUserDto> GetByIdAsync(Guid Id);
        Task<List<GetAllUserDto>> GetAllAsync();
        Task<(User,bool, string)> CreateAsync(CretaeUserDto cretaeUserDto);
        Task<(bool, string)> UpdateAsync(UpdateUserDto updateUserDto);
        Task<(bool, string)> DeleteAsync(Guid Id);
        Task<State> GetStateById(int Id);

        Task<List<State>> GetAllState();
        Task<Country> GetCountryById(int StateId);
        Task<IList<State>> GetStateByCountryId(int CountryId);
        Task<List<Country>> GetAllCountry();
        //Task<bool> SetPassword(SetPasswordDto setPasswordDto); 
        Task<(bool success, string message)> VerifyEmail(string Id);
        Task<(bool,string,User)> Login(loginDto loginDto);

    }
}
