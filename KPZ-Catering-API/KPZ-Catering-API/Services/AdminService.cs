using KPZ_Catering_API.Database.Entities;
using KPZ_Catering_API.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Catering_API.Services
{
    public interface IAdminService
    {
        Admin Authenticate(string username, string password);
        IEnumerable<Admin> GetAll();
    }
    public class AdminService : IAdminService
    {
        private List<Admin> _admins = Database.Logic.DatabaseController.GetAdmins();

        private readonly AppSettings _appSettings;
        public AdminService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Admin Authenticate(string username, string password)
        {
            var admin = _admins.SingleOrDefault(x => x.login == username && x.haslo == password);
            // return null if user not found
            if (admin == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.admin_id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            admin.token = tokenHandler.WriteToken(token);

            return admin;
        }

        public IEnumerable<Admin> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
