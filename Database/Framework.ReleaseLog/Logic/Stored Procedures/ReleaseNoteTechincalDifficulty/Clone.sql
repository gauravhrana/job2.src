IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteTechnicalDifficultyClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteTechnicalDifficultyClone'
	DROP  Procedure ReleaseNoteTechnicalDifficultyClone
END
GO

PRINT 'Creating Procedure ReleaseNoteTechnicalDifficultyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseNoteTechnicalDifficultyClone
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
CREATE Procedure dbo.ReleaseNoteTechnicalDifficultyClone
(
		@ReleaseNoteTechnicalDifficultyId		INT				= NULL 	OUTPUT	
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
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteTechnicalDifficulty'
)
AS
BEGIN

	IF @ReleaseNoteTechnicalDifficultyId IS NULL OR @ReleaseNoteTechnicalDifficultyId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteTechnicalDifficultyId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.ReleaseNoteTechnicalDifficulty
	WHERE	ReleaseNoteTechnicalDifficultyId		= @ReleaseNoteTechnicalDifficultyId

	EXEC dbo.ReleaseNoteTechnicalDifficultyInsert 
			@ReleaseNoteTechnicalDifficultyId	=	NULL
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
			@SystemEntityType		= 'ReleaseNoteTechnicalDifficulty'
		,	@EntityKey				= @ReleaseNoteTechnicalDifficultyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
