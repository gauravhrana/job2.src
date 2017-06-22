IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteQualitativeUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteQualitativeUpdate'
	DROP  Procedure  ReleaseNoteQualitativeUpdate
END
GO

PRINT 'Creating Procedure ReleaseNoteQualitativeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNoteQualitativeUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReleaseNoteQualitativeUpdate
(
		@ReleaseNoteQualitativeId		INT			 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT						 
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseNoteQualitative'
)
AS
BEGIN 

	DECLARE		@DateModified		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@DateModified		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId


	UPDATE	dbo.ReleaseNoteQualitative 
	SET		Name						=	@Name		
		,	Description					=	@Description				
		,	SortOrder					=	@SortOrder	
		,   DateModified				=	@DateModified
		,	ModifiedByAuditId			=   @ModifiedByAuditId	
	WHERE	ReleaseNoteQualitativeId	=	@ReleaseNoteQualitativeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteQualitative'
		,	@EntityKey				= @ReleaseNoteQualitativeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO