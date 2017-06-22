IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailClone')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailClone'
	DROP  Procedure SuperKeyDetailClone
END
GO

PRINT 'Creating Procedure SuperKeyDetailClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SuperKeyDetailClone
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

CREATE Procedure dbo.SuperKeyDetailClone
(
		@SuperKeyDetailId		INT				= NULL 	OUTPUT	
	,   @ApplicationId			INT				= NULL	
	,	@EntityKey				INT	
	,	@SuperKeyId				INT							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKeyDetail'
)
AS
BEGIN					
	
	SELECT	@ApplicationId			= ApplicationId
		,	@EntityKey				= EntityKey
		,	@SuperKeyId				= SuperKeyId				
	FROM	dbo.SuperKeyDetail
	WHERE   SuperKeyDetailId		= @SuperKeyDetailId
	ORDER BY SuperKeyDetailId

	EXEC dbo.SuperKeyDetailInsert 
			@SuperKeyDetailId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@EntityKey				=	@EntityKey
		,	@SuperKeyId				=	@SuperKeyId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKeyDetail'
		,	@EntityKey				= @SuperKeyDetailId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
