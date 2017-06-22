IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditTestData')
BEGIN
	PRINT 'Dropping Procedure AuditTestData'
	DROP  Procedure  AuditTestData
END
GO

PRINT 'Creating Procedure AuditTestData'
GO

/******************************************************************************
**		File: 
**		Name: AuditTestData
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.AuditTestData
(
	@SystemEntityTypeId		INT	=	NULL
)
AS
BEGIN

	DECLARE @sql NVARCHAR(512)
	DECLARE @EntityName VARCHAR(50) = 'Person'
	DECLARE @MyCount AS INT = 0 

	
	CREATE TABLE #TempRecords
	(		
			SystemEntityTypeId  INT			
		,	SystemEntityType    VARCHAR(50)
		,   TestDataCount		INT
		,   AuditHistoryCount	INT
	) 

	CREATE TABLE #TempMain
	(		
			SystemEntityTypeId	INT			
		,	SystemEntityType    VARCHAR(50)
	)

	INSERT INTO #TempRecords (SystemEntityTypeId, SystemEntityType)
	SELECT	b.SystemEntityTypeId
		,	a.name 
	FROM		dbo.sys.Tables a
	INNER JOIN	Configuration.dbo.SystemEntityType b ON a.name = b.EntityName
	WHERE	b.SystemEntityTypeId	=
			CASE
				WHEN @SystemEntityTypeId IS NULL THEN b.SystemEntityTypeId
				ELSE @SystemEntityTypeId
			END	

	SELECT @MyCount = COUNT(*) FROM  #TempRecords

	DECLARE @iCNT AS INT = 1
	DECLARE @tmpCNT AS INT
	DECLARE @SystemEntityId AS INT

	WHILE @iCNT <= @MyCount
		BEGIN 
		
			SET ROWCOUNT @iCNT

			INSERT INTO #TempMain
			SELECT   a.SystemEntityTypeId
				,    a.SystemEntityType
			FROM #TempRecords a
			ORDER BY a.SystemEntityTypeId ASC

			SET ROWCOUNT 0
						
			SELECT	@EntityName = a.SystemEntityType
				,	@SystemEntityId = a.SystemEntityTypeId 
			FROM #TempRecords a
			WHERE a.SystemEntityTypeId = 
			(	
				SELECT MAX(SystemEntityTypeId)
				FROM #TempMain
			) 

			SET @iCNT = @iCNT + 1
			DELETE FROM #TempMain

			BEGIN TRY
        
				-- Set Dynamic SQL
				SET @sql =  N'SELECT @tmpCNT = COUNT(*) FROM TaskTimeTracker.dbo.'+ @EntityName 
						+	' WHERE '+ @EntityName +'Id < 0'	

				-- Execute Dynamic SQL to get the next value depending on person's range
				EXEC sp_executesql 
					@query = @sql,
					@params = N'@tmpCNT INT OUTPUT', 
					@tmpCNT = @tmpCNT OUTPUT
			
				--SELECT @tmpCNT, @EntityName
			
				UPDATE	#TempRecords 
				SET		TestDataCount		= @tmpCNT
				WHERE	SystemEntityType	= @EntityName
			
				UPDATE	#TempRecords
				SET		AuditHistoryCount = (SELECT COUNT(a.AuditHistoryId) FROM AuditHistory a
						WHERE a.SystemEntityId = @SystemEntityId)
				WHERE	SystemEntityType	= @EntityName
     
			END TRY
			BEGIN CATCH
			END CATCH
		END 
    
	SELECT	a.SystemEntityTypeId   
		,	a.SystemEntityType    
		,	a.TestDataCount		
		,	a.AuditHistoryCount	
	FROM	#TempRecords a	
	ORDER BY a.TestDataCount DESC
	
	--SELECT	a.SystemEntityId
	--,		b.EntityName
	--,		COUNT(a.AuditHistoryId) AS 'Count' 
	--FROM		dbo.AuditHistory		a
	--INNER JOIN	dbo.SystemEntityType	b	ON	a.SystemEntityId = b.SystemEntityTypeId
	--WHERE		a.EntityKey < 0
	--AND			a.SystemEntityId	=
	--			CASE
	--				WHEN @SystemEntityTypeId IS NULL THEN a.SystemEntityId
	--				ELSE @SystemEntityTypeId
	--			END	
	--GROUP BY	a.SystemEntityId
	--		,	b.EntityName
	--ORDER BY	'Count' DESC	

END
GO