using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Domain.Entities
{
      class ExaminationEntities : DbContext
    {
        static ExaminationEntities()
        {
            //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<ExaminationEntities>());
           Database.SetInitializer<ExaminationEntities>(null);
            //this.Configuration.LazyLoadingEnabled = true;
           // System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ExaminationEntities>());
        }

        public DbSet<Answer> Answers { set; get; }
        public DbSet<Exam> Exams { set; get; }
        public DbSet<ExamType> ExamTypes { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserScore> UserScores { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
           // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserScore>()
            //          .HasOptional(c => c.Answer)
            //          .WithMany()
            //          .HasForeignKey(c=>c.UserId)
            //          .Map(m => m.MapKey("AnswerId"))
            //          .WillCascadeOnDelete(false);
            //modelBuilder.Entity<UserScore>()
            //          .HasRequired(c => c.Question)
            //          .WithOptional()
            //          .Map(m => m.MapKey("QuestionId"))
            //          .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserScore>()
                      .HasRequired(c => c.User)
                      .WithMany()
                      .HasForeignKey(c=>c.UserId)
            //          .Map(m => m.MapKey("UserId"))
                      .WillCascadeOnDelete(false);


            modelBuilder.Entity<Answer>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.Answer)
                .HasForeignKey(e => e.CorrectAnswerId);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.UserScores)
                .WithOptional(e => e.Answer)
                .HasForeignKey(e => e.UserAnswerId);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.QuestionId)
                .WillCascadeOnDelete(false);




            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

   
        }
    }
}
