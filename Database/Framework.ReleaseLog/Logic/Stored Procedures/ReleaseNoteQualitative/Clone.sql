IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteQualitativeClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteQualitativeClone'
	DROP  Procedure ReleaseNoteQualitativeClone
END
GO

PRINT 'Creating Procedure ReleaseNoteQualitativeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseNoteQualitativeClone
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
CREATE Procedure dbo.ReleaseNoteQualitativeClone
(
		@ReleaseNoteQualitativeId		INT				= NULL 	OUTPUT	
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
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteQualitative'
)
AS
BEGIN

	IF @ReleaseNoteQualitativeId IS NULL OR @ReleaseNoteQualitativeId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteQualitativeId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.ReleaseNoteQualitative
	WHERE	ReleaseNoteQualitativeId		= @ReleaseNoteQualitativeId

	EXEC dbo.ReleaseNoteQualitativeInsert 
			@ReleaseNoteQualitativeId	=	NULL
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
			@SystemEntityType		= 'ReleaseNoteQualitative'
		,	@EntityKey				= @ReleaseNoteQualitativeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
