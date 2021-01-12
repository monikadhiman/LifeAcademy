using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApi.Core.Dtos;
using UserApi.Core.Models;
using UserApi.Core.Repository;
using UserApi.Core.Service;
using UserApi.Data.Context;

namespace UserApi.Data.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper imapper;

        public UserRepository(DataContext _dataContext, IMapper _mapper)
        {
            dataContext = _dataContext;
            imapper = _mapper;

        }
        public async Task<(User, bool, string)> CreateAsync(CretaeUserDto cretaeUserDto)
        {
            try
            {
                Hash hash = new Hash();
                string hashedPassword = hash.GenerateSha256Hash(cretaeUserDto.Password);
                hashedPassword = hashedPassword.Replace("-", string.Empty).Substring(0, 16);
                //cretaeUserDto.Password = hashedPassword;
                //cretaeUserDto.StateName = await dataContext.states.FirstOrDefaultAsync(x => x.Id == cretaeUserDto.StateId && x.StateName== cretaeUserDto.StateName) ;
                using (var ms = new MemoryStream())
                {
                    cretaeUserDto.file.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    //data:image/jpeg;base64
                    string s = Convert.ToBase64String(fileBytes);

                    string image = $"data:{cretaeUserDto.file.ContentType};base64,{s}";
                    // act on the Base64 data
                    cretaeUserDto.Image1 = image;
                }
                var data = await dataContext.users.SingleOrDefaultAsync(x => x.Email == cretaeUserDto.Email);
                if (data != null)
                    return (null, false, "Email Address Already Exists.");

                var data1 = imapper.Map<User>(cretaeUserDto);
                data1.Password = hashedPassword;

                await dataContext.AddAsync(data1);
                await dataContext.SaveChangesAsync();
                return (data1, true, "Successfully Created");


            }
            catch (Exception ex)
            {
                return (null, false, "Failed to Create");
            }



        }

        public async Task<(bool, string)> DeleteAsync(Guid Id)
        {
            try
            {
                var data = await dataContext.users.FindAsync(Id);
                if (data == null)

                    return (false, "Id does not Exist");

                data.IsDeleted = true;
                dataContext.users.Update(data);
                await dataContext.SaveChangesAsync();
                return (true, "Successfully Removed");

            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }



        public async Task<List<GetAllUserDto>> GetAllAsync()
        {

            return await (from u in dataContext.users
                          join s in dataContext.states
                          on u.StateId equals s.Id
                          join c in dataContext.countries
                          on s.CountryId equals c.Id
                          where (u.IsDeleted == false)
                          select new GetAllUserDto
                          {
                              Guid = u.Guid,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              Address = u.Address,
                              Phone = u.Phone,
                              Email = u.Email,
                              Password = u.Password,
                              Image1 = u.Image1,
                              StateDto = new StateDto { Country = c, Id = s.Id, StateName = s.StateName }

                              //state = new State { Country=c.CountryName,}

                          }).ToListAsync();

            //var user = await dataContext.users.ToListAsync();

            //return imapper.Map<List<GetAllUserDto>>(user);
        }



        public async Task<State> GetStateById(int Id)
        {
            var state = await dataContext.states.Where(x => x.Id == Id).SingleOrDefaultAsync();
            return state;
        }

        public async Task<List<State>> GetAllState()
        {
            return await dataContext.states.ToListAsync();
        }

        public async Task<GetAllUserDto> GetByIdAsync(Guid Id)
        {
            return await (from u in dataContext.users
                          join s in dataContext.states
                          on u.StateId equals s.Id
                          join c in dataContext.countries
                          on s.CountryId equals c.Id
                          where (u.Guid == Id && u.IsDeleted == false)
                          select new GetAllUserDto
                          {
                              Guid = u.Guid,
                              FirstName = u.FirstName,
                              LastName = u.LastName,
                              Address = u.Address,
                              Phone = u.Phone,
                              Email = u.Email,
                              Password = u.Password,
                              Image1 = u.Image1,
                              StateDto = new StateDto { Country = new Country { CountryName = c.CountryName }, Id = s.Id, StateName = s.StateName }
                          }).FirstOrDefaultAsync();


            //var data = await dataContext.users.Where(x => x.Guid == Id && x.IsDeleted == false).SingleOrDefaultAsync();
            //return imapper.Map<GetAllUserDto>(data);



        }

        public async Task<List<Country>> GetAllCountry()
        {
            return await dataContext.countries.ToListAsync();

        }

        public async Task<Country> GetCountryById(int StateId)
        {
            var data = await dataContext.countries.Where(X => X.Id == StateId).SingleOrDefaultAsync();
            return data;

        }
        public async Task<IList<State>> GetStateByCountryId(int CountryId)
        {
            var data = await dataContext.states.Where(X => X.CountryId == CountryId).ToListAsync();
            return data;

        }



        public async Task<(bool, string)> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var data = await dataContext.users.Where(x => x.Guid == updateUserDto.Guid).AsNoTracking().FirstOrDefaultAsync();
            if (data == null)
            {
                return (false, "Id Not Exist");
            }
            else
            {
                User user = new User();
                user.Guid = updateUserDto.Guid;
                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.Address = updateUserDto.Address;
                user.Email = updateUserDto.Email;
                user.Password = updateUserDto.Password;
                user.Phone = updateUserDto.Phone;
                user.Guid = updateUserDto.Guid;
                user.StateId = updateUserDto.StateId;
                using (var ms = new MemoryStream())
                {
                    updateUserDto.file.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    //data:image/jpeg;base64
                    string s = Convert.ToBase64String(fileBytes);

                    string image = $"data:{updateUserDto.file.ContentType};base64,{s}";
                    // act on the Base64 data
                    updateUserDto.Image1 = image;
                    user.Image1 = updateUserDto.Image1;
                }

                dataContext.Update(user);
                await dataContext.SaveChangesAsync();
                return (true, "Successfully Updated");

            }
        }

        public async Task<(bool success, string message)> VerifyEmail(string Id)
        {
            try
            {
                var data = await dataContext.users.SingleOrDefaultAsync(x => x.Guid == Guid.Parse(Id));
                if (data != null)
                {
                    data.isVerified = true;
                    await dataContext.SaveChangesAsync();
                    return (true, "Email Verified Successfully");
                }
                return (false, "Invalid Id");
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }

        }

        public async Task<(bool, string, User)> Login(loginDto loginDto)
        {
            Hash hash = new Hash();
            string hashedPassword = hash.GenerateSha256Hash(loginDto.Password);
            hashedPassword = hashedPassword.Replace("-", string.Empty).Substring(0, 16);
            var data = await dataContext.users.Where(x => x.Email == loginDto.Email && x.Password == hashedPassword).FirstOrDefaultAsync();
            if (data != null)
            {
                if (!data.isVerified)
                {
                    return (false, "Email is not Confirmed", null);
                }
                return (true, "", data);
            }

            return (false, "Email is not Confirmed", null);
        }
}
}
    