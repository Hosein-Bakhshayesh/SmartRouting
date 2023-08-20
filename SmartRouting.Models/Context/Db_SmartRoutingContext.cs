using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartRouting.Models.Models;

namespace SmartRouting.Models.Context;

public partial class Db_SmartRoutingContext : DbContext
{
    public Db_SmartRoutingContext()
    {
    }

    public Db_SmartRoutingContext(DbContextOptions<Db_SmartRoutingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TGlcdossierDetailInfo> TGlcdossierDetailInfos { get; set; }

    public virtual DbSet<TGlcdriverInfo> TGlcdriverInfos { get; set; }

    public virtual DbSet<TGlcdriverShiftInfo> TGlcdriverShiftInfos { get; set; }

    public virtual DbSet<TGlcfuelTypeInfo> TGlcfuelTypeInfos { get; set; }

    public virtual DbSet<TGlcnavyInfo> TGlcnavyInfos { get; set; }

    public virtual DbSet<TGlcnavyOwnerInfo> TGlcnavyOwnerInfos { get; set; }

    public virtual DbSet<TGlcnavyRoomTypeInfo> TGlcnavyRoomTypeInfos { get; set; }

    public virtual DbSet<TGlcnavyTypeInfo> TGlcnavyTypeInfos { get; set; }

    public virtual DbSet<TGlcterminalInfo> TGlcterminalInfos { get; set; }

    public virtual DbSet<TGlcuserRoleInfo> TGlcuserRoleInfos { get; set; }

    public virtual DbSet<TGlcusersInfo> TGlcusersInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.127.11.113;Initial Catalog=Db_SmartRouting;User Id=h.bakhshayesh;Password=Hb09378149897;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TGlcdossierDetailInfo>(entity =>
        {
            entity.HasKey(e => e.GlcdossierDetailInfoId).HasName("PK_T_Shipping");

            entity.ToTable("T_GLCDossierDetailInfo");

            entity.Property(e => e.GlcdossierDetailInfoId).HasColumnName("GLCDossierDetailInfo_ID");
            entity.Property(e => e.DossierDetailCaddress).HasColumnName("DossierDetail_CAddress");
            entity.Property(e => e.DossierDetailCmobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DossierDetail_CMobile");
            entity.Property(e => e.DossierDetailCustomer).HasColumnName("DossierDetail_Customer");
            entity.Property(e => e.DossierDetailDriverId).HasColumnName("DossierDetail_DriverID");
            entity.Property(e => e.DossierDetailHour).HasColumnName("DossierDetail_Hour");
            entity.Property(e => e.DossierDetailLength).HasColumnName("DossierDetail_Length");
            entity.Property(e => e.DossierDetailNumber)
                .HasMaxLength(50)
                .HasColumnName("DossierDetail_Number");
            entity.Property(e => e.DossierDetailProduct).HasColumnName("DossierDetail_Product");
            entity.Property(e => e.DossierDetailQuantity).HasColumnName("DossierDetail_Quantity");
            entity.Property(e => e.DossierDetailType).HasColumnName("DossierDetail_Type");
            entity.Property(e => e.DossierDetailWidth).HasColumnName("DossierDetail_Width");

            entity.HasOne(d => d.DossierDetailDriver).WithMany(p => p.TGlcdossierDetailInfos)
                .HasForeignKey(d => d.DossierDetailDriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_LGCDriver_T_Shipping");
        });

        modelBuilder.Entity<TGlcdriverInfo>(entity =>
        {
            entity.HasKey(e => e.GlcdriverInfoId).HasName("PK_T_LGCDriver");

            entity.ToTable("T_GLCDriverInfo");

            entity.Property(e => e.GlcdriverInfoId).HasColumnName("GLCDriverInfo_ID");
            entity.Property(e => e.GlcdriverLastName).HasColumnName("GLCDriver_LastName");
            entity.Property(e => e.GlcdriverMobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GLCDriver_Mobile");
            entity.Property(e => e.GlcdriverName).HasColumnName("GLCDriver_Name");
            entity.Property(e => e.GlcdriverNationalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GLCDriver_NationalCode");
            entity.Property(e => e.GlcdriverNavyInfoId).HasColumnName("GLCDriver_NavyInfoID");
            entity.Property(e => e.GlcdriverPhotoPath).HasColumnName("GLCDriver_PhotoPath");

            entity.HasOne(d => d.GlcdriverNavyInfo).WithMany(p => p.TGlcdriverInfos)
                .HasForeignKey(d => d.GlcdriverNavyInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_NLGC_T_LGCDriver");
        });

        modelBuilder.Entity<TGlcdriverShiftInfo>(entity =>
        {
            entity.HasKey(e => e.GlcdriverShiftInfoId).HasName("PK_DriverShift");

            entity.ToTable("T_GLCDriverShiftInfo");

            entity.Property(e => e.GlcdriverShiftInfoId).HasColumnName("GLCDriverShiftInfo_ID");
            entity.Property(e => e.GlcdriverShiftBeginHour)
                .HasMaxLength(50)
                .HasColumnName("GLCDriverShift_BeginHour");
            entity.Property(e => e.GlcdriverShiftDay)
                .HasMaxLength(50)
                .HasColumnName("GLCDriverShift_Day");
            entity.Property(e => e.GlcdriverShiftDriverId).HasColumnName("GLCDriverShift_DriverID");
            entity.Property(e => e.GlcdriverShiftEndHour)
                .HasMaxLength(50)
                .HasColumnName("GLCDriverShift_EndHour");
            entity.Property(e => e.GlcdriverShiftTerminalId).HasColumnName("GLCDriverShift_TerminalID");

            entity.HasOne(d => d.GlcdriverShiftDriver).WithMany(p => p.TGlcdriverShiftInfos)
                .HasForeignKey(d => d.GlcdriverShiftDriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_LGCDriver_T_DriverShift");

            entity.HasOne(d => d.GlcdriverShiftTerminal).WithMany(p => p.TGlcdriverShiftInfos)
                .HasForeignKey(d => d.GlcdriverShiftTerminalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Terminal_T_DriverShift");
        });

        modelBuilder.Entity<TGlcfuelTypeInfo>(entity =>
        {
            entity.HasKey(e => e.GlcfuelTypeId).HasName("PK_TGLCFuelTypeInfo");

            entity.ToTable("T_GLCFuelTypeInfo");

            entity.Property(e => e.GlcfuelTypeId).HasColumnName("GLCFuelType_ID");
            entity.Property(e => e.GlcfurlTypeName).HasColumnName("GLCFurlType_Name");
        });

        modelBuilder.Entity<TGlcnavyInfo>(entity =>
        {
            entity.HasKey(e => e.GlcnavyInfoId).HasName("PK_T_NLGC");

            entity.ToTable("T_GLCNavyInfo");

            entity.Property(e => e.GlcnavyInfoId).HasColumnName("GLCNavyInfo_ID");
            entity.Property(e => e.GlcnavyOwnerType).HasColumnName("GLCNavy_OwnerType");
            entity.Property(e => e.GlcnavyPelak2).HasColumnName("GLCNavy_Pelak2");
            entity.Property(e => e.GlcnavyPelak3).HasColumnName("GLCNavy_Pelak3");
            entity.Property(e => e.GlcnavyPelakChar)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("GLCNavy_PelakChar");
            entity.Property(e => e.GlcnavyPelakIran).HasColumnName("GLCNavy_PelakIran");
            entity.Property(e => e.GlcnavyType).HasColumnName("GLCNavy_Type");

            entity.HasOne(d => d.GlcnavyOwnerTypeNavigation).WithMany(p => p.TGlcnavyInfos)
                .HasForeignKey(d => d.GlcnavyOwnerType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GLCNavyOwner_T_GLCNavyInfo1");

            entity.HasOne(d => d.GlcnavyTypeNavigation).WithMany(p => p.TGlcnavyInfos)
                .HasForeignKey(d => d.GlcnavyType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GLCNavyType_T_GLCNavyInfo1");
        });

        modelBuilder.Entity<TGlcnavyOwnerInfo>(entity =>
        {
            entity.HasKey(e => e.GlcnavyOwnerId).HasName("PK_TGLCNavyOwnerInfo");

            entity.ToTable("T_GLCNavyOwnerInfo");

            entity.Property(e => e.GlcnavyOwnerId).HasColumnName("GLCNavyOwner_ID");
            entity.Property(e => e.GlcnavyOwnerName).HasColumnName("GLCNavyOwner_Name");
        });

        modelBuilder.Entity<TGlcnavyRoomTypeInfo>(entity =>
        {
            entity.HasKey(e => e.GlcnavyRoomTypeId).HasName("PK_TGLCNavyRoomTypeInfo");

            entity.ToTable("T_GLCNavyRoomTypeInfo");

            entity.Property(e => e.GlcnavyRoomTypeId).HasColumnName("GLCNavyRoomType_ID");
            entity.Property(e => e.GlcnavyRoomTypeName).HasColumnName("GLCNavyRoomType_Name");
        });

        modelBuilder.Entity<TGlcnavyTypeInfo>(entity =>
        {
            entity.HasKey(e => e.GlcnavyTypeInfoId).HasName("PK_T_Navgan");

            entity.ToTable("T_GLCNavyTypeInfo");

            entity.Property(e => e.GlcnavyTypeInfoId).HasColumnName("GLCNavyTypeInfo_ID");
            entity.Property(e => e.GlcnavyTypeFuel).HasColumnName("GLCNavyType_Fuel");
            entity.Property(e => e.GlcnavyTypeHeight).HasColumnName("GLCNavyType_Height");
            entity.Property(e => e.GlcnavyTypeLength).HasColumnName("GLCNavyType_Length");
            entity.Property(e => e.GlcnavyTypeModel).HasColumnName("GLCNavyType_Model");
            entity.Property(e => e.GlcnavyTypeName).HasColumnName("GLCNavyType_Name");
            entity.Property(e => e.GlcnavyTypeWidth).HasColumnName("GLCNavyType_Width");

            entity.HasOne(d => d.GlcnavyTypeFuelNavigation).WithMany(p => p.TGlcnavyTypeInfos)
                .HasForeignKey(d => d.GlcnavyTypeFuel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GLCNavyFuel_T_GLCNavyTypeInfo");

            entity.HasOne(d => d.GlcnavyTypeModelNavigation).WithMany(p => p.TGlcnavyTypeInfos)
                .HasForeignKey(d => d.GlcnavyTypeModel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GLCNavyModel_T_GLCNavyTypeInfo");
        });

        modelBuilder.Entity<TGlcterminalInfo>(entity =>
        {
            entity.HasKey(e => e.GlcterminalInfoId).HasName("PK_T_Terminal");

            entity.ToTable("T_GLCTerminalInfo");

            entity.Property(e => e.GlcterminalInfoId).HasColumnName("GLCTerminalInfo_ID");
            entity.Property(e => e.GlcterminalId).HasColumnName("GLCTerminal_ID");
            entity.Property(e => e.GlcterminalName).HasColumnName("GLCTerminal_Name");
        });

        modelBuilder.Entity<TGlcuserRoleInfo>(entity =>
        {
            entity.HasKey(e => e.GlcuserRoleInfoId);

            entity.ToTable("T_GLCUserRoleInfo");

            entity.Property(e => e.GlcuserRoleInfoId).HasColumnName("GLCUserRoleInfo_ID");
            entity.Property(e => e.GlcuserRoleName).HasColumnName("GLCUserRole_Name");
        });

        modelBuilder.Entity<TGlcusersInfo>(entity =>
        {
            entity.HasKey(e => e.GlcusersInfoId).HasName("PK_T_LGCUser");

            entity.ToTable("T_GLCUsersInfo");

            entity.Property(e => e.GlcusersInfoId).HasColumnName("GLCUsersInfo_ID");
            entity.Property(e => e.GlcusersLastName).HasColumnName("GLCUsers_LastName");
            entity.Property(e => e.GlcusersMobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GLCUsers_Mobile");
            entity.Property(e => e.GlcusersName).HasColumnName("GLCUsers_Name");
            entity.Property(e => e.GlcusersNationalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GLCUsers_NationalCode");
            entity.Property(e => e.GlcusersRoleId).HasColumnName("GLCUsers_RoleID");

            entity.HasOne(d => d.GlcusersRole).WithMany(p => p.TGlcusersInfos)
                .HasForeignKey(d => d.GlcusersRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_GLCUsersRoleInfo_T_GLCUsersInfo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
