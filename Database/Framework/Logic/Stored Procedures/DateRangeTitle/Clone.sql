IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleClone')
BEGIN
	PRINT 'Dropping Procedure DateRangeTitleClone'
	DROP  Procedure DateRangeTitleClone
END
GO

PRINT 'Creating Procedure DateRangeTitleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DateRangeTitleClone
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.DateRangeTitleClone
(
		@DateRangeTitleId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DateRangeTitle'
)
AS
BEGIN

	IF @DateRangeTitleId IS NULL OR @DateRangeTitleId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DateRangeTitleId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Description			=	[Description]
		,	@SortOrder				=	SortOrder				
	FROM	dbo.DateRangeTitle
	WHERE   DateRangeTitleId		=	@DateRangeTitleId
	ORDER BY DateRangeTitleId

	EXEC dbo.DateRangeTitleInsert 
			@DateRangeTitleId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DateRangeTitle'
		,	@EntityKey				= @DateRangeTitleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
