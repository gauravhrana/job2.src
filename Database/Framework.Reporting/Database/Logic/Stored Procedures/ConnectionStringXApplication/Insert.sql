IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringXApplicationInsert')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationInsert'
	DROP  Procedure ConnectionStringXApplicationInsert
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ConnectionStringXApplicationInsert
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

CREATE Procedure dbo.ConnectionStringXApplicationInsert
(
		@ConnectionStringXApplicationId				INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT								
	,	@ConnectionStringId							INT								
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'ConnectionStringXApplication'
)
AS
BEGIN

	INSERT INTO dbo.ConnectionStringXApplication 
	( 
			ApplicationId						
		,	ConnectionStringId						
	)
	VALUES 
	(  
			@ApplicationId			
		,	@ConnectionStringId			
	)

	SELECT @ConnectionStringXApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringXApplicationId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 