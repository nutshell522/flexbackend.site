using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EFModels.EFModels
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityCategory> ActivityCategories { get; set; }
        public virtual DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<CouponCategory> CouponCategories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponSending> CouponSendings { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<OneToOneReservation> OneToOneReservations { get; set; }
        public virtual DbSet<ProjectTagItem> ProjectTagItems { get; set; }
        public virtual DbSet<ProjectTag> ProjectTags { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<SpeakerField> SpeakerFields { get; set; }
        public virtual DbSet<Speaker> Speakers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityCategory>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.ActivityCategory)
                .HasForeignKey(e => e.fk_ActivityCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ActivityStatus>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.ActivityStatus)
                .HasForeignKey(e => e.fk_ActivityStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.OneToOneReservations)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.fk_BranchId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Speakers)
                .WithOptional(e => e.Branch)
                .HasForeignKey(e => e.fk_SpeakerBranchId);

            modelBuilder.Entity<CouponCategory>()
                .HasMany(e => e.Coupons)
                .WithRequired(e => e.CouponCategory)
                .HasForeignKey(e => e.fk_CouponCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coupon>()
                .HasMany(e => e.CouponSendings)
                .WithRequired(e => e.Coupon)
                .HasForeignKey(e => e.fk_CouponId);

            modelBuilder.Entity<ProjectTag>()
                .HasMany(e => e.Coupons)
                .WithOptional(e => e.ProjectTag)
                .HasForeignKey(e => e.fk_RequiredProjectTagID);

            modelBuilder.Entity<ProjectTag>()
                .HasMany(e => e.Discounts)
                .WithOptional(e => e.ProjectTag)
                .HasForeignKey(e => e.fk_ProjectTagId);

            modelBuilder.Entity<ProjectTag>()
                .HasMany(e => e.ProjectTagItems)
                .WithRequired(e => e.ProjectTag)
                .HasForeignKey(e => e.fk_ProjectTagId);

            modelBuilder.Entity<ReservationStatus>()
                .HasMany(e => e.OneToOneReservations)
                .WithRequired(e => e.ReservationStatus)
                .HasForeignKey(e => e.fk_ReservationStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShoppingCart>()
                .HasOptional(e => e.CartItem)
                .WithRequired(e => e.ShoppingCart);

            modelBuilder.Entity<SpeakerField>()
                .HasMany(e => e.Speakers)
                .WithRequired(e => e.SpeakerField)
                .HasForeignKey(e => e.fk_SpeakerFieldId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Speaker>()
                .Property(e => e.SpeakerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Speaker>()
                .Property(e => e.SpeakerImg)
                .IsUnicode(false);

            modelBuilder.Entity<Speaker>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.Speaker)
                .HasForeignKey(e => e.fk_SpeakerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Speaker>()
                .HasMany(e => e.OneToOneReservations)
                .WithRequired(e => e.Speaker)
                .HasForeignKey(e => e.fk_ReservationSpeakerId)
                .WillCascadeOnDelete(false);
        }
    }
}
