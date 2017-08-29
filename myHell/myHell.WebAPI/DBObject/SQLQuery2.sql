USE [Test]
GO

/****** Object:  StoredProcedure [dbo].[myHell_SET_USER]    Script Date: 29/08/2017 11:08:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [dbo].[myHell_SET_USER]
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


