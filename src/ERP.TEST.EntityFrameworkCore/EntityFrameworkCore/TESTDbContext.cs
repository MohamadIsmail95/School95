using ERP.TEST.Cources;
using ERP.TEST.Courses;
using ERP.TEST.Students;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
namespace ERP.TEST.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TESTDbContext :
    AbpDbContext<TESTDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Cosurse { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }


    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public TESTDbContext(DbContextOptions<TESTDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
        //*************************************************
        builder.Entity<Student>(b =>
        {
            b.ToTable(TESTConsts.DbTablePrefix + "Students", TESTConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .HasMaxLength(StudentConsts.MaxNameLength)
                .IsRequired();


            //many-to-many relationship with courses table => StudentCourse
            b.HasMany(x => x.Courses).WithOne().HasForeignKey(x => x.StudentId).IsRequired();
        });

        builder.Entity<Course>(b =>
        {
            b.ToTable(TESTConsts.DbTablePrefix + "Courses", TESTConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .HasMaxLength(CourseConsts.MaxNameLength)
                .IsRequired();
            b.HasMany(x => x.Students).WithOne().HasForeignKey(x => x.CourseId).IsRequired();

        });

        builder.Entity<StudentCourse>(b =>
        {
            b.ToTable(TESTConsts.DbTablePrefix + "StudentCourses", TESTConsts.DbSchema);
            b.ConfigureByConvention();

            //define composite key
            b.HasKey(x => new { x.StudentId, x.CourseId });

            //many-to-many configuration
            b.HasOne<Student>().WithMany(x => x.Courses).HasForeignKey(x => x.StudentId).IsRequired();
            b.HasOne<Course>().WithMany(x=>x.Students).HasForeignKey(x => x.CourseId).IsRequired();
            b.HasIndex(x => new { x.StudentId, x.CourseId });
        });
    }


}

    //builder.Entity<YourEntity>(b =>
    //{
    //    b.ToTable(TESTConsts.DbTablePrefix + "YourEntities", TESTConsts.DbSchema);
    //    b.ConfigureByConvention(); //auto configure for the base class props
    //    //...
    //});
