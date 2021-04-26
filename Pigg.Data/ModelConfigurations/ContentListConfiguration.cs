using Pigg.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Pigg.Data.ModelConfigurations
{
    public class ContentListConfiguration : EntityTypeConfiguration<ContentList>
    {
        public ContentListConfiguration()
        {
            ToTable("ContentList");

            HasKey(k => k.ContentListId);
            Property(p => p.ContentListId)
                .HasColumnName("ContentListId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}