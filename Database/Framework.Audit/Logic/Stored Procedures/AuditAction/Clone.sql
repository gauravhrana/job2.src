IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionClone')
BEGIN
	PRINT 'Dropping Procedure AuditActionClone'
	DROP  Procedure AuditActionClone
END
GO

PRINT 'Creating Procedure AuditActionClone'
GO

/*********************************************************************************************
**		File: 
**		Name: AuditActionClone
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

CREATE Procedure dbo.AuditActionClone
(
		@AuditActionId		INT		= NULL 	OUTPUT			
	,	@Name				VARCHAR(50)						
	,	@Description		VARCHAR(50)						
	,	@SortOrder			INT								
	,	@AuditId			INT									
	,	@AuditDate			DATETIME	= NULL				
	,	@SystemEntityType	VARCHAR(50)	= 'AuditAction'				
)
AS
BEGIN		
	
	SELECT	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.AuditAction
	WHERE	AuditActionId	= @AuditActionId

	EXEC dbo.AuditActionInsert 
			@AuditActionId		=	NULL
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AuditActionId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
