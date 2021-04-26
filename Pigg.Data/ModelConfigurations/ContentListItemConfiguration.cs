using Pigg.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Pigg.Data.ModelConfigurations
{
    public class ContentListItemConfiguration : EntityTypeConfiguration<ContentListItem>
    {
        public ContentListItemConfiguration()
        {
            ToTable("ContentListItem");

            HasKey(k => k.ContentListItemId);
            Property(p => p.ContentListItemId)
                .HasColumnName("ContentListItemId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}