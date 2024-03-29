USE [Kyguidalat]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Desciption] [nvarchar](max) NULL,
	[ParentID] [int] NULL,
	[Images] [nvarchar](255) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Birth] [date] NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[ProductID] [int] NOT NULL,
	[ImPortDay] [date] NULL,
	[DiscountLevel] [int] NULL,
	[DiscountedCash] [float] NULL,
	[CustomerID] [int] NULL,
	[EmployeeID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Birth] [date] NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[Possition] [nvarchar](255) NULL,
	[Salary] [float] NULL,
	[StoreID] [int] NULL,
	[UserName] [nvarchar](255) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Export](
	[EInvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Number] [int] NULL,
	[StoreID] [int] NULL,
	[ExportDay] [date] NULL,
	[ExportKind] [int] NULL,
	[ExportPrice] [float] NULL,
	[CustomerID] [int] NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_Export] PRIMARY KEY CLUSTERED 
(
	[EInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Import](
	[IInvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[Number] [int] NULL,
	[ImportDay] [date] NULL,
	[ImportKind] [int] NULL,
	[ImportPrice] [float] NULL,
	[EndDate] [date] NULL,
	[StoreID] [int] NULL,
	[EmployeeID] [int] NULL,
	[CustomerID] [int] NULL,
 CONSTRAINT [PK_Import] PRIMARY KEY CLUSTERED 
(
	[IInvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Units] [nvarchar](255) NULL,
	[Size] [nvarchar](255) NULL,
	[Price] [float] NULL,
	[Images] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[MetaKey] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[CategoryID] [int] NULL,
	[CustomerID] [int] NULL,
 CONSTRAINT [PK_Goods] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[StoreID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[StoreKeepers] [int] NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[StoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store_Product](
	[StoreID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Number] [int] NULL,
	[Note] [nvarchar](255) NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Customer]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_Employee] FOREIGN KEY([StoreKeepers])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_Employee]
GO
ALTER TABLE [dbo].[Store_Product]  WITH CHECK ADD  CONSTRAINT [FK_Store_Product_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Store_Product] CHECK CONSTRAINT [FK_Store_Product_Product]
GO
ALTER TABLE [dbo].[Store_Product]  WITH CHECK ADD  CONSTRAINT [FK_Store_Product_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([StoreID])
GO
ALTER TABLE [dbo].[Store_Product] CHECK CONSTRAINT [FK_Store_Product_Store]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã danh mục' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'CategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên danh mục' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mô tả' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Desciption'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Danh mục cha' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hình ảnh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Images'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã khách hàng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Họ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'FirstName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'LastName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Địa chỉ mail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày sinh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Birth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Địa chỉ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số điện thoại' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Phân loại khách hàng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên đăng nhập' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mật khẩu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Discount', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày nhập kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Discount', @level2type=N'COLUMN',@level2name=N'ImPortDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mức chiết khấu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Discount', @level2type=N'COLUMN',@level2name=N'DiscountLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tiền chiết khấu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Discount', @level2type=N'COLUMN',@level2name=N'DiscountedCash'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã khách hàng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Discount', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã nhân viện' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Discount', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã nhân viên' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Họ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'FirstName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'LastName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày sinh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Birth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Địa chỉ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Địa chỉ mail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Điện thoại' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Chức vụ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Possition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mức lương' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Salary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'StoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên đăng nhập' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mật khẩu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Employee', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã hóa đơn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'EInvoiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số lượng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'mã kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'StoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày xuất kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'ExportDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kiểu xuất kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'ExportKind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Giá xuất' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'ExportPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã khách hàng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã nhân viên' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Export', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã hóa đơn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'IInvoiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'số lượng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày nhập' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'ImportDay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kiểu nhập' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'ImportKind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Giá nhập' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'ImportPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ngày hết hạn' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'StoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã nhân viên' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'mã khách hàng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Import', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mô tả' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Đơn vị tính' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Units'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Kích thước' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Giá bán' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hình ảnh' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Images'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trạng thái' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'từ khóa tìm kiếm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'MetaKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Từ khóa tìm kiếm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'MetaDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã danh mục' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'CategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã khách hàng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'StoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Địa chỉ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số điện thoại' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã thủ kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'StoreKeepers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã kho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store_Product', @level2type=N'COLUMN',@level2name=N'StoreID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã sản phẩm' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store_Product', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Số lượng' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store_Product', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ghi chú' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Store_Product', @level2type=N'COLUMN',@level2name=N'Note'
GO
