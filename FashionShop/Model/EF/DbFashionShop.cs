namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbFashionShop : DbContext
    {
        public DbFashionShop()
            : base("name=DbFashionShop")
        {
        }

        public virtual DbSet<contact> contact { get; set; }
        public virtual DbSet<content> content { get; set; }
        public virtual DbSet<content_tag> content_tag { get; set; }
        public virtual DbSet<feedback> feedback { get; set; }
        public virtual DbSet<footer> footer { get; set; }
        public virtual DbSet<introduce> introduce { get; set; }
        public virtual DbSet<menu> menu { get; set; }
        public virtual DbSet<menu_type> menu_type { get; set; }
        public virtual DbSet<news_category> news_category { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<product_category> product_category { get; set; }
        public virtual DbSet<slide> slide { get; set; }
        public virtual DbSet<tag> tag { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<order_detail> order_detail { get; set; }
        public virtual DbSet<user_group> user_group { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<permission> permission { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<content>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<content>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<content>()
                .Property(e => e.tag_id)
                .IsUnicode(false);

            modelBuilder.Entity<content_tag>()
                .Property(e => e.tag_id)
                .IsUnicode(false);

            modelBuilder.Entity<feedback>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<feedback>()
                .Property(e => e.subject)
                .IsUnicode(false);

            modelBuilder.Entity<footer>()
                .Property(e => e.footer_id)
                .IsUnicode(false);

            modelBuilder.Entity<introduce>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<introduce>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<news_category>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.prod_code)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<product>()
                .Property(e => e.discount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<product_category>()
                .Property(e => e.alias)
                .IsUnicode(false);

            modelBuilder.Entity<slide>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<slide>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<tag>()
                .Property(e => e.tag_id)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsUnicode(false);
        }
    }
}
