using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HorseRidingAPI.Models
{
    public partial class HorseRidingClubContext : DbContext
    {
        public HorseRidingClubContext()
        {
        }

        public HorseRidingClubContext(DbContextOptions<HorseRidingClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId)
                    .ValueGeneratedNever()
                    .HasColumnName("clientID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthDate")
                    .HasDefaultValueSql("('1980-08-03 00:00:00')");

                entity.Property(e => e.ClientEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clientEmail");

                entity.Property(e => e.ClientPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("clientPhone");

                entity.Property(e => e.EnsurenceValidity)
                    .HasColumnType("datetime")
                    .HasColumnName("ensurenceValidity")
                    .HasDefaultValueSql("('0000-00-00 00:00:00')");

                entity.Property(e => e.FName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fName");

                entity.Property(e => e.IdentityDoc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("identityDoc");

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("identityNumber");

                entity.Property(e => e.InscriptionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("inscriptionDate");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lName");

                entity.Property(e => e.LicenceValidity)
                    .HasColumnType("datetime")
                    .HasColumnName("licenceValidity")
                    .HasDefaultValueSql("('0000-00-00 00:00:00')");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("notes");

                entity.Property(e => e.Passwd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("passwd");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.PriceRate).HasColumnName("priceRate");

                entity.Property(e => e.SessionToken)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("sessionToken");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("clientID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Notes)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.NotesNavigation)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Notes_Clients");
            });

            modelBuilder.Entity<Seance>(entity =>
            {
                entity.Property(e => e.SeanceId).HasColumnName("seanceID");

                entity.Property(e => e.ClientId).HasColumnName("clientID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.DurationMinut)
                    .HasColumnName("durationMinut")
                    .HasDefaultValueSql("((120))");

                entity.Property(e => e.IsDone).HasColumnName("isDone");

                entity.Property(e => e.MonitorId).HasColumnName("monitorID");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("paymentID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SeanceGrpId).HasColumnName("seanceGrpID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Seances)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seances__clientI__440B1D61");

                entity.HasOne(d => d.Monitor)
                    .WithMany(p => p.Seances)
                    .HasForeignKey(d => d.MonitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seances__monitor__44FF419A");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(e => e.StartDate, "UQ__Tasks__CEABCBE7D922C3FF")
                    .IsUnique();

                entity.Property(e => e.TaskId).HasColumnName("taskID");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.DurationMinut)
                    .HasColumnName("durationMinut")
                    .HasDefaultValueSql("((60))");

                entity.Property(e => e.IsDone)
                    .HasColumnType("datetime")
                    .HasColumnName("isDone")
                    .HasDefaultValueSql("('0000-00-00 00:00:00')");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UserFk).HasColumnName("user_Fk");

                entity.HasOne(d => d.UserFkNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.UserFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tasks__user_Fk__412EB0B6");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserEmail, "UQ__Users__D54ADF55015DA2B3")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.AdminLevel).HasColumnName("adminLevel");

                entity.Property(e => e.ContractDate)
                    .HasColumnType("datetime")
                    .HasColumnName("contractDate")
                    .HasDefaultValueSql("('0000-00-00 00:00:00')");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.DisplayColor)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("displayColor")
                    .HasDefaultValueSql("('#0000FF')");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("datetime")
                    .HasColumnName("lastLoginTime");

                entity.Property(e => e.SessionToken)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sessionToken");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userEmail");

                entity.Property(e => e.UserFname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userFName");

                entity.Property(e => e.UserLname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userLName");

                entity.Property(e => e.UserPasswd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userPasswd");

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("userPhone");

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userType");

                entity.Property(e => e.Userphoto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userphoto")
                    .HasDefaultValueSql("('default.jpg')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
