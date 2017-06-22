IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='PublishXDevelopmentSearch')
BEGIN
	PRINT 'Dropping Procedure PublishXDevelopmentSearch'
	DROP Procedure PublishXDevelopmentSearch
END
GO

PRINT 'Creating Procedure PublishXDevelopmentSearch'
GO

/******************************************************************************
**		File: 
**		Name: PublishXDevelopmentSearch
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
			EXEC PublishXDevelopmentSearch NULL	, NULL	, NULL
			EXEC PublishXDevelopmentSearch NULL	, 'K'	, NULL
			EXEC PublishXDevelopmentSearch 1	, 'K'	, NULL
			EXEC PublishXDevelopmentSearch 1	, NULL	, NULL
			EXEC PublishXDevelopmentSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------
**    
*******************************************************************************/
Create procedure dbo.PublishXDevelopmentSearch
(
		@PublishXDevelopmentId		INT				= NULL 	
	,	@PublishId					INT				= NULL 	
	,	@DevelopmentId				INT				= NULL 
	,	@ApplicationId				INT				= NULL 		
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'PublishXDevelopment'		
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0	 
)
WITH RECOMPILE	
AS
BEGIN	

	SET	NOCOUNT ON

	SELECT	a.PublishXDevelopmentId												
		,	a.PublishId																
		,	a.DevelopmentId
		,	a.ApplicationId	
				
	FROM		dbo.PublishXDevelopment	a
	
	WHERE a.DevelopmentId		= ISNULL(@DevelopmentId, a.DevelopmentId)
	AND a.PublishId				= ISNULL(@PublishId, a.PublishId)
	AND a.PublishXDevelopmentId = ISNULL(@PublishXDevelopmentId, a.PublishXDevelopmentId)
	
	IF @AddAuditInfo = 1 
		BEGIN
			
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'PublishXDevelopment'
				,	@EntityKey				= @PublishXDevelopmentId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

