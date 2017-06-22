IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyChildrenGet')
BEGIN
	PRINT 'Dropping Procedure SuperKeyChildrenGet'
	DROP  Procedure SuperKeyChildrenGet
END
GO

PRINT 'Creating Procedure SuperKeyChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: SuperKeyChildrenGet
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SuperKeyChildrenGet
(
		@SuperKeyId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'SuperKey'
)
AS
BEGIN

	-- GET SuperKeyDetail Records
	SELECT	a.SuperKeyDetailId
		,	a.ApplicationId	
		,	a.EntityKey
		,	a.SuperKeyId	
		,	b.Name					AS	'SuperKey'		
	FROM		dbo.SuperKeyDetail	a
	INNER JOIN	dbo.SuperKey		b	ON	a.SuperKeyId	=	b.SuperKeyId
	WHERE	a.SuperKeyId = @SuperKeyId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   