using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;

namespace Users.Persistense.EntityTypeConfiguration
{
	internal class UserStateConfiguration : IEntityTypeConfiguration<UserState>
	{
		public void Configure(EntityTypeBuilder<UserState> builder)
		{
			builder.ToTable("user_state");

			builder.HasKey(s => s.Id);
			builder.HasIndex(s => s.Id).IsUnique();
			builder.HasIndex(s => s.Code).IsUnique();
			
			builder.Property(s => s.Id)
				.ValueGeneratedOnAdd()
				.HasColumnName("id")
				.IsRequired();
			builder.Property(s => s.Code)
				.HasColumnName("code")
				.HasConversion<string>()
				.IsRequired();
			builder.Property(s => s.Description)
				.HasColumnName("description");
		}
	}
}
