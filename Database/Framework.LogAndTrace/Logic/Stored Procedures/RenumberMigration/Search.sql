IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name='RenumberMigrationSearch')
BEGIN
	PRINT 'Dropping Procedure RenumberMigrationSearch'
	DROP Procedure RenumberMigrationSearch
END
GO

PRINT 'Creating Procedure RenumberMigrationSearch'
GO

/******************************************************************************
**		File: 
**		Level: RenumberMigrationSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC RenumberMigrationSearch NULL	, NULL	, NULL
			EXEC RenumberMigrationSearch NULL	, 'K'	, NULL
			EXEC RenumberMigrationSearch 1		, 'K'	, NULL
			EXEC RenumberMigrationSearch 1		, NULL	, NULL
			EXEC RenumberMigrationSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure dbo.RenumberMigrationSearch
(
		@RenumberMigrationId	INT				= NULL 		
	,	@ApplicationId			INT				= NULL
	,	@SystemEntityTypeId		INT				= NULL
	,	@OriginalKey			INT				= NULL	
	,	@MigratedKey			INT				= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'RenumberMigration'	
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0
)
WITH RECOMPILE
AS
BEGIN	

	SET  NOCOUNT ON	 

	SELECT		a.RenumberMigrationId			
			,	a.ApplicationId			
			,	a.SystemEntityTypeId		
			,	a.OriginalKey				
			,	a.MigratedKey			
			,	a.RecordDate	
			,	b.EntityName		AS	'SystemEntityType'	        
	FROM	dbo.RenumberMigration  a
	INNER JOIN	Configuration.dbo.SystemEntityType	b
		ON	a.SystemEntityTypeId = b.SystemEntityTypeId
	WHERE	a.ApplicationId				=	ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.SystemEntityTypeId		=	ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId)
	AND		a.OriginalKey				=	ISNULL(@OriginalKey, a.OriginalKey)
	AND		a.MigratedKey				=	ISNULL(@MigratedKey, a.MigratedKey)	
	AND		a.RenumberMigrationId		=	ISNULL(@RenumberMigrationId, a.RenumberMigrationId)
	ORDER BY a.RecordDate					DESC
		,	 a.RenumberMigrationId			ASC

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert			
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @RenumberMigrationId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

