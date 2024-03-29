﻿// <auto-generated />
using System;
using DAL.BILLING;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.BILLING.Migrations
{
    [DbContext(typeof(BillingDbContext))]
    partial class BillingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BOL.BILLING.Orders", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptedTermsConditions")
                        .HasColumnType("bit");

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazorpayOrderID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazorpayPaymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazorpaySignature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubscriptionID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("SubscriptionID");

                    b.ToTable("Orders","bill");
                });

            modelBuilder.Entity("BOL.PLAN.AdvertisementPlan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("PlanID");

                    b.ToTable("AdvertisementPlan","plan");
                });

            modelBuilder.Entity("BOL.PLAN.BannerPlan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MonthlyPrice")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("PlanID");

                    b.ToTable("BannerPlan","plan");
                });

            modelBuilder.Entity("BOL.PLAN.DataPlan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("PlanID");

                    b.ToTable("DataPlan","plan");
                });

            modelBuilder.Entity("BOL.PLAN.EmailPlans", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("PlanID");

                    b.ToTable("EmailPlans","plan");
                });

            modelBuilder.Entity("BOL.PLAN.MagazinePlan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("PlanID");

                    b.ToTable("MagazinePlan","plan");
                });

            modelBuilder.Entity("BOL.PLAN.Period", b =>
                {
                    b.Property<int>("PeriodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DurationInMonths")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PeriodID");

                    b.ToTable("Period","plan");
                });

            modelBuilder.Entity("BOL.PLAN.Plan", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Address")
                        .HasColumnType("bit");

                    b.Property<bool>("Analytics")
                        .HasColumnType("bit");

                    b.Property<bool>("Branches")
                        .HasColumnType("bit");

                    b.Property<bool>("BrandShowcase")
                        .HasColumnType("bit");

                    b.Property<string>("CSSClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Categories")
                        .HasColumnType("int");

                    b.Property<bool>("Certification")
                        .HasColumnType("bit");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<bool>("ClientLogo")
                        .HasColumnType("bit");

                    b.Property<bool>("Communication")
                        .HasColumnType("bit");

                    b.Property<bool>("Company")
                        .HasColumnType("bit");

                    b.Property<bool>("CustomServices")
                        .HasColumnType("bit");

                    b.Property<bool>("EmailNotifications")
                        .HasColumnType("bit");

                    b.Property<bool>("FollowListing")
                        .HasColumnType("bit");

                    b.Property<int>("JobPostings")
                        .HasColumnType("int");

                    b.Property<bool>("LikeDislike")
                        .HasColumnType("bit");

                    b.Property<bool>("MembershipBadge")
                        .HasColumnType("bit");

                    b.Property<bool>("MessagesInbox")
                        .HasColumnType("bit");

                    b.Property<bool>("MonopolyProducts")
                        .HasColumnType("bit");

                    b.Property<int>("MonthlyPrice")
                        .HasColumnType("int");

                    b.Property<bool>("MultipleAssemblies")
                        .HasColumnType("bit");

                    b.Property<bool>("MultipleCities")
                        .HasColumnType("bit");

                    b.Property<bool>("MultipleLocations")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Offers")
                        .HasColumnType("int");

                    b.Property<bool>("OneHourService")
                        .HasColumnType("bit");

                    b.Property<bool>("PartnerProfile")
                        .HasColumnType("bit");

                    b.Property<bool>("PaymentMethods")
                        .HasColumnType("bit");

                    b.Property<bool>("PhotoGallery")
                        .HasColumnType("bit");

                    b.Property<bool>("PreferredPlaces")
                        .HasColumnType("bit");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<bool>("PriorityListing")
                        .HasColumnType("bit");

                    b.Property<int>("Products")
                        .HasColumnType("int");

                    b.Property<bool>("Profile")
                        .HasColumnType("bit");

                    b.Property<int>("RecentProjects")
                        .HasColumnType("int");

                    b.Property<bool>("Reviews")
                        .HasColumnType("bit");

                    b.Property<bool>("SmsNotifications")
                        .HasColumnType("bit");

                    b.Property<bool>("SocialShareButtons")
                        .HasColumnType("bit");

                    b.Property<bool>("SocialSites")
                        .HasColumnType("bit");

                    b.Property<bool>("Specialisation")
                        .HasColumnType("bit");

                    b.Property<bool>("Team")
                        .HasColumnType("bit");

                    b.Property<bool>("UserHistory")
                        .HasColumnType("bit");

                    b.Property<bool>("Wallet")
                        .HasColumnType("bit");

                    b.Property<bool>("WorkingHours")
                        .HasColumnType("bit");

                    b.HasKey("PlanID");

                    b.ToTable("Plan","plan");
                });

            modelBuilder.Entity("BOL.PLAN.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanAmount")
                        .HasColumnType("int");

                    b.Property<int?>("PlanID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("Product","plan");
                });

            modelBuilder.Entity("BOL.PLAN.SMSPlans", b =>
                {
                    b.Property<int>("PlanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("PlanID");

                    b.ToTable("SMSPlans","plan");
                });

            modelBuilder.Entity("BOL.PLAN.Subscription", b =>
                {
                    b.Property<int>("SubscriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptedTermsConditions")
                        .HasColumnType("bit");

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PeriodID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("RazorpayOrderID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazorpayPaymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazorpaySignature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("SubscriptionID");

                    b.HasIndex("PeriodID");

                    b.HasIndex("ProductID");

                    b.ToTable("Subscription","plan");
                });

            modelBuilder.Entity("BOL.BILLING.Orders", b =>
                {
                    b.HasOne("BOL.PLAN.Subscription", "Subscription")
                        .WithMany("Orders")
                        .HasForeignKey("SubscriptionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BOL.PLAN.Subscription", b =>
                {
                    b.HasOne("BOL.PLAN.Period", "Period")
                        .WithMany("Subscription")
                        .HasForeignKey("PeriodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BOL.PLAN.Product", "Product")
                        .WithMany("Subscription")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
