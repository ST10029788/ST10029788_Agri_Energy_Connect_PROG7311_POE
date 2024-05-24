-- Set ANSI_NULLS ON to ensure that all comparisons to NULL are handled consistently
SET ANSI_NULLS ON
GO

-- Set QUOTED_IDENTIFIER ON to specify that identifiers delimited by double quotation marks are treated as quoted identifiers
SET QUOTED_IDENTIFIER ON
GO

-- Create the Employee table
CREATE TABLE [dbo].[Employee](
      NOT NULL, -- Define the Employee_ID column as a non-nullable nvarchar with a maximum length of 100 characters
      NOT NULL -- Define the Password column as a non-nullable nvarchar with a maximum length of 100 characters
) ON [PRIMARY] -- Specify the filegroup for the table
GO

-- Set ANSI_PADDING ON to ensure that padding is used when the column size is defined using CHAR, VARCHAR, BINARY, or VARBINARY data types
SET ANSI_PADDING ON
GO

-- Add a primary key constraint to the Employee table on the Employee_ID column
ALTER TABLE [dbo].[Employee] ADD CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
    [Employee_ID] ASC -- Specify the Employee_ID column as the primary key in ascending order
) WITH (
    STATISTICS_NORECOMPUTE = OFF, -- Disable automatic statistics updating
    IGNORE_DUP_KEY = OFF, -- Do not ignore duplicate keys
    ONLINE = OFF, -- Do not allow online index operations
    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF -- Do not optimize for sequential key values
) ON [PRIMARY] -- Specify the filegroup for the index
GO

-- Create the Farmer table
SET ANSI_NULLS ON -- Ensure ANSI_NULLS is ON before creating the table
GO
SET QUOTED_IDENTIFIER ON -- Ensure QUOTED_IDENTIFIER is ON before creating the table
GO
CREATE TABLE [dbo].[Farmer](
      NOT NULL, -- Define the Farmer_ID column as a non-nullable nvarchar with a maximum length of 100 characters
      NULL, -- Define the FarmerName column as a nullable nvarchar with a maximum length of 100 characters
      NOT NULL -- Define the Password column as a non-nullable nvarchar with a maximum length of 100 characters
) ON [PRIMARY] -- Specify the filegroup for the table
GO

-- Set ANSI_PADDING ON to ensure that padding is used when the column size is defined using CHAR, VARCHAR, BINARY, or VARBINARY data types
SET ANSI_PADDING ON
GO

-- Add a primary key constraint to the Farmer table on the Farmer_ID column
ALTER TABLE [dbo].[Farmer] ADD CONSTRAINT [PK_Farmer] PRIMARY KEY CLUSTERED 
(
    [Farmer_ID] ASC -- Specify the Farmer_ID column as the primary key in ascending order
) WITH (
    STATISTICS_NORECOMPUTE = OFF, -- Disable automatic statistics updating
    IGNORE_DUP_KEY = OFF, -- Do not ignore duplicate keys
    ONLINE = OFF, -- Do not allow online index operations
    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF -- Do not optimize for sequential key values
) ON [PRIMARY] -- Specify the filegroup for the index
GO

-- Create the Product table
SET ANSI_NULLS ON -- Ensure ANSI_NULLS is ON before creating the table
GO
SET QUOTED_IDENTIFIER ON -- Ensure QUOTED_IDENTIFIER is ON before creating the table
GO
CREATE TABLE [dbo].[Product](
    [Product_ID] [int] IDENTITY(1,1) NOT NULL, -- Define the Product_ID column as an identity column starting from 1 with a seed of 1
      NOT NULL, -- Define the Farmer_ID column as a non-nullable nvarchar with a maximum length of 100 characters
      NOT NULL, -- Define the Product_Name column as a non-nullable varchar with a maximum length of 100 characters
      NOT NULL, -- Define the Product_Type column as a non-nullable varchar with a maximum length of 100 characters
    [Product_Date] [date] NULL, -- Define the Product_Date column as a nullable date
    [Product_Price] [decimal](18, 2) NULL, -- Define the Product_Price column as a nullable decimal with precision 18 and scale 2
    [Product_Quantity] [int] NULL -- Define the Product_Quantity column as a nullable int
) ON [PRIMARY] -- Specify the filegroup for the table
GO

-- Add a primary key constraint to the Product table on the Product_ID column
ALTER TABLE [dbo].[Product] ADD PRIMARY KEY CLUSTERED 
(
    [Product_ID] ASC -- Specify the Product_ID column as the primary key in ascending order
) WITH (
    STATISTICS_NORECOMPUTE = OFF, -- Disable automatic statistics updating
    IGNORE_DUP_KEY = OFF, -- Do not ignore duplicate keys
    ONLINE = OFF, -- Do not allow online index operations
    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF -- Do not optimize for sequential key values
) ON [PRIMARY] -- Specify the filegroup for the index
GO

-- Add a foreign key constraint to the Product table referencing the Farmer table on the Farmer_ID column
ALTER TABLE [dbo].[Product]  WITH CHECK ADD CONSTRAINT [FK_Product_Farmer] FOREIGN KEY([Farmer_ID])
REFERENCES [dbo].[Farmer] ([Farmer_ID]) -- Specify the referenced table and column
GO

-- Ensure the foreign key constraint on the Product table is checked
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Farmer]
GO
