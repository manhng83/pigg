using Pigg.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Pigg.Data.ModelConfigurations
{
    public class WebPartPlacementConfiguration : EntityTypeConfiguration<WebPartPlacement>
    {
        public WebPartPlacementConfiguration()
        {
            ToTable("WebPartPlacement");

            HasKey(k => k.WebPartPlacementId);
            Property(p => p.WebPartPlacementId)
                .HasColumnName("WebPartPlacementId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}