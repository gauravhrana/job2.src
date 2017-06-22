IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'BackupTableData')
BEGIN
	PRINT 'Dropping Procedure BackupTableData'
	DROP  Procedure BackupTableData
END
GO

PRINT 'Creating Procedure BackupTableData'
GO


/******************************************************************************
**		File: 
**		EntityName: BackupTableData
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

CREATE Procedure dbo.BackupTableData
(
		@DBName			VARCHAR(50)
	,	@TableName		VARCHAR(50)		=	NULL
)
AS
BEGIN
	
	CREATE TABLE #sp_tables
	(
		TABLE_QUALIFIER NVARCHAR (128),
		TABLE_OWNER NVARCHAR (128),
		TABLE_NAME NVARCHAR (128),
		TABLE_TYPE VARCHAR (32),
		REMARKS VARCHAR (254)
	)
	
	DECLARE @ProcName	AS VARCHAR(50) = 'sp_tables'
	DECLARE @sql AS NVARCHAR(1000)	

	IF	@TableName IS NULL
		BEGIN
			SET		@sql =  N'INSERT INTO #sp_tables '
				+	'EXEC ' + @DBName + '.sys.' + @ProcName 
		END
	ELSE
		BEGIN
			SET		@sql =  N'INSERT INTO #sp_tables '
				+	'EXEC ' + @DBName + '.sys.' + @ProcName +' @Table_Name='''  + @TableName + ''''
		END

	--INSERT INTO #sp_tables
	EXEC	sp_executesql 
			@query = @sql

	DECLARE @BKPDATE 	VARCHAR(128)
	SET		@BKPDATE = CONVERT(VARCHAR(8), GETDATE(), 112)

	DECLARE		cursor_table CURSOR FOR
		SELECT	TABLE_NAME 
		FROM	#sp_tables
		WHERE	TABLE_TYPE = 'TABLE'
		AND		TABLE_NAME NOT IN ('sysdiagrams')

	DECLARE @Table_Name 	NVARCHAR(128)
	OPEN cursor_table
		FETCH NEXT FROM cursor_table INTO @Table_Name
	WHILE @@FETCH_STATUS=0
	BEGIN

		DECLARE @InsertSQL NVARCHAR(1000)
	
		SELECT @InsertSQL = 'SELECT * '
			+ ' INTO A' + @BKPDATE  + '_' +  @DBName + '_' + @Table_Name +
			+ ' FROM ' + @DBName + '.dbo.' + @Table_Name 
	  
		--PRINT @InsertSQL
		EXEC	sp_executesql 
				@query = @InsertSQL
	

	FETCH NEXT FROM cursor_table INTO @Table_Name
	END

	CLOSE cursor_table
	DEALLOCATE cursor_table
	
END			
GO
   