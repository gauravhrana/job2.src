IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailInsert')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailInsert'
	DROP  Procedure SuperKeyDetailInsert
END
GO

PRINT 'Creating Procedure SuperKeyDetailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SuperKeyDetailInsert
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.SuperKeyDetailInsert
(
		@SuperKeyDetailId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@EntityKey					INT	
	,	@SuperKeyId					INT		
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKeyDetail'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SuperKeyDetailId OUTPUT, @AuditId

	INSERT INTO dbo.SuperKeyDetail 
	( 
			ApplicationId
		,	EntityKey
		,	SuperKeyId						
	)
	VALUES 
	(  
			@ApplicationId
		,	@EntityKey
		,	@SuperKeyId									
	)

	SET @SuperKeyDetailId = SCOPE_IDENTITY()

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SuperKeyDetailId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 