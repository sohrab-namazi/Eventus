﻿using ArsamBackend.Models;
using ArsamBackend.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArsamBackend.Security
{
    public static class JWTokenHandler
    {
        public static string GenerateToken(AppUser user)
        {
            var TokenSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.TokenSignKey));
            var Creds = new SigningCredentials(TokenSignKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = Creds
            };

            var Token = TokenHandler.CreateToken(TokenDescriptor);

            return TokenHandler.WriteToken(Token);
        }

        #region utilities
        public static bool ValidateToken(string token)
        {
            var TokenSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.TokenSignKey));
            var TokenHandler = new JwtSecurityTokenHandler();
            try
            {
                TokenHandler.ValidateToken(token, new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = TokenSignKey,
                    ValidateAudience = false,
                    ValidateIssuer = false
                }, out SecurityToken validatedToken);
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public static string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        #endregion utilities

    }
}