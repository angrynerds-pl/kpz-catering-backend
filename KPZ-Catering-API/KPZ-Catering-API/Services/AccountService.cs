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
    public interface IAccountService
    {
        Konto Authenticate(string username, string password);
        IEnumerable<Konto> GetAll();
    }
    public class AccountService : IAccountService
    {
        private List<Konto> _accounts = Database.Logic.DatabaseController.getAccounts();

        private readonly AppSettings _appSettings;
        public AccountService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Konto Authenticate(string username, string password)
        {
            var account = _accounts.SingleOrDefault(x => x.login == username && x.haslo == password);
            // return null if user not found
            if (account == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.konto_id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            account.token = tokenHandler.WriteToken(token);

            return account;
        }

        public IEnumerable<Konto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
