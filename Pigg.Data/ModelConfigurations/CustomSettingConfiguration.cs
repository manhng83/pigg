using Pigg.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Pigg.Data.ModelConfigurations
{
    public class CustomSettingConfiguration : EntityTypeConfiguration<CustomSetting>
    {
        public CustomSettingConfiguration()
        {
            ToTable("CustomSetting");

            HasKey(k => k.CustomSettingId);
            Property(p => p.CustomSettingId)
                .HasColumnName("CustomSettingId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
        }
    }
}