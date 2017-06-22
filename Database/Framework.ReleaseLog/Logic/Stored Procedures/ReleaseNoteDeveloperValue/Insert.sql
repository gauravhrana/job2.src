IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteDeveloperValueInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteDeveloperValueInsert'
	DROP  Procedure ReleaseNoteDeveloperValueInsert
END
GO

PRINT 'Creating Procedure ReleaseNoteDeveloperValueInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseNoteDeveloperValueInsert
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
CREATE Procedure dbo.ReleaseNoteDeveloperValueInsert
(
		@ReleaseNoteDeveloperValueId		INT				= NULL 	OUTPUT
	,	@ApplicationId					INT				= NULL		
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)					
	,	@SortOrder						INT									
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteDeveloperValue'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteDeveloperValueId OUTPUT, @AuditId

	DECLARE		@DateCreated		AS		DATETIME
	DECLARE		@DateModified		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @DateCreated		= GETDATE() 
	SET @DateModified		= @DateCreated
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId

	
	INSERT INTO dbo.ReleaseNoteDeveloperValue 
	( 
			ReleaseNoteDeveloperValueId
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
			@ReleaseNoteDeveloperValueId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder	
		,	@DateCreated
		,	@DateModified
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @ReleaseNoteDeveloperValueId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteDeveloperValue'
		,	@EntityKey				= @ReleaseNoteDeveloperValueId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 