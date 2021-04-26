using Pigg.Contracts;
using Pigg.Data.ModelConfigurations;
using Pigg.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Pigg.Data
{
    public class PiggDbContext : DbContext, IUnitOfWork
    {
        public DbSet<CustomPage> CustomPages { get; set; }

        public DbSet<WebPartPlacement> WebPartPlacements { get; set; }

        public DbSet<ContentList> ContentLists { get; set; }

        public DbSet<ContentListItem> ContentListItems { get; set; }

        public DbSet<CustomSetting> CustomSettings { get; set; }

        public DbSet<Person> People { get; set; }

        public PiggDbContext()
        {
            Database.SetInitializer(new PiggInitializer());
        }

        //protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, System.Collections.Generic.IDictionary<object, object> items)
        //{
        //    var eb = entityEntry.Entity as EntityBase;
        //    if (eb != null)
        //    {
        //        switch (entityEntry.State)
        //        {
        //            case System.Data.EntityState.Added:
        //                //eb.EntityId = Guid.NewGuid();
        //                eb.SysDateInsert = DateTime.Now;
        //                eb.SysUtenteInsert = Environment.UserName;
        //                break;
        //            case System.Data.EntityState.Modified:
        //                eb.SysDateUpdate = DateTime.Now;
        //                eb.SysUtenteUpdate = Environment.UserName;
        //                break;
        //        }
        //    }
        //    return base.ValidateEntity(entityEntry, items);
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomPageConfiguration());
            modelBuilder.Configurations.Add(new WebPartPlacementConfiguration());
            modelBuilder.Configurations.Add(new ContentListConfiguration());
            modelBuilder.Configurations.Add(new ContentListItemConfiguration());
            modelBuilder.Configurations.Add(new CustomSettingConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());

            modelBuilder.Entity<ContentList>().Property(i => i.ContentListId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CustomSetting>().Property(i => i.CustomSettingId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CustomPage>().Property(i => i.CustomPageId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ContentListItem>().Property(i => i.ContentListItemId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}