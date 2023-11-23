using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

public class RequestLoggingMiddleware
    {
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;


    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
        _next = next;
        _logger = logger;

        }

    public async Task Invoke(HttpContext context)
        {
        _logger.LogInformation($"Received request: {context.Request.Path}");

        HttpContext ctx = context;

        if (String.IsNullOrEmpty(context.Request.Headers["Authorization"]))
            {
            _logger.LogError($"Missing JWT Token Header: JWT)");
            }
        else
            {
            var JWT = context.Request.Headers["Authorization"];
            var principal = ExtractClaimsPrincipal(JWT);
            context.User = principal;
            }


        await _next(context);
        }

    private bool IsAdmin(ClaimsPrincipal principal)
        {
        // Implement the logic to check if the user has the "admin" role
        // For simplicity, let's assume the role is stored in the "role" claim.
        return principal?.IsInRole("admin") ?? false;
        }
    private ClaimsPrincipal ExtractClaimsPrincipal(string jwtToken)
        {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

        if (jsonToken == null)
            {
            throw new SecurityTokenException("Invalid JWT Token");
            }

        var claims = new List<Claim>();

        // Extract claims from the JWT token
        claims.AddRange(jsonToken.Claims);

        // Add custom claims or additional processing as needed
        // For example, extract roles and add them to claims
        var roles = jsonToken.Claims.Where(c => c.Type == "role").Select(c => c.Value);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var identity = new ClaimsIdentity(claims, "jwt", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        return new ClaimsPrincipal(identity);
        }
    }

