USE [master]
GO
/****** Object:  Database [Test]    Script Date: 29/08/2017 13:19:20 ******/
CREATE DATABASE [Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Test.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Test_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Test] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [Test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Test] SET  MULTI_USER 
GO
ALTER DATABASE [Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Test] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Test] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Test]
GO
/****** Object:  User [PCSAPP]    Script Date: 29/08/2017 13:19:21 ******/
CREATE USER [PCSAPP] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[myHell_User]    Script Date: 29/08/2017 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[myHell_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Email] [varchar](500) NULL,
	[Password] [varchar](500) NULL,
	[Active] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[myHell_GET_USER]    Script Date: 29/08/2017 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[myHell_GET_USER]
@Id int,
@Error varchar(max) output,
@Message varchar(max) output
AS
BEGIN
Set @Error='NoError'
set @Message='NoMessage'
Declare @ErrorDescription varchar(max)
Declare @Result varchar(max)
declare @RowCount varchar(max)
BEGIN TRY
	--set @ErrorDescription = 'Test Error'
    --RAISERROR ('CustomError', 16, 1  );  
	if @Id=0
	begin
	select @RowCount=count(*) from myHell_User 
		if @RowCount=0 
		begin
		 set @ErrorDescription = 'Users are not available.'
         RAISERROR ('CustomError', 16, 1  );
		end
		else
		begin
		select * from myHell_User 
		end
		

	end
	else
	begin
	select @RowCount=count(*) from myHell_User  where Id=@Id
		if @RowCount=0 
		begin
		 set @ErrorDescription = 'Not Available.'
         RAISERROR ('CustomError', 16, 1  );
		end
		else
		begin
		select * from myHell_User where Id=@Id
		end
	
	end

END TRY
BEGIN CATCH
	set @Error = ERROR_MESSAGE()
	if Charindex(ERROR_MESSAGE(),'CustomError',1)>0
	begin
		select @Error = @ErrorDescription
	end
	else
	begin
		SET  @Result=CAST(ERROR_LINE() AS VARCHAR(100)) +':'+CAST(ERROR_NUMBER() AS VARCHAR(100)) +':'+ERROR_MESSAGE()
		select @Error=@Result
	end
END CATCH
end




GO
/****** Object:  StoredProcedure [dbo].[myHell_SET_USER]    Script Date: 29/08/2017 13:19:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[myHell_SET_USER]
@Id int,
@Name	varchar(500),
@Email	varchar(500),
@Password	varchar(500),
@Active	bit,
@Error varchar(max) output,
@Message varchar(max) output
AS
BEGIN
Set @Error='NoError'
set @Message='NoMessage'
Declare @ErrorDescription varchar(max)
Declare @Result varchar(max)
BEGIN TRY
	--set @ErrorDescription = 'Test Error'
    --RAISERROR ('CustomError', 16, 1  );  
BEGIN TRY 
	set xact_abort on
	BEGIN TRAN

	if @Id=0
	begin

INSERT INTO [dbo].[myHell_User]
           ([Name]
           ,[Email]
           ,[Password]
           ,[Active]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           (@Name
           ,@Email
           ,@Password
           ,@Active
           ,GETDATE()
           ,GETDATE())

	end

	if @Id > 0
	begin


UPDATE [dbo].[myHell_User]
   SET [Name] =@Name
      ,[Email] = @Email
      ,[Password] = @Password
      ,[Active] = @Active
      ,[UpdatedDate] = GETDATE()
 WHERE Id=@Id


	end
	
		
		COMMIT TRAN
		set @Message= 'Data Saved ...!!!'
	END TRY
	BEGIN CATCH
		IF (XACT_STATE()) = -1 
		BEGIN
			ROLLBACK TRAN
			SET  @Result=CAST(ERROR_LINE() AS VARCHAR(100)) +':'+CAST(ERROR_NUMBER() AS VARCHAR(100)) +':'+ERROR_MESSAGE()
			Set @Error=@Result
		END
		 IF (XACT_STATE()) = 1
		BEGIN
			COMMIT  TRAN
			SET  @Result=CAST(ERROR_LINE() AS VARCHAR(100)) +':'+CAST(ERROR_NUMBER() AS VARCHAR(100)) +':'+ERROR_MESSAGE()
			Set @Error=@Result
		END
	END CATCH	 
END TRY
BEGIN CATCH
	set @Error = ERROR_MESSAGE()
	if Charindex(ERROR_MESSAGE(),'CustomError',1)>0
	begin
		select @Error = @ErrorDescription
	end
	else
	begin
		SET  @Result=CAST(ERROR_LINE() AS VARCHAR(100)) +':'+CAST(ERROR_NUMBER() AS VARCHAR(100)) +':'+ERROR_MESSAGE()
		select @Error=@Result
	end
END CATCH
end



GO
USE [master]
GO
ALTER DATABASE [Test] SET  READ_WRITE 
GO
