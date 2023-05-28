using Microsoft.IdentityModel.Tokens;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting;
using Suyaa;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Runtime.InteropServices;
using Suyaa.EFCore;
using System.Security.Claims;
using System.Reflection;

namespace sy
{
    /// <summary>
    /// Jwt相关
    /// </summary>
    public static class Jwt
    {
        /// <summary>
        /// Jwt交互令牌密钥名称
        /// </summary>
        public static string TokenName { get; set; } = "Jwt-Token";
        /// <summary>
        /// Jwt交互令牌密钥
        /// </summary>
        public static string TokenKey { get; set; } = "0b26efe2c64e4eb8ae8e97384935cb2c";
        /// <summary>
        /// Jwt交互令牌路由
        /// </summary>
        public static string RouteName { get; set; } = "Jwt";

        /// <summary>
        /// 转化为Jwt对象
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="HostException"></exception>
        public static object GetData(string token, Type type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenKey);
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
            var info = sy.Assembly.Create(type);
            if (info is null) throw new HostException($"Type '{type.FullName}' instance fail.");
            //读取信息
            foreach (var claim in jwt.Claims)
            {
                string name = claim.Type;
                var pro = type.GetProperty(name);
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
        /// 读取Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T GetData<T>(string token) where T : IJwtData, new()
        {
            return (T)GetData(token, typeof(T));
        }

        /// <summary>
        /// 创建一个Jwt令牌
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static string CreateToken<T>(T data) where T : IJwtData
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenKey);

            // 组织内容
            List<Claim> claims = new List<Claim>();
            var type = typeof(T);
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pro in pros)
            {
                string value = "";
                var proValue = pro.GetValue(data);
                if (proValue != null) value = proValue.ToString().ToNotNull();
                claims.Add(new Claim(pro.Name, value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
