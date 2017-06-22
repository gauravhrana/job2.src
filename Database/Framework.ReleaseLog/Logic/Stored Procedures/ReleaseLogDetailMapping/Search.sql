IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseLogDetailMappingSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailMappingSearch'
	DROP Procedure ReleaseLogDetailMappingSearch
END
GO

PRINT 'Creating Procedure ReleaseLogDetailMappingSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogDetailMappingSearch
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
			EXEC ReleaseLogDetailMappingSearch NULL	, NULL	, NULL
			EXEC ReleaseLogDetailMappingSearch NULL	, 'K'	, NULL
			EXEC ReleaseLogDetailMappingSearch 1		, 'K'	, NULL
			EXEC ReleaseLogDetailMappingSearch 1		, NULL	, NULL
			EXEC ReleaseLogDetailMappingSearch NULL	, NULL	, 'W'

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
Create procedure dbo.ReleaseLogDetailMappingSearch
(
		@ReleaseLogDetailMappingId			INT				= NULL 	
	,	@ParentReleaseLogDetailId			INT				= NULL 	
	,	@ChildReleaseLogDetailId			INT				= NULL 
	,	@ApplicationId						INT				= NULL 		
	,	@AuditId							INT							
	,	@AuditDate							DATETIME		= NULL	
	,	@SystemEntityType					VARCHAR(50)		= 'ReleaseLogDetailMapping'		
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	 
)
WITH RECOMPILE	
AS
BEGIN	

	SET	NOCOUNT ON

	SELECT	a.ReleaseLogDetailMappingId												
		,	a.ParentReleaseLogDetailId																
		,	a.ChildReleaseLogDetailId
		,	a.ApplicationId	
				
	FROM		dbo.ReleaseLogDetailMapping	a	
	
	WHERE a.ChildReleaseLogDetailId	= ISNULL(@ChildReleaseLogDetailId, a.ChildReleaseLogDetailId)
	AND a.ParentReleaseLogDetailId	= ISNULL(@ParentReleaseLogDetailId, a.ParentReleaseLogDetailId)
	AND a.ReleaseLogDetailMappingId	= ISNULL(@ReleaseLogDetailMappingId, a.ReleaseLogDetailMappingId)
	
	IF @AddAuditInfo = 1 
		BEGIN		
	
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'ReleaseLogDetailMapping'
				,	@EntityKey				= @ReleaseLogDetailMappingId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

