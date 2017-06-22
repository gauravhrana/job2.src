IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SetupConfigurationInsert')
BEGIN
	PRINT 'Dropping Procedure SetupConfigurationInsert'
	DROP  Procedure SetupConfigurationInsert
END
GO

PRINT 'Creating Procedure SetupConfigurationInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SetupConfigurationInsert
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
**		Date:		Author:				EntityName:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.SetupConfigurationInsert
(
		@EntityId					INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT					
	,	@EntityName					VARCHAR(50)											
	,	@ConnectionKeyName			VARCHAR(50)						
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'SetupConfiguration'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @EntityId OUTPUT, @AuditId
	
	INSERT INTO dbo.SetupConfiguration 
	( 
			EntityId	
		,   ApplicationId							
		,	EntityName					
		,	ConnectionKeyName						
	)
	VALUES 
	(  
			@EntityId	
		,   @ApplicationId						
		,	@EntityName				
		,	@ConnectionKeyName
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @EntityId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 