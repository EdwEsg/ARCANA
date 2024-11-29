using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RDF.Arcana.API.Models.ExternalDb;

public partial class ExternalDbContext : DbContext
{
    public ExternalDbContext()
    {
    }

    public ExternalDbContext(DbContextOptions<ExternalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<BusinessCategory> BusinessCategories { get; set; }

    public virtual DbSet<CancellationReason> CancellationReasons { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CoaAccount> CoaAccounts { get; set; }

    public virtual DbSet<CoaCompany> CoaCompanies { get; set; }

    public virtual DbSet<CoaDepartment> CoaDepartments { get; set; }

    public virtual DbSet<CoaLocation> CoaLocations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DeptLocation> DeptLocations { get; set; }

    public virtual DbSet<FarmSource> FarmSources { get; set; }

    public virtual DbSet<IssueItem> IssueItems { get; set; }

    public virtual DbSet<MeatType> MeatTypes { get; set; }

    public virtual DbSet<MiscellaneousIssue> MiscellaneousIssues { get; set; }

    public virtual DbSet<MiscellaneousReceipt> MiscellaneousReceipts { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<MoveOrder> MoveOrders { get; set; }

    public virtual DbSet<MoveOrderItem> MoveOrderItems { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<ReceiptItem> ReceiptItems { get; set; }

    public virtual DbSet<Receiving> Receivings { get; set; }

    public virtual DbSet<ReceivingItem> ReceivingItems { get; set; }

    public virtual DbSet<RmMasterlist> RmMasterlists { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleModule> RoleModules { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Uom> Uoms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=10.10.2.6;Initial Catalog=MoveOrder;User ID=sa;Password=ULtR@MaVD3p0t2o22;Connect Timeout=600;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadOnly;Multi Subnet Failover=False;Pooling=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("area");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Area1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("area");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<BusinessCategory>(entity =>
        {
            entity.ToTable("business_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.BusinessCategory1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("business_category");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<CancellationReason>(entity =>
        {
            entity.ToTable("cancellation_reason");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.CancellationReason1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cancellation_reason");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Category1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<CoaAccount>(entity =>
        {
            entity.ToTable("coa_account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Account)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<CoaCompany>(entity =>
        {
            entity.ToTable("coa_company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<CoaDepartment>(entity =>
        {
            entity.ToTable("coa_department");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<CoaLocation>(entity =>
        {
            entity.ToTable("coa_location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.BusinessCategoryId).HasColumnName("business_category_id");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_code");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Org)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("org");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("department");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Department1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<DeptLocation>(entity =>
        {
            entity.ToTable("dept_location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
        });

        modelBuilder.Entity<FarmSource>(entity =>
        {
            entity.ToTable("farm_source");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.FarmSource1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("farm_source");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<IssueItem>(entity =>
        {
            entity.ToTable("issue_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.FarmSource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("farm_source");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemReference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_reference");
            entity.Property(e => e.ProductionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("production_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Slab).HasColumnName("slab");
        });

        modelBuilder.Entity<MeatType>(entity =>
        {
            entity.ToTable("meat_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.MeatType1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("meat_type");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<MiscellaneousIssue>(entity =>
        {
            entity.ToTable("miscellaneous_issue");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_code");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_title");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.AdjustmentDate)
                .HasColumnType("date")
                .HasColumnName("adjustment_date");
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_code");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department_code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location_code");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("transaction_date");
            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("warehouse_code");
        });

        modelBuilder.Entity<MiscellaneousReceipt>(entity =>
        {
            entity.ToTable("miscellaneous_receipt");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_code");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_title");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.AdjustmentDate)
                .HasColumnType("date")
                .HasColumnName("adjustment_date");
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_code");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department_code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location_code");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("transaction_date");
            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("warehouse_code");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("module");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.ModuleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("module_name");
            entity.Property(e => e.PathName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("path_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<MoveOrder>(entity =>
        {
            entity.ToTable("move_order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_code");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_title");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.BulkWeight).HasColumnName("bulk_weight");
            entity.Property(e => e.CancellationReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cancellation_reason");
            entity.Property(e => e.CancelledBy).HasColumnName("cancelled_by");
            entity.Property(e => e.CancelledDate)
                .HasColumnType("date")
                .HasColumnName("cancelled_date");
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_code");
            entity.Property(e => e.Crates).HasColumnName("crates");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department_code");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location_code");
            entity.Property(e => e.MeatType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("meat_type");
            entity.Property(e => e.MoveOrderTransactDate)
                .HasColumnType("date")
                .HasColumnName("move_order_transact_date");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reference");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.TransactBy).HasColumnName("transact_by");
            entity.Property(e => e.TransactStatus).HasColumnName("transact_status");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("transaction_date");
            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("warehouse_code");
        });

        modelBuilder.Entity<MoveOrderItem>(entity =>
        {
            entity.ToTable("move_order_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualQuantity).HasColumnName("actual_quantity");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.FarmSource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("farm_source");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemReference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_reference");
            entity.Property(e => e.MoveId).HasColumnName("move_id");
            entity.Property(e => e.ProductionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("production_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Slab).HasColumnName("slab");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("product_category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.ProductCategory1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_category");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.ToTable("reason");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Reason1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ReceiptItem>(entity =>
        {
            entity.ToTable("receipt_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.FarmSource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("farm_source");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemReference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_reference");
            entity.Property(e => e.ProductionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("production_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.Slab).HasColumnName("slab");
        });

        modelBuilder.Entity<Receiving>(entity =>
        {
            entity.ToTable("receiving");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_code");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("account_title");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_code");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department_code");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("location_code");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.ReceivingDate)
                .HasColumnType("date")
                .HasColumnName("receiving_date");
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reference");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("transaction_date");
            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("warehouse_code");
        });

        modelBuilder.Entity<ReceivingItem>(entity =>
        {
            entity.ToTable("receiving_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.FarmSource)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("farm_source");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemReference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_reference");
            entity.Property(e => e.ProductionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("production_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReceiveId).HasColumnName("receive_id");
            entity.Property(e => e.Slab).HasColumnName("slab");
        });

        modelBuilder.Entity<RmMasterlist>(entity =>
        {
            entity.HasKey(e => e.Int);

            entity.ToTable("rm_masterlist");

            entity.Property(e => e.Int).HasColumnName("int");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Conversion)
                .HasColumnType("decimal(18, 3)")
                .HasColumnName("conversion");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("item_code");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("item_description");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UomId).HasColumnName("uom_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<RoleModule>(entity =>
        {
            entity.ToTable("role_module");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("supplier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.SupplierCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("supplier_code");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("supplier_name");
        });

        modelBuilder.Entity<Uom>(entity =>
        {
            entity.ToTable("uom");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Uom1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uom");
            entity.Property(e => e.UomDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uom_description");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_added");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Warehouse1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("warehouse");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        throw new InvalidOperationException("Read-only context: changes are not allowed.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Read-only context: changes are not allowed.");
    }



}
