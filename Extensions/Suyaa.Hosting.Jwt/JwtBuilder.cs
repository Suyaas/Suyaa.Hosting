using Microsoft.IdentityModel.Tokens;
using Suyaa.Hosting.Infrastructure.Exceptions;
using Suyaa.Hosting.Jwt.Configures;
using Suyaa.Hosting.Jwt.Dependency;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt构建器
    /// </summary>
    public sealed class JwtBuilder<TData> : IJwtBuilder<TData>
        where TData : class, IJwtData, new()
    {
        // 配置项
        private readonly JwtConfig _option;
        private readonly Type _type;

        /// <summary>
        /// Jwt构建器
        /// </summary>
        /// <param name="option">配置项</param>
        public JwtBuilder(JwtConfig option)
        {
            _option = option;
            _type = typeof(TData);
        }

        /// <summary>
        /// 配置项
        /// </summary>
        public JwtConfig Option => _option;

        /// <summary>
        /// 转化为Jwt对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="HostException"></exception>
        public TData GetData(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_option.TokenKey);
            JwtSecurityToken jwt;
            // 检验Token
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);
                jwt = (JwtSecurityToken)validatedToken;
            }
            catch
            {
                throw new HostException($"Jwt invalid.");
            }
            if (jwt is null) throw new HostException("Jwt invalid.");
            var info = new TData();
            //读取信息
            foreach (var claim in jwt.Claims)
            {
                string name = claim.Type;
                var pro = _type.GetProperty(name);
                if (pro is null) continue;
                string value = claim.Value;
                var typeCode = pro.PropertyType.GetTypeCode();
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        pro.SetValue(info, value.IsTrue());
                        break;
                    case TypeCode.DateTime:
                        pro.SetValue(info, sy.Time.Parse(value).DateTime);
                        break;
                    case TypeCode.Byte:
                        pro.SetValue(info, value.ToByte());
                        break;
                    case TypeCode.Char:
                        pro.SetValue(info, value.IsNullOrWhiteSpace() ? '\0' : value[0]);
                        break;
                    case TypeCode.Int16:
                        pro.SetValue(info, (Int16)value.ToInteger());
                        break;
                    case TypeCode.Int32:
                        pro.SetValue(info, value.ToInteger());
                        break;
                    case TypeCode.Int64:
                        pro.SetValue(info, value.ToLong());
                        break;
                    case TypeCode.UInt16:
                        pro.SetValue(info, (UInt16)value.ToInteger());
                        break;
                    case TypeCode.UInt32:
                        pro.SetValue(info, (UInt32)value.ToInteger());
                        break;
                    case TypeCode.UInt64:
                        pro.SetValue(info, (UInt64)value.ToLong());
                        break;
                    case TypeCode.Single:
                        pro.SetValue(info, value.ToFloat());
                        break;
                    case TypeCode.Double:
                        pro.SetValue(info, value.ToDouble());
                        break;
                    case TypeCode.Decimal:
                        pro.SetValue(info, (decimal)value.ToDouble());
                        break;
                    case TypeCode.String:
                        pro.SetValue(info, value);
                        break;
                    default:
                        throw new HostException($"Unsupported property type '{typeCode}' for '{name}'.");
                }
            }
            return info;
        }

        /// <summary>
        /// 创建一个Jwt令牌
        /// </summary>
        /// <param name="data"></param>
        /// <param name="expires"></param>
        public JwtToken CreateToken(TData data, DateTime? expires = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_option.TokenKey);

            // 组织内容
            var claims = new List<Claim>();
            var pros = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pro in pros)
            {
                string value = "";
                var proValue = pro.GetValue(data);
                if (proValue != null) value = proValue.ToString().Fixed();
                claims.Add(new Claim(pro.Name, value));
            }
            // 处理失效时间，默认为1天
            expires ??= DateTime.UtcNow.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // 返回令牌信息
            return new JwtToken()
            {
                Token = tokenHandler.WriteToken(token),
                Expires = expires.Value,
            };
        }
    }
}
