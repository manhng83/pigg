using Pigg.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Pigg.Data.ModelConfigurations
{
    internal class CustomPageConfiguration : EntityTypeConfiguration<CustomPage>
    {
        public CustomPageConfiguration()
        {
            ToTable("CustomPage");

            HasKey(k => k.CustomPageId);
            Property(p => p.CustomPageId)
                .HasColumnName("PageId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            HasOptional(a => a.CustomPage1)
                .WithMany()
                .HasForeignKey(a => a.ParentCustomPageId)
                .WillCascadeOnDelete(false);
        }
    }
}