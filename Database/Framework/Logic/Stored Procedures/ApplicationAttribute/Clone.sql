IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationAttributeClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationAttributeClone'
	DROP  Procedure ApplicationAttributeClone
END
GO

PRINT 'Creating Procedure ApplicationAttributeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationAttributeClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationAttributeClone
(
		@ApplicationId				INT				
	,	@RenderApplicationFilter	INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationAttribute'
)
AS
BEGIN	
	
	SELECT	@ApplicationId					=	ApplicationId
		,	@RenderApplicationFilter		=	RenderApplicationFilter		
	FROM	dbo.ApplicationAttribute
	WHERE   ApplicationId		=	@ApplicationId
	ORDER BY ApplicationId

	EXEC dbo.ApplicationAttributeInsert 		
			@ApplicationId			=   ApplicationId
		,	@RenderApplicationFilter=	@RenderApplicationFilter
		
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationAttribute'
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
