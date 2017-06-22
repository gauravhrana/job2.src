IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'GetNextSequencePerson')
BEGIN
	PRINT 'Dropping Procedure GetNextSequencePerson'
	DROP  Procedure GetNextSequencePerson
END
GO

PRINT 'Creating Procedure GetNextSequencePerson'
GO


/******************************************************************************
**		File: 
**		EntityName: GetNextSequencePerson
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityDescription:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.GetNextSequencePerson
(
		@PersonId		INT
	,	@EntityName		VARCHAR(100)
	,	@NextValue		INT				OUTPUT
)
AS
BEGIN
	
	DECLARE @Upper INT
	DECLARE @Lower INT
	DECLARE @sql NVARCHAR(512)		
	DECLARE @DBName				AS	VARCHAR(50)			
	
	-- Get Database
	SELECT	@DBName				= a.ConnectionKeyName
	FROM	Configuration.dbo.SetUpConfiguration a
	WHERE	a.EntityName		= @EntityName
	
	IF	@DBName = 'ApplicationServices'
	BEGIN
		SET @DBName = 'CommonServices'
	END

	-- Get Person's Range
	SELECT	@Upper = a.RangeTo
		,	@Lower = a.RangeFrom 
	FROM	dbo.SystemDevNumbers a
	WHERE	a.PersonId = @PersonId
	
	IF	@DBName IS NOT NULL
		BEGIN
			-- Set Dynamic SQL
			SET		@sql =  N'SELECT @NextValue = (MIN(' + @EntityName + 'Id)-1)  '
				+	' FROM ' + @DBName + '.dbo.' + @EntityName 
				+	' WHERE ' + @EntityName + 'Id BETWEEN '+ CAST(@Upper AS VARCHAR(10)) +' AND '+ CAST(@Lower AS VARCHAR(10))	

		END
	ELSE
		BEGIN
			-- Set Dynamic SQL
			SET		@sql =  N'SELECT @NextValue = (MIN(' + @EntityName + 'Id)-1)  '
				+	' FROM dbo.' + @EntityName 
				+	' WHERE ' + @EntityName + 'Id BETWEEN '+ CAST(@Upper AS VARCHAR(10)) +' AND '+ CAST(@Lower AS VARCHAR(10)) 	

		END

	-- Execute Dynamic SQL to get the next value depending on person's range
	EXEC	sp_executesql 
			@query = @sql, 
			@params = N'@NextValue INT OUTPUT', 
			@NextValue = @NextValue OUTPUT 
 
	-- Check if no value exists then set the next value to lower range limit
	IF	@NextValue IS NULL
		BEGIN
			SET @NextValue = @Lower	
		END
	
END			
GO
   