IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteDeveloperValueUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteDeveloperValueUpdate'
	DROP  Procedure  ReleaseNoteDeveloperValueUpdate
END
GO

PRINT 'Creating Procedure ReleaseNoteDeveloperValueUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNoteDeveloperValueUpdate
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
CREATE Procedure dbo.ReleaseNoteDeveloperValueUpdate
(
		@ReleaseNoteDeveloperValueId		INT			 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT						 
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseNoteDeveloperValue'
)
AS
BEGIN 

	DECLARE		@DateModified		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@DateModified		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId


	UPDATE	dbo.ReleaseNoteDeveloperValue 
	SET		Name						=	@Name		
		,	Description					=	@Description				
		,	SortOrder					=	@SortOrder	
		,   DateModified				=	@DateModified
		,	ModifiedByAuditId			=   @ModifiedByAuditId	
	WHERE	ReleaseNoteDeveloperValueId	=	@ReleaseNoteDeveloperValueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteDeveloperValue'
		,	@EntityKey				= @ReleaseNoteDeveloperValueId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO