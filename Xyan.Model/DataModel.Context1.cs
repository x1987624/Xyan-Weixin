﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xyan.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataModelContainer : DbContext
    {
        public DataModelContainer()
            : base("name=DataModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BaseUser> BaseUser { get; set; }
        public virtual DbSet<R_User_Role> R_User_Role { get; set; }
        public virtual DbSet<BaseRole> BaseRole { get; set; }
        public virtual DbSet<BasePermission> BasePermission { get; set; }
        public virtual DbSet<R_User_Permission> R_User_Permission { get; set; }
        public virtual DbSet<R_Role_Permission> R_Role_Permission { get; set; }
        public virtual DbSet<BasePermissionGroup> BasePermissionGroup { get; set; }
        public virtual DbSet<R_Group_Permission> R_Group_Permission { get; set; }
        public virtual DbSet<R_Group_User> R_Group_User { get; set; }
        public virtual DbSet<R_Group_Role> R_Group_Role { get; set; }
    }
}
