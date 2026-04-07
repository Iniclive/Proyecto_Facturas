namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("sub")?.Value;

            if (int.TryParse(claim, out var userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("El token no contiene un ID de usuario válido.");
        }
    }
}
