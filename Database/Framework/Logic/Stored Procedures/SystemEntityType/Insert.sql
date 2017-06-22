IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeInsert')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeInsert'
	DROP  Procedure SystemEntityTypeInsert
END
GO

PRINT 'Creating Procedure SystemEntityTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		EntityName:SystemEntityTypeInsert
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
**		Date:		Author:				EntityDescription:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SystemEntityTypeInsert
(
		@SystemEntityTypeId		INT				= NULL 	OUTPUT		
	,	@EntityName				VARCHAR(100)				
	,	@EntityDescription		VARCHAR(50)	
	,	@PrimaryDatabase		VARCHAR(50)		
	,	@CreatedDate			DateTime					
	,	@NextValue				INT							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL						
	,	@SystemEntityType		VARCHAR(50)		= 'SystemEntityType'										
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'SystemEntityType', @SystemEntityTypeId OUTPUT
	
	INSERT INTO dbo.SystemEntityType 
	( 
			SystemEntityTypeId						
		,	EntityName				
		,	EntityDescription		
		,	NextValue
		,	PrimaryDatabase
		,	CreatedDate							
	)
	VALUES 
	(  
			@SystemEntityTypeId		
		,	@EntityName				
		,	@EntityDescription		
		,	@NextValue
		,	PrimaryDatabase		= ISNULL(@PrimaryDatabase, 'Configuration')
		,	CreatedDate			= ISNULL(@CreatedDate, GETDATE())			
					
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemEntityTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO

 