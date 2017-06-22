IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteDeveloperValueClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteDeveloperValueClone'
	DROP  Procedure ReleaseNoteDeveloperValueClone
END
GO

PRINT 'Creating Procedure ReleaseNoteDeveloperValueClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseNoteDeveloperValueClone
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
CREATE Procedure dbo.ReleaseNoteDeveloperValueClone
(
		@ReleaseNoteDeveloperValueId		INT				= NULL 	OUTPUT	
	,	@ApplicationId					INT				= NULL
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)						
	,	@SortOrder						INT	
	,	@DateCreated					DATETIME	= NULL
	,	@DateModified					DATETIME	= NULL
	,	@CreatedByAuditId				INT			= NULL
	,	@ModifiedByAuditId				INT			= NULL								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	
		= NULL			
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteDeveloperValue'
)
AS
BEGIN

	IF @ReleaseNoteDeveloperValueId IS NULL OR @ReleaseNoteDeveloperValueId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteDeveloperValueId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.ReleaseNoteDeveloperValue
	WHERE	ReleaseNoteDeveloperValueId		= @ReleaseNoteDeveloperValueId

	EXEC dbo.ReleaseNoteDeveloperValueInsert 
			@ReleaseNoteDeveloperValueId	=	NULL
		,	@ApplicationId				=	@ApplicationId
		,	@Name						=	@Name
		,	@Description				=	@Description
		,	@SortOrder					=	@SortOrder
		,	@DateCreated				=	@DateCreated
		,	@DateModified				=	@DateModified
		,	@CreatedByAuditId			=	@CreatedByAuditId
		,	@ModifiedByAuditId			=	@ModifiedByAuditId
		,	@AuditId					=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteDeveloperValue'
		,	@EntityKey				= @ReleaseNoteDeveloperValueId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
