using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;

namespace Users.Persistense.EntityTypeConfiguration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("user");

			builder.HasKey(u => u.Id);
			builder.HasIndex(u => u.Id).IsUnique();
			
			builder.HasOne(u => u.GroupRelation)
				.WithMany(g => g.Users)
				.HasForeignKey(u => u.GroupId);
			builder.HasOne(u => u.StateRelation)
				.WithMany(g => g.Users)
				.HasForeignKey(u => u.StateId);

			builder.Property(u => u.Id)
				.HasColumnName("id")
				.IsRequired();
			builder.Property(u => u.Login)
				.HasColumnName("login")
				.IsRequired();
			builder.Property(u => u.Password)
				.HasColumnName("password")
				.IsRequired();
			builder.Property(u => u.CreationTime)
				.HasColumnName("created_date")
				.IsRequired();
			builder.Property(u => u.GroupId)
				.HasColumnName("user_group_id")
				.IsRequired();
			builder.Property(u => u.StateId)
				.HasColumnName("user_state_id")
				.IsRequired();
		}
	}
}
