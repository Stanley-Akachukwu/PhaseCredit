using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PhaseCredit.IdentityServer4.Data
{
	public class AspNetIdentityDbContext : IdentityDbContext
	{
		public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
		  : base(options)
		{
		}
	}
}
