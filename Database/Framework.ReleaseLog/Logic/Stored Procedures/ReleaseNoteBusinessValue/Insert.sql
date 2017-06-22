IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessValueInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessValueInsert'
	DROP  Procedure ReleaseNoteBusinessValueInsert
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessValueInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseNoteBusinessValueInsert
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
**********************************************************************************************/
CREATE Procedure dbo.ReleaseNoteBusinessValueInsert
(
		@ReleaseNoteBusinessValueId		INT				= NULL 	OUTPUT
	,	@ApplicationId					INT				= NULL		
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)					
	,	@SortOrder						INT									
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteBusinessValue'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteBusinessValueId OUTPUT, @AuditId

	DECLARE		@DateCreated		AS		DATETIME
	DECLARE		@DateModified		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @DateCreated		= GETDATE() 
	SET @DateModified		= @DateCreated
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId

	
	INSERT INTO dbo.ReleaseNoteBusinessValue 
	( 
			ReleaseNoteBusinessValueId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder	
		,	DateCreated
		,	DateModified
		,	CreatedByAuditId
		,	ModifiedByAuditId
			
	)
	VALUES 
	(  
			@ReleaseNoteBusinessValueId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder	
		,	@DateCreated
		,	@DateModified
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @ReleaseNoteBusinessValueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteBusinessValue'
		,	@EntityKey				= @ReleaseNoteBusinessValueId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 