﻿using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GdeIzaci.Repository.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Kreiranje tvrdnji (claims) i dodavanje tvrdnje o email adresi korisnika
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));


            // Dodavanje tvrdnji za uloge korisnika
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Kreiranje ključa za potpisivanje tokena
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            // Kreiranje potvrda (credentials) za potpisivanje tokena
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Kreiranje JWT tokena sa specificiranim podacima
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            // Pretvaranje tokena u string i vraćanje rezultata
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
