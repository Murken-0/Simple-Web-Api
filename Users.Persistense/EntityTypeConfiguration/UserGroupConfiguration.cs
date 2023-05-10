using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;

namespace Users.Persistense.EntityTypeConfiguration
{
	public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
	{
		public void Configure(EntityTypeBuilder<UserGroup> builder)
		{
			builder.ToTable("user_group");

			builder.HasKey(g => g.Id);
			builder.HasIndex(g => g.Id).IsUnique();
			builder.HasIndex(g => g.Code).IsUnique();

			builder.Property(g => g.Id)
				.ValueGeneratedOnAdd()
				.HasColumnName("id")
				.IsRequired();
			builder.Property(g => g.Code)
				.HasColumnName("code")
				.IsRequired();
			builder.Property(g => g.Description)
				.HasColumnName("description");
		}
	}
}
