IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityTypeClone')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeClone'
	DROP  Procedure SystemEntityTypeClone
END
GO

PRINT 'Creating Procedure SystemEntityTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SystemEntityTypeClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.SystemEntityTypeClone
(
		@SystemEntityTypeId		INT			= NULL 	OUTPUT		
	,	@EntityName				VARCHAR(100)						
	,	@EntityDescription		VARCHAR(50)	
	,	@PrimaryDatabase		VARCHAR(50)		
	,	@CreatedDate			DateTime							
	,	@NextValue				INT								
	,	@IncreaseBy				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		INT			= 'SystemEntityType'				
)
AS
BEGIN		
	
	SELECT	@EntityName			=	EntityName						
		,	@EntityDescription	=	EntityDescription		
		,	@NextValue			=	NextValue				
		,	@IncreaseBy			=	IncreaseBy	
		,	@PrimaryDatabase	=	PrimaryDatabase
		,	@CreatedDate		=	CreatedDate					
	FROM	dbo.SystemEntityType 
	WHERE	SystemEntityTypeId	= @SystemEntityTypeId
	ORDER BY SystemEntityTypeId

	EXEC dbo.SystemEntityTypeInsert 
			@SystemEntityTypeId		=	NULL
		,	@EntityName				=	@EntityName						
		,	@EntityDescription		=	@EntityDescription		
		,	@NextValue				=	@NextValue				
		,	@IncreaseBy				=	@IncreaseBy

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @SystemEntityTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
