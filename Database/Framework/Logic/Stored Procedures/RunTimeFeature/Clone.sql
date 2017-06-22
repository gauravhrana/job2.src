IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RunTimeFeatureClone')
BEGIN
	PRINT 'Dropping Procedure RunTimeFeatureClone'
	DROP  Procedure RunTimeFeatureClone
END
GO

PRINT 'Creating Procedure RunTimeFeatureClone'
GO

/*********************************************************************************************
**		File: 
**		Name: RunTimeFeatureClone
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

CREATE Procedure dbo.RunTimeFeatureClone
(
		@RunTimeFeatureId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'RunTimeFeature'
)
AS
BEGIN

	IF @RunTimeFeatureId IS NULL OR @RunTimeFeatureId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @RunTimeFeatureId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.RunTimeFeature
	WHERE   RunTimeFeatureId		=	@RunTimeFeatureId
	ORDER BY RunTimeFeatureId

	EXEC dbo.RunTimeFeatureInsert 
			@RunTimeFeatureId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'RunTimeFeature'
		,	@EntityKey				= @RunTimeFeatureId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
