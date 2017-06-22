IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationAttributeInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationAttributeInsert'
	DROP  Procedure ApplicationAttributeInsert
END
GO

PRINT 'Creating Procedure ApplicationAttributeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationAttributeInsert
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

CREATE Procedure dbo.ApplicationAttributeInsert
(
		@ApplicationId				INT				
	,	@RenderApplicationFilter	INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationAttribute'
)
AS
BEGIN
   
	
	INSERT INTO dbo.ApplicationAttribute 
	( 				
			ApplicationId						
		,	RenderApplicationFilter					
								
	)
	VALUES 
	(  
			@ApplicationId	
		,   @RenderApplicationFilter		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 